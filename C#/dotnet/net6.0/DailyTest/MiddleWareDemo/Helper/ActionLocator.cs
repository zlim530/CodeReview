using System.Reflection;

namespace MiddleWareDemo;

public class ActionLocator
{
    private Dictionary<string, MethodInfo> data = new(StringComparer.OrdinalIgnoreCase);

    private static bool IsControllerType(Type t)
    {
        return t.IsClass && !t.IsAbstract&&t.Name.EndsWith("Controller");
    }

    public ActionLocator(IServiceCollection services, Assembly assemblyWeb)
    {
        var controllerTypes = assemblyWeb.GetTypes().Where(IsControllerType);
        foreach (Type controllerType in controllerTypes) 
        {
            services.AddScoped(controllerType);
            // 去掉结尾的 Controller 后缀
            int index = controllerType.Name.LastIndexOf("Controller");
            var controllerName = controllerType.Name.Substring(0, index);
            var methods = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            foreach (var method in methods) 
            {
                string actionName = method.Name;
                data[$"{controllerName}.{actionName}"] = method;
            }
        }
    }

    public MethodInfo? LocateActionMethod(string controllerName, string actionName)
    {
        var key = $"{controllerName}.{actionName}";
        data.TryGetValue(key, out MethodInfo? method);
        return method;
    }

}