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

//Estructura necesaria para acceso excluyente HO POSEM AL ACCEDIR VARIABLES GLOBALS
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;


typedef struct {
	char nombre[20];
	int socket;
} Conectado;

typedef struct {
	Conectado conectados[100];
	int num;
} ListaConectados;

ListaConectados miLista; //Lista de conectados con su socket y nombre

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

NotificacionesPendientes misNotificaciones; //Notificaciones de Partidas Pendientes

typedef struct {
	int numPartida;
	char jugador[20];
	int money;
	int lastPos;
	//afegim parametres
} Jugada;

typedef struct {
	Jugada jugadas[1000];
	int num;	
} listaJugadas; 

listaJugadas misJugadas; //Info de partidas ya consolidadas y empezadas

char consulta[500];
int i;
int sockets[100];


MYSQL *conn;
int err;
MYSQL_RES *resultado;
MYSQL_ROW row;

char *p;

int PonSocket(int socket) //devuelve posicion socket en la lista
{
	//añade socket a la lista de sockets
	int found=0;
	int j=0;
	while((found==0)&&(j!=100))
	{
		if(sockets[j]==0)
		{
			sockets[j]=socket;
			found=1;
		}
		else
			j++;
	}
	return j;
}

int liberaSocket(int socket) //1 si no encuentra socket
{
	//asigna 0 a socket liberado
	int found=0;
	int j=0;
	while((found==0)&&(j!=100))
	{
		if(sockets[j]==socket)
		{
			sockets[j]=0;
			found=1;
		}
		j++;
	}
	if(found==0)
		return 1;
	else
		return 0;
}

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

void conectar(char entrada[200],char respuestaFuncion[200],int socketconn) //comprueba que login i psw este bien y si es asi responde 1
{ //si el login es correcto introduce el usuario a lista de conectados
	p = strtok(entrada, "/");
	char pass[20];
	char ID[20];
	char nombre[20];
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
		int e = PonConectado(&miLista,nombre,socketconn);
		if (e == 0)
			printf("\n introducido en la lista\n");
		else if(e == 1)
			printf("\n error al introducir en la lista\n");
		strcpy(ID,row[0]);	
		strcpy(respuestaFuncion,"1/1/");
		strcat(respuestaFuncion,ID);
	}
}

void LogOut(char entrada[200])  //elimina conectado de la lista y notifica
{
	pthread_mutex_lock( &mutex);
	p = strtok(entrada, "/");
	char nombre[20];
	strcpy (nombre, p);	
	EliminaConectado(&miLista,nombre);
	pthread_mutex_unlock( &mutex);
	Notificar();
}


void recuperaPSW(char entrada[200],char respuestaFuncion[200]) //devuelve el psw si el usuario lo ha olvidado
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

void MasPuntos(char respuestaFuncion[200]) //devuelve el jugador con mas puntos
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

void CuantasPartidas(char respuestaFuncion[200]) //devuelve cuantas partidas totales se han jugado
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

void CuantoDinero(char entrada[200],char respuestaFuncion[200]) //devuelve cuanto dinero tiene un jugador
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

