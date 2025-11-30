using Microsoft.EntityFrameworkCore;
using VoeMais.Data;
using VoeMais.Repositories.Implementations;
using VoeMais.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<VoeMaisContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
// Repositories
builder.Services.AddScoped<IEmpresaAereaRepository, EmpresaAereaRepository>();
builder.Services.AddScoped<IAviaoRepository, AviaoRepository>();
builder.Services.AddScoped<IAeroportoRepository, AeroportoRepository>();
builder.Services.AddScoped<IVooRepository, VooRepository>();
builder.Services.AddScoped<IEscalaRepository, EscalaRepository>();
builder.Services.AddScoped<IPoltronaRepository, PoltronaRepository>();
builder.Services.AddScoped<IVooPoltronaRepository, VooPoltronaRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
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
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
