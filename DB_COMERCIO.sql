USE master
GO

IF EXISTS(SELECT * FROM sys.databases WHERE name = 'COMERCIO_DB')
    DROP DATABASE COMERCIO_DB
GO

CREATE DATABASE COMERCIO_DB
GO

USE COMERCIO_DB
GO

CREATE TABLE Marcas (
    Id INT IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1
)
GO

CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1),
    NombreUsuario VARCHAR(50) NOT NULL,
    Contrasena VARCHAR(100) NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    TipoUsuario INT NOT NULL,
    Activo BIT NOT NULL DEFAULT 1
)
GO

-- Datos prueba
INSERT INTO Marcas (Nombre, Activo) VALUES 
('Samsung', 1),
('LG', 1),
('Sony', 1),
('Philips', 1)
GO

INSERT INTO Usuarios (NombreUsuario, Contrasena, Nombre, Apellido, TipoUsuario, Activo) VALUES 
('admin', 'admin123', 'Orlando', 'Administrador', 2, 1),
('vendedor', 'vendedor123', 'Albano', 'Vendedor', 1, 1)
GO
