#include <stdio.h>
#include <string.h>

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
int main(int argc, char *argv[]) {
	ListaConectados miLista;
	miLista.num = 0;
	int e;
	char nombre[20] = "Marcel";
	PonConectado(&miLista,"Manolo",4);
	PonConectado(&miLista,"Marco",6);
	PonConectado(&miLista,"Carla",7);
	e = PonConectado(&miLista,nombre,5);
	if (e == 1)
		printf("error al introducir usuario \n");
	else
		printf("usuario: %s ha sido añadido correctamente \n",nombre);
	
	int socket;
	socket = BuscaSocket(&miLista,nombre);
	int pos_1;
	printf("%s\n",miLista.conectados[0].nombre);
	pos_1 = BuscaPosicion(&miLista,"Manolo");
	printf("%d \n",pos_1);
	printf("%d \n",miLista.conectados[pos_1].socket);
	printf("%d \n",miLista.num);
	int res = EliminaConectado(&miLista,nombre);
	if(res == 0)
		printf("%s ha sido eliminado de la lista\n",nombre);
	else
		printf("no se ha podido elminar al usuario\n");
	char conectados[100];
	DameConectados(&miLista,conectados);
	printf("%s \n",conectados);
	
	char *p = strtok(conectados,"/");
	int num = atoi(p);
	printf("en total hay %d usuarios\n",num);
	while(p != NULL)
	{
		p = strtok(NULL,"/");
		strcpy(nombre,p);
		printf("el nombre del usuario es %s\n",nombre);
	}
	return 0;
}

