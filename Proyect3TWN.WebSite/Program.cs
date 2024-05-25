using Proyect3TWN.WebSite.Services;
using Proyect3TWN.WebSite.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5125);
    options.ListenAnyIP(7290, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});
// Add services to the container.
builder.Services.AddRazorPages();

// Añade IHttpContextAccessor al contenedor de servicios
builder.Services.AddHttpContextAccessor();

// Registra el servicio de sesión
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Configura el tiempo de espera de la sesión
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmploymentHistoryService, EmploymentHistoryService>();
builder.Services.AddScoped<IPerfomanceReviewService, PerfomanceReviewService>();
builder.Services.AddScoped<IPyrollsService, PyrollsService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IVacationsAbsencesService, VacationsAbsencesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Añade el middleware de sesión
app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.Run();