using System.Reflection;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Default.Api.DependencyInjection;

public static class SwaggerServiceCollectionExtension
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.OperationFilter<OperationIdFilter>();
            options.CustomSchemaIds(type => type.ToString());
            options.SchemaFilter<NullabilitySchemaFilter>();
            options.UseAllOfToExtendReferenceSchemas();

            options.MapType<TimeSpan>(() => new OpenApiSchema { Type = JsonSchemaType.String });
            options.MapType<TimeSpan?>(() => new OpenApiSchema { Type = JsonSchemaType.String });
            options.CustomOperationIds(api =>
                $"{api.ActionDescriptor.RouteValues["controller"]}_{api.ActionDescriptor.RouteValues["action"]}");
        });

        return services;
    }
}

public class OperationIdFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.OperationId = context.MethodInfo.Name;
    }
}

public class NullabilitySchemaFilter : ISchemaFilter
{
    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.MemberInfo is not PropertyInfo)
            return;

        _ = new NullabilityInfoContext();
    }
}
