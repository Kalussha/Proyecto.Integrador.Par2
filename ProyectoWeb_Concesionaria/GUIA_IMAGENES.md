# ??? Guía de Uso de Imágenes en ASP.NET Core

## ?? Ubicación de las Imágenes

Todas las imágenes deben ir en la carpeta **`wwwroot/images/`**

```
ProyectoWeb_Concesionaria/
??? wwwroot/                    ? Carpeta pública
    ??? css/
    ?   ??? site.css
    ??? images/                 ? AQUÍ van las imágenes
    ?   ??? logo.png
    ?   ??? icono-alta.png
    ?   ??? icono-baja.png
    ?   ??? icono-cambios.png
    ?   ??? icono-consultar.png
    ?   ??? icono-listado.png
    ?   ??? fondo-header.jpg
    ?   ??? coche-ejemplo.jpg
    ??? js/
```

---

## ?? Cómo Referenciar Imágenes

### 1. En archivos Razor (.cshtml)

#### Método Recomendado con `~`
```html
<img src="~/images/logo.png" alt="Logo" />
```

#### Con ruta absoluta
```html
<img src="/images/logo.png" alt="Logo" />
```

#### Con Tag Helper (cache busting automático)
```html
<img asp-append-version="true" src="~/images/logo.png" alt="Logo" />
```

### 2. En archivos CSS

#### Desde wwwroot/css/site.css
```css
.banner {
    background-image: url('../images/fondo.jpg');
}

.logo {
    background: url('/images/logo.png') no-repeat center;
}
```

#### En estilos inline dentro de Razor
```html
<div style="background-image: url('@Url.Content("~/images/fondo.jpg")')"></div>
```

---

## ?? Ejemplos Prácticos

### Ejemplo 1: Logo en el Header

**Layout.cshtml**
```html
<header>
    <nav class="navbar">
        <div class="container">
            <img src="~/images/logo.png" alt="Logo" class="logo" />
            <a class="navbar-brand" href="@Url.Action("Index", "Coches")">
                ?? Concesionaria
            </a>
        </div>
    </nav>
</header>

<style>
.logo {
    height: 40px;
    margin-right: 15px;
    vertical-align: middle;
}
</style>
```

### Ejemplo 2: Iconos en las Tarjetas del Menú

**Index.cshtml**
```html
<a href="@Url.Action("Alta", "Coches")" class="menu-card">
    <div class="card-icon">
        <img src="~/images/icono-alta.png" alt="Alta" class="icon-img" />
    </div>
    <h3>ALTA</h3>
    <p>Registrar nuevo vehículo</p>
</a>

<style>
.icon-img {
    width: 80px;
    height: 80px;
    object-fit: contain; /* mantiene proporción */
}
</style>
```

### Ejemplo 3: Imagen de Fondo

**Index.cshtml**
```html
<div class="hero-section">
    <h1>Bienvenido a la Concesionaria</h1>
</div>

<style>
.hero-section {
    background-image: url('~/images/fondo-coches.jpg');
    background-size: cover;
    background-position: center;
    background-repeat: no-repeat;
    padding: 100px 20px;
    color: white;
    text-align: center;
}

/* Opcional: Agregar overlay oscuro */
.hero-section::before {
    content: '';
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: rgba(0, 0, 0, 0.5);
}
</style>
```

### Ejemplo 4: Galería de Vehículos

**Listado.cshtml**
```html
@foreach (var coche in Model)
{
    <div class="coche-card">
        <img src="~/images/coches/@(coche.Placa).jpg" 
             alt="@coche.Marca @coche.Modelo" 
             class="coche-img"
             onerror="this.src='~/images/coche-default.jpg'" />
        
        <h3>@coche.Marca @coche.Modelo</h3>
        <p>Placa: @coche.Placa</p>
    </div>
}

<style>
.coche-img {
    width: 100%;
    height: 200px;
    object-fit: cover;
    border-radius: 8px;
}
</style>
```

---

## ?? Formatos de Imagen Recomendados

| Uso | Formato Recomendado | Motivo |
|-----|---------------------|--------|
| Logos | PNG o SVG | Transparencia y calidad |
| Iconos | PNG o SVG | Escalables y nítidos |
| Fotos | JPG | Tamaño pequeño |
| Fondos | JPG optimizado | Balance calidad/tamaño |
| Gráficos | SVG | Escalan sin perder calidad |

---

## ?? Optimización de Imágenes

