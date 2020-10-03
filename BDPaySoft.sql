CREATE DATABASE BDPaySoft
GO
USE BDPaySoft

CREATE TABLE SALON
(
ID_Salon  int IDENTITY(1,1) NOT NULL PRIMARY KEY ,
Nombre VARCHAR(30) NOT NULL,
Estado VARCHAR(30) NOT NULL
)

CREATE TABLE MESA
(
ID_Mesa int IDENTITY (1,1) NOT NULL PRIMARY KEY,
Nombre VARCHAR(30) NOT NULL,
Salon int NOT NULL REFERENCES SALON(ID_Salon) ON DELETE CASCADE,
Estado_Vida VARCHAR(30),
Estado_Disponibilidad VARCHAR(30) NULL

)

CREATE TABLE PROPIEDAD_MESA
(
id_propiedades int IDENTITY(1,1) primary key,
x int,
y int,
tamano_letra int
)

CREATE TABLE GRUPO_PRODUCTO
(
ID_Grupo int IDENTITY(1,1) PRIMARY KEY,
Nombre VARCHAR(30),
Por_Defecto VARCHAR(30),
Icono IMAGE,
Estado VARCHAR(30),
Estado_Icono VARCHAR(30)
)

CREATE TABLE PRODUCTO
(
ID_Producto int IDENTITY(1,1) PRIMARY KEY,
Descripcion VARCHAR(50),
Imagen IMAGE,
ID_Grupo int REFERENCES GRUPO_PRODUCTO (ID_Grupo) ON DELETE CASCADE,
Usa_Inventario VARCHAR(50),
Stock VARCHAR(50),
Precio_Venta numeric(18,2),
Stock_Minimo numeric(18,2),
Precio_Compra numeric(18,2),
Estado_Imagen VARCHAR(50)
)



