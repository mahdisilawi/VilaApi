using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
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
           

            var pathComment = Path.Combine(AppContext.BaseDirectory, "SwaggerComment.xml");
            options.IncludeXmlComments(pathComment);
        }
    }
}