### Tamaños Recomendados:

```
Logo del header:     200x60px   (máx 50KB)
Iconos del menú:     128x128px  (máx 30KB)
Fotos de coches:     800x600px  (máx 200KB)
Imagen de fondo:     1920x1080px (máx 500KB)
Thumbnails:          300x200px  (máx 50KB)
```

### Herramientas para Optimizar:
- [TinyPNG](https://tinypng.com/) - Comprimir PNG/JPG
- [SVGOMG](https://jakearchibald.github.io/svgomg/) - Optimizar SVG
- [Squoosh](https://squoosh.app/) - Comprimir cualquier formato

---

## ?? Imágenes Dinámicas desde la Base de Datos

Si quieres guardar imágenes de cada coche:

### Opción 1: Guardar URL en la BD

**Agregar columna a la tabla:**
```sql
ALTER TABLE coches ADD COLUMN imagen_url VARCHAR(255);
```

**Usar en la vista:**
```html
@if (!string.IsNullOrEmpty(coche.ImagenUrl))
{
    <img src="@coche.ImagenUrl" alt="@coche.Marca" />
}
else
{
    <img src="~/images/coche-default.jpg" alt="Sin imagen" />
}
```

### Opción 2: Guardar como archivo en wwwroot

**Estructura:**
```
wwwroot/images/coches/
    ??? ABC123.jpg  (por placa)
    ??? XYZ789.jpg
    ??? default.jpg
```

**Uso:**
```html
<img src="~/images/coches/@(coche.Placa).jpg" 
     onerror="this.src='/images/coches/default.jpg'" 
     alt="@coche.Marca" />
```

---

## ?? Imágenes Desde el Proyecto Windows Forms

Si tienes imágenes en tu proyecto Windows Forms en la carpeta `resources/`:

### Copiarlas al proyecto web:

```powershell
# Desde PowerShell
Copy-Item -Path ".\resources\*" -Destination ".\ProyectoWeb_Concesionaria\wwwroot\images\" -Recurse
```

O manualmente:
1. Abre la carpeta `resources/` de tu proyecto Windows Forms
2. Copia las imágenes
3. Pégalas en `ProyectoWeb_Concesionaria\wwwroot\images\`

---

## ?? Errores Comunes

### 1. Imagen no se muestra
```
? Problema: La ruta está mal
? Solución: Verifica que la imagen esté en wwwroot/images/
? Usa siempre ~/images/ o /images/
```

### 2. Imagen fuera de wwwroot
```
? Problema: Puse la imagen en la raíz del proyecto
? Solución: SOLO las imágenes en wwwroot/ son accesibles desde el navegador
```

### 3. Ruta con espacios o caracteres especiales
```
? mala-imagen.jpg
? Usa guiones o guiones bajos: buena-imagen.jpg
? Sin espacios: logo_empresa.png
```

### 4. Caché del navegador
```
? Solución: Presiona Ctrl+Shift+R para forzar recarga
? O usa: <img asp-append-version="true" src="~/images/logo.png" />
```

---

## ?? Ejemplo Completo: Personalizar tu Proyecto

### 1. Crea la estructura de carpetas:
```
wwwroot/images/
    ??? iconos/
    ?   ??? alta.png
    ?   ??? baja.png
    ?   ??? cambios.png
    ?   ??? consultar.png
    ?   ??? listado.png
    ??? fondos/
    ?   ??? header-bg.jpg
    ??? logo.png
```

### 2. Actualiza _Layout.cshtml:
```html
<header style="background-image: url('~/images/fondos/header-bg.jpg');">
    <img src="~/images/logo.png" alt="Logo" style="height: 50px;" />
    <h1>Concesionaria</h1>
</header>
```

### 3. Actualiza Index.cshtml:
```html
<div class="card-icon">
    <img src="~/images/iconos/alta.png" alt="Alta" class="icon-img" />
</div>
```

---

## ?? Resumen Rápido

| Tarea | Código |
|-------|--------|
| Agregar imagen | `<img src="~/images/foto.jpg" alt="Foto" />` |
| Fondo CSS | `background-image: url('/images/fondo.jpg');` |
| Con cache busting | `<img asp-append-version="true" src="~/images/logo.png" />` |
| Imagen por defecto | `<img src="..." onerror="this.src='/images/default.jpg'" />` |

---

**Ubicación importante**: Todas las imágenes van en `wwwroot/images/` ?
