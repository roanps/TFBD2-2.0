using Microsoft.EntityFrameworkCore;
using VoeMais.Repository;
using VoeMais.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<VoeMaisDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Repositorios
builder.Services.AddScoped<IAeronaveRepository, AeronaveRepository>();
builder.Services.AddScoped<IAeroportoRepository, AeroportoRepository>();
builder.Services.AddScoped<IEmpresaAereaRepository, EmpresaAereaRepository>();
builder.Services.AddScoped<IEscalaRepository, EscalaRepository>();
builder.Services.AddScoped<IPassageiroRepository, PassageiroRepository>();
builder.Services.AddScoped<IPoltronaRepository, PoltronaRepository>();
builder.Services.AddScoped<IVooRepository, VooRepository>();
builder.Services.AddScoped<IVooEscalaRepository, VooEscalaRepository>();
builder.Services.AddScoped<IPassagemRepository, PassagemRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<VoeMaisDbContext>();
    DbInitializer.Initialize(db);
}

app.Run();
