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

ListaConectados miLista;

typedef struct {
	int gameNum;
	char player1[20];
	char player2[20];
	char player3[20];
	char player4[20];
} Game;

typedef struct {
	Game partidas[100];
	int num;
} ListaPartidas;

ListaPartidas misPartidas;

typedef struct {
	int numPartida;
	char invitador[20];
	char jugador[20];
	int aceptado;
} Notificacion;

typedef struct {
	Notificacion invitaciones[100];
	int num;	
} NotificacionesPendientes;

char consulta[500];
NotificacionesPendientes misNotificaciones;

int i;
int num;
int sockets[100];


MYSQL *conn;
int err;
MYSQL_RES *resultado;
MYSQL_ROW row;

char nombre[20];
char *p;

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
	int k;
	int socket;
	for(k= 0; k<lista->num; k++)
	{
		if(strcmp(lista->conectados[k].nombre,nombre) == 0 )
			socket = lista->conectados[k].socket;
	}
	if ( socket != NULL)
		return socket;
	else 
		return -1;
	
}

int BuscaPosicion (ListaConectados *lista, char nombre[20])
{
	//retorna la posicion en la lista, -1 si no esta en la lista
	int k = 0;
	int pos;
	int encontrado = 0;
	while(k<lista->num && encontrado != 1)
	{
		if(strcmp(lista->conectados[k].nombre,nombre) == 0 )
		{
			pos = k;
			encontrado = 1;
		}
		k ++;
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
	int k;
	if (pos == -1)
		return -1;
	else
	{
		for(k = pos; k<lista->num - 1 ;k++)
		{
			strcpy(lista->conectados[k].nombre,lista->conectados[k+1].nombre);
			lista->conectados[k].socket = lista->conectados[k+1].socket;
		}
		lista->num --;
		return 0;
	}
}

void DameConectados(ListaConectados *lista,char conectados[100])
{
	//pone los nombres de los conectados en la lista que se envia como parametro, con la fomra 2/nombre_1/nombre_2
	sprintf(conectados,"%d",lista->num);
	int k;
	for(k = 0; k<lista->num; k++)
	{
		sprintf(conectados,"%s/%s",conectados,lista->conectados[k].nombre);
	}
}

void conectar(char entrada[200],char respuestaFuncion[200],int socketconn)
{
	p = strtok(entrada, "/");
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
		strcpy(respuestaFuncion,"1/0");
	else
	{
		pthread_mutex_lock( &mutex ); //No me interrumpas ahora
		int e = PonConectado(&miLista,nombre,socketconn);
		if (e == 0)
			printf("\n introducido en la lista\n");
		else if(e == 1)
			printf("\n error al introducir en la lista\n");
		strcpy(ID,row[0]);	
		strcpy(respuestaFuncion,"1/1/");
		strcat(respuestaFuncion,ID);
		pthread_mutex_unlock( &mutex); //ya puedes interrumpirme
	}
}

void LogOut()
{
	pthread_mutex_lock( &mutex); //ya puedes interrumpirme
	EliminaConectado(&miLista,nombre);
	pthread_mutex_unlock( &mutex); //ya puedes interrumpirme
	Notificar();
}


void recuperaPSW(char entrada[200],char respuestaFuncion[200])
{
	char ID[10];
	p = strtok(entrada, "/");
	strcpy(ID, p);
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
	sprintf(respuestaFuncion,"2/%s",row[0]);
}

void MasPuntos(char respuestaFuncion[200])
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
	sprintf(respuestaFuncion,"3/%s",row[0]);
}

void CuantasPartidas(char respuestaFuncion[200])
{
	strcpy(consulta, "SELECT SUM(PlayedGames) FROM Players;");
	err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("\n Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	sprintf(respuestaFuncion,"4/%s",row[0]);
}

void CuantoDinero(char entrada[200],char respuestaFuncion[200])
{
	p = strtok(entrada, "/");
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
		strcpy(respuestaFuncion,"5/1");
	}
	else
	{
		strcpy(respuestaFuncion,"5/0/");	
		strcat(respuestaFuncion,row[0]);
	}
}

