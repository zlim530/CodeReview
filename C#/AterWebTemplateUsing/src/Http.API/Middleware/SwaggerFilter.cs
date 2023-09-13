using System.Reflection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Http.API.Middleware;
public class SwaggerFilter
{
}

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema model, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            //model.Enum.Clear();
            //model.Description = "desp";
            OpenApiArray name = new();
            Enum.GetNames(context.Type)
                .ToList()
                .ForEach(n =>
                {
                    name.Add(new OpenApiString(n));
                });
            model.Extensions.Add("x-enumNames", name);
        }
    }
}

public class NewEnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum) 
        {
            var name = new OpenApiArray();
            var enumData = new OpenApiArray();
            FieldInfo[] fields = context.Type.GetFields();
            foreach ( var field in fields ) 
            {
                if (field.Name != "value__")
                {
                    name.Add(new OpenApiString(field.Name));
                    CustomAttributeData? desAttr = field.CustomAttributes.Where(a => a.AttributeType.Name == "DescriptionAttribute").FirstOrDefault();

                    if (desAttr != null) 
                    {
                        CustomAttributeTypedArgument des = desAttr.ConstructorArguments.FirstOrDefault();
                        if (des.Value != null)
                        {
                            enumData.Add(new OpenApiObject()
                            {
                                ["name"] = new OpenApiString(field.Name),
                                ["value"] = new OpenApiInteger((int)field.GetRawConstantValue()!),
                                ["description"] = new OpenApiString(des.Value.ToString())
                            });
                        }
                    }
                }
            }
            schema.Extensions.Add("x-enumNames",name);
            schema.Extensions.Add("x-enumData", enumData);
        }
    }
}