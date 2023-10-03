CREATE DATABASE DBPRUEBA

USE DBPRUEBA

CREATE TABLE USUARIO(
	idUsuario int primary key identity, 
	nombreUsuario varchar(50),
	correo varchar(50),
	clave varchar(100),
)

Select * from usuario