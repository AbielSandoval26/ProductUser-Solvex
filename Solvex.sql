
-- Creación de la base de datos
CREATE DATABASE Solvex;
GO

USE Solvex;
GO

-- Tabla Usuarios
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    Rol NVARCHAR(50) NOT NULL CHECK (Rol IN ('Admin', 'Seller', 'User')),
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    FechaModificacion DATETIME NULL
);
GO

-- Tabla Productos
CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(150) NOT NULL,
    Descripcion NVARCHAR(500) NULL,
    ImagenUrl NVARCHAR(MAX) NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    FechaModificacion DATETIME NULL
);
GO

-- Tabla Variaciones de Producto
CREATE TABLE ProductVariation(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProductoId INT NOT NULL FOREIGN KEY REFERENCES Products(Id) ON DELETE CASCADE,
    Color NVARCHAR(50) NOT NULL,
    Precio DECIMAL(18,2) NOT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    FechaModificacion DATETIME NULL,
    CONSTRAINT UQ_Producto_Color UNIQUE (ProductoId, Color) -- Asegura que un producto no tenga colores duplicados
);
GO

CREATE PROCEDURE sp_InsertarUsuario
    @Nombre NVARCHAR(100),
    @Email NVARCHAR(255),
    @PasswordHash NVARCHAR(MAX),
    @Rol NVARCHAR(50)
AS
BEGIN
    IF @Rol NOT IN ('Admin', 'Seller', 'User')
    BEGIN
        RAISERROR ('El rol no es válido.', 16, 1);
        RETURN;
    END

    INSERT INTO Users (Nombre, Email, PasswordHash, Rol, FechaCreacion)
    VALUES (@Nombre, @Email, @PasswordHash, @Rol, GETDATE());
END;
GO


CREATE PROCEDURE sp_ActualizarUsuario
    @Id INT,
    @Nombre NVARCHAR(100),
    @Email NVARCHAR(255),
    @Rol NVARCHAR(50)
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Id = @Id)
    BEGIN
        RAISERROR ('El usuario no existe.', 16, 1);
        RETURN;
    END

    UPDATE Users
    SET Nombre = @Nombre,
        Email = @Email,
        Rol = @Rol,
        FechaModificacion = GETDATE()
    WHERE Id = @Id;
END;
GO

CREATE PROCEDURE sp_EliminarUsuario
    @Id INT
AS
BEGIN
    DELETE FROM Users WHERE Id = @Id;
END;
GO

CREATE PROCEDURE sp_InsertarProducto
    @Nombre NVARCHAR(150),
    @Descripcion NVARCHAR(500),
    @ImagenUrl NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Products(Nombre, Descripcion, ImagenUrl, FechaCreacion)
    VALUES (@Nombre, @Descripcion, @ImagenUrl, GETDATE());
END;
GO

CREATE PROCEDURE sp_InsertarVariacionProducto
    @ProductoId INT,
    @Color NVARCHAR(50),
    @Precio DECIMAL(18,2)
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Products WHERE Id = @ProductoId)
    BEGIN
        RAISERROR ('El producto no existe.', 16, 1);
        RETURN;
    END

    INSERT INTO ProductVariation(ProductoId, Color, Precio, FechaCreacion)
    VALUES (@ProductoId, @Color, @Precio, GETDATE());
END;
GO

CREATE PROCEDURE sp_ObtenerProductosConVariaciones
AS
BEGIN
    SELECT 
        p.Id AS ProductoId,
        p.Nombre,
        p.Descripcion,
        p.ImagenUrl,
        v.Id AS VariacionId,
        v.Color,
        v.Precio
    FROM Products p
    LEFT JOIN ProductVariation v ON p.Id = v.ProductoId
    ORDER BY p.Nombre, v.Color;
END;
GO

CREATE VIEW vw_Usuarios
AS
SELECT 
    Id,
    Nombre,
    Email,
    Rol,
    FechaCreacion,
    FechaModificacion
FROM Users;
GO

CREATE VIEW vw_ProductosConVariaciones
AS
SELECT 
    p.Id AS ProductoId,
    p.Nombre AS Producto,
    v.Color,
    v.Precio,
    p.Descripcion,
    p.ImagenUrl,
    p.FechaCreacion,
    p.FechaModificacion
FROM Products p
LEFT JOIN ProductVariation v ON p.Id = v.ProductoId;
GO

