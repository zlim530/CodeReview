namespace ConfigService
{
	public interface IConfigProvider
	{
		/// <summary>
		/// 如果配置找不到，返回 null
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public string GetValue(string name);
	}
}