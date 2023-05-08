using CloudinaryDotNet;
using FrancaSW.Comun;
using FrancaSW.DataContext;
using FrancaSW.Services;
using FrancaSW.Services.AgregarProducto;
using FrancaSW.Services.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IServiceLogin, ServiceLogin>();
builder.Services.AddScoped<IServiceRegister, ServiceRegister>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<IServiceMateriaPrima, ServiceMateriaPrima>();
builder.Services.AddScoped<IServiceProducto, ServiceProducto>();
builder.Services.AddScoped<IServiceColorProducto, ServiceColorProducto>();
builder.Services.AddScoped<IServiceDisenioProducto, ServiceDisenioProducto>();
builder.Services.AddScoped<IServiceMedidaProducto, ServiceMedidaProducto>();
builder.Services.AddScoped<IServicePrecioProducto, ServicePrecioProducto>();
builder.Services.AddScoped<IServiceTipoProducto, ServiceTipoProducto>();
builder.Services.AddScoped<IServiceProveedor, ServiceProveedor>();
builder.Services.AddScoped<IServicePrecioMatPrimaProv, ServicePrecioMatPrimaProv>();
builder.Services.AddScoped<IServiceProvinciaLocalidad, ServiceProvinciaLocalidad>();
builder.Services.AddScoped<IServiceConsultaPrMpProv, ServiceConsultaPrMpProv>();
builder.Services.AddScoped<IServiceCliente, ServiceCliente>();
builder.Services.AddScoped<IServiceCatalogo, ServiceCatalogo>();
builder.Services.AddScoped<IServiceStockMateriaPrima, ServiceStockMateriaPrima>();
builder.Services.AddScoped<IServiceStockProductos, ServiceStockProductos>();
builder.Services.AddScoped<IServiceHistorialStockMP, ServiceHistorialStockMP>();
builder.Services.AddScoped<IServiceHistorialStockProductos, ServiceHistorialStockProductos>();

// Add services to the container.
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRazorPages();
builder.Services.AddCors();
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<FrancaSwContext>(options => options.UseSqlServer("Data Source=DESKTOP-HFJMQO3\\SQLEXPRESS;Initial Catalog=FrancaSW; Trusted_Connection=true; Encrypt=False"));
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");
});



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
