using System;
using UnitTests;
using Xunit;
using Moq;

namespace InterfaceExample.Tests
{
    public class DeskFanTests
    {
        [Fact]
        public void PowerLowerThanZero_OK()
        {
            var mock = new Mock<IPowerSupple>();
            // mock.Setup���Ǵ���һ��ʵ��  Returns��Ҫһ��lambda���ʽ
            mock.Setup(ps => ps.GetPower()).Returns(()=>0);
            var fan = new DeskFan(mock.Object);
            var expected = "Won't work.";
            var actual = fan.Work();
            Assert.Equal(expected,actual);
            //var fan = new DeskFan(new PowerSuppleLowerThanZero());
            //var expected = "Won't work.";
            //var actual = fan.Work();
            //Assert.Equal(expected,actual);

        }

        [Fact]
        public void PowerHigherThan200_Warning()
        {
            var mock = new Mock<IPowerSupple>();
            // mock.Setup���Ǵ���һ��ʵ��
            mock.Setup(ps => ps.GetPower()).Returns(() => 220);
            var fan = new DeskFan(mock.Object);
            var expected = "Warning!";
            var actual = fan.Work();
            Assert.Equal(expected,actual);

            //var fan = new DeskFan(new PowerSupplyHigherThan200());
            //var expected = "Warning!";
            //var actual = fan.Work();
            //Assert.Equal(expected,actual);
        }
    }

    //class PowerSuppleLowerThanZero: IPowerSupple
    //{
    //    public int GetPower()
    //    {
    //        return 0;
    //    }
    //}

    //class PowerSupplyHigherThan200 : IPowerSupple
    //{
    //    public int GetPower()
    //    {
    //        return 220;
    //    }
    //}
}
