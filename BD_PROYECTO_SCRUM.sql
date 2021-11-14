CREATE DATABASE proyecto_scrum;
USE proyecto_scrum;

DROP TABLE IF EXISTS sc_spring_meeting;
DROP TABLE IF EXISTS sc_spring_meeting_type;
DROP TABLE IF EXISTS sc_spring_backlog;
DROP TABLE IF EXISTS sc_spring;
DROP TABLE IF EXISTS sc_historia_usuario;
DROP TABLE IF EXISTS sc_epica;
DROP TABLE IF EXISTS sc_tipo;
DROP TABLE IF EXISTS sc_equipo;
DROP TABLE IF EXISTS sc_proyecto;
DROP TABLE IF EXISTS sc_usuario;
DROP TABLE IF EXISTS sc_rol;
DROP TABLE IF EXISTS sc_estado_proyecto;
DROP TABLE IF EXISTS sc_estado_historia_usuario;
DROP TABLE IF EXISTS sc_estado_spring;

CREATE TABLE sc_rol
(
	id_rol INT PRIMARY KEY NOT NULL,
	rol VARCHAR(50) NOT NULL,
	fecha_creacion DATETIME DEFAULT GETDATE(),
);

INSERT INTO sc_rol (id_rol,rol) VALUES (1,'PRODUCT OWNER'), (2,'SCRUM MASTER'),(3,'DEVELOPTMENT TEAM');

CREATE TABLE sc_usuario
(
	id_usuario INT NOT NULL IDENTITY PRIMARY KEY,
	nombre VARCHAR(200) NOT NULL,
	email VARCHAR(150) UNIQUE NOT NULL,
	contrasena VARCHAR(300) NOT NULL,
	fk_rol INT NOT NULL,
	fecha_creacion DATETIME DEFAULT GETDATE(),
	FOREIGN KEY (fk_rol) REFERENCES sc_rol(id_rol)
);

INSERT INTO sc_usuario (nombre,email,contrasena,fk_rol) VALUES ('Admin','jorgepq75@gmail.com','1234',1);

CREATE TABLE sc_estado_proyecto
(
	id_estado INT PRIMARY KEY NOT NULL,
	estado VARCHAR(50)
);

INSERT INTO sc_estado_proyecto (id_estado,estado) VALUES (1,'INICIADO'), (2,'CANCELADO'), (3,'FINALIZADO');

CREATE TABLE sc_proyecto
(
	id_proyecto BIGINT PRIMARY KEY IDENTITY NOT NULL,
	nombre VARCHAR (200) NOT NULL,
	descripcion VARCHAR(600) NOT NULL,
	fk_creado_por INT NOT NULL,
	fk_estado_proyecto INT NOT NULL DEFAULT 1,
	fecha_creacion DATETIME DEFAULT GETDATE(),
	fecha_cierre DATE,
	FOREIGN KEY (fk_creado_por) REFERENCES sc_usuario(id_usuario),
	FOREIGN KEY (fk_estado_proyecto) REFERENCES sc_estado_proyecto(id_estado)
);

CREATE TABLE sc_equipo
(
	id_equipo INT PRIMARY KEY IDENTITY,
	fk_proyecto BIGINT NOT NULL,
	fk_usuario INT NOT NULL,
	FOREIGN KEY (fk_usuario) REFERENCES sc_usuario(id_usuario),
	FOREIGN KEY (fk_proyecto) REFERENCES sc_proyecto(id_proyecto),
	fecha_creacion DATETIME DEFAULT GETDATE(),
);



CREATE TABLE sc_epica
(
	id_epica BIGINT IDENTITY PRIMARY KEY NOT NULL,
	titulo VARCHAR(200) NOT NULL,
	fk_proyecto BIGINT NOT NULL,
	descricion VARCHAR(600) NOT NULL,
	fk_creado_por INT NOT NULL,
	fecha_creacion DATETIME DEFAULT GETDATE(),
	FOREIGN KEY (fk_creado_por) REFERENCES sc_usuario(id_usuario),
	FOREIGN KEY (fk_proyecto) REFERENCES sc_proyecto(id_proyecto)
);

