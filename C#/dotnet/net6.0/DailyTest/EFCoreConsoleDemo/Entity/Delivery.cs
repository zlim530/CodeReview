namespace EFCoreConsoleDemo
{
    public class Delivery
    {
        public long Id { get; set; }

        public string CompanyName { get; set; }

        public string Number { get; set; }

        public Order Order { get; set; }

        /// <summary>
        /// 一对一关系：必须显式的在其中一个实体类中声明一个外键属性
        /// </summary>
        public long OrderId { get; set; }

    }
}