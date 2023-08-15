using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NetworkSecurity
{
    class Program
    {
        public static string FirewallSystemWeight;
        public static string NetworkIsolationSystemWeight;
        public static string MainSystemProtectionWeight;

        /// <summary>
        /// 获取不同安全防护措施系统的权重指数
        /// </summary>
        static void GetWeightValues()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var config = builder.Build();
            FirewallSystemWeight = config["FirewallSystem"];
            NetworkIsolationSystemWeight = config["NetworkIsolationSystem"];
            MainSystemProtectionWeight = config["MainSystemProtection"];
        }

        /// <summary>
        /// 获取安全防护措施系统中的各月侵染率
        /// </summary>
        /// <param name="type">安全防护措施代码</param>
        /// <param name="array">月份(数组索引下标)-每个月该类防护措施入侵事件拦截数量</param>
        /// <returns></returns>
        public static InfectionRate GetInfectionRates(SafetyProtectionEnum type, List<int> array)
        {
            if (array == null || !array.Any())
            {
                throw new ArgumentNullException("请输入过去每个月该类防护措施入侵事件拦截数量！");
            }
            GetWeightValues();
            var count = array.Count;
            double middle = count % 2 == 0 ? (array[(count / 2) - 1] + array[count / 2]) / 2 : array[count / 2];
            double sum = array.Sum();
            double avg = array.Average();

            var result = new InfectionRate();
            double median = (middle / sum);
            double average = (avg / sum);
            switch (type)
            {
                case SafetyProtectionEnum.FirewallSystem:
                    median *= double.Parse(FirewallSystemWeight);
                    average *= double.Parse(FirewallSystemWeight);
                    result.Median = Math.Round(median, 2);
                    result.Average = Math.Round(average, 2);
                    break;
                case SafetyProtectionEnum.NetworkIsolationSystem:
                    median *= double.Parse(NetworkIsolationSystemWeight);
                    average *= double.Parse(NetworkIsolationSystemWeight);
                    result.Median = Math.Round(median, 2);
                    result.Average = Math.Round(average, 2);
                    break;
                case SafetyProtectionEnum.MainSystemProtection:
                    median *= double.Parse(MainSystemProtectionWeight);
                    average *= double.Parse(MainSystemProtectionWeight);
                    result.Median = Math.Round(median, 2);
                    result.Average = Math.Round(average, 2);
                    break;
                default:
                    break;
            }

            return result;
        }

        static void Main01(string[] args)
        {
            //Console.WriteLine(NetworkIsolationSystemWeight);
            //var result = GetInfectionRates(SafetyProtectionEnum.FirewallSystem, new List<int>() { 10,20,30,40});
            //var result = GetInfectionRates(SafetyProtectionEnum.MainSystemProtection, new List<int>() { 10,13,10});
            var result = GetInfectionRates(SafetyProtectionEnum.MainSystemProtection, new List<int>() { 10,20,30,40,50,60,70,80,90,333});
            Console.WriteLine(result);
        }
    }
}