void registro(char entrada[200],char respuestaFuncion[200]) //añade el nuevo usuario a la base de datos
{ //solo lo añade si el nombre de usuario no existe aún
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

void crearPartida(char respuestaFuncion[200]) //crea una nueva partida y la añade a la DB
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

void Conectados(char respuestaFuncion[200]) //printea los conectados para devolver
{
	char conectados[100];
	DameConectados(&miLista,conectados);
	sprintf(respuestaFuncion,"7/%s",conectados);
}

void DameInvitados(char entrada[200], char Invitados[200],char Invitador[200], int GameNum, NotificacionesPendientes *misNotificaciones)
{ //añade las notificaciones a la lista de notis, una noti para cada persona invitada a la partida indicada
	strcpy(Invitados,"");
	p = strtok(entrada,"/");
	printf("\n el usuario %s ha invitado a :",p);
	strcpy(Invitador,p);
	p = strtok(NULL,"/");
	while(p != NULL)
	{
		sprintf(Invitados,"%s/%s",Invitados, p);
		Notificacion n;
		n.numPartida = GameNum;
		strcpy(n.invitador, Invitador);
		strcpy(n.jugador, p);
		n.aceptado = 0;
		misNotificaciones->invitaciones[misNotificaciones->num] = n;
		misNotificaciones->num = misNotificaciones->num + 1;
		p = strtok(NULL,"/");
	}
	printf("%s \n",Invitados);
}
void EliminaNotificacionesPartida(int partida) //elimina todas las notis referentes a una partida
{
	int k=0;
	int j = 0;
	int encontrado = 0;
	while (j<misNotificaciones.num)
	{
		if(misNotificaciones.invitaciones[j].numPartida == partida)
		{
			for(k=j;k<(misNotificaciones.num-1);k++){
				misNotificaciones.invitaciones[k]=misNotificaciones.invitaciones[k+1];
			}
			misNotificaciones.invitaciones[misNotificaciones.num-1].aceptado=0;
			misNotificaciones.invitaciones[misNotificaciones.num-1].numPartida=0;
			strcpy(misNotificaciones.invitaciones[misNotificaciones.num-1].jugador,"");
			strcpy(misNotificaciones.invitaciones[misNotificaciones.num-1].invitador,"");
			misNotificaciones.num--;
		}
		else
		   j++;
	}
}

void misInvitaciones(NotificacionesPendientes *misNotificaciones, char jugador[20], char listaInvitaciones[200]) //devuelve las notis totales de un usuario
{
	int i = 0;
	for(i = 0; i < misNotificaciones->num; i++)
		if((strcmp(misNotificaciones->invitaciones[i].jugador, jugador) == 0) && (misNotificaciones->invitaciones[i].aceptado == 0))
		{
			sprintf(listaInvitaciones,"%s%s/%d/", listaInvitaciones, misNotificaciones->invitaciones[i].invitador, misNotificaciones->invitaciones[i].numPartida);
			printf("\n El invitador %s ha invitado a %s a la partida %d \n", misNotificaciones->invitaciones[i].invitador, jugador, misNotificaciones->invitaciones[i].numPartida);
		}

}

void Notificar() //notifica a todas las personas conectadas la lista de conectados
{
	char notificacion[200];
	Conectados(notificacion);
	int j;
	for (j = 0; j < 100; j++)
	{
		if(sockets[j]!=0)
			write (sockets[j],notificacion, strlen(notificacion));
	}
}
void ComprobarStart (int partida, NotificacionesPendientes *Noti,char respuestaFuncion[200]) //comprueba si una partida ya puede empezar
{
	int aceptadas=0;
	int rechazadas=0;
	int totales=0;
	for(int j=0;j<Noti->num;j++)
	{
		if(Noti->invitaciones[j].numPartida==partida)
		{
			if(Noti->invitaciones[j].aceptado==1)
				aceptadas++;
			else if(Noti->invitaciones[j].aceptado==-1)
				rechazadas++;
			totales++;
		}
	}
	if(((aceptadas+rechazadas==totales)||(aceptadas==3)) && aceptadas >= 1)
		strcpy(respuestaFuncion,"1");
	else if ((aceptadas+rechazadas==totales) && (aceptadas == 0))
		strcpy(respuestaFuncion,"2");
	else
		strcpy(respuestaFuncion,"0");
}

void NotificaStart(int partida, NotificacionesPendientes *Noti, listaJugadas *Jug,char invitador[20]) //notifca en formato 12/partida/jug1/jug2...
{ //notifica el comienzo de una partida a todas aquellas personas que la han acepado y posteriormente borra las notificaciones pendientes
	char ans[200];
	sprintf(ans,"12/%d/%s",partida,invitador);
	for(int j=0;j<Noti->num;j++)
	{
		if((Noti->invitaciones[j].numPartida==partida)&&(Noti->invitaciones[j].aceptado==1))
		{
			sprintf(ans,"%s/%s",ans,Noti->invitaciones[j].jugador);
		}
	}
	for(int j=0;j<Noti->num;j++)
	{
		if((Noti->invitaciones[j].numPartida==partida)&&(Noti->invitaciones[j].aceptado==1))
		{
			int s=BuscaSocket(&miLista,Noti->invitaciones[j].jugador);
			write (s,ans, strlen(ans));
			PonJugada(Jug,Noti->invitaciones[j].jugador,partida,0,0);
		}
	}
	int s=BuscaSocket(&miLista,invitador);
	write (s,ans, strlen(ans));
	PonJugada(Jug,invitador,partida,0,0);
}

void RespuestaInvitacion (char entrada[200], NotificacionesPendientes *Noti, char CancelarPartida[200], int partida)
{ //actualiza el estado de una notificacion en respuesta del cliente
	p = strtok(entrada, "/");
	int valor = atoi(p);
	char nombre[20];
	p = strtok(NULL,"/");
	strcpy(nombre,p);
	p = strtok(NULL, "/");
	while(p != NULL)
	{
		partida = atoi(p);
		int j = 0;
		int encontrado = 0;
		int e;
		while ((j<Noti->num) && (encontrado == 0))
		{
			if ((strcmp(Noti->invitaciones[j].jugador,nombre) == 0) && (Noti->invitaciones[j].numPartida == partida))
				encontrado = 1;
			else 
				j++;
		}
		if((encontrado == 1) && (valor == 1))
			Noti->invitaciones[j].aceptado = 1;
		else if((encontrado == 1) && (valor == -1))
			Noti->invitaciones[j].aceptado = -1;
		char result[200];
		ComprobarStart(partida,&misNotificaciones,result);
		if(strcmp(result,"1")==0)
		{
			NotificaStart(partida,&misNotificaciones,&misJugadas,Noti->invitaciones[j].invitador);
			EliminaNotificacionesPartida(partida);
		}
		if(strcmp(result,"2")==0)
		{
			sprintf(CancelarPartida,"14/%d/-1",partida);
		}
		p = strtok(NULL, "/");
	}
}

void enviaChat(char entrada[200],listaJugadas *Jug, char mensaje[512]) //reenvía un  mensaje a todos los jugadores de cierta partida
{
	p = strtok(entrada,"/");
	int partida=atoi(p);
	for(int j=0;j<Jug->num;j++)
	{
		if((Jug->jugadas[j].numPartida==partida))
		{
			int s=BuscaSocket(&miLista,Jug->jugadas[j].jugador);
			write (s,mensaje, strlen(mensaje));
		}
	}
}

int PonJugada(listaJugadas *lista, char nombre[20], int numPartida,int money1,int lastPos1)
{
	// Actualiza las jugadas, 0 si va bien, -1 si va mal
	int j = 0;
	int encontrado = 0;
	while ((j<lista->num) && (encontrado == 0))
	{
		if((lista->jugadas[j].numPartida == numPartida) && (strcmp(lista->jugadas[j].jugador,nombre) == 0))
		{
			encontrado = 1;
			lista->jugadas[j].money = lista->jugadas[j].money + money1;
			lista->jugadas[j].lastPos = lista->jugadas[j].lastPos + lastPos1;
			return 0;
		}
		else
			j++;
	}
	if (encontrado == 0)
	{
		if (lista->num == 100)
			return -1;
		else
		{
			lista->jugadas[lista->num].numPartida=numPartida;
			strcpy(lista->jugadas[lista->num].jugador,nombre);
			lista->jugadas[lista->num].money=money1;
			lista->jugadas[lista->num].lastPos=lastPos1;
			lista->num ++;
			return 0;
		}
	}
}
void DameInvitador(int partida, char invitador[20]) //devuelve el invitador de la partida solicitada
{
	int j = 0;
	int encontrado = 0;
	while ((j<misNotificaciones.num) && (encontrado == 0))
	{
		if(misNotificaciones.invitaciones[j].numPartida == partida)
		{
			encontrado = 1;
			strcpy(invitador,misNotificaciones.invitaciones[j].invitador);
		}
		else
		   j++;
	}
}

void DameEstatusPartida(int partida, char respuesta[200], listaJugadas *jug) //devuelve el estado y info de cada jugador de una partida
{
	int j;
	int count = 0;
	sprintf(respuesta,"15/%d/",partida);
	char respuesta1[200];
	for (j = 0; j<jug->num; j++)
	{
		if(jug->jugadas[j].numPartida == partida)
		{
			sprintf(respuesta1,"%s%s/%d/%d/",respuesta1,jug->jugadas[j].jugador,jug->jugadas[j].money,jug->jugadas[j].lastPos);
			count ++;
		}
	}
	sprintf(respuesta,"%s%d/%s",respuesta,count,respuesta1);
	int sockets[100];
	for (j = 0; j<jug->num; j++)
	{
		if(jug->jugadas[j].numPartida == partida)
		{
			int e = BuscaSocket(&miLista,jug->jugadas[j].jugador); 
			write(e,respuesta, strlen(respuesta));
		}
	}
}

void *AtenderCliente (void *socket) //thread dedicadoa atender a cada cliente
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
		char peticionInicial[512];
		strcpy(peticionInicial,peticion);
		
		printf ("Peticion: %s \n", peticion);
		// vamos a ver que quieren
		p = strtok(peticion, "/");
		int codigo =  atoi(p);
		// Ya tenemos el c?digo de la petici?n
		if (codigo !=0)
		{			
			printf ("Codigo: %d", codigo);
		}
		
		if (codigo ==0) //petici?n de desconexi?n
		{
			pthread_mutex_lock( &mutex); //no puedes interrumpirme
			printf("Cliente Desconectado");
			liberaSocket(sock_conn);
			terminar=1;
			pthread_mutex_unlock( &mutex); //ya puedes interrumpirme
			Notificar();
		}
		else if (codigo ==1) //CODI LOGIN
		{
			//El hace el log in en el juego
			pthread_mutex_lock( &mutex);
			conectar(NULL,respuesta,sock_conn);
			pthread_mutex_unlock( &mutex);
			Notificar();
		}
		else if (codigo ==2)
		{
			//el cliente quiere recuperar la contraseña
			recuperaPSW(NULL,respuesta);
			//codi 2 de tornada
		}
		else if (codigo == 3)
		{
			// El cliente pide quien es la persona con mas puntos
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
			// El cliente se registra 
			pthread_mutex_lock( &mutex);
			registro(NULL,respuesta);
			pthread_mutex_unlock( &mutex);
			//codi 6 de tornada
		}
		else if (codigo == 7)
		{
			//El cliente pide la lista de conectados. Esta funcion se hace al conectar el cliente al servidor. A partir de ese momento el servidor
			//la envia de manera automatica
			Conectados(respuesta);
			//codi 7 de tornada
		}
		else if (codigo == 8)
		{
			//El cliente envia una lista de jugadores a los que quiere invitar, y el numero de la partida a la que estan invitados
			pthread_mutex_lock( &mutex);
			char Invitados[200];
			char Invitador[200];
			char GameNum[10];
			p = strtok(NULL,"/");
			strcpy(GameNum,p);
			int GameNum1 = atoi(GameNum);
			DameInvitados(NULL, Invitados, Invitador, GameNum1, &misNotificaciones);
			pthread_mutex_unlock( &mutex);
			p = strtok(Invitados,"/");
			sprintf(respuesta,"8/%s/%s",Invitador,GameNum);
			while(p!=NULL)
			{
				int s=BuscaSocket(&miLista,p);
				write (s,respuesta, strlen(respuesta));
				p = strtok(NULL,"/");
			}
			//falta crear taula partides
			//codi 8 de tornada
		}
		else if (codigo == 9)
		{
			//El cliente hace un log out, sale de la partida pero sigue conectado al servidor
			pthread_mutex_lock( &mutex);
			LogOut(NULL);
			pthread_mutex_unlock( &mutex);
		}
		else if (codigo ==10)
		{
			//El cliente pide las invitaciones que tiene pendientes
			char jugador[200];
			char listaInvitaciones[200];
			strcpy(listaInvitaciones, "");
			p = strtok(NULL,"/");
			strcpy(jugador,p);
			misInvitaciones(&misNotificaciones, jugador, listaInvitaciones);
			sprintf(respuesta,"10/%s",listaInvitaciones);
		}
	
		else if (codigo == 11)
		{
			//El cliente ha creado una nueva partida
			crearPartida(respuesta);
		}
		
		else if (codigo == 12)
		{
			//El cliente envia un 12/1 o 12/-1 seguido de los numeros de partida. 1 quiere decir que el cliente ha aceptado la partida, -1 no la ha aceptado
			/*	El codi per part del servidor ve donat com 12/1/Jugador/numPartida1/numPartida2/...   El segon paràmetre del codi és 1 només si s'ha acceptat la invitació*/
			char respuestaCancelar [200];
			strcpy(respuestaCancelar,"nada");
			int partida;
			pthread_mutex_lock( &mutex);
			RespuestaInvitacion(NULL,&misNotificaciones, respuestaCancelar, partida);
			pthread_mutex_unlock( &mutex);
			if (strcmp(respuestaCancelar,"nada") != 0)
			{
				char invitador[20];
				DameInvitador(partida, invitador);
				int j = BuscaSocket(&miLista,invitador);
				write (j,respuestaCancelar, strlen(respuestaCancelar));
			}
		}
		else if (codigo==13)
		{
			enviaChat(NULL,&misJugadas,peticionInicial);
		}
		else if (codigo==14)
		{
			p = strtok(NULL,"/");
			int num = atoi(p);
			DameEstatusPartida(num, respuesta, &misJugadas);
			printf("%s",respuesta);
		}
		else if (codigo == 15)
		{
			p = strtok(NULL,"/");
			int tirada = atoi(p);
			p = strtok(NULL,"/");
			char jugador[20];
			strcpy(jugador,p);
			p = strtok(NULL,"/");
			int numpart = atoi(p);	
			pthread_mutex_lock( &mutex);
			int e = PonJugada(&misJugadas,jugador, numpart,0,tirada);
			pthread_mutex_unlock( &mutex);
			if (e == 0)
			{
				DameEstatusPartida(numpart, respuesta, &misJugadas);
			}
			else
				sprintf(respuesta,"15/-1");
		}
		if (codigo !=0 && codigo!=8 && codigo!=9 && codigo!=12 && codigo!=13 && codigo!=15)
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
	conn = mysql_real_connect (conn, "shiva2.upc.es","root", "mysql","Jellyfish", 0, NULL, 0);
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
	serv_adr.sin_port = htons(9051);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	pthread_t thread;
	for (;;){
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		
		int j = PonSocket(sock_conn);
		//sock_conn es el socket que usaremos para este cliente
		
		// Crear thead y decirle lo que tiene que hacer
		
		pthread_create (&thread, NULL, AtenderCliente,&sockets[j]);
	}	
}

