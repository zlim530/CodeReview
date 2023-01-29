namespace ConfigService
{
    public class LayeredConfigProvider : IConfigReader
    {
        private readonly IEnumerable<IConfigProvider> services;

        public LayeredConfigProvider(IEnumerable<IConfigProvider> services)
        {
            this.services = services;
        }

        public string GetValue(string name)
        {
            string value = null;
            foreach (var service in services)
            {
                string newValue = service.GetValue(name);
                if (newValue != null) { value = newValue; } // 最后一个找到的不为 null 的值就是最终值：”可覆盖的配置读取器“
            }
            return value;
        }
    }
}