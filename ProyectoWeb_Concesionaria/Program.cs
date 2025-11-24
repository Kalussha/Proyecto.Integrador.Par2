var builder = WebApplication.CreateBuilder(args);

// agregamos servicios MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// configuramos el pipeline de requests
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// ruta por defecto - va directo al menu principal
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Coches}/{action=Index}/{id?}");

app.Run();
