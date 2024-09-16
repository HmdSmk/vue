using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace sample_vue_app_server.Attributes;


public class SwaggerExcludeFilter : ISchemaFilter
{
    //hides properties with SwaggerIgnore attribute from swagger doc.

    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema?.Properties == null || context?.Type == null)
        {
            return;
        }

        var excludedProperties = context.Type.GetProperties()
            .Where(t =>
                t.GetCustomAttribute<SwaggerIgnoreAttribute>()
                != null);

        foreach (var excludedProperty in excludedProperties)
        {
            var propToExclude = schema.Properties.Keys.ToList().FirstOrDefault(x => x.Equals(excludedProperty.Name, StringComparison.InvariantCultureIgnoreCase));
            if (propToExclude != null)
                schema.Properties.Remove(propToExclude);
        }
    }
}