using Microsoft.EntityFrameworkCore;
using Vila.WebApi.Context;
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
#endregion

services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
