using System;

namespace UnitTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var fan = new DeskFan(new PowerSupple());
            Console.WriteLine(fan.Work());
        }
    }

    public interface IPowerSupple
    {
        int GetPower();
    }

    public class PowerSupple:IPowerSupple
    {
        public int GetPower()
        {
            return 220;
        }
    }

    public class DeskFan
    {
        private IPowerSupple _powerSupply;

        public DeskFan(IPowerSupple powerSupple)
        {
            _powerSupply = powerSupple;
        }

        public string Work()
        {
            int power = _powerSupply.GetPower();
            if ( power <= 0)
            {
                return "Won't work.";
            }
            else if ( power < 100)
            {
                return "Slow.";
            }
            else if ( power < 200)
            {
                return "Work fine.";
            }
            else
            {
                return "Warning!";
            }
        }
    }
}
