IF EXISTS(SELECT * FROM sys.databases WHERE name = 'COMERCIO_DB')
    DROP DATABASE COMERCIO_DB;
GO

CREATE DATABASE COMERCIO_DB;
GO

USE COMERCIO_DB;
GO

-- Usuarios
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario VARCHAR(50) NOT NULL UNIQUE,
    Contrasena VARCHAR(100) NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    TipoUsuario TINYINT NOT NULL, -- Tipo usuario
    Activo BIT NOT NULL DEFAULT(1)
);
GO

-- Clientes
CREATE TABLE Clientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    DNI VARCHAR(50) NULL,
    Email VARCHAR(200) NULL,
    Telefono VARCHAR(50) NULL,
    Direccion VARCHAR(250) NULL,
    Activo BIT NOT NULL DEFAULT(1)
);
GO

-- Proveedores
CREATE TABLE Proveedores (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RazonSocial VARCHAR(200) NOT NULL,
    CUIL VARCHAR(50) NULL,
    Email VARCHAR(200) NULL,
    Telefono VARCHAR(50) NULL,
    Direccion VARCHAR(250) NULL,
    Activo BIT NOT NULL DEFAULT(1)
);
GO

-- Categorias
CREATE TABLE Categorias (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(150) NOT NULL,
    Descripcion VARCHAR(500) NULL,
    Activo BIT NOT NULL DEFAULT(1)
);
GO

-- Marcas
CREATE TABLE Marcas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Activo BIT NOT NULL DEFAULT(1)
);
GO

-- Productos
CREATE TABLE Productos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(200) NOT NULL,
    Descripcion VARCHAR(1000) NULL,
    MarcaId INT NULL,
    CategoriaId INT NULL,
    ProveedorId INT NULL,
    PrecioCompra DECIMAL(18,2) NOT NULL DEFAULT(0.00),
    PorcentajeGanancia INT NOT NULL DEFAULT(0), -- Porcentaje gan.
    StockActual INT NOT NULL DEFAULT(0),
    StockMinimo INT NOT NULL DEFAULT(0),
    Activo BIT NOT NULL DEFAULT(1),
    PrecioVenta AS ROUND(PrecioCompra * (1.0 + PorcentajeGanancia / 100.0), 2) PERSISTED, -- Precio venta
    CONSTRAINT FK_Productos_Marcas FOREIGN KEY (MarcaId) REFERENCES Marcas(Id),
    CONSTRAINT FK_Productos_Categorias FOREIGN KEY (CategoriaId) REFERENCES Categorias(Id),
    CONSTRAINT FK_Productos_Proveedores FOREIGN KEY (ProveedorId) REFERENCES Proveedores(Id)
);
GO

CREATE INDEX IX_Productos_MarcaId ON Productos(MarcaId);
CREATE INDEX IX_Productos_CategoriaId ON Productos(CategoriaId);
CREATE INDEX IX_Productos_ProveedorId ON Productos(ProveedorId);
GO

-- Compras
CREATE TABLE Compras (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProveedorId INT NOT NULL,
    FechaCompra DATETIME NOT NULL DEFAULT(GETDATE()),
    Recibida BIT NOT NULL DEFAULT(0),
    Total DECIMAL(18,2) NULL,
    CONSTRAINT FK_Compras_Proveedores FOREIGN KEY (ProveedorId) REFERENCES Proveedores(Id)
);
GO

-- DetalleCompras
CREATE TABLE DetalleCompras (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CompraId INT NOT NULL,
    ProductoId INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    SubTotal AS ROUND(Cantidad * PrecioUnitario,2) PERSISTED, -- Subtotal línea
    CONSTRAINT FK_DetalleCompras_Compras FOREIGN KEY (CompraId) REFERENCES Compras(Id) ON DELETE CASCADE,
    CONSTRAINT FK_DetalleCompras_Productos FOREIGN KEY (ProductoId) REFERENCES Productos(Id)
);
GO

CREATE INDEX IX_DetalleCompras_CompraId ON DetalleCompras(CompraId);
CREATE INDEX IX_DetalleCompras_ProductoId ON DetalleCompras(ProductoId);
GO

-- Ventas
CREATE TABLE Ventas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NumeroFactura INT NULL,
    TipoFactura CHAR(1) NULL, -- Tipo factura
    Fecha DATETIME NOT NULL DEFAULT(GETDATE()),
    ClienteId INT NULL,
    UsuarioId INT NULL, -- Usuario vendedor
    Total DECIMAL(18,2) NULL,
    CONSTRAINT FK_Ventas_Clientes FOREIGN KEY (ClienteId) REFERENCES Clientes(Id),
    CONSTRAINT FK_Ventas_Usuarios FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
);
GO

-- DetalleVentas
CREATE TABLE DetalleVentas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    VentaId INT NOT NULL,
    ProductoId INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    SubTotal AS ROUND(Cantidad * PrecioUnitario,2) PERSISTED, -- Subtotal línea
    CONSTRAINT FK_DetalleVentas_Ventas FOREIGN KEY (VentaId) REFERENCES Ventas(Id) ON DELETE CASCADE,
    CONSTRAINT FK_DetalleVentas_Productos FOREIGN KEY (ProductoId) REFERENCES Productos(Id)
);
GO

CREATE INDEX IX_DetalleVentas_VentaId ON DetalleVentas(VentaId);
CREATE INDEX IX_DetalleVentas_ProductoId ON DetalleVentas(ProductoId);
GO

-- Datos prueba
INSERT INTO Marcas (Nombre, Activo) VALUES 
('Samsung', 1),
('LG', 1),
('Sony', 1),
('Philips', 1);

INSERT INTO Usuarios (NombreUsuario, Contrasena, Nombre, Apellido, TipoUsuario, Activo) VALUES 
('admin', 'admin123', 'Orlando', 'Administrador', 2, 1),
('vendedor', 'vendedor123', 'Albano', 'Vendedor', 1, 1);

INSERT INTO Categorias (Nombre, Descripcion, Activo) VALUES
('Televisores','Televisores LED/LCD/OLED',1),
('Audio','Equipos de audio y parlantes',1),
('Electrodomésticos','Pequeños electrodomésticos',1);

INSERT INTO Proveedores (RazonSocial, CUIL, Email, Telefono, Direccion, Activo) VALUES
('Proveedor Uno','20-12345678-9','prov1@example.com','011-1234-5678','Calle Falsa 123',1);

INSERT INTO Clientes (Nombre, Apellido, DNI, Email, Telefono, Direccion, Activo) VALUES
('Albano','Suarez','12345678','albano@mail.com','011-9876-5432','Av kurama 742',1),
('Orlando','Aguilera','98764543','orlan@mail.com','011-1234-5678','Av. Konoha 345',1);
GO
