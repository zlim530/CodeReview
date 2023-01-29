namespace ConfigService
{
    public class EnvVarConfigProvider : IConfigProvider
    {
        public string GetValue(string name)
        {
            return Environment.GetEnvironmentVariable(name);
        }
    }
}