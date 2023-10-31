#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <pthread.h>
#include <mysql.h>
#include <pthread.h>

//Estructura necesaria para acceso excluyente HO POSEM AL POSAR O TREURE CONECTADOS
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;
//HO POSEM AL POSAR O TREURE CONECTADOS

typedef struct {
	char nombre[20];
	int socket;
} Conectado;

typedef struct {
	Conectado conectados[100];
	int num;
} ListaConectados;

int PonConectado(ListaConectados *lista, char nombre[20], int socket)
{
	// Introduce un nuevo conectado, 0 si va bien, 1 si va mal.
	if (lista->num == 100)
		return -1;
	else
	{
		strcpy(lista->conectados[lista->num].nombre,nombre);
		lista->conectados[lista->num].socket = socket;
		lista->num ++;
		return 0;
	}
}

int BuscaSocket (ListaConectados *lista, char nombre[20])
{
	//retorna el socket, -1 si no esta en la lista
	int i;
	int socket;
	for(i = 0; i<lista->num; i++)
	{
		if(strcmp(lista->conectados[i].nombre,nombre) == 0 )
			socket = lista->conectados[i].socket;
	}
	if ( socket != NULL)
		return socket;
	else 
		return -1;
	
}

int BuscaPosicion (ListaConectados *lista, char nombre[20])
{
	//retorna la posicion en la lista, -1 si no esta en la lista
	int i = 0;
	int pos;
	int encontrado = 0;
	while(i<lista->num && encontrado != 1)
	{
		if(strcmp(lista->conectados[i].nombre,nombre) == 0 )
		{
			pos = i;
			encontrado = 1;
		}
		i ++;
	}
	if ( encontrado == 1)
		return pos;
	else 
		return -1;
	
}

int EliminaConectado (ListaConectados *lista, char nombre[20])
{
	//retorna o si elmina la persona, -1 si no lo elminina
	int pos = BuscaPosicion(lista,nombre);
	int i;
	if (pos == -1)
		return -1;
	else
	{
		for(i = pos; i<lista->num - 1 ;i++)
		{
			strcpy(lista->conectados[i].nombre,lista->conectados[i+1].nombre);
			lista->conectados[i].socket = lista->conectados[i+1].socket;
		}
		lista->num --;
		return 0;
	}
}

void DameConectados(ListaConectados *lista,char conectados[100])
{
	//pone los nombres de los conectados en la lista que se envia como parametro, con la fomra 2/nombre_1/nombre_2
	sprintf(conectados,"%d",lista->num);
	int i;
	for(i = 0; i<lista->num; i++)
	{
		sprintf(conectados,"%s/%s",conectados,lista->conectados[i].nombre);
	}
}

ListaConectados miLista;

