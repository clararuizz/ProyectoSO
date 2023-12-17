#include <string.h>
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
#include<unistd.h>


//Estructura necesaria para acceso excluyente.
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;


// Structs utilizados durante el código
typedef struct {
	char nombre[20];
	int socket;
} Conectado;

typedef struct {
	Conectado conectados[100];
	int num;
} ListaConectados;

//Lista de conectados con su socket y nombre
ListaConectados miLista; 

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

//Notificaciones de Partidas Pendientes
NotificacionesPendientes misNotificaciones; 

typedef struct {
	int numPartida;
	char jugador[20];
	int money;
	int lastPos;
	int ListaPropiedades[40];
} Jugada;

typedef struct {
	Jugada jugadas[1000];
	int num;	
} listaJugadas; 

//Info de partidas ya consolidadas y empezadas
listaJugadas misJugadas; 

typedef struct {
	int numPartida;
	int turno;
	int numJugadores;
	int carcel1;
	int carcel2;
	int carcel3;
	int carcel4;
	int acabado1;
	int acabado2;
	int acabado3;
	int acabado4;
} TurnoPartida;

typedef struct {
	TurnoPartida turnos[1000];
	int num;	
} listaTurnos; 

// Definicion de los turnos dependiendo del numero de jugadores
listaTurnos misTurnos; 

typedef struct{
	int id;
	char nombre[60];
	int precio;
	int alquiler;
} Propiedad;

typedef Propiedad Propiedades[39];

// Propiedades existentes en el Monopoly
Propiedades Propiedades1;

int CajaDeComunidad = 0;

char consulta[500];
int i;
int sockets[100];

// Conexion con la base de datos
MYSQL *conn;
int err;
MYSQL_RES *resultado;
MYSQL_ROW row;

char *p;

