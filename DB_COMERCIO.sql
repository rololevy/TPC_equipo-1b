USE master;
GO
IF EXISTS(SELECT * FROM sys.databases WHERE name = 'COMERCIO_DB')
    DROP DATABASE COMERCIO_DB;
GO
CREATE DATABASE COMERCIO_DB;
GO
USE COMERCIO_DB;
GO
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario VARCHAR(50) NOT NULL UNIQUE,
    Contrasena VARCHAR(100) NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    TipoUsuario INT NOT NULL,  --1 vendedor, 2 admin
    Activo BIT NOT NULL DEFAULT(1)
);
GO
CREATE TABLE Clientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RazonSocial varchar(100) NOT NULL,
    Cuit VARCHAR(11) NOT NULL,
    Telefono VARCHAR(20) NULL,
    Direccion VARCHAR(100) NULL,
    Email VARCHAR(100) NULL,
    Activo BIT NOT NULL DEFAULT(1)
);
GO
CREATE TABLE Proveedores (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RazonSocial VARCHAR(100) NOT NULL,
    CUIT VARCHAR(11) NOT NULL, 
    Email VARCHAR(100) NULL,
    Telefono VARCHAR(20) NULL,
    Direccion VARCHAR(100) NULL,
    Activo BIT NOT NULL DEFAULT(1)
);
GO
CREATE TABLE Categorias (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(150) NOT NULL,
    Descripcion VARCHAR(500) NULL,
    Activo BIT NOT NULL DEFAULT(1)
);
GO

CREATE TABLE Marcas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Activo BIT NOT NULL DEFAULT(1)
);
GO

CREATE TABLE Productos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(200) NOT NULL,
    Descripcion VARCHAR(1000) NULL,
    MarcaId INT NULL,
    CategoriaId INT NULL,
    ProveedorId INT NULL,
    PrecioCompra DECIMAL(18,2) NOT NULL DEFAULT(0.00),
    PorcentajeGanancia INT NOT NULL DEFAULT(0),
    StockActual INT NOT NULL DEFAULT(0),
    StockMinimo INT NOT NULL DEFAULT(0),
    Activo BIT NOT NULL DEFAULT(1),
    PrecioVenta AS ROUND(PrecioCompra * (1.0 + PorcentajeGanancia / 100.0), 2) PERSISTED,
    FOREIGN KEY (MarcaId) REFERENCES Marcas(Id),
    FOREIGN KEY (CategoriaId) REFERENCES Categorias(Id),
    FOREIGN KEY (ProveedorId) REFERENCES Proveedores(Id)
);
GO

CREATE TABLE Compras (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProveedorId INT NOT NULL,
    FechaCompra DATETIME NOT NULL DEFAULT(GETDATE()),
    Recibida BIT NOT NULL DEFAULT(0),
    Total DECIMAL(18,2) NULL,
    FOREIGN KEY (ProveedorId) REFERENCES Proveedores(Id)
);
GO

CREATE TABLE DetalleCompras (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CompraId INT NOT NULL,
    ProductoId INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    SubTotal AS ROUND(Cantidad * PrecioUnitario,2) PERSISTED,
    FOREIGN KEY (CompraId) REFERENCES Compras(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProductoId) REFERENCES Productos(Id)
);
GO

CREATE TABLE Ventas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NumeroFactura INT NULL,
    TipoFactura CHAR(1) NULL,
    Fecha DATETIME NOT NULL DEFAULT(GETDATE()),
    ClienteId INT NULL,
    UsuarioId INT NULL,
    Total DECIMAL(18,2) NULL,
    MedioPago CHAR(1) NULL,
    NroCierreCaja INT NULL,  
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id),
    FOREIGN KEY (NroCierreCaja) REFERENCES ResumenVenta(NroDeCierre)
);
GO

CREATE TABLE DetalleVentas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    VentaId INT NOT NULL,
    ProductoId INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    SubTotal AS ROUND(Cantidad * PrecioUnitario,2) PERSISTED,
    FOREIGN KEY (VentaId) REFERENCES Ventas(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProductoId) REFERENCES Productos(Id)
);
GO

