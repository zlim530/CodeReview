namespace NetworkSecurity
{
    /// <summary>
    /// 侵染率
    /// </summary>
    public class InfectionRate
    {
        /// <summary>
        /// 中位侵染率
        /// </summary>
        public double Median { get; set; }

        /// <summary>
        /// 平均侵染率
        /// </summary>
        public double Average { get; set; }

        public override string ToString()
        {
            return $"Median:{Median},Average:{Average}";
        }
    }
}
