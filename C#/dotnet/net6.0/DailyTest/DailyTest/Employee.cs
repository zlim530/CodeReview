namespace DailyTest
{
    public class Employee
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }
        public double Salary { get; set; }

        public override string ToString()
        {
            return $"Id={Id},Name={Name},Age={Age},Gender={Gender},Salary={Salary}";
        }
    }
}
