<!-- Placeholder para ejemplo -->

# 📁 Carpeta de Imágenes

Esta carpeta contiene todas las imágenes públicas del proyecto web.

## 📂 Estructura Recomendada

```
images/
├── iconos/              # Iconos para el menú
│   ├── alta.png
│   ├── baja.png
│   ├── cambios.png
│   ├── consultar.png
│   └── listado.png
│
├── coches/              # Fotos de vehículos
│   ├── default.jpg      # Imagen por defecto
│   └── [placa].jpg      # Imágenes por placa
│
├── fondos/              # Imágenes de fondo
│   └── header-bg.jpg
│
└── logo.png             # Logo principal
```

## 🎨 Formatos Recomendados

- **Logos e iconos**: PNG (con transparencia) o SVG
- **Fotos**: JPG optimizado
- **Fondos**: JPG (1920x1080px máximo)

## 📏 Tamaños Sugeridos

- Logo: 200x60px
- Iconos del menú: 128x128px
- Fotos de coches: 800x600px
- Fondos: 1920x1080px

## 🚀 Cómo Usar

En tus archivos .cshtml:
```html
<img src="~/images/logo.png" alt="Logo" />
```

En archivos CSS:
```css
background-image: url('../images/fondo.jpg');
```

## 📝 Notas

- Comprime las imágenes antes de subirlas
- Usa nombres descriptivos sin espacios: `icono-alta.png`
- Mantén los archivos pequeños para carga rápida
