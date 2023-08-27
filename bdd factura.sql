create database factura

use factura

CREATE TABLE Cliente (
    ID_Cliente INT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Telefono VARCHAR(20),
    Direccion VARCHAR(255)
);

CREATE TABLE Producto (
    ID_Producto INT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    Stock INT NOT NULL
);

CREATE TABLE FacturaCabecera (
    ID_FacturaCabecera INT PRIMARY KEY,
    FechaFactura DATE DEFAULT GETDATE(),
    ID_Cliente INT,
    Total DECIMAL(10, 2),
    FOREIGN KEY (ID_Cliente) REFERENCES Cliente (ID_Cliente)
);

CREATE TABLE FacturaDetalle (
    ID_FacturaDetalle INT PRIMARY KEY,
    ID_FacturaCabecera INT,
    ID_Producto INT,
    Cantidad INT,
    PrecioUnitario DECIMAL(10, 2),
    Total DECIMAL(10, 2),
    FOREIGN KEY (ID_FacturaCabecera) REFERENCES FacturaCabecera (ID_FacturaCabecera),
    FOREIGN KEY (ID_Producto) REFERENCES Producto (ID_Producto)
);

CREATE TABLE MetodoPago (
    ID_MetodoPago INT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL
);


select * from Cliente;
select * from Producto;
select * from FacturaCabecera;
select * from FacturaDetalle;
select * from MetodoPago;