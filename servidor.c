#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <pthread.h>
#include <mysql.h>


int contador;

//Estructura necesaria para acceso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

int i;
int sockets[100];
void *AtenderCliente (void *socket)
{
	int sock_conn;
	int *s;
	s= (int *) socket;
	sock_conn= *s;
	
	//int socket_conn = * (int *) socket;
	
	char peticion[512];
	char respuesta[512];
	char respuesta_1 [20];
	char respuesta_2 [20];
	char respuesta_3 [20];
	int ret;
	
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
	conn = mysql_real_connect (conn, "localhost","root", "mysql", "ProjectDemo",0, NULL, 0);
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
		
		
		printf ("\n Peticion: %s",peticion);
		// vamos a ver que quieren
		char *p = strtok(peticion, "/");
		int codigo =  atoi (p);
		char funcion[20];
		char nombre[20];
		
		printf("%d",codigo);
		
		if (codigo !=0)
		{
			p = strtok( NULL, "/");
			strcpy (funcion, p);
			printf ("\n Codigo: %d, Funcion: %s\n", codigo, funcion);
		}
		int numForm;
		// Ya tenemos el c?digo de la petici?n
		
		if (codigo ==0) //petici?n de desconexi?n
		{
			terminar=1;
		}
		
		else if (codigo == 1) //piden la longitd del nombre
		{
			strcpy (consulta,"SELECT PersonalData.Name from PersonalData, Jugadores WHERE Jugadores.BestScore=(select max(BestScore) from Jugadores) AND PersonalData.ID = Jugadores.ID");
			err = mysql_query (conn, consulta);
			if (err!=0)
			{
				printf ("\n Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			strcpy(respuesta_1,row[0]);
			sprintf(respuesta,"1/%s",respuesta_1);
			strcat(respuesta,"/");
			printf("\n respuesta enviada %s\n",respuesta);
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
			strcpy(respuesta_1,row[0]);
			sprintf(respuesta,"2/%s",respuesta_1);
			strcat(respuesta,"/");
			printf("\n respuesta enviada %s\n",respuesta);
		}

		else if (codigo == 3) //quantes partides shan jugat
		{
			printf("SELECT SUM(GamesPlayed) FROM Jugadores; \n");
			strcpy(consulta, "SELECT SUM(GamesPlayed) FROM Jugadores;");
			err = mysql_query(conn, consulta);
			printf(err);
			if (err!=0)
			{
				printf ("\n Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			strcpy(respuesta_1,row[0]);
			sprintf(respuesta,"3/%s",respuesta_1);
			strcat(respuesta,"/");
			printf("\n respuesta enviada %s\n",respuesta);
			
		}
		else if (codigo == 4) //diners que te una persona
		{
			p = strtok(NULL, "/");
			strcpy (nombre, p);
			strcpy (consulta,"SELECT (Valores.Money) FROM Valores, Jugadores, PersonalData WHERE PersonalData.Name='");
			strcat(consulta, nombre);
			strcat(consulta, "' AND PersonalData.ID=Jugadores.ID AND Valores.IDPlayer=Jugadores.ID;");
			err = mysql_query (conn, consulta);
			if (err!=0)
			{
				printf ("\n Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			strcpy(respuesta_1,row[0]);
			sprintf(respuesta,"4/%s",respuesta_1);
			strcat(respuesta,"/");
			printf("\n respuesta enviada %s\n",respuesta);
		}

			
		if (codigo !=0)
		{
			
			printf ("Respuesta: %s\n", respuesta);
			// Enviamos respuesta
			write(sock_conn,respuesta, strlen(respuesta));
		}
	}
	// Se acabo el servicio para este cliente
	close(sock_conn); 
}

int main(int argc, char *argv[])
{		
	int sock_conn, sock_listen;
	struct sockaddr_in serv_adr;
	
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
	serv_adr.sin_port = htons(9132);
	
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	contador =0;
	
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
		i=i+1;
		
	}
}
