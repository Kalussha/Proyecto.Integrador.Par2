-- Script de verificación de la base de datos
-- Ejecuta esto en MySQL Workbench para verificar que todo esté correcto

-- Verificar que la base de datos existe
SHOW DATABASES LIKE 'concesionaria';

-- Usar la base de datos
USE concesionaria;

-- Mostrar la estructura de la tabla
DESCRIBE coches;

-- Contar cuántos registros hay
SELECT COUNT(*) as total_vehiculos FROM coches;

-- Mostrar todos los vehículos
SELECT * FROM coches ORDER BY id DESC;

-- Si necesitas crear la base de datos desde cero:
/*
CREATE DATABASE IF NOT EXISTS concesionaria;
USE concesionaria;

CREATE TABLE coches (
    id INT AUTO_INCREMENT PRIMARY KEY,
    placa VARCHAR(10) NOT NULL UNIQUE,
    marca VARCHAR(50) NOT NULL,
    modelo VARCHAR(50) NOT NULL,
    anio INT NOT NULL,
    tipo VARCHAR(20) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
*/
