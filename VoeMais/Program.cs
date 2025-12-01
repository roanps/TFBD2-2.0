using Microsoft.EntityFrameworkCore;
using VoeMais.Data;
using VoeMais.Repositories;
using VoeMais.Repositories.Implementations;
using VoeMais.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Banco de dados
builder.Services.AddDbContext<VoeMaisContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositórios
builder.Services.AddScoped<IAeroportoRepository, AeroportoRepository>();
builder.Services.AddScoped<IAviaoRepository, AviaoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IEmpresaAereaRepository, EmpresaAereaRepository>();
builder.Services.AddScoped<IEscalaRepository, EscalaRepository>();
builder.Services.AddScoped<IPassagemRepository, PassagemRepository>();
builder.Services.AddScoped<IPoltronaRepository, PoltronaRepository>();
builder.Services.AddScoped<IVooRepository, VooRepository>();
builder.Services.AddScoped<IVooPoltronaRepository, VooPoltronaRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

// Arquivos estáticos (css, js, imagens…)
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Rotas MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
