# ?? INICIO RÁPIDO - 3 Pasos

## Paso 1: Verificar MySQL ?

Asegúrate de que MySQL esté corriendo y que tengas la base de datos `concesionaria`.

```bash
# Abre MySQL Workbench o ejecuta en terminal:
mysql -u root -p

# Luego en MySQL:
SHOW DATABASES LIKE 'concesionaria';
USE concesionaria;
SHOW TABLES;
```

Si NO tienes la base de datos, ejecútala desde el archivo `database_script.sql` de tu proyecto Windows Forms.

---

## Paso 2: Abrir el Proyecto ??

### Opción A: Visual Studio
1. Haz doble click en `ProyectoWeb_Concesionaria.csproj`
2. Visual Studio se abrirá automáticamente

### Opción B: Visual Studio Code
1. Abre VSCode
2. File ? Open Folder
3. Selecciona la carpeta `ProyectoWeb_Concesionaria`

### Opción C: Terminal
```bash
cd ProyectoWeb_Concesionaria
code .   # Abre en VS Code
```

---

## Paso 3: Ejecutar ??

### Desde Visual Studio:
1. Presiona **F5** (o click en el botón verde ?)
2. Tu navegador se abrirá automáticamente
3. ¡Listo! Ya puedes usar la aplicación

### Desde Terminal:
```bash
dotnet run
```

Luego abre tu navegador en: **https://localhost:5001** (o el puerto que te muestre)

---

## ?? ¡Ya Está!

Deberías ver el menú principal con 5 opciones:
- ?? ALTA - Registrar nuevo vehículo
- ??? BAJA - Eliminar vehículo  
- ?? CAMBIOS - Modificar datos
- ?? CONSULTAR - Buscar por placa
- ?? LISTADO - Ver todos

---

## ?? Problemas Comunes

### "Unable to connect to MySQL"
- ? Verifica que MySQL esté corriendo
- ? Verifica usuario/contraseña en `Models/DatabaseConnection.cs`

### "Port already in use"
- ? Cierra otras instancias del proyecto
- ? O cambia el puerto en `Properties/launchSettings.json`

### Página en blanco
- ? Presiona Ctrl+F5 para refrescar
- ? Verifica la consola por errores

---

## ?? Notas

- **Puerto**: Puede cambiar cada vez que ejecutas (5001, 5002, etc.)
- **HTTPS**: Por defecto usa HTTPS, acepta el certificado si te lo pide
- **Hot Reload**: Los cambios en archivos .cshtml se reflejan automáticamente
- **Debug**: Puedes poner breakpoints igual que en Windows Forms

---

## ?? Diferencias con Windows Forms

| Windows Forms | ASP.NET Web |
|--------------|-------------|
| Exe que se ejecuta | Servidor web que corre |
| Una ventana a la vez | Varias pestañas |
| Solo en Windows | Cualquier SO con navegador |
| Un usuario | Múltiples usuarios |

---

**Siguiente paso**: Lee `README.md` para más detalles o `GUIA_MIGRACION.md` para entender cómo se migró cada parte.
