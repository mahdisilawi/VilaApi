using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using Vila.WebApi.Context;
using Vila.WebApi.Mapping;
using Vila.WebApi.Services.Customer;
using Vila.WebApi.Services.Detail;
using Vila.WebApi.Services.Vila;
using Vila.WebApi.Utility;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.

//Connecting to DataBase
services.AddDbContext<DataContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("Local")
   )
);

#region Dependencies
services.AddTransient<IVilaService,VilaService>();
services.AddTransient<IDetailService,DetailService>();
services.AddTransient<ICustomerService, CustomerService>();
#endregion
#region AutoMapper
services.AddAutoMapper(typeof(ModelsMapper));
#endregion
#region ApiVersioning

services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
    options.ReportApiVersions = true;
    //Get Version From header
    //options.ApiVersionReader = new HeaderApiVersionReader("X-ApiVersion");

}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVVV";
});
#endregion
#region Swagger
services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerVilaDocument>();
services.AddSwaggerGen();
#endregion
#region Jwt

var key = Encoding.ASCII.GetBytes(" jwt vila api ");

services.AddAuthentication(x =>
{
    x.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {

        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuerSigningKey = true,
        ValidIssuer = "Vila Api",
        ValidateIssuer = true,
        ValidAudience = "Jwt Auth",
        ValidateAudience = true,
        ValidateLifetime = true
    };
});
#endregion

services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        var provider = app.Services.CreateScope().ServiceProvider
        .GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var item in provider.ApiVersionDescriptions)
        {
            x.SwaggerEndpoint($"/swagger/{item.GroupName}/swagger.json",item.GroupName.ToString());

        }

        //x.SwaggerEndpoint("/swagger/VilaOpenApi/swagger.json", "Vila Open Api");
        x.RoutePrefix = "";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