CREATE TABLE sc_estado_historia_usuario
(
	id_estado INT PRIMARY KEY NOT NULL,
	estado VARCHAR(50)
);
INSERT INTO sc_estado_historia_usuario (id_estado,estado) VALUES (1,'PENDIENTE'), (2,'EN PROGRESO'), (3,'FINALIZADO');

CREATE TABLE sc_historia_usuario
(
	id_historia_usuario BIGINT IDENTITY PRIMARY KEY NOT NULL,
	titulo VARCHAR(200) NOT NULL,
	descricion VARCHAR(600) NOT NULL,
	fk_epica BIGINT,
	fk_asignado_a INT,
	fk_creado_por INT NOT NULL,
	fk_estado_historia_usuario INT NOT NULL DEFAULT 1,
	fecha_creacion DATETIME DEFAULT GETDATE(),
	FOREIGN KEY (fk_asignado_a) REFERENCES sc_usuario(id_usuario),
	FOREIGN KEY (fk_creado_por) REFERENCES sc_usuario(id_usuario),
	FOREIGN KEY (fk_epica) REFERENCES sc_epica(id_epica),
	FOREIGN KEY (fk_estado_historia_usuario) REFERENCES sc_estado_historia_usuario(id_estado)
);


CREATE TABLE sc_estado_spring
(
	id_estado INT PRIMARY KEY NOT NULL,
	estado VARCHAR(50)
);
INSERT INTO sc_estado_spring (id_estado,estado) VALUES (1,'PENDIENTE'), (2,'EN PROGRESO'), (3,'FINALIZADO');

CREATE TABLE sc_spring
(
	id_spring BIGINT NOT NULL PRIMARY KEY IDENTITY,
	nombre VARCHAR(200) NOT NULL,
	fk_creado_por INT NOT NULL,
	fecha_creacion DATETIME DEFAULT GETDATE(),
	fk_proyecto BIGINT NOT NULL,
	fk_estado_spring INT NOT NULL DEFAULT 1,
	FOREIGN KEY (fk_creado_por) REFERENCES sc_usuario(id_usuario),
	FOREIGN KEY (fk_proyecto) REFERENCES sc_proyecto(id_proyecto),
	FOREIGN KEY (fk_estado_spring) REFERENCES sc_estado_spring(id_estado)
);


CREATE TABLE sc_spring_meeting_type
(
	id_meeting_type INT PRIMARY KEY NOT NULL,
	nombre VARCHAR(80) NOT NULL,
	fecha_creacion DATETIME DEFAULT GETDATE(),
);

INSERT INTO sc_spring_meeting_type (id_meeting_type,nombre) VALUES (1,'SPRING DAILY'), (2,'SPRING REVIEW'), (3,'SPRING RETROSPECTIVE');


CREATE TABLE sc_spring_meeting
(
	id_spring_meeting INT PRIMARY KEY IDENTITY NOT NULL,
	fk_spring BIGINT NOT NULL,
	fk_spring_meeting_type INT NOT NULL,
	fecha_creacion DATETIME DEFAULT GETDATE(),
	fk_creado_por INT NOT NULL,
	comment VARCHAR(600) NOT NULL,
	FOREIGN KEY (fk_creado_por) REFERENCES sc_usuario(id_usuario),
	FOREIGN KEY (fk_spring_meeting_type) REFERENCES sc_spring_meeting_type(id_meeting_type),
	FOREIGN KEY (fk_spring) REFERENCES sc_spring(id_spring)
)

CREATE TABLE sc_spring_backlog
(
	id_spring_backlog INT PRIMARY KEY IDENTITY,
	fk_spring BIGINT NOT NULL,
	fk_historia_usuario BIGINT NOT NULL,
	fecha_creacion DATETIME DEFAULT GETDATE(),
	FOREIGN KEY (fk_spring) REFERENCES sc_spring(id_spring),
	FOREIGN KEY (fk_historia_usuario) REFERENCES sc_historia_usuario(id_historia_usuario)
);