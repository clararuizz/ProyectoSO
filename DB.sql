DROP DATABASE IF EXISTS Jellyfish;
CREATE DATABASE Jellyfish;

USE Jellyfish;


CREATE TABLE Players (
	ID INTEGER NOT NULL AUTO_INCREMENT,
	LastScore FLOAT,
	BestScore FLOAT,
	PlayedGames INTEGER,
	PRIMARY KEY (ID)
);

CREATE TABLE PersonalData (
	ID INTEGER NOT NULL AUTO_INCREMENT,
	Email TEXT,
	Name TEXT,
	PSW TEXT,
	PRIMARY KEY (ID),
	FOREIGN KEY (ID) REFERENCES Players(ID)
);

CREATE TABLE Games (
	ID INTEGER PRIMARY KEY NOT NULL,
	CreationDate DATE
);

CREATE TABLE PropertiesList (
	ID INTEGER PRIMARY KEY NOT NULL,
	Name TEXT NOT NULL,
	Price INTEGER NOT NULL
);

CREATE TABLE PropertiesRelations (
	IDGame INTEGER NOT NULL,
	IDPlayer INTEGER NOT NULL,
	IDProperty INTEGER NOT NULL,
	FOREIGN KEY (IDGame) REFERENCES Games(ID),
	FOREIGN KEY (IDPlayer) REFERENCES Players(ID),
	FOREIGN KEY (IDProperty) REFERENCES PropertiesList(ID)
);

CREATE TABLE GameData (
	IDPlayer INTEGER NOT NULL,
	IDGame INTEGER NOT NULL,
	LastPos INTEGER,
	Money INTEGER,
	FOREIGN KEY (IDGame) REFERENCES Games(ID),
	FOREIGN KEY (IDPlayer) REFERENCES Players(ID)
);

INSERT INTO Players(LastScore,BestScore,PlayedGames) VALUES(23,40,2);
INSERT INTO Players(LastScore,BestScore,PlayedGames) VALUES(13,13,3);
INSERT INTO Players(LastScore,BestScore,PlayedGames) VALUES(16,33,2);
INSERT INTO Players(LastScore,BestScore,PlayedGames) VALUES(12,50,3);

INSERT INTO PersonalData(Email,Name,PSW) VALUES('email_1','Lluc','123');
INSERT INTO PersonalData(Email,Name,PSW) VALUES('email_2','Marta','1234');
INSERT INTO PersonalData(Email,Name,PSW) VALUES('email_3','Clara','12345');
INSERT INTO PersonalData(Email,Name,PSW) VALUES('email_4','Marcel','123456');

INSERT INTO Games VALUES (1,'2012-01-01');
INSERT INTO PropertiesList VALUES (1,'LaCaixa',5000);
INSERT INTO GameData VALUES (1,1,9,6000);

--SELECT
SELECT (GameData.Money) FROM GameData, Players, PersonalData WHERE 
PersonalData.Name='Lluc'AND 
PersonalData.ID=Players.ID AND 
GameData.IDPlayer=Players.ID;

--SELECT
SELECT (Players.ID) FROM Players, PersonalData WHERE 
PersonalData.ID=Players.ID AND
PersonalData.Name ='Lluc' AND
PersonalData.PSW='123';

--SELECT
SELECT COUNT(ID) from Players;