void *AtenderCliente (void *socket)
{
	int sock_conn;
	int *s;
	s= (int *) socket;
	sock_conn= *s;
	
	//int socket_conn = * (int *) socket;
	
	char peticion[512];
	char respuesta[512];
	int ret;
	
	//SQL Comandos
	int i;
	MYSQL *conn;
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	conn = mysql_init(NULL);
	if (conn==NULL)
	{
		printf ("Error_1 al crear la conexion: %u %s\n",mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	conn = mysql_real_connect (conn, "localhost","root", "mysql", "Jellyfish",0, NULL, 0);
	if (conn==NULL)
	{
		printf ("Error_2 al inicializar la conexion: %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	char consulta [500];
	
	
	int terminar =0;
	// Entramos en un bucle para atender todas las peticiones de este cliente
	//hasta que se desconecte
	while (terminar ==0)
	{
		// Ahora recibimos la petici?n
		ret=read(sock_conn,peticion, sizeof(peticion));
		printf ("Recibido\n");
		
		// Tenemos que a?adirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		peticion[ret]='\0';
		
		
		printf ("Peticion: %s\n",peticion);
		
		// vamos a ver que quieren
		char *p = strtok( peticion, "/");
		int codigo =  atoi (p);
		// Ya tenemos el c?digo de la petici?n
		char nombre[20];
		
		if (codigo !=0)
		{			
			printf ("Codigo: %d", codigo);
		}
		
		if (codigo ==0) //petici?n de desconexi?n
		{
			pthread_mutex_lock( &mutex); //ya puedes interrumpirme
			printf("Cliente Desconectado");
			EliminaConectado(&miLista,nombre);
			terminar=1;
			pthread_mutex_unlock( &mutex); //ya puedes interrumpirme
		}
		else if (codigo ==1) //CODI LOGIN
		{
			p = strtok(NULL, "/");
			char pass[20];
			char ID[20];
			strcpy (nombre, p);			
			p = strtok(NULL, "/");
			strcpy(pass,p);
			strcpy (consulta,"SELECT (Players.ID) FROM Players, PersonalData WHERE PersonalData.ID=Players.ID AND PersonalData.Name ='");
			strcat(consulta, nombre);
			strcat(consulta, "' AND PersonalData.PSW='");
			strcat(consulta,pass);
			strcat(consulta,"';");
			err = mysql_query (conn, consulta);
			if (err!=0)
			{
				printf ("\n Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			if (row==NULL)
				strcpy(respuesta,"0/");
			else
			{
				pthread_mutex_lock( &mutex ); //No me interrumpas ahora
				int e = PonConectado(&miLista,nombre,1);
				if (e == 0)
					printf("\n introducido en la lista\n");
				else if(e == 1)
					printf("\n error al introducir en la lista\n");
				strcpy(ID,row[0]);	
				strcpy(respuesta,"1/");
				strcat(respuesta,ID);
				pthread_mutex_unlock( &mutex); //ya puedes interrumpirme
			}
		}
		else if (codigo ==2)
		{
			char ID[10];
			p = strtok(NULL, "/");
			strcpy(ID, p);
			printf("Tu ID es: %s\n", ID);
			strcpy(consulta,"SELECT PersonalData.PSW from PersonalData WHERE PersonalData.ID =");
			strcat(consulta,ID);
			strcat(consulta, ";");
			err = mysql_query(conn, consulta);
			if (err!=0)
			{
				printf ("\n Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			strcpy(respuesta,row[0]);
		}
		else if (codigo == 3)
		{
			strcpy (consulta,"SELECT PersonalData.Name from PersonalData, Players WHERE Players.BestScore=(select max(BestScore) from Players) AND PersonalData.ID = Players.ID");
			err = mysql_query (conn, consulta);
			if (err!=0)
			{
				printf ("\n Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			strcpy(respuesta,row[0]);
		}
		
		else if (codigo == 4) //quantes partides shan jugat
		{
			strcpy(consulta, "SELECT SUM(PlayedGames) FROM Players;");
			err = mysql_query(conn, consulta);
			printf(err);
			if (err!=0)
			{
				printf ("\n Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			strcpy(respuesta,row[0]);
			
		}
		else if (codigo == 5) //diners que te una persona
		{
			p = strtok(NULL, "/");
			char nombre[20];
			strcpy (nombre, p);
			strcpy (consulta,"SELECT (GameData.Money) FROM GameData, Players, PersonalData WHERE PersonalData.Name='");
			strcat(consulta, nombre);
			strcat(consulta, "' AND PersonalData.ID=Players.ID AND GameData.IDPlayer=Players.ID;");
			err = mysql_query (conn, consulta);
			if (err!=0)
			{
				printf ("\n Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			if(row==NULL) //1 si no hi ha aquest nom
			{
				strcpy(respuesta,"1/");
			}
			else
			{
				strcpy(respuesta,"0/");	
				strcat(respuesta,row[0]);
			}

		}
		else if (codigo == 6) //register
		{
			char user[512];
			char psw[512];
			char email[512];
			p = strtok(NULL, "/");
			strcpy(user, p);
			p = strtok(NULL, "/");
			strcpy(psw,p);
			p = strtok(NULL, "/");
			strcpy(email,p);
			strcpy(consulta,"SELECT * from PersonalData WHERE PersonalData.Name ='");
			strcat(consulta,user);
			strcat(consulta, "';");
			err = mysql_query(conn, consulta);
			if (err!=0)
			{
				printf ("\n Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			if(row==NULL) //devuelve 0 si ya existe ese nombre
			{
				strcpy(respuesta,"0/");
				strcpy(consulta,"INSERT INTO Players(LastScore,BestScore,PlayedGames) VALUES(NULL,NULL,NULL)");
				err = mysql_query(conn, consulta);
				if (err!=0)
				{
					printf ("\n Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				strcpy(consulta,"INSERT INTO PersonalData(Email,Name,PSW) VALUES('");
				strcat(consulta,email);
				strcat(consulta,"','");
				strcat(consulta,user);
				strcat(consulta,"','");
				strcat(consulta,psw);
				strcat(consulta,"');");
				err = mysql_query(conn, consulta);
				if (err!=0)
				{
					printf ("\n Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
			}
			else
			{
				strcpy(respuesta,"1/");
			}
		}
		else if (codigo == 7)
		{
			char conectados[100];
			DameConectados(&miLista,conectados);
			strcpy(respuesta,conectados);
			printf("\n \n %s",conectados);
			p = strtok(conectados,"/");
			int num = atoi(p);
			printf("en total hay %d usuarios\n",num);
			p = strtok(NULL,"/");
			while(p != NULL)
			{
				strcpy(nombre,p);
				printf("el nombre del usuario es %s\n",nombre);
				p = strtok(NULL,"/");
			}
			printf("\n la respuesta es: %s \n",respuesta);
		}
		if (codigo !=0)
		{
			
			printf ("Respuesta: %s\n", respuesta);
			// Enviamos respuesta
			write (sock_conn,respuesta, strlen(respuesta));
		}
	}
	// Se acabo el servicio para este cliente
	close(sock_conn); 

}


int main(int argc, char *argv[])
{
	
	int sock_conn, sock_listen;
	struct sockaddr_in serv_adr;
	
	//SQL Comandos
	
	// INICIALITZACIONS
	// Obrim el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	// Fem el bind al port
	
	
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// establecemos el puerto de escucha
	serv_adr.sin_port = htons(9030);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	int i = 0;
	int sockets[100];
	pthread_t thread;


	for (;;){
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		
		sockets[i] =sock_conn;
		//sock_conn es el socket que usaremos para este cliente
		
		// Crear thead y decirle lo que tiene que hacer
		
		pthread_create (&thread, NULL, AtenderCliente,&sockets[i]);
		i ++;
	}	
}
