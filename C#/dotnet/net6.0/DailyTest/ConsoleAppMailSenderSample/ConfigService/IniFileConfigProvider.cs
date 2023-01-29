namespace ConfigService
{
    class IniFileConfigProvider : IConfigProvider
    {
        public string FilePath { get; set; }


        public string GetValue(string name)
        {
            var kv = File.ReadAllLines(FilePath).Select(s => s.Split('=')).Select(strs => new { Name = strs[0], Value = strs[1] })
                .SingleOrDefault(kv => kv.Name == name);

            if (kv != null)
            {
                return kv.Value;
            }
            else
            {
                return null;
            }
        }
    }
}