CREATE TABLE ResumenVenta (
    NroDeCierre INT IDENTITY(1,1) PRIMARY KEY,
    TotalGeneral DECIMAL(18,2) NOT NULL DEFAULT(0),
    TotalEfectivo DECIMAL(18,2) NOT NULL DEFAULT(0),
    TotalTarjeta DECIMAL(18,2) NOT NULL DEFAULT(0),
    TotalQr DECIMAL(18,2) NOT NULL DEFAULT(0),
    TotalFA DECIMAL(18,2) NOT NULL DEFAULT(0),
    TotalFB DECIMAL(18,2) NOT NULL DEFAULT(0),
    TotalFC DECIMAL(18,2) NOT NULL DEFAULT(0),
    TotalOperaciones INT NOT NULL DEFAULT(0),
    FechaResumenVenta DATE NOT NULL,
    Cerrado BIT NOT NULL DEFAULT(0)
);
GO


INSERT INTO Usuarios (NombreUsuario, Contrasena, Nombre, Apellido, TipoUsuario, Activo) VALUES
('admin', 'admin123', 'Orlando', 'Administrador', 2, 1),
('vendedor', 'vendedor123', 'Albano', 'Suarez', 1, 1);
GO


INSERT INTO Clientes (RazonSocial, Cuit, Telefono, Direccion, Email, Activo) VALUES
('Juan Perez', '20345678901', '1145671234', 'Belgrano 1120', 'juanp@mail.com', 1),
('Laura Gomez', '27322789451', '1123478900', 'Av. Rivadavia 3320', 'lgomez@mail.com', 1),
('Carlos Medina', '23123876543', '1134567800', 'Humahuaca 450', 'cmedina@mail.com', 1),
('Sonia Duarte', '27456712389', '1124908871', 'San Juan 2301', 'sduarte@mail.com', 1);
GO


INSERT INTO Proveedores (RazonSocial, CUIT, Email, Telefono, Direccion, Activo) VALUES
('Distribuidora Andes', '30789456123', 'andes@prov.com', '1122334455', 'Av Siempre Viva 123', 1),
('Tech Import SA', '30987654321', 'tech@prov.com', '1133445566', 'Mitre 2200', 1),
('Global Parts', '30771234567', 'parts@prov.com', '1144556677', 'Sarmiento 845', 1),
('Mercurio Logística', '30888999123', 'mercurio@prov.com', '1155667788', 'Perón 3140', 1),
('Importadora Solaris', '30776554321', 'solaris@prov.com', '1166778899', 'Corrientes 1500', 1);
GO


INSERT INTO Marcas (Nombre, Activo) VALUES
('Samsung', 1),
('LG', 1),
('Sony', 1),
('Philips', 1);
GO


INSERT INTO Categorias (Nombre, Descripcion, Activo) VALUES
('Televisores','Televisores LED, LCD y OLED',1),
('Audio','Parlantes y equipos de sonido',1),
('Electrodomésticos','Pequeños electrodomésticos',1);
GO

INSERT INTO Productos (Nombre, Descripcion, MarcaId, CategoriaId, ProveedorId, PrecioCompra, PorcentajeGanancia, StockActual, StockMinimo, Activo) VALUES
('TV Samsung 55" 4K', 'Televisor Samsung 55 pulgadas Ultra HD 4K', 1, 1, 1, 450000, 35, 10, 5, 1),
('TV LG OLED 65"', 'Televisor LG OLED 65 pulgadas', 2, 1, 2, 850000, 30, 5, 3, 1),
('Soundbar Sony HT-S350', 'Barra de sonido Sony 2.1 canales', 3, 2, 3, 120000, 40, 15, 8, 1),
('Microondas Philips 20L', 'Microondas digital 20L', 4, 3, 4, 65000, 45, 20, 10, 1),
('Smart TV Samsung 43"', 'Smart TV Samsung Full HD 43"', 1, 1, 5, 280000, 35, 12, 6, 1);
GO