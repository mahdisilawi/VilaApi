using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Vila.WebApi.Utility
{
    public class SwaggerVilaDocument : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        public SwaggerVilaDocument(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }
        public void Configure(SwaggerGenOptions options)
        {

            foreach (var item in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(item.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = $"Vila Api version {item.ApiVersion}",
                    Version = $"{item.ApiVersion}",
                    Description = $"this is a UI for Vila Api Version{item.ApiVersion}",
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
            }


            //Adding Authorization to Swagger Document
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "این کادر برای احراز هویت (jwt) هست \r\n\r\n" +
             "برای تست بعد از 'Bearer' \r\n\r\n" +
             " و یک فاصله توکن خود را وارد کنید .\r\n\r\n" +
             "example : Bearer dl;fgkd;fgldfgjdlfkgjiogjighuieredghsdklfghsjkdfhsdkljfl",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });


            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        },
                        Name="Bearer",
                        In=ParameterLocation.Header
                    },
                    new List<string>()
                }
            });


            var pathComment = Path.Combine(AppContext.BaseDirectory, "SwaggerComment.xml");
            options.IncludeXmlComments(pathComment);
        }
    }
}
