using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Vila.WebApi.Context;
using Vila.WebApi.Mapping;
using Vila.WebApi.Services.Detail;
using Vila.WebApi.Services.Vila;

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
#endregion
#region AutoMapper
services.AddAutoMapper(typeof(ModelsMapper));
#endregion
#region Swagger
services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("VilaOpenApi", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "Vila Api",
        Version = "1",
        Description = "this is a UI for Vila Api",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
        {
            Name = "Mahdi silavi",
            Email = "silawimahdi2002@gmail.com"
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense()
        {
            Name = "Vila Api License",
            Url = new Uri("https://github.com/mahdisilawi")
        }

    });

    var pathComment = Path.Combine(AppContext.BaseDirectory, "SwaggerComment.xml");
    option.IncludeXmlComments(pathComment);
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
        x.SwaggerEndpoint("/swagger/VilaOpenApi/swagger.json", "Vila Open Api");
        x.RoutePrefix = "";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
