# ?? Guía de Migración: Windows Forms ? ASP.NET Core MVC

## Comparativa de Arquitectura

### Windows Forms (Antes)
```
??? Forms (.cs + .Designer.cs)
?   ??? frminicio.cs         ? Menú principal
?   ??? frmalta.cs           ? Formulario de alta
?   ??? frmbaja.cs           ? Formulario de baja
?   ??? frmcambios.cs        ? Formulario de cambios
?   ??? frmconsultar.cs      ? Formulario de consulta
??? Models
?   ??? Coche.cs
?   ??? CocheDAO.cs
?   ??? DatabaseConnection.cs
??? Program.cs
```

### ASP.NET Core MVC (Ahora)
```
??? Controllers
?   ??? CochesController.cs  ? Maneja TODAS las acciones
??? Views
?   ??? Coches/
?       ??? Index.cshtml     ? Menú principal
?       ??? Alta.cshtml      ? Formulario de alta
?       ??? Baja.cshtml      ? Formulario de baja
?       ??? Cambios.cshtml   ? Formulario de cambios
?       ??? Consultar.cshtml ? Formulario de consulta
?       ??? Listado.cshtml   ? (NUEVO) Lista completa
??? Models
?   ??? Coche.cs            ? SIN CAMBIOS
?   ??? CocheDAO.cs         ? SIN CAMBIOS
?   ??? DatabaseConnection.cs ? SIN CAMBIOS
??? Program.cs
```

---

## ?? Migración Detallada por Componente

### 1. MENÚ PRINCIPAL

#### Windows Forms (frminicio.cs)
```csharp
private void btnalta_Click(object sender, EventArgs e)
{
    this.Hide();
    frmalta frm = new frmalta();
    frm.FormClosed += (s, args) => this.Show();
    frm.Show();
}
```

#### ASP.NET (Index.cshtml)
```html
<a href="@Url.Action("Alta", "Coches")" class="menu-card">
    <h3>ALTA</h3>
    <p>Registrar nuevo vehículo</p>
</a>
```

**Cambios**:
- ? Ya no se abren/cierran ventanas
- ? Se navega entre URLs con links
- ? El controlador maneja las acciones

---

### 2. FORMULARIO DE ALTA

#### Windows Forms (frmalta.cs)
```csharp
private void btnguardar_Click(object sender, EventArgs e)
{
    // validaciones
    if (string.IsNullOrWhiteSpace(txtplaca.Text) || ...)
    {
        MessageBox.Show("Complete todos los campos.");
        return;
    }

    // crear objeto
    Coche nuevoCoche = new Coche
    {
        Placa = txtplaca.Text.Trim().ToUpper(),
        Marca = txtmarca.Text.Trim(),
        // ...
    };

    // guardar
    bool resultado = CocheDAO.InsertarCoche(nuevoCoche);
    
    if (resultado)
    {
        MessageBox.Show("Vehículo registrado correctamente.");
        LimpiarFormulario();
    }
}
```

#### ASP.NET (CochesController.cs)
```csharp
[HttpPost]
public IActionResult Alta(Coche coche)
{
    // validaciones
    if (string.IsNullOrWhiteSpace(coche.Placa) || ...)
    {
        TempData["Error"] = "Complete todos los campos.";
        return RedirectToAction("Alta");
    }

    // normalizar datos
    coche.Placa = coche.Placa.Trim().ToUpper();
    coche.Marca = coche.Marca.Trim();

    // guardar
    bool resultado = CocheDAO.InsertarCoche(coche);

    if (resultado)
    {
        TempData["Exito"] = "Vehículo registrado correctamente.";
        return RedirectToAction("Alta");
    }
}
```

**Cambios**:
- ? `MessageBox.Show()` ? ? `TempData["Mensaje"]`
- ? Leer de `txtplaca.Text` ? ? Recibir objeto `Coche` en parámetro
- ? Eventos de botón ? ? Métodos HTTP POST
- ? La lógica de validación y guardado es **idéntica**

---

### 3. FORMULARIO DE BAJA

#### Windows Forms (frmbaja.cs)
```csharp
private void btnbajaeliminar_Click(object sender, EventArgs e)
{
    string placaEliminar = txtbajaplaca.Text.Trim().ToUpper();

    // buscar el coche
    Coche coche = CocheDAO.ConsultarCochePorPlaca(placaEliminar);
    
    if (coche == null)
    {
        MessageBox.Show("Placa no encontrada.");
        return;
    }

    // pedir confirmación
    var confirm = MessageBox.Show(
        $"¿Está seguro?\nPlaca: {coche.Placa}\n...",
        "Confirmar",
        MessageBoxButtons.YesNo);

    if (confirm == DialogResult.Yes)
    {
        bool resultado = CocheDAO.EliminarCoche(placaEliminar);
        // ...
    }
}
```

#### ASP.NET (CochesController.cs)
```csharp
[HttpPost]
public IActionResult Baja(string placa, string confirmar)
{
    string placaEliminar = placa?.Trim().ToUpper() ?? "";

    // buscar el coche
    Coche? coche = CocheDAO.ConsultarCochePorPlaca(placaEliminar);

    if (coche == null)
    {
        TempData["Error"] = "Placa no encontrada.";
        return RedirectToAction("Baja");
    }

    // si no ha confirmado, mostrar datos
    if (confirmar != "SI")
    {
        ViewBag.CocheAEliminar = coche;
        return View();
    }

    // si confirmó, eliminar
    bool resultado = CocheDAO.EliminarCoche(placaEliminar);
    // ...
}
```

