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
                if (newValue != null) { value = newValue; } // ���һ���ҵ��Ĳ�Ϊ null ��ֵ��������ֵ�����ɸ��ǵ����ö�ȡ����
            }
            return value;
        }
    }
}