// Devuelve posicion socket en la lista
int PonSocket(int socket) 
{
	// Añade socket a la lista de sockets
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

int liberaSocket(int socket) 
{
	//1 si no encuentra socket
	// Asigna 0 a socket liberado
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
	// Retorna el socket, -1 si este no esta en la lista
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
	// Devuelve la posicion en la lista, -1 si no esta en la lista
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
	// Devuelve si elmina la persona, -1 si no lo elminina
	int pos = BuscaPosicion(lista,nombre);
	int k;
	if (pos == -1)
		return -1;
	else
	{
		for(k = pos; k<lista->num;k++)
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
	// Pone los nombres de los conectados en la lista que se envia como parametro.
	// 2/nombre_1/nombre_2
	sprintf(conectados,"%d",lista->num);
	int k;
	for(k = 0; k<lista->num; k++)
	{
		sprintf(conectados,"%s/%s",conectados,lista->conectados[k].nombre);
	}
}

void conectar(char entrada[200],char respuestaFuncion[200],int socketconn) 
{ 	// comprueba que login i psw este bien y si es asi responde 1
	// si el login es correcto introduce el usuario a lista de conectados
	p = strtok(entrada, "/");
	char pass[20];
	char ID[20];
	char nombre[20];
	strcpy (nombre, p);
	p = strtok(NULL, "/");
	strcpy(pass,p);
	int u=0;
	int found=0;
	while (u<miLista.num&&(found==0))
	{
		if(strcmp(miLista.conectados[u].nombre,nombre)==0)
			found=1;
		u++;
	}
	if(found==1)
	   strcpy(respuestaFuncion,"1/3/");
	else
	{ 
		strcpy (consulta,"SELECT (PersonalData.ID) FROM PersonalData WHERE PersonalData.Name ='");
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
}
void jugadoresJugados(char entrada[200], char respuestaFuncion[200])
{
	//devuelve jugadores con los que se ha jugado alguna partida MIRAR SI DISTINCT FUNCIONA
	char jugador[20];
	p = strtok(entrada, "/");
	strcpy(jugador, p);
	sprintf(consulta,"SELECT DISTINCT Name FROM JUGADAS WHERE IDGame IN (SELECT IDGame FROM JUGADAS WHERE Name = '%s');",jugador);
	err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("\n Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	strcpy(respuestaFuncion,"3/");
	row = mysql_fetch_row (resultado);
	while(row!=NULL){
		if (strcmp(jugador, row[0]) != 0)
		{
			sprintf(respuestaFuncion,"%s%s/",respuestaFuncion,row[0]);
			row = mysql_fetch_row (resultado);
		}
		else{
			row = mysql_fetch_row (resultado);
		}
	}	
}

void ganadorJugadas(char entrada[200], char respuestaFuncion[200])
{
	//ganador de las partidas ganadas con X persona
	char miNombre[20];
	p = strtok(entrada, "/");
	strcpy(miNombre, p);
	char jugador[20];
	p = strtok(entrada, "/");
	strcpy(jugador, p);
	sprintf(consulta,"SELECT DISTINCT Ganadores.Ganador,Ganadores.IDGame FROM Ganadores,JUGADAS WHERE JUGADAS.IDGame=Ganadores.IDGame AND Ganadores.IDGame IN (SELECT IDGame FROM JUGADAS WHERE Name = '%s') AND Ganadores.IDGame IN(SELECT IDGame FROM JUGADAS WHERE Name = '%s');",jugador,miNombre);
	err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("\n Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	strcpy(respuestaFuncion,"4/");
	row = mysql_fetch_row (resultado);
	while(row!=NULL){
		sprintf(respuestaFuncion,"%s%s_%s/",respuestaFuncion,row[1],row[0]);
		row = mysql_fetch_row (resultado);
	}	
}
void partidasTiempo(char entrada[200], char respuestaFuncion[200])
{
	//ganador de las partidas ganadas con X persona
	char tiempoFrom[20];
	strcpy(tiempoFrom,"");
	p = strtok(entrada, "/");
	strcpy(tiempoFrom, p);
	char tiempoTo[20];
	strcpy(tiempoTo,"");
	p = strtok(entrada, "/");
	strcpy(tiempoTo, p);
	sprintf(consulta,"SELECT ID,CreationDate FROM Games WHERE CreationDate >= '%s'AND CreationDate <= '%s';",tiempoFrom,tiempoTo);
	err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("\n Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	strcpy(respuestaFuncion,"5/");
	row = mysql_fetch_row (resultado);
	while(row!=NULL){
		sprintf(respuestaFuncion,"%s%s_%s/",respuestaFuncion,row[0],row[1]);
		row = mysql_fetch_row (resultado);
	}	
}

void recuperaPSW(char entrada[200],char respuestaFuncion[200]) 
{
	// Devuelve el psw si el usuario lo ha olvidado.
	// 2/contraseña.
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
void EliminaBBDD(char entrada[200], char respuesta[200])
{
	p = strtok(entrada, "/");
	char nombre[20];
	strcpy(nombre, p);
	char consulta[200];
	strcpy(consulta, "DELETE FROM PersonalData WHERE PersonalData.Name ='");
	strcat(consulta, nombre);
	strcat(consulta, "';");
	err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("\n Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
		sprintf(respuesta, "23/1");
	}
	else
	{
		sprintf(respuesta, "23/0");
	}
};
void registro(char entrada[200],char respuestaFuncion[200]) 
{ 
	// Añade el nuevo usuario a la base de datos. Solo lo añade si el nombre de usuario no existe aún
	// 6/0 si se ha realizado con exito
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
	// Crea una nueva partida y la añade a la DB
	// 11/numPartida
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
	strcpy(consulta,"INSERT INTO Games(CreationDate) VALUES(CURRENT_DATE);");
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
	// Printea los conectados para devolver
	// 7/conectados
	char conectados[100];
	DameConectados(&miLista,conectados);
	sprintf(respuestaFuncion,"7/%s",conectados);
}

void DameInvitados(char entrada[200],NotificacionesPendientes *misNotificaciones)
{ 
	// Añade las notificaciones a la lista de notis, una noti para cada persona invitada a la partida indicada
	char Invitados[200];
	char invitador[20];
	char respuesta[200];
	p = strtok(entrada,"/");
	int GameNum = atoi(p);
	p = strtok(NULL,"/");
	strcpy(invitador,p);
	printf("\n el usuario %s ha invitado a :",invitador);
	p = strtok(NULL,"/");
	while(p != NULL)
	{
		sprintf(Invitados,"%s/%s",Invitados, p);
		Notificacion n;
		n.numPartida = GameNum;
		strcpy(n.invitador, invitador);
		strcpy(n.jugador, p);
		n.aceptado = 0;
		misNotificaciones->invitaciones[misNotificaciones->num] = n;
		misNotificaciones->num = misNotificaciones->num + 1;
		sprintf(respuesta,"8/%s/%d",invitador,GameNum);
		int s = BuscaSocket(&miLista,p);
		write (s,respuesta, strlen(respuesta));
		p = strtok(NULL,"/");
	}
	printf("%s \n",Invitados);
}


void EliminaNotificacionesPartida(int partida) 
{
	// Elimina todas las notis referentes a una partida
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

void misInvitaciones(NotificacionesPendientes *misNotificaciones, char entrada[200], char respuesta[200]) 
{
	// Devuelve las notis totales de un usuario
	char jugador[200];
	char listaInvitaciones[200];
	strcpy(listaInvitaciones, "");
	p = strtok(entrada,"/");
	strcpy(jugador,p);
	int i = 0;
	for(i = 0; i < misNotificaciones->num; i++)
	{
		if((strcmp(misNotificaciones->invitaciones[i].jugador, jugador) == 0) && (misNotificaciones->invitaciones[i].aceptado == 0))
		{
			sprintf(listaInvitaciones,"%s%s/%d/", listaInvitaciones, misNotificaciones->invitaciones[i].invitador, misNotificaciones->invitaciones[i].numPartida);
		}
	}
	sprintf(respuesta,"10/%s",listaInvitaciones);

}

void Notificar() 
{
	// Notifica a todas las personas conectadas la lista de conectados
	char notificacion[200];
	Conectados(notificacion);
	int j;
	for (j = 0; j < 100; j++)
	{
		if(sockets[j]!=0)
			write (sockets[j],notificacion, strlen(notificacion));
	}
}
void ComprobarStart (int partida, NotificacionesPendientes *Noti,char respuestaFuncion[200]) 
{
	// Comprueba si una partida ya puede empezar
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

void SacaConectado(char entrada[200],ListaConectados *lista)
{
	p = strtok(entrada,"/");
	char nombre[20];
	strcpy (nombre, p);	
	EliminaConectado(lista,nombre);
	Notificar();
}

void NotificaStart(int partida, NotificacionesPendientes *Noti, listaJugadas *Jug,char invitador[20])
{ 
	// Notifica el comienzo de una partida a todas aquellas personas que la han acepado y posteriormente borra las notificaciones pendientes.
	// 12/partida/jug1/jug2...
	char ans[200];
	int numPlayers;
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
			IniciaPartida(Jug,Noti->invitaciones[j].jugador,partida,1500,0);
			numPlayers++;
		}
	}
	int s=BuscaSocket(&miLista,invitador);
	write (s,ans, strlen(ans));
	IniciaPartida(Jug,invitador,partida,1500,0);
	numPlayers++;
	PonTurnoPartida(&misTurnos,1,partida,numPlayers);
}

int IniciaPartida(listaJugadas *lista, char nombre[20], int numPartida,int money1,int lastPos1)
{
	// Inicia una partida, retorna la posicion final si todo va bien, -1 si va mal
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

int PonTurnoPartida(listaTurnos *lista, int turno, int partida,int players)
{
	if (lista->num == 1000)
		return -1;
	else
	{
		lista->turnos[lista->num].turno = turno;
		lista->turnos[lista->num].numPartida = partida;
		lista->turnos[lista->num].numJugadores = players;
		lista->num ++;
		return 0;
	}
}
void PonCarcel(listaTurnos *lista,char entrada[200])
{
	p = strtok(entrada, "/");
	int partida = atoi(p);
	p = strtok(NULL, "/");
	int numJugador =atoi(p);
	int j=0;
	int found=0;
	while((j<lista->num)&&(found==0))
	{
		if(lista->turnos[j].numPartida==partida)
		{
			found=1;
		}
		else
		   j++;
	}
	if(numJugador==1){
		lista->turnos[j].carcel1=2;
	}
	else if(numJugador==2){
		lista->turnos[j].carcel2=2;
	}
	else if(numJugador==3){
		lista->turnos[j].carcel3=2;
	}
	else if(numJugador==4){
		lista->turnos[j].carcel4=2;
	}
}

void PonPerdido(listaTurnos *lista, char entrada[200])
{
	p = strtok(entrada, "/");
	int partida = atoi(p);
	p = strtok(NULL, "/");
	int numJugador =atoi(p);
	int j=0;
	int found=0;
	while((j<lista->num)&&(found==0))
	{
		if(lista->turnos[j].numPartida==partida)
		{
			found=1;
		}
		else
		   j++;
	}
	if(numJugador==1){
		lista->turnos[j].acabado1=1;
	}
	else if(numJugador==2){
		lista->turnos[j].acabado2=1;
	}
	else if(numJugador==3){
		lista->turnos[j].acabado3=1;
	}
	else if(numJugador==4){
		lista->turnos[j].acabado4=1;
	}
}
void SiguienteTurno(listaTurnos *lista,listaJugadas *list, int partida)
{
	int j=0;
	int found=0;
	while((j<lista->num)&&(found==0))
	{
		if(lista->turnos[j].numPartida==partida)
		{
			while(found==0)
			{
				if(lista->turnos[j].turno==lista->turnos[j].numJugadores)
					lista->turnos[j].turno=1;
				else
					lista->turnos[j].turno=lista->turnos[j].turno+1;
				if((lista->turnos[j].carcel1>0)&&(lista->turnos[j].turno==1))
					lista->turnos[j].carcel1=lista->turnos[j].carcel1-1;
				else if((lista->turnos[j].carcel2>0)&&(lista->turnos[j].turno==2))
					lista->turnos[j].carcel2=lista->turnos[j].carcel2-1;
				else if((lista->turnos[j].carcel3>0)&&(lista->turnos[j].turno==3))
					lista->turnos[j].carcel3=lista->turnos[j].carcel3-1;
				else if((lista->turnos[j].carcel4>0)&&(lista->turnos[j].turno==4))
					lista->turnos[j].carcel4=lista->turnos[j].carcel4-1;
				else if((lista->turnos[j].acabado1==1)&&(lista->turnos[j].turno==1))
					found=0;
				else if((lista->turnos[j].acabado2==1)&&(lista->turnos[j].turno==2))
					found=0;
				else if((lista->turnos[j].acabado3==1)&&(lista->turnos[j].turno==3))
					found=0;
				else if((lista->turnos[j].acabado4==1)&&(lista->turnos[j].turno==4))
					found=0;
				else
					found=1;
			}
			int perdidos=lista->turnos[j].acabado4+lista->turnos[j].acabado3+lista->turnos[j].acabado2+lista->turnos[j].acabado1;
			if(perdidos>=(lista->turnos[j].numJugadores-1))
			{
				char part[20];
				strcpy(part,"");
				sprintf(part,"%d/",partida);
				DameGanador(part,list);		
			}
		}
		else
		   j++;
	}
	char respuestaTurno[200];
	sprintf(respuestaTurno,"18/%d/%d",partida,lista->turnos[j].turno);
	for(int j=0;j<list->num;j++)
	{
		if((list->jugadas[j].numPartida==partida))
		{
			int s=BuscaSocket(&miLista,list->jugadas[j].jugador);
			write (s,respuestaTurno, strlen(respuestaTurno));
			printf("\n %s \n", respuestaTurno);
		}
	}
}

void RespuestaInvitacion (char entrada[200], NotificacionesPendientes *Noti)
{ 
	// Actualiza el estado de una notificacion en respuesta del cliente
	char CancelarPartida[200];
	int partida;
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
			char invitador[20];
			DameInvitador(partida, invitador);
			int j = BuscaSocket(&miLista,invitador);
			write (j,CancelarPartida, strlen(CancelarPartida));
		}
		p = strtok(NULL, "/");
	}
}

void enviaChat(char entrada[200],listaJugadas *Jug) 
{
	// Reenvía un  mensaje a todos los jugadores de cierta partida
	char respuesta[512];
	p = strtok(entrada,"/");
	int partida=atoi(p);
	char nombre[20];
	char mensaje[400];
	p = strtok(NULL,"/");
	strcpy(nombre,p);
	p = strtok(NULL,"/");
	strcpy(mensaje,p);	
	sprintf(respuesta,"13/%d/%s/%s",partida,nombre,mensaje);
	printf("\n %s \n",respuesta);
	for(int j=0;j<Jug->num;j++)
	{
		if((Jug->jugadas[j].numPartida==partida))
		{
			int s=BuscaSocket(&miLista,Jug->jugadas[j].jugador);
			write (s,respuesta, strlen(respuesta));
		}
	}
}

int PonJugada(listaJugadas *lista,int numPartida, char nombre[20], int tirada, char dinero[20])
{
	// Actualiza las jugadas, retorna la posicion final si todo va bien, -1 si va mal
	int j = 0;
	int encontrado = 0;
	while ((j<lista->num) && (encontrado == 0))
	{
		if((lista->jugadas[j].numPartida == numPartida) && (strcmp(lista->jugadas[j].jugador,nombre) == 0))
		{
			encontrado = 1;
			if ((lista->jugadas[j].lastPos + tirada) < 40)
			{
				lista->jugadas[j].lastPos = lista->jugadas[j].lastPos + tirada;
				sprintf(dinero,"%d",lista->jugadas[j].money);
				return lista->jugadas[j].lastPos;
			}
			else
			{
				lista->jugadas[j].lastPos = lista->jugadas[j].lastPos + tirada - 40;
				lista->jugadas[j].money = lista->jugadas[j].money + 200;
				sprintf(dinero,"%d",lista->jugadas[j].money);
				return lista->jugadas[j].lastPos;
			}
		}
		else
			j++;
	}
	if (encontrado == 0)
		return -1;
}
void DameInvitador(int partida, char invitador[20]) 
{
	// Devuelve el invitador de la partida solicitada
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

void DameEstatusPartida(int partida, listaJugadas *jug) 
{
	// Devuelve el estado y info de cada jugador de una partida
	int j;
	int count = 0;
	char respuesta2[200];
	strcpy(respuesta2,"");
	sprintf(respuesta2,"15/%d/",partida);
	char respuesta1[200];
	strcpy(respuesta1,"");
	for (j = 0; j<jug->num; j++)
	{
		if(jug->jugadas[j].numPartida == partida)
		{
			sprintf(respuesta1,"%s%s/%d/%d/",respuesta1,jug->jugadas[j].jugador,jug->jugadas[j].money,jug->jugadas[j].lastPos);
			count ++;
		}
	}
	sprintf(respuesta2,"%s%d/%s",respuesta2,count,respuesta1);
	int sockets[100];
	for (j = 0; j<jug->num; j++)
	{
		if(jug->jugadas[j].numPartida == partida)
		{
			int e = BuscaSocket(&miLista,jug->jugadas[j].jugador); 
			write(e,respuesta2, strlen(respuesta2));
			printf("\n %s \n",respuesta2);
		}
	}
}

void PuedoComprar(listaJugadas *lista, int numPartida, char nombre[20], int Pos, char respuesta[200], Propiedades Propie, int dinero)
{
	// Define si un usuario puede comprar o no una propiedad.
	// 16/0/numPartida/nombrePropiedad/AlquilerPropiedad/nombreJugador/nombrePropietario si la propiedad ya es de alguien
	// 16/1/1/numPartida/Posicion/nombrePropiedad/precioPropiedad si el jugador puede comprar la propiedad
	int j = 0;
	int encontrado = 0;
	int e = 0;
	if (Pos != 0)
	{
		while ((j<lista->num) && (encontrado == 0))
		{
			if(lista->jugadas[j].numPartida == numPartida)
			{	
				if (lista->jugadas[j].ListaPropiedades[Pos] == 1)
				{
					encontrado = 1;
					sprintf(respuesta,"16/0/%d/%s/%d/%s/%s",numPartida,Propie[Pos].nombre,Propie[Pos].alquiler,nombre,lista->jugadas[j].jugador);
				}
				else
					j++;
			}
			else
			   j++;
		}
		if (encontrado == 0)
		{		
			if (Propie[Pos].precio <= dinero)
			{
				printf("\n %s \n", Propie[Pos].nombre);
				sprintf(respuesta,"16/1/1/%d/%d/%s/%d",numPartida,Pos,Propie[Pos].nombre,Propie[Pos].precio); //si el jugador puede comprar la propiedad, se notifica
				printf("\n %s \n %s \n", respuesta,Propie[Pos].nombre);
			}
			else
			{
				sprintf(respuesta,"16/1/-1/%d/%s/%d",numPartida,Propie[Pos].nombre,Propie[Pos].precio); //el jugador no puede comprar la propiedad
			}
		}
	}
}


void ComienzaTablero (Propiedades *Propidades1)
{
	// Se insertan todas las propiedades en la lista.
	Propiedades1[0].id = 0;
	strcpy(Propiedades1[0].nombre,"Salida");
	Propiedades1[0].precio = 0;
	Propiedades1[0].alquiler = 0;
	
	Propiedades1[1].id = 1;
	strcpy(Propiedades1[1].nombre,"Carrer de Roger de Lluria");
	Propiedades1[1].precio = 60;
	Propiedades1[1].alquiler = 2;
	
	Propiedades1[2].id = 2;
	strcpy(Propiedades1[2].nombre,"Caja de Comunidad");
	Propiedades1[2].precio = 0;
	Propiedades1[2].alquiler = 0;
	
	Propiedades1[3].id = 3;
	strcpy(Propiedades1[3].nombre,"Carrer de Rosello");
	Propiedades1[3].precio = 60;
	Propiedades1[3].alquiler = 4;
	
	Propiedades1[4].id = 4;
	strcpy(Propiedades1[4].nombre,"Impost sobre el Capital");
	Propiedades1[4].precio = -200; //Com que es un impost, el preu es negatiu
	Propiedades1[4].alquiler = 0;
	
	Propiedades1[5].id = 5;
	strcpy(Propiedades1[5].nombre,"Estacio FF.CC");
	Propiedades1[5].precio = 200;
	Propiedades1[5].alquiler = 25;
	
	Propiedades1[6].id = 6;
	strcpy(Propiedades1[6].nombre,"Carrer de La Marina");
	Propiedades1[6].precio = 100;
	Propiedades1[6].alquiler = 6;
	
	Propiedades1[7].id = 7;
	strcpy(Propiedades1[7].nombre,"Suerte");
	Propiedades1[7].precio = 0;
	Propiedades1[7].alquiler = 0;
	
	Propiedades1[8].id = 8;
	strcpy(Propiedades1[8].nombre,"Carrer de Compte d'Urgell");
	Propiedades1[8].precio = 100;
	Propiedades1[8].alquiler = 6;
	
	Propiedades1[9].id = 9;
	strcpy(Propiedades1[9].nombre,"Carrer del Consell de Cent");
	Propiedades1[9].precio = 120; 
	Propiedades1[9].alquiler = 8;
	
	Propiedades1[10].id = 10;
	strcpy(Propiedades1[10].nombre,"Carcel");
	Propiedades1[10].precio = 0;
	Propiedades1[10].alquiler = 0; 
	
	Propiedades1[11].id = 11;
	strcpy(Propiedades1[11].nombre,"Carrer de Muntaner");
	Propiedades1[11].precio = 140;
	Propiedades1[11].alquiler = 10;
	
	Propiedades1[12].id = 12;
	strcpy(Propiedades1[12].nombre,"Companyia d'electricitat");
	Propiedades1[12].precio = 150;
	Propiedades1[12].alquiler = 4; //hem de multiplicar el valor del dau per 4 o 10, depen de si te les dues propietats
	
	Propiedades1[13].id = 13;
	strcpy(Propiedades1[13].nombre,"Carrer d'Aribau");
	Propiedades1[13].precio = 140;
	Propiedades1[13].alquiler = 10;
	
	Propiedades1[14].id = 14;
	strcpy(Propiedades1[14].nombre,"Avinguda de Josep Tarradelles");
	Propiedades1[14].precio = 160; 
	Propiedades1[14].alquiler = 12;
	
	Propiedades1[15].id = 15;
	strcpy(Propiedades1[15].nombre,"Estacio Passeig de Gracia");
	Propiedades1[15].precio = 200;
	Propiedades1[15].alquiler = 25; //1 estacio = 25, 2 = 50, 3 = 100, 4 = 200
	
	Propiedades1[16].id = 16;
	strcpy(Propiedades1[16].nombre,"Passeig de sant Joan");
	Propiedades1[16].precio = 180;
	Propiedades1[16].alquiler = 14;
	
	Propiedades1[17].id = 17;
	strcpy(Propiedades1[17].nombre,"Caja de comunidad");
	Propiedades1[17].precio = 0;
	Propiedades1[17].alquiler = 0;
	
	Propiedades1[18].id = 18;
	strcpy(Propiedades1[18].nombre,"Carrer de la Diputació");
	Propiedades1[18].precio = 180;
	Propiedades1[18].alquiler = 14;
	
	Propiedades1[19].id = 19;
	strcpy(Propiedades1[19].nombre,"Carrer d'aragó");
	Propiedades1[19].precio = 200; 
	Propiedades1[19].alquiler = 16;
	
	Propiedades1[20].id = 20;
	strcpy(Propiedades1[20].nombre,"Caja de comunidad");
	Propiedades1[20].precio = 0; //Es queda tot el que hi ha en aquell moment a la recaudació d'impostos
	Propiedades1[20].alquiler = 0; 
	
	Propiedades1[21].id = 2;
	strcpy(Propiedades1[21].nombre,"Placa Urquinaona");
	Propiedades1[21].precio = 220;
	Propiedades1[21].alquiler = 18;
	
	Propiedades1[22].id = 22;
	strcpy(Propiedades1[22].nombre,"Suerte");
	Propiedades1[22].precio = 0;
	Propiedades1[22].alquiler = 0;
	
	Propiedades1[23].id = 23;
	strcpy(Propiedades1[23].nombre,"Carrer de Fontanella");
	Propiedades1[23].precio = 220;
	Propiedades1[23].alquiler = 18;
	
	Propiedades1[24].id = 24;
	strcpy(Propiedades1[24].nombre,"Ronda de Sant Pere");
	Propiedades1[24].precio = 240; 
	Propiedades1[24].alquiler = 20;
	
	Propiedades1[25].id = 25;
	strcpy(Propiedades1[25].nombre,"Estacio De Franca");
	Propiedades1[25].precio = 200;
	Propiedades1[25].alquiler = 25;
	
	Propiedades1[26].id = 26;
	strcpy(Propiedades1[26].nombre,"Les Rambles");
	Propiedades1[26].precio = 260;
	Propiedades1[26].alquiler = 22;
	
	Propiedades1[27].id = 27;
	strcpy(Propiedades1[27].nombre,"Carrer de Pau Claris");
	Propiedades1[27].precio = 260;
	Propiedades1[27].alquiler = 22;
	
	Propiedades1[28].id = 28;
	strcpy(Propiedades1[28].nombre,"Companyia d'aigües");
	Propiedades1[28].precio = 150;
	Propiedades1[28].alquiler = 4;
	
	Propiedades1[29].id = 29;
	strcpy(Propiedades1[29].nombre,"Plaça de Catalunya");
	Propiedades1[29].precio = 280; 
	Propiedades1[29].alquiler = 24;
	
	Propiedades1[30].id = 30;
	strcpy(Propiedades1[30].nombre,"Carcel");
	Propiedades1[30].precio = 0;
	Propiedades1[30].alquiler = 0; 
	
	Propiedades1[31].id = 31;
	strcpy(Propiedades1[31].nombre,"Avinguda Portal de l'Angel");
	Propiedades1[31].precio = 300;
	Propiedades1[31].alquiler = 26;

	Propiedades1[32].id = 32;
	strcpy(Propiedades1[32].nombre,"Carrer de Pelai");
	Propiedades1[32].precio = 300;
	Propiedades1[32].alquiler = 26;
	
	Propiedades1[33].id = 33;
	strcpy(Propiedades1[33].nombre,"Caja de Comunidad");
	Propiedades1[33].precio = 0;
	Propiedades1[33].alquiler = 0;
	
	Propiedades1[34].id = 3;
	strcpy(Propiedades1[34].nombre,"Via Augusta");
	Propiedades1[34].precio = 320; 
	Propiedades1[34].alquiler = 28;
	
	Propiedades1[35].id = 35;
	strcpy(Propiedades1[35].nombre,"Estacio de Sants");
	Propiedades1[35].precio = 200;
	Propiedades1[35].alquiler = 25; //1 estacio = 25, 2 = 50, 3 = 100, 4 = 200
	
	Propiedades1[36].id = 36;
	strcpy(Propiedades1[36].nombre,"Suerte");
	Propiedades1[36].precio = 0; 
	Propiedades1[36].alquiler = 0;
	
	Propiedades1[37].id = 37;
	strcpy(Propiedades1[37].nombre,"Carrer de Balmes");
	Propiedades1[37].precio = 350;
	Propiedades1[37].alquiler = 35;
	
	Propiedades1[38].id = 38;
	strcpy(Propiedades1[38].nombre,"Impost de Luxe");
	Propiedades1[38].precio = -100;
	Propiedades1[38].alquiler = 0;
	
	Propiedades1[39].id = 39;
	strcpy(Propiedades1[39].nombre,"Passeig de Gracia");
	Propiedades1[39].precio = 400;
	Propiedades1[39].alquiler = 50;
}

void DameDineroRestante(int precio, char name[200], listaJugadas *misJugadas, char respuesta[200])
{
	// Indica cuanto dinero le queda al jugador
	// 17/nombre/dinero
	for (int i = 0; i < misJugadas->num; i++)
	{
		if (strcmp(misJugadas->jugadas[i].jugador, name) == 0)
		{
			int dif = misJugadas->jugadas[i].money - precio;
			misJugadas->jugadas[i].money = dif;
			sprintf(respuesta, "17/%s/%d \n", name, misJugadas->jugadas[i].money);
		}
	}
	printf("\n El jugador %s ha comprado y ahora tiene %d dinero", name, misJugadas->jugadas[i].money);
}

void ComprarPropiedad(listaJugadas *jug, char entrada[200])
{
	//Permite comprar una propiedad, retorna 0 si todo va bien y -1 si algo va mal.
	// 17/numPartida/jugador/nombrePropiedad
	p = strtok(entrada,"/");
	int numPart = atoi(p);
	p = strtok(entrada, "/");
	int pos = atoi(p);
	char jugador[200];
	p = strtok(NULL, "/");
	strcpy(jugador, p);
	char propiedad[200];
	p = strtok(NULL, "/");
	strcpy(propiedad, p);
	p = strtok(NULL, "/");
	int precio = atoi(p);
	int i = 0;
	int encontrado = 0;
	while ((i<jug->num) && (encontrado == 0))
	{
		if ((jug->jugadas[i].numPartida == numPart) && (strcmp(jugador,jug->jugadas[i].jugador) == 0))
		{
			encontrado = 1;
			jug->jugadas[i].ListaPropiedades[pos] = 1;
			jug->jugadas[i].money = jug->jugadas[i].money - precio;
		}
		else
			i++;	
	}
	char notificacion[200];
	sprintf(notificacion,"17/%d/%s/%s/%d",numPart,jugador,propiedad,pos);
	for (int j = 0; j<jug->num; j++)
	{
		if(jug->jugadas[j].numPartida == numPart)
		{
			int e = BuscaSocket(&miLista,jug->jugadas[j].jugador); 
			write(e,notificacion, strlen(notificacion));
			printf("\n \n %s \n \n ", notificacion);
		}
	}	
	if (encontrado == 1)
	{
		sleep(1);
		DameEstatusPartida(numPart, jug);
	}
}

void CobrarAlquiler(listaJugadas *misJugadas, char entrada[200])
{
	// Cobra el alquiler de la propiedad en la que has caído.
	p = strtok(entrada, "/");
	int numGame = atoi(p);
	p = strtok(entrada, "/");
	char name[200];
	strcpy(name, p);
	p = strtok(NULL, "/");
	int alquiler = atoi(p);
	p = strtok(NULL, "/");
	char propietario[200];
	strcpy(propietario, p);
	int encontrado = 0;
	int i=0;
	while ((i < misJugadas->num) && (encontrado == 0))
	{
		if ((misJugadas->jugadas[i].numPartida == numGame) && (strcmp(name, misJugadas->jugadas[i].jugador) == 0))
		{
			encontrado = 1;
			misJugadas->jugadas[i].money = misJugadas->jugadas[i].money - alquiler;
		}
		else
		{
			i++;
		}
	}
	i = 0;
	encontrado = 0;
	while ((i < misJugadas->num) && (encontrado == 0))
	{
		if ((misJugadas->jugadas[i].numPartida == numGame) && (strcmp(propietario, misJugadas->jugadas[i].jugador) == 0))
		{
			encontrado = 1;
			misJugadas->jugadas[i].money = misJugadas->jugadas[i].money + alquiler;
		}
		else
		{
			i++;
		}
	}
	if (encontrado == 1)
	{
		ComprobarPerdedor(misJugadas,numGame); //miramos si el cliente ha perdido, si ha perdido responde con 
		//24/numpart/ nombre perdedor
		sleep(1);
		DameEstatusPartida(numGame, misJugadas);
	}
}
int PagarMulta(char nombre[20], int numPart, listaJugadas *list, int cantidad,int recaudacion, int sock_conn)
{
	// Paga una multa, retorna el estatus de la Caja de comunidad si va bien, -1 si va mal
	// 19/numPartida/dinero
	char respuesta[200];
	int j = 0;
	int encontrado = 0;
	while ((j < list->num) && (encontrado == 0))
	{
		if ((list->jugadas[j].numPartida == numPart) && (strcmp(nombre, list->jugadas[j].jugador) == 0))
		{
			encontrado = 1;
			list->jugadas[j].money = list->jugadas[j].money - cantidad;
			recaudacion = recaudacion + cantidad;
		}
		else
		{
			j++;
		}
	}
	if (encontrado == 1)
	{
		sprintf(respuesta, "19/%d/Has pagado una multa de %d euros",numPart,cantidad);
		write (sock_conn,respuesta, strlen(respuesta));
		return recaudacion;
	}
	else 
	{
		return -1;
	}
}

int Cobrar(char nombre[20], int numPart, listaJugadas *list, int cantidad, int sock_conn)
{
	// Cobra los 200 euros de la casilla de salida, retorna el 0 si va bien, -1 si va mal
	// 19/numPartida/mensaje
	char respuesta[200];
	int j = 0;
	int encontrado = 0;
	while ((j < list->num) && (encontrado == 0))
	{
		if ((list->jugadas[j].numPartida == numPart) && (strcmp(nombre, list->jugadas[j].jugador) == 0))
		{
			encontrado = 1;
			list->jugadas[j].money = list->jugadas[j].money + cantidad;
		}
		else
		{
			j++;
		}
	}
	if (encontrado == 1)
	{
		sprintf(respuesta, "19/%d/Has cobrado %d euros",numPart, cantidad);
		write (sock_conn,respuesta, strlen(respuesta));
		return 0;
	}
	else 
	{
		return -1;
	}
}
void PosicionCarcel(char nombre[20], int numPart, listaJugadas *list,int sock_conn)
{ 
	char respuesta[200];
	int encontrado = 0;
	int j = 0;
	while ((j < list->num) && (encontrado == 0))
	{
		if ((list->jugadas[j].numPartida == numPart) && (strcmp(nombre, list->jugadas[j].jugador) == 0))
		{
			encontrado = 1;
			list->jugadas[j].lastPos = 10;
		}
		else
		{
			j++;
		}
	}
	if (encontrado == 1)
	{
		sprintf(respuesta,"20/%d",numPart);
		write (sock_conn,respuesta, strlen(respuesta));
	}
}	

int PosicionSuerte(int numero, char nombre[20], int numPart, listaJugadas *list, int posicioninicial,int sock_conn)
{ 
	// Notifica la tarjeta de la suerte que te ha tocado
	// 19/numPartida/mensaje
	char respuesta[200];
	int j = 0;
	int encontrado = 0;
	int posicionfinal = -1;
	if (numero == 1)
	{
		while ((j < list->num) && (encontrado == 0))
		{
			if ((list->jugadas[j].numPartida == numPart) && (strcmp(nombre, list->jugadas[j].jugador) == 0))
			{
				encontrado = 1;
				list->jugadas[j].money = list->jugadas[j].money - 15;
			}
			else
			{
				j++;
			}
		}
		if (encontrado == 1)
			sprintf(respuesta, "19/%d/Has pagado una multa de 15 euros",numPart);
	}
	else if (numero == 2)
	{
		posicionfinal = 5;
		while ((j < list->num) && (encontrado == 0))
		{
			if ((list->jugadas[j].numPartida == numPart) && (strcmp(nombre, list->jugadas[j].jugador) == 0))
			{
				encontrado = 1;
				list->jugadas[j].lastPos = 5;
			}
			else
			{
				j++;
			}
		}
		if (encontrado == 1)
			sprintf(respuesta, "19/%d/Viaja hasta la estacion FF.CC.",numPart);
	}
	else if (numero == 3)
	{
		posicionfinal = 39;
		while ((j < list->num) && (encontrado == 0))
		{
			if ((list->jugadas[j].numPartida == numPart) && (strcmp(nombre, list->jugadas[j].jugador) == 0))
			{
				encontrado = 1;
				list->jugadas[j].lastPos = 39;
			}
			else
			{
				j++;
			}
		}
		if (encontrado == 1)
			sprintf(respuesta, "19/%d/Viaja hasta la estacion Passeig de Gracia",numPart);
	}
	else if (numero == 4)
	{
		posicionfinal = 0;
		while ((j < list->num) && (encontrado == 0))
		{
			if ((list->jugadas[j].numPartida == numPart) && (strcmp(nombre, list->jugadas[j].jugador) == 0))
			{
				encontrado = 1;
				list->jugadas[j].money = list->jugadas[j].money + 200;
				list->jugadas[j].lastPos = 0;
			}
			else
			{
				j++;
			}
		}
		if (encontrado == 1)
			sprintf(respuesta, "19/%d/Has ganado 200 euros",numPart);
	}
	else if (numero == 5)
	{
		while ((j < list->num) && (encontrado == 0))
		{
			if ((list->jugadas[j].numPartida == numPart) && (strcmp(nombre, list->jugadas[j].jugador) == 0))
			{
				encontrado = 1;
				list->jugadas[j].money = list->jugadas[j].money - 50;
			}
			else
			{
				j++;
			}
		}
		if (encontrado == 1)
			sprintf(respuesta, "19/%d/Has pagado una multa de 50 euros",numPart);
	}
	else if (numero == 6)
	{
		while ((j < list->num) && (encontrado == 0))
		{
			if ((list->jugadas[j].numPartida == numPart) && (strcmp(nombre, list->jugadas[j].jugador) == 0))
			{
				encontrado = 1;
				list->jugadas[j].money = list->jugadas[j].money + 50;
			}
			else
			{
				j++;
			}
		}
		if (encontrado == 1)
			sprintf(respuesta, "19/%d/Has ganado 50 euros",numPart);
	}
	printf("\n %s \n", respuesta);
	write (sock_conn,respuesta, strlen(respuesta));
	return posicionfinal;
}
int CobrarElectricidad (char nombre[20], int numPart, listaJugadas *list,int sock_conn, int tirada)
{ 
	// Cobra el impuesto de la electricidad
	// 19/numPartida/mensaje
	int j = 0;
	int encontrado = 0;
	int comprada = 0;
	char respuesta[200];
	char propietario[20];
	int cuanto;
	int encontrado2  = 0;
	while ((j < list->num) && (comprada == 0))
	{
		if ((list->jugadas[j].numPartida == numPart) && (list->jugadas[j].ListaPropiedades[12] == 1))
		{
			comprada = 1;
		}
		else
		{
			j++;
		}
	}
	if (comprada == 0)
	{
		return 0;
	}
	j = 0;
	while ((j < list->num) && (encontrado == 0) && (comprada == 1))
	{
		if ((list->jugadas[j].numPartida == numPart) && (strcmp(nombre, list->jugadas[j].jugador) == 0))
		{
			encontrado = 1;
			list->jugadas[j].money = list->jugadas[j].money - tirada*4;
		}
		else
		{
			j++;
		}
	}
	if (encontrado == 1 && comprada == 1)
	{
		j = 0;
		while ((j < list->num) && (encontrado2 == 0))
		{
			if ((list->jugadas[j].numPartida == numPart) && (list->jugadas[j].ListaPropiedades[12] == 1))
			{
				encontrado2 = 1;
				strcpy(propietario,list->jugadas[j].jugador);
				cuanto = tirada*4;
				list->jugadas[j].money = list->jugadas[j].money + tirada*4;
			}
			else
			{
				j++;
			}
		}
	}
	if (comprada == 1 && encontrado == 1 && encontrado2 == 1)
	{
		sprintf(respuesta,"19/%d/has pagado %d a %s",numPart,cuanto,propietario);
		write (sock_conn,respuesta, strlen(respuesta));
		return 1;
	}
	else
		return -1;
}

int CobrarAguas (char nombre[20], int numPart, listaJugadas *list,int sock_conn, int tirada)
{ 
	// Cobra el impuesto del agua.
	// 19/numPartida/mensaje
	int j = 0;
	int encontrado = 0;
	int comprada = 0;
	char respuesta[200];
	char propietario[20];
	int cuanto;
	int encontrado2  = 0;
	while ((j < list->num) && (comprada == 0))
	{
		if ((list->jugadas[j].numPartida == numPart) && (list->jugadas[j].ListaPropiedades[28] == 1))
		{
			comprada = 1;
		}
		else
		{
			j++;
		}
	}
	if (comprada == 0)
	{
		return 0;
	}
	j = 0;
	while ((j < list->num) && (encontrado == 0) && (comprada == 1))
	{
		if ((list->jugadas[j].numPartida == numPart) && (strcmp(nombre, list->jugadas[j].jugador) == 0))
		{
			encontrado = 1;
			list->jugadas[j].money = list->jugadas[j].money - tirada*4;
		}
		else
		{
			j++;
		}
	}
	if (encontrado == 1 && comprada == 1)
	{
		j = 0;
		while ((j < list->num) && (encontrado2 == 0))
		{
			if ((list->jugadas[j].numPartida == numPart) && (list->jugadas[j].ListaPropiedades[28] == 1))
			{
				encontrado2 = 1;
				strcpy(propietario,list->jugadas[j].jugador);
				cuanto = tirada*4;
				list->jugadas[j].money = list->jugadas[j].money + tirada*4;
			}
			else
			{
				j++;
			}
		}
	}
	if (comprada == 1 && encontrado == 1 && encontrado2 == 1)
	{
		sprintf(respuesta,"19/%d/has pagado %d a %s",numPart,cuanto,propietario);
		write (sock_conn,respuesta, strlen(respuesta));
		return 1;
	}
	else
		return -1;
}

void GuardaPartida(char entrada[200], listaJugadas *list)
{
	p = strtok(entrada,"/");
	int numpart = atoi(p);
	char inicio[2000];
	int j;
	int e;
	int counter = 0;
	char elimina[200];
	sprintf(elimina,"DELETE FROM JUGADAS where IDGame = %d;",numpart);
	err = mysql_query(conn, elimina);
	if (err!=0)
	{
		printf ("\n Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	sprintf(inicio,"INSERT INTO JUGADAS (IDGame, Name, Dinero,Posicion, Propiedades) VALUES(");
	for (j = 0; j < list->num; j++)
	{	
		char total[200];
		char medio[200];
		char fin[200];
		strcpy(total,"");
		strcpy(medio,"");
		strcpy(fin,"");
		if(list->jugadas[j].numPartida == numpart)
		{
			sprintf(medio,"%d,'%s',%d,%d,'",numpart,list->jugadas[j].jugador,list->jugadas[j].money,list->jugadas[j].lastPos);
			for(e = 0; e <39; e++)
			{
				if(list->jugadas[j].ListaPropiedades[e] == 1)
				{
					sprintf(fin,"%s%d/",fin,e);
					counter ++;
				}
			}
			if (counter != 0)
				sprintf(total,"%s%s%s');",inicio,medio,fin);
			else
				sprintf(total,"%s%sNULL');",inicio,medio,fin);
			printf("\n %s \n",total);
			err = mysql_query(conn, total);
			if (err!=0)
			{
				printf ("\n Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
		}
	}
}

void RecuperarPartida(char entrada[200], listaJugadas *list, ListaConectados *conectados, char respuesta[200],int sock_conn)
{
	p = strtok(entrada,"/");
	int numpart = atoi(p);
	for(int e = 0; e<list->num;e++)
	{
		if(list->jugadas[e].numPartida == numpart)
			list->jugadas[e].numPartida = 0;
	}
	char lista[200];
	sprintf(lista,"12/%d",numpart);
	char jugadores[200];
	strcpy(jugadores,"");
	int numPlayers = 0;
	int falta = 0;
	char consulta[200];
	sprintf(consulta,"SELECT * FROM JUGADAS where IDGame = %d;",numpart);
	int numero;
	char *p;
	err=mysql_query (conn,consulta);
	if (err!=0)
	{
		printf ("Error al consultar datos de la base %u %s\n",mysql_errno(conn),mysql_error(conn));
	}
	resultado = mysql_store_result (conn);
	if (resultado == NULL)
		printf ("No se han obtenido datos en la consulta\n");
	else
	{
		row = mysql_fetch_row (resultado);
		while ((row !=NULL) && (falta == 0))
		{
			numero = list->num;
			strcpy(list->jugadas[numero].jugador,row[1]);
			int s = BuscaSocket(conectados, row[1]);
			if (s != -1)
			{
				sprintf(lista,"%s/%s",lista,row[1]);
				sprintf(jugadores,"%s/%s",jugadores,row[1]);
				numPlayers++;
				list->jugadas[numero].numPartida = numpart;
				list->jugadas[numero].money = atoi(row[2]);
				list->jugadas[numero].lastPos = atoi(row[3]);
				if(strcmp(row[4],"NULL") != 0)
				{
					p = strtok(row[4],"/");
					while(p != NULL)
					{
						list->jugadas[numero].ListaPropiedades[atoi(p)] = 1;
						p = strtok(NULL,"/");
					}
				}
				list->num = list->num + 1;
				row = mysql_fetch_row (resultado);
			}
			else 
			{
				falta = 1;
			}
		}
	}
	if (falta == 1)
	{
		strcpy(respuesta,"25/");
		write(sock_conn,respuesta,strlen(respuesta));
	}
	else
	{
		p = strtok(jugadores,"/");
		for(int j=0;j<numPlayers;j++)
		{
			int s=BuscaSocket(conectados,p);
			write (s,lista, strlen(lista));
			p = strtok(NULL,"/");
		}
		PonTurnoPartida(&misTurnos,1,numpart,numPlayers);
	}
}

int PosicionCajaComunidad(int numero, char nombre[20], int numPart, listaJugadas *list, int posicioninicial,int sock_conn)
{ 
	char respuesta[200];
	int j = 0;
	int encontrado = 0;
	int encontrado2 = 0;
	int posicionfinal = -1;
	if (numero == 1)
	{
		while ((j < list->num) && (encontrado == 0))
		{
			if ((list->jugadas[j].numPartida == numPart) && (strcmp(nombre, list->jugadas[j].jugador) == 0))
			{
				encontrado = 1;
				list->jugadas[j].money = list->jugadas[j].money - 100;
			}
			else
			{
				j++;
			}
		}
		if (encontrado == 1)
			sprintf(respuesta, "19/%d/Paga las facturas del hospital, -100 euros",numPart);
	}
	else if (numero == 2)
	{
		while ((j < list->num) && (encontrado == 0))
		{
			if ((list->jugadas[j].numPartida == numPart) && (strcmp(nombre, list->jugadas[j].jugador) == 0))
			{
				encontrado = 1;
				list->jugadas[j].money = list->jugadas[j].money + 100;
			}
			else
			{
				j++;
			}
		}
		if (encontrado == 1)
			sprintf(respuesta, "19/%d/Heredas 100 euros",numPart);
	}
	else if (numero == 3)
	{
		posicionfinal = 0;
		while ((j < list->num) && (encontrado == 0))
		{
			if ((list->jugadas[j].numPartida == numPart) && (strcmp(nombre, list->jugadas[j].jugador) == 0))
			{
				encontrado = 1;
				list->jugadas[j].lastPos = 0;
				list->jugadas[j].money = list->jugadas[j].money + 200;
			}
			else
			{
				j++;
			}
		}
		if (encontrado == 1)
			sprintf(respuesta, "19/%d/Colocate en la casilla de salida y cobra 200 euros",numPart);
	}
	else if (numero == 4)
	{
		while ((j < list->num) && (encontrado == 0))
		{
			if ((list->jugadas[j].numPartida == numPart) && (strcmp(nombre, list->jugadas[j].jugador) == 0))
			{
				encontrado = 1;
				list->jugadas[j].money = list->jugadas[j].money - 50;
			}
			else
			{
				j++;
			}
		}
		if (encontrado == 1)
			sprintf(respuesta, "19/%d/Paga gastos escolares, -50 euros",numPart);
	}
	else if (numero == 5)
	{
		while ((j < list->num) && (encontrado == 0))
		{
			if ((list->jugadas[j].numPartida == numPart) && (strcmp(nombre, list->jugadas[j].jugador) == 0))
			{
				encontrado = 1;
				list->jugadas[j].money = list->jugadas[j].money + 10;
			}
			else
			{
				j++;
			}
		}
		if (encontrado == 1)
			sprintf(respuesta, "19/%d/Has ganado el segundo premio de un concurso de belleza, cobra 10 euros",numPart);
	}
	else if (numero == 6)
	{
		int contador = 0;
		while (j < list->num)
		{
			if ((list->jugadas[j].numPartida == numPart) && (strcmp(nombre, list->jugadas[j].jugador) != 0))
			{
				list->jugadas[j].money = list->jugadas[j].money - 10;
				contador ++;
				j++;
			}
			else
			{
				j++;
			}
		}
		if (contador != 0)
		{
			j = 0;
			while ((j < list->num) && (encontrado2 == 0))
			{
				if ((list->jugadas[j].numPartida == numPart) && (strcmp(nombre, list->jugadas[j].jugador) == 0))
				{
					encontrado2 = 1;
					list->jugadas[j].money = list->jugadas[j].money + contador*10;
				}
				else
				{
					j++;
				}
			}
		}
		if (contador != 0 && encontrado2 ==1)
			sprintf(respuesta, "19/%d/Es tu cumpleanos, recibe 10 euros de cada jugador",numPart);
	}
	printf("\n %s \n", respuesta);
	write (sock_conn,respuesta, strlen(respuesta));
	return posicionfinal;
}

int ComprobarPerdedor(listaJugadas *list,int numpart){
	int j=0;
	int found=0;
	char perdedor[20];
	char respuesta[512];
	while((j<list->num)&&(found==0)){
		if((list->jugadas[j].numPartida==numpart)&&(list->jugadas[j].money<0))
			found=1;
		else
			j++;					
	}
	if(found==1){
		list->jugadas[j].money=NULL;
		list->jugadas[j].lastPos = NULL;
		int e;
		for(e = 0; e < 40 ; e++)
		{
			list->jugadas[j].ListaPropiedades[e] = NULL;
		}
		strcpy(perdedor,list->jugadas[j].jugador);
		sprintf(respuesta,"24/%d/%s",numpart,perdedor);
		for(int j=0;j<list->num;j++)
		{
			if((list->jugadas[j].numPartida==numpart))
			{
				int s=BuscaSocket(&miLista,list->jugadas[j].jugador);
				write (s,respuesta, strlen(respuesta));
			}
		}
	}
}

int Juego(char entrada[200], listaJugadas *list,listaTurnos *turnos, int sock_conn, char respuesta[200])
{
	strcpy(respuesta,"");
	p = strtok(entrada, "/");
	int numpart = atoi(p);
	SiguienteTurno(turnos,list,numpart);
	p = strtok(NULL, "/");
	char jugador[20];
	strcpy(jugador,p);
	p = strtok(NULL, "/");
	int tirada = atoi(p);
	char dinero[20];
	int e = PonJugada(list, numpart,jugador,tirada,dinero); 
	//Cuando hay una nueva jugada, esta funcion la introduce, actualizando la posicion del jugador, que finalmente retorna
	int money = atoi(dinero); //Para mirar si el jugador puede comprar propiedades o pagar alquileres
	if (e != -1)
	{
		if ((e > 39) || (e == 0)) //el jugador ha pasado por la salida, o ha caido en ella
		{
			int  k = Cobrar(jugador,numpart,list,200, sock_conn);
		}//responde con un 19/ has cobrado...
		else if(e == 7 || e == 22 || e == 36)// el jugador ha caido en SUERTE
		{
			int numero = rand() % 7 + 1; 
			int posicionfinal = PosicionSuerte(numero, jugador, numpart, list, e, sock_conn);
			//PosicionSuerte retorna un 19/ seguido del mensaje correspondiente a la carta de suerte, solo al ususario (es privado)
			if (posicionfinal != -1)
			{//en algunas cartas de suerte se modifica la posicion del jugador, si es el caso hay que mirar si puede
				// o no comprar alguna propiedad o pagar un alquiler
				PuedoComprar(list, numpart,jugador, posicionfinal,respuesta,Propiedades1, money);
			}
		}
		else if (e == 2 || e == 17 || e == 33) // el jugador ha caido en CAJA DE COMUNIDAD
		{
			int numero = rand() % 7 + 1; 
			int posicionfinal = PosicionCajaComunidad(numero, jugador, numpart,list, e, sock_conn);
			//La funcion PosicionCajaComunidad hace lo mismo que la funcion de suerte, pero con diferentes "cartas"
			//Tambien responde con un 19/...
			if (posicionfinal != -1)
			{
				PuedoComprar(list, numpart,jugador, posicionfinal,respuesta,Propiedades1, money);
			}
		}
		else if (e == 4)
		{//el jugador tiene que pagar una multa
			CajaDeComunidad = PagarMulta(jugador,numpart,list, 200 ,CajaDeComunidad, sock_conn);
		}// lo notifica con un 19/ has pagado una multa de...
		else if (e == 38)
		{//el jugador tiene que pagar una multa
			CajaDeComunidad = PagarMulta(jugador,numpart, list, 100 ,CajaDeComunidad, sock_conn);
		}// lo notifica con un 19/ has pagado una multa de...
		else if (e == 20)
		{//el jugador cobra lo que hay en parking gratuito
			int  k = Cobrar(jugador,numpart,list, CajaDeComunidad, sock_conn);
			CajaDeComunidad = 0;
		}//responde con un 19/ has cobrado...
		else if (e==30)
		{//e jugador esta en la carcel
			PosicionCarcel(jugador,numpart,list,sock_conn);
		}//responde al usuario con un 20/ numero de partida
		else if (e == 28)
		{//el cliente tiene que pagar su tirada * 4, solo si alguien ha comprado antes la Propiedad
			//si no tiene dueño, la puede comprar
			int k = CobrarAguas(jugador, numpart, list,sock_conn,tirada);
			if (k == 0)
			{
				sleep(1);
				PuedoComprar(list, numpart,jugador, e, respuesta,Propiedades1, money);
			}
		}
		else if (e == 12)
		{//el cliente tiene que pagar su tirada * 4, solo si alguien ha comprado antes la Propiedad
			//si no tiene dueño, la puede comprar
			int k = CobrarElectricidad(jugador, numpart, list,sock_conn,tirada);
			if (k == 0)
			{
				sleep(1);
				PuedoComprar(list, numpart,jugador, e, respuesta,Propiedades1, money);
			}
		}
		else
		{//Mira si en la posició en la que esta el jugador hi ha alguna propietat en venda o algun lloguer.
			sleep(1);
			PuedoComprar(list, numpart,jugador, e, respuesta,Propiedades1, money);
		}
		sleep(1);
		DameEstatusPartida(numpart, list);//Retorna totes les dades de cada jugador, i ho notifica a tots
	}
	else
	{//ha ocurrido una tirada erronea
		sprintf(respuesta,"15/-1");
	}
	ComprobarPerdedor(list,numpart); //mira si algun cliente tiene dinero negativo (ha perdido)
	//si alguien ha perdido lo notifica con 24/num part/ nombre perdedor
	SiguienteTurno(list,turnos,numpart); //asigna el siguiente turno
}

void DameGanador(char entrada[200], listaJugadas *list)
{//retorna el dinero del jugador que ha ganado, y el nombre en ganador
	int numpart;
	char respuesta[200];
	p = strtok(entrada, "/");
	numpart = atoi(p);
	int j;
	int maxdinero = 0;
	char ganador1[20];
	strcpy(ganador1, "");
	int dinero = 0;
	int e;
	for (j = 0; j < list->num; j++)
	{
		if(list->jugadas[j].numPartida == numpart)
		{
			dinero = list->jugadas[j].money;
			for (e = 0; e < 39; e++)
			{
				if (list->jugadas[j].ListaPropiedades[e] == 1)
				{
					dinero = dinero + Propiedades1[e].precio;
				}
			}
		}
		if (dinero > maxdinero)
		{
			maxdinero = dinero;
			strcpy(ganador1,list->jugadas[j].jugador);
		}
		else if (dinero = maxdinero)
		{
			maxdinero = dinero;
			strcpy(ganador1, "empate");
		}
	}
	if (maxdinero > 0)
	{
		int j;
		sprintf(respuesta, "21/%d/%s/%d", numpart, ganador1, maxdinero);
		for (j = 0; j < 100; j++)
		{
			if(sockets[j]!=0)
				write (sockets[j], respuesta, strlen(respuesta));
		}
	}
	sprintf(consulta,"INSERT INTO Ganadores(IDGame,Ganador) VALUES(%d,'%s');",numpart,ganador1);
	err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("\n Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
		exit (1);
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
			//Hace el log in en el juego
			pthread_mutex_lock( &mutex);
			conectar(NULL,respuesta,sock_conn);
			pthread_mutex_unlock( &mutex);
			Notificar();
			//retorna 1/3, si el jugador ya esta en la BD, 1/1 si todo va bien, y 1/0 si hay algun error
		}
		else if (codigo ==2)
		{
			//el cliente quiere recuperar la contraseña
			recuperaPSW(NULL,respuesta);
			//responde con 2/ la contraseña
		}
		else if (codigo ==3)
		{
			//el cliente pide los jugadores con los que ha jugado alguna partida
			jugadoresJugados(NULL,respuesta);
			//responde con 3/el nombre de todos los jugadores
		}
		else if (codigo ==4)
		{
			//el cliente pide resultado partidas jugadas con X jugador
			ganadorJugadas(NULL,respuesta);
		}
		else if (codigo ==5)
		{
			//el cliente pide partidas jugadas en X margen de tiempo
			partidasTiempo(NULL,respuesta);
		}
		else if (codigo == 6) //register
		{
			// El cliente se registra 
			pthread_mutex_lock( &mutex);
			registro(NULL,respuesta);
			pthread_mutex_unlock( &mutex);
			//responde con 6/1 si el usuario ya esta en la BD, y 6/0 si lo ha introducido bien
		}
		else if (codigo == 7)
		{
			//El cliente pide la lista de conectados. Esta funcion se hace al conectar el cliente al servidor.
			//A partir de ese momento el servidor la envia de manera automatica
			Conectados(respuesta);
			//responde con 7/ la lista de conectados
		}
		else if (codigo == 8)
		{
			//El cliente envia una lista de jugadores a los que quiere invitar, y el numero de la partida 
			//a la que estan invitados
			pthread_mutex_lock( &mutex);
			DameInvitados(NULL, &misNotificaciones);
			pthread_mutex_unlock( &mutex);
			//responde con 8/nombre de la persona que ha invitado/ numero de la partida
		}
		else if (codigo == 9)
		{
			//El cliente hace un log out, sale de la partida pero sigue conectado al servidor
			//Como que un cliente se ha desconectado, hay que notificar a todos los demas
			pthread_mutex_lock( &mutex);
			SacaConectado(NULL,&miLista);			
			pthread_mutex_unlock( &mutex);
			
		}
		else if (codigo ==10)
		{
			//El cliente pide las invitaciones que tiene pendientes
			misInvitaciones(&misNotificaciones,NULL ,respuesta);
			//Responde con 10/nombre de la persona que ha invitado al cliente/numero de la partida
		}
	
		else if (codigo == 11)
		{
			//El cliente ha creado una nueva partida
			crearPartida(respuesta);
			//El servidor responde con 11/ seguido del numero de partida que toca, el siguiente que esta libre.
		}
		
		else if (codigo == 12)
		{
			//El cliente envia un 12/1 o 12/-1 seguido de los numeros de partida. 1 quiere decir que el cliente ha aceptado la partida, -1 no la ha aceptado
			//El codi per part del servidor ve donat com 12/1/Jugador/numPartida1/numPartida2/...   El segon paràmetre del codi és 1 només si s'ha acceptat la invitació*/
			pthread_mutex_lock( &mutex);
			RespuestaInvitacion(NULL,&misNotificaciones);
			pthread_mutex_unlock( &mutex);
			//Dependiendo de si todos los ususarios han dado una respuesta a la invitacion o no, y el tiempo,
			//la partida empieza
		}
		else if (codigo==13)
		{
			//El cliente quiere enviar por el chat
			enviaChat(NULL,&misJugadas);
			//La respuesta es 13/numero de partida/persona que envia/mensaje
		}
		else if (codigo==14)
		{
			//el cliente pide el estatus de la partida, solo se hace al iniciar la partida. A partir de ese moment es automatico
			p = strtok(NULL,"/");
			int num = atoi(p);
			DameEstatusPartida(num, &misJugadas);
			//la respuesta es: 15/numero de partida/ jugador1 /dinero del jugador1/ posicion del jugador1 /jugador2/...
		}
		else if (codigo == 15)
		{ //en este codigo se hace el programa principal del juego, dependiendo de en que estado esta el jugador, la 
			//respuesta es 19/, 20/...
			pthread_mutex_lock(&mutex);
			Juego(NULL,&misJugadas,&misTurnos,sock_conn, respuesta);
			pthread_mutex_unlock(&mutex);
			//Mas info en la funcion
		}
		else if (codigo == 16)
		{//el cliente quiere comprar una propiedad
			pthread_mutex_lock(&mutex);
			ComprarPropiedad(&misJugadas, NULL);
			pthread_mutex_unlock(&mutex);
		}//el servidor notifica a todos los clientes con 17/numpart/comprador/propiedad/posicion
		else if (codigo==17)
		{//se cobra un alquiler
			pthread_mutex_lock(&mutex);
			CobrarAlquiler(&misJugadas, NULL);
			pthread_mutex_unlock(&mutex);
		}//el servidor no responde, pero notifica el nuevo estatus de la partida
		else if (codigo ==  18)
		{//permite guardar la partida en la base de datos
			GuardaPartida(NULL, &misJugadas);
		}
		else if (codigo == 19)// PER TORNAR A LA PARTIDA ANTIGA, EL CLIENT HA DE TENIR UN BUTTON PER FER-HO
		{//permite reempezar la partida, solo lo hace si todos los jugadores estan conectados
			pthread_mutex_lock(&mutex);
			RecuperarPartida(NULL, &misJugadas, &miLista, respuesta,sock_conn);
			pthread_mutex_unlock(&mutex);
		}// si algun jugador no esta conectado, responde con un 25/
		else if (codigo == 20) // qui ha guanyay?
		{//el cliente pide quien es el gandor o ganadora
			DameGanador(NULL,&misJugadas);			
		}//responde con 21/numpart/ganador/dinero del ganador
		else if (codigo == 21) //cliente perdedor envia su numturno para actualizar turno
		{//el servidor saca a este usuario de los turnos
			pthread_mutex_lock(&mutex);
			PonPerdido(&misTurnos,NULL);
			pthread_mutex_unlock(&mutex);
		}
		else if (codigo == 23)
		{//el usuario quiere eliminar sus datos de la base de datos
			EliminaBBDD(NULL, respuesta);
		}//23/1 si va mal, 23/0 si va bien
		else if (codigo==24) //asigna jugador carcel
		{//el cliente esta en la carcel
			pthread_mutex_lock(&mutex);
			PonCarcel(&misTurnos,NULL);
			pthread_mutex_unlock(&mutex);
		}
		if (codigo !=0 && codigo!=8 && codigo!=9 && codigo!=12 && codigo!=13 && codigo!=16 && codigo!=14 && codigo!=18 && codigo!=17 && codigo != 19 && codigo != 21 && codigo != 20 &&codigo!=24)
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
	conn = mysql_real_connect (conn, "shiva2.upc.es","root", "mysql", "Jellyfish",0, NULL, 0);
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
	serv_adr.sin_port = htons(50053);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	pthread_t thread;
	
	ComienzaTablero(Propiedades1);
	
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

