namespace ConfigService
{
	public interface IConfigReader
	{
        /// <summary>
        /// ��������Ҳ��������� null
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetValue(string name);
    }
}