void registro(char entrada[200],char respuestaFuncion[200])
{
	char user[512];
	char psw[512];
	char email[512];
	p = strtok(entrada, "/");
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
	if(row==NULL) //devuelve 1 si ya existe ese nombre
	{
		strcpy(respuestaFuncion,"6/0");
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
		strcpy(respuestaFuncion,"6/1");
	}
}

void crearPartida(char respuestaFuncion[200])
{
	int Num;
	strcpy(consulta,"SELECT COUNT(ID) from Games;");
	err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("\n Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	Num=atoi(row[0])+1;
	strcpy(consulta,"INSERT INTO Games(CreationDate) VALUES(NULL);");
	err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("\n Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	else
	{
		sprintf(respuestaFuncion,"11/%d",Num);
	}
}

void Conectados(char respuestaFuncion[200])
{
	char conectados[100];
	DameConectados(&miLista,conectados);
	sprintf(respuestaFuncion,"7/%s",conectados);
}

int num = 0;
void DameInvitados(char entrada[200], char Invitados[200],char Invitador[200], int GameNum, NotificacionesPendientes *misNotificaciones)
{
	strcpy(Invitados,"");
	p = strtok(entrada,"/");
	printf("\n el usuario %s ha invitado a :",p);
	strcpy(Invitador,p);
	p = strtok(NULL,"/");
	while(p != NULL)
	{
		sprintf(Invitados,"%s%s",Invitados, p);
		Notificacion n;
		n.numPartida = GameNum;
		strcpy(n.invitador, Invitador);
		strcpy(n.jugador, Invitados);
		misNotificaciones->invitaciones[num] = n;
		misNotificaciones->num = num + 1;
		p = strtok(NULL,"/");
	}
	printf("%s \n",Invitados);	
}

void misInvitaciones(NotificacionesPendientes *misNotificaciones, char jugador[20], char listaInvitaciones[200])
{
	int i = 0;
	for(i = 0; i < misNotificaciones->num; i++)
		if(strcmp(misNotificaciones->invitaciones[i].jugador, jugador) == 0)
		{
			sprintf(listaInvitaciones,"%s%s/%d/", listaInvitaciones, misNotificaciones->invitaciones[i].invitador, misNotificaciones->invitaciones[i].numPartida);
			printf("\n El invitador %s ha invitado a %s a la partida %d \n", misNotificaciones->invitaciones[i].invitador, jugador, misNotificaciones->invitaciones[i].numPartida);
		}

}

void Aceptar(int aceptacion, char partidasAceptadas[200], NotificacionesPendientes *misNotificaciones, char Jugador[20], char numeroPartida[200])
{
	int i;
	strcpy(numeroPartida, "");
	if (aceptacion == 1)
	{
		while (p != NULL)
		{
			p = strtok(partidasAceptadas, "/");
			int numero;
			numero = atoi(p);
			for (i = 0; i < misNotificaciones->num; i++)
			{
				if (misNotificaciones->invitaciones[i].numPartida == numero)
				{
					if (strcmp(misNotificaciones->invitaciones[i].jugador, Jugador) == 0)
					{
						misNotificaciones->invitaciones[i].aceptado = 1;
						char num = (char)misNotificaciones->invitaciones[i].numPartida;
						sprintf("%s%s/", numeroPartida, num);
						printf ( "El jugador %s ha aceptado tu partida con numero %d, %s \n", misNotificaciones->invitaciones[i].jugador, misNotificaciones->invitaciones[i].numPartida, misNotificaciones->invitaciones[i].invitador);
					}
				}
			}
		}
	}
	else
	{
		strcpy(numeroPartida, "0");
		printf("Ha habido un error al aceptar la partida \n");
	}
}


void Notificar()
{
	char notificacion[200];
	Conectados(notificacion);
	int j;
	for (j = 0; j < i; j++)
	{
		write (sockets[j],notificacion, strlen(notificacion));
	}
}

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
		
		
		printf ("Peticion: %s \n", peticion);
		// vamos a ver que quieren
		p = strtok(peticion, "/");
		int codigo =  atoi(p);
		char nombre[20];
		char ListaDeSockets[200];
		// Ya tenemos el c?digo de la petici?n
		if (codigo !=0)
		{			
			printf ("Codigo: %d", codigo);
		}
		
		if (codigo ==0) //petici?n de desconexi?n
		{
			pthread_mutex_lock( &mutex); //no puedes interrumpirme
			printf("Cliente Desconectado");
			EliminaConectado(&miLista,nombre);
			terminar=1;
			pthread_mutex_unlock( &mutex); //ya puedes interrumpirme
			Notificar();
		}
		else if (codigo ==1) //CODI LOGIN
		{
			conectar(NULL,respuesta,sock_conn);
			Notificar();
		}
		else if (codigo ==2)
		{
			recuperaPSW(NULL,respuesta);
			//codi 2 de tornada
		}
		else if (codigo == 3)
		{
			MasPuntos(respuesta);
			// codi 3 de tornada
		}
		
		else if (codigo == 4) //quantes partides shan jugat
		{
			CuantasPartidas(respuesta);
			//codi 4 de tornada
		}
		else if (codigo == 5) //diners que te una persona
		{
			CuantoDinero(NULL,respuesta);
			//codi 5 de tornada
		}
		else if (codigo == 6) //register
		{
			registro(NULL,respuesta);
			//codi 6 de tornada
		}
		else if (codigo == 7)
		{
			Conectados(respuesta);
			//codi 7 de tornada
		}
		else if (codigo == 8)
		{
			pthread_mutex_lock(&mutex);
			char Invitados[200];
			char Invitador[200];
			char GameNum[10];
			p = strtok(NULL,"/");
			strcpy(GameNum,p);
			int GameNum1 = atoi(GameNum);
			DameInvitados(NULL, Invitados, Invitador, GameNum1, &misNotificaciones);
			p = strtok(Invitados,"/");
			sprintf(respuesta,"8/%s/%s",Invitador,GameNum);
			while(p!=NULL)
			{
				int s=BuscaSocket(&miLista,p);
				write (s,respuesta, strlen(respuesta));
				p = strtok(NULL,"/");
			}
			//falta crear taula partides
			pthread_mutex_unlock( &mutex);
			//codi 8 de tornada
		}
		else if (codigo == 9)
		{
			//codi 9 de tornada
			LogOut();
		}
		else if (codigo ==10)
		{
			pthread_mutex_lock( &mutex);
			char jugador[200];
			char listaInvitaciones[200];
			strcpy(listaInvitaciones, "");
			p = strtok(NULL,"/");
			strcpy(jugador,p);
			misInvitaciones(&misNotificaciones, jugador, listaInvitaciones);
			sprintf(respuesta,"10/%s",listaInvitaciones);
			write(sock_conn,respuesta, strlen(respuesta));
			pthread_mutex_unlock(&mutex);
		}
	
		else if (codigo == 11)
		{
			crearPartida(respuesta);
		}
		
		else if (codigo == 12)
		{
			/*	El codi per part del servidor ve donat com 12/1/Jugador/numPartida1/numPartida2/...   El segon paràmetre del codi és 1 només si s'ha acceptat la invitació*/
			pthread_mutex_lock( &mutex);
			int aceptacion;
			p = strtok(NULL, "/");
			aceptacion = atoi(p);
			char Jugador[20];
			p = strtok(NULL, "/");
			strcpy(Jugador, p);
			char partidasAceptadas[200];
			strcpy(partidasAceptadas, NULL);
			printf("%s \n", partidasAceptadas);
			char numeroPartida[200];
			Aceptar(aceptacion, partidasAceptadas, &misNotificaciones, Jugador, numeroPartida);
			/*Aceptar rep el codi d'acceptacio = 1, el nom del jugador que ha acceptat i el nombre de les partides acceptades, recorre les notificacions i quan troba la correcta
			posa l'atribut aceptar = 1 i envia per numeroPartida, els nombres dels jocs que s'han acceptat perquè el client els pugui obrir, a part d'escriure un breu missatge al servidor*/
			sprintf(respuesta,"12/%s",numeroPartida);
			write(sock_conn,respuesta, strlen(respuesta));
			pthread_mutex_unlock(&mutex);
		}
		
		if (codigo !=0 && codigo!=8 && codigo!=9)
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
	serv_adr.sin_port = htons(9058);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	
	pthread_t thread;
	
	i=0;
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

