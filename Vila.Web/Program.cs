using Vila.Web.Services.Customer;
using Vila.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

#region Session
builder.Services.AddSession(x =>
{
    x.IdleTimeout = TimeSpan.FromDays(7);
    x.Cookie.HttpOnly = true;
});
#endregion
#region ApiUrls
var apiUrlsSection = builder.Configuration.GetSection("ApiUrls");
builder.Services.Configure<ApiUrls>(apiUrlsSection);
#endregion
#region Dependencies
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
#endregion
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
