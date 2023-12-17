DROP DATABASE IF EXISTS Jellyfish;
CREATE DATABASE Jellyfish;

USE Jellyfish;

CREATE TABLE PersonalData (
	ID INTEGER NOT NULL AUTO_INCREMENT,
	Email TEXT,
	Name TEXT,
	PSW TEXT,
	PRIMARY KEY (ID)
);

CREATE TABLE Games (
	ID INTEGER NOT NULL AUTO_INCREMENT,
	CreationDate DATE,
	PRIMARY KEY (ID)
);

CREATE TABLE JUGADAS(
	IDGame INTEGER,
	Name TEXT,
	Dinero INTEGER,
	Posicion INTEGER,
	Propiedades TEXT,
	FOREIGN KEY (IDGame) REFERENCES Games(ID)
);

CREATE TABLE Ganadores(
	IDGame INTEGER,
	Ganador TEXT,
	FOREIGN KEY (IDGame) REFERENCES Games(ID)
);

INSERT INTO PersonalData(Email,Name,PSW) VALUES('email_1','Lluc','123');
INSERT INTO PersonalData(Email,Name,PSW) VALUES('email_2','Marta','1234');
INSERT INTO PersonalData(Email,Name,PSW) VALUES('email_3','Clara','12345');
INSERT INTO PersonalData(Email,Name,PSW) VALUES('email_4','Marcel','123456');
