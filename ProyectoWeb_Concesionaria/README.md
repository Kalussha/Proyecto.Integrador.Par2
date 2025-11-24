# ?? Sistema de Gestión de Vehículos - ASP.NET Core MVC

## Migración de Windows Forms a ASP.NET Web

Este proyecto es la **versión web** del sistema de gestión de concesionaria que originalmente estaba en Windows Forms. 
Tiene todas las mismas funcionalidades pero ahora funciona en un navegador web.

## ?? Funcionalidades (igual que en Windows Forms)

- ? **ALTA**: Registrar nuevos vehículos
- ? **BAJA**: Eliminar vehículos existentes
- ? **CAMBIOS**: Modificar datos de vehículos
- ? **CONSULTAR**: Buscar vehículos por placa
- ? **LISTADO**: Ver todos los vehículos (extra en web)

## ?? Requisitos

- .NET 8.0 SDK
- MySQL Server (con la misma base de datos que usabas en Windows Forms)
- Visual Studio 2022 o Visual Studio Code

## ??? Base de Datos

**IMPORTANTE**: Este proyecto usa la **misma base de datos** que tu aplicación Windows Forms.
No necesitas crear nada nuevo, solo asegúrate de que MySQL esté corriendo.

La conexión está configurada para:
- **Server**: localhost
- **Database**: concesionaria
- **User**: root
- **Password**: 8875421390

Si necesitas cambiar estos datos, modifica el archivo `Models/DatabaseConnection.cs`

## ?? Cómo ejecutar el proyecto

### Opción 1: Desde Visual Studio

1. Abre el archivo `ProyectoWeb_Concesionaria.csproj` en Visual Studio
2. Presiona F5 o click en el botón "? Run"
3. El navegador se abrirá automáticamente en `https://localhost:XXXX`

### Opción 2: Desde terminal/consola

```bash
# Navega a la carpeta del proyecto
cd ProyectoWeb_Concesionaria

# Restaura los paquetes NuGet
dotnet restore

# Ejecuta el proyecto
dotnet run
```

Luego abre tu navegador en la URL que aparece en consola (generalmente `https://localhost:5001`)

## ?? Estructura del Proyecto

```
ProyectoWeb_Concesionaria/
??? Controllers/
?   ??? CochesController.cs      # Maneja todas las acciones (reemplaza a los eventos de los forms)
??? Models/
?   ??? Coche.cs                 # Misma clase del proyecto Windows Forms
?   ??? CocheDAO.cs              # Mismas operaciones de base de datos
?   ??? DatabaseConnection.cs   # Misma conexión a MySQL
??? Views/
?   ??? Coches/
?   ?   ??? Index.cshtml        # Menú principal (reemplaza a frminicio)
?   ?   ??? Alta.cshtml         # Form de alta (reemplaza a frmalta)
?   ?   ??? Baja.cshtml         # Form de baja (reemplaza a frmbaja)
?   ?   ??? Cambios.cshtml      # Form de cambios (reemplaza a frmcambios)
?   ?   ??? Consultar.cshtml    # Form de consulta (reemplaza a frmconsultar)
?   ?   ??? Listado.cshtml      # Tabla con todos los coches (nuevo)
?   ??? Shared/
?       ??? _Layout.cshtml       # Layout base de la app
??? wwwroot/
?   ??? css/
?       ??? site.css             # Estilos CSS
??? Program.cs                   # Configuración de la app
??? appsettings.json             # Configuraciones

```

## ?? Diferencias con Windows Forms

### Lo que cambió:
- **UI**: De forms de escritorio a páginas web HTML/CSS
- **Navegación**: En vez de abrir/cerrar ventanas, navegas entre URLs
- **Mensajes**: En vez de MessageBox, se usan alertas en la página
- **Eventos**: En vez de eventos de botones, se usan métodos HTTP (GET/POST)

### Lo que NO cambió:
- ? La clase `Coche` es **idéntica**
- ? El `CocheDAO` es **idéntico** (mismas consultas SQL)
- ? La `DatabaseConnection` es **idéntica**
- ? Todas las validaciones son **iguales**
- ? La lógica de negocio es **la misma**

## ?? Características de la Web

- Diseño moderno y responsive
- Colores y estilos profesionales
- Mensajes de éxito y error claros
- Navegación intuitiva
- Compatible con todos los navegadores modernos

## ?? Notas Importantes

1. **Los datos son compartidos**: Si registras un coche en la app Windows Forms, lo verás en la web y viceversa
2. **Sin cambios en BD**: No necesitas modificar nada en MySQL
3. **Puerto dinámico**: El puerto puede cambiar cada vez que ejecutas (lo verás en la consola)
4. **HTTPS**: Por defecto usa HTTPS, si tienes problemas acepta el certificado de desarrollo

## ?? Solución de Problemas

### Error de conexión a MySQL
```
Error: Unable to connect to any of the specified MySQL hosts
```
**Solución**: Verifica que MySQL esté corriendo y que las credenciales sean correctas

### Error de puerto en uso
```
Error: Unable to bind to https://localhost:5001
```
**Solución**: Cierra cualquier otra instancia del proyecto que esté corriendo

### No se cargan los estilos
**Solución**: Presiona Ctrl+F5 para refrescar sin caché

## ?? Soporte

Si tienes problemas, verifica:
1. ? MySQL está corriendo
2. ? La base de datos `concesionaria` existe
3. ? Las credenciales de conexión son correctas
4. ? .NET 8 SDK está instalado (`dotnet --version`)

---

**Desarrollado por**: Joshua Rafael  
**Proyecto**: Parcial 2 - Programación Visual  
**Versión**: 1.0 (Migración a ASP.NET)