**Cambios**:
- ? `MessageBox` con botones YesNo ? ? Formulario de confirmación en la vista
- ? Diálogo modal ? ? Dos pasos: mostrar datos, luego confirmar
- ? La consulta y eliminación es **idéntica**

---

### 4. FORMULARIO DE CAMBIOS

#### Windows Forms (frmcambios.cs)
```csharp
// Paso 1: Buscar
private void btnconsultacambio_Click(object sender, EventArgs e)
{
    string placaBuscada = txtplaca.Text.Trim().ToUpper();
    cocheEditando = CocheDAO.ConsultarCochePorPlaca(placaBuscada);
    
    if (cocheEditando != null)
    {
        txtmarca.Text = cocheEditando.Marca;
        txtmodelo.Text = cocheEditando.Modelo;
        cmbaño.SelectedItem = cocheEditando.Anio.ToString();
        // ...
    }
}

// Paso 2: Guardar
private void btnguardarcambios_Click(object sender, EventArgs e)
{
    cocheEditando.Marca = txtmarca.Text.Trim();
    cocheEditando.Modelo = txtmodelo.Text.Trim();
    // ...
    bool resultado = CocheDAO.ActualizarCoche(cocheEditando);
}
```

#### ASP.NET (CochesController.cs)
```csharp
// Paso 1: Buscar
[HttpPost]
public IActionResult BuscarParaEditar(string placa)
{
    Coche? coche = CocheDAO.ConsultarCochePorPlaca(placa);
    
    if (coche != null)
    {
        ViewBag.CocheEncontrado = coche;
        // La vista mostrará el formulario pre-llenado
    }
    return View("Cambios");
}

// Paso 2: Guardar
[HttpPost]
public IActionResult GuardarCambios(Coche coche)
{
    coche.Marca = coche.Marca.Trim();
    coche.Modelo = coche.Modelo.Trim();
    // ...
    bool resultado = CocheDAO.ActualizarCoche(coche);
}
```

**Cambios**:
- ? Variable de instancia `cocheEditando` ? ? `ViewBag` para pasar datos a la vista
- ? Llenar textboxes manualmente ? ? Razor pre-llena el form con `value="@coche.Marca"`
- ? La actualización en BD es **idéntica**

---

### 5. FORMULARIO DE CONSULTA

#### Windows Forms (frmconsultar.cs)
```csharp
private void btnconsultar_Click(object sender, EventArgs e)
{
    string placaBuscada = txtplacaconsultar.Text.Trim().ToUpper();
    Coche coche = CocheDAO.ConsultarCochePorPlaca(placaBuscada);

    if (coche != null)
    {
        txtmarca.Text = coche.Marca;
        txtmodelo.Text = coche.Modelo;
        txtaño.Text = coche.Anio.ToString();
        txttipo.Text = coche.Tipo;
    }
}
```

#### ASP.NET (CochesController.cs)
```csharp
[HttpPost]
public IActionResult Consultar(string placa)
{
    Coche? coche = CocheDAO.ConsultarCochePorPlaca(placa);

    if (coche != null)
    {
        ViewBag.Coche = coche;
        // La vista mostrará los datos
    }
    return View();
}
```

**Cambios**:
- ? Asignar a textboxes ? ? Pasar objeto a la vista con `ViewBag`
- ? TextBox de solo lectura ? ? HTML que muestra los datos
- ? La consulta SQL es **idéntica**

---

## ?? Componentes Sin Cambios

### ? Coche.cs
```csharp
// EXACTAMENTE IGUAL en ambos proyectos
public class Coche
{
    public int Id { get; set; }
    public string Placa { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public int Anio { get; set; }
    public string Tipo { get; set; }
}
```

### ? CocheDAO.cs
```csharp
// EXACTAMENTE IGUAL en ambos proyectos
public static bool InsertarCoche(Coche coche) { ... }
public static Coche ConsultarCochePorPlaca(string placa) { ... }
public static bool ActualizarCoche(Coche coche) { ... }
public static bool EliminarCoche(string placa) { ... }
```

### ? DatabaseConnection.cs
```csharp
// EXACTAMENTE IGUAL en ambos proyectos
private static string connectionString = "Server=localhost;...";
public static MySqlConnection GetConnection() { ... }
```

---

## ?? Patrón de Migración

### Windows Forms Pattern
```
Usuario ? [Form] ? Evento Click ? Validar ? DAO ? MySQL ? MessageBox
```

### ASP.NET MVC Pattern
```
Usuario ? [Vista] ? HTTP POST ? [Controller] ? Validar ? DAO ? MySQL ? TempData ? RedirectToAction
```

---

## ?? Ventajas de la Migración Web

1. **Accesibilidad**: Se puede acceder desde cualquier navegador
2. **Sin instalación**: No hay que instalar nada en el cliente
3. **Actualización centralizada**: Cambias el código en un solo lugar
4. **Multiplataforma**: Funciona en Windows, Mac, Linux, móviles
5. **Concurrent users**: Múltiples usuarios pueden usarlo al mismo tiempo
6. **Responsive**: Se adapta a diferentes tamaños de pantalla

---

## ?? Resumen de Conversiones

| Windows Forms | ASP.NET MVC |
|--------------|-------------|
| `Form` | `View (.cshtml)` |
| `Button Click Event` | `[HttpPost] Action Method` |
| `MessageBox.Show()` | `TempData["Mensaje"]` |
| `txtNombre.Text` | `<input name="Nombre">` |
| `this.Hide() / Show()` | `RedirectToAction()` |
| `Controls` | `HTML Elements` |
| Event Handlers | Action Methods |
| `.Designer.cs` | Razor Syntax |

---

**Conclusión**: La lógica de negocio (Models + DAO) se mantiene intacta. Solo cambia la capa de presentación (UI).
