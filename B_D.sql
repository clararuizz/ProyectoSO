DROP DATABASE IF EXISTS ProjectDemo;
CREATE DATABASE ProjectDemo;

USE ProjectDemo;


CREATE TABLE Jugadores (
    ID INTEGER PRIMARY KEY NOT NULL,
    LastScore FLOAT,
    BestScore FLOAT,
    GamesPlayed INTEGER
);


CREATE TABLE PersonalData (
    ID INTEGER PRIMARY KEY NOT NULL,
    Email TEXT,
    Name TEXT,
    PSW TEXT,
    FOREIGN KEY (ID) REFERENCES Jugadores(ID)
);


CREATE TABLE Game (
    IDGame INTEGER PRIMARY KEY NOT NULL,
	FechaCreacion DATE
);

CREATE TABLE ListaPropiedades (
	IDPropiedad INTEGER PRIMARY KEY NOT NULL,
	Nombre TEXT NOT NULL,
	PrecioCompra INTEGER NOT NULL
);


CREATE TABLE PropiedadesRelacion (
	IDGame INTEGER NOT NULL,
	IDJugador INTEGER NOT NULL,
	IDPropiedad INTEGER NOT NULL,
	FOREIGN KEY (IDGame) REFERENCES Game(IDGame),
	FOREIGN KEY (IDJugador) REFERENCES Jugadores(ID),
	FOREIGN KEY (IDPropiedad) REFERENCES ListaPropiedades(IDPropiedad)
);


CREATE TABLE Valores (
    IDPlayer INTEGER NOT NULL,
    IDGame INTEGER NOT NULL,
    LastPos INTEGER,
    Money INTEGER,
    FOREIGN KEY (IDGame) REFERENCES Game(IDGame),
    FOREIGN KEY (IDPlayer) REFERENCES Jugadores(ID)
);

INSERT INTO Jugadores(ID,LastScore,BestSCore,GamesPlayed) VALUES(1,23,40,2);
INSERT INTO Jugadores(ID,LastScore,BestSCore,GamesPlayed) VALUES(2,13,13,3);
INSERT INTO Jugadores(ID,LastScore,BestSCore,GamesPlayed) VALUES(3,16,33,2);
INSERT INTO Jugadores(ID,LastScore,BestSCore,GamesPlayed) VALUES(4,12,50,3);

INSERT INTO PersonalData(ID,Email,Name,PSW) VALUES(1,'email_1','Lluc','123');
INSERT INTO PersonalData(ID,Email,Name,PSW) VALUES(2,'email_2','Marta','1234');
INSERT INTO PersonalData(ID,Email,Name,PSW) VALUES(3,'email_3','Clara','12345');
INSERT INTO PersonalData(ID,Email,Name,PSW) VALUES(4,'email_4','Marcel','123456');


