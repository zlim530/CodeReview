using System;
using System.Collections.Generic;
using Demo;
using Xunit;

namespace DemoTest {
    public class PatientShould {
        // Assert 方法应用
        // Assert.True,Assert.False
        [Fact]
        public void BeNewWhenCreated() {
            // Arrange
            var patient = new Patient();

            // Act
            var result = patient.IsNew;

            // Assert
            Assert.True(result);
        }

        // 字符串结果测试：Assert.Equal
        [Fact]
        public void HaveCorrectFullName() {
            var patient = new Patient {
                FirstName = "Nick",
                LastName = "Carter"
            };

            var fullName = patient.FullName;

            Assert.Equal("Nick Carter", fullName);
            Assert.StartsWith("Nick", fullName);
            Assert.EndsWith("Carter", fullName);
            Assert.Contains("Carter", fullName); // 包含
            Assert.Contains("Car", fullName);
            Assert.NotEqual("CAR", fullName); // 不相等
            Assert.Matches(@"^[A-Z][a-z]*\s[A-Z][a-z]*", fullName); // 正则表达式

        }

        // 数字结果测试
        [Fact]
        public void HaveDefaultBloodSugarWhenCreated() {
            var p = new Patient();
            var bloodSugar = p.BloodSugar;

            Assert.Equal(4.9f, bloodSugar);
            Assert.InRange(bloodSugar, 3.9, 6.1); // 判断是否在某一个范围内
        }

        // 判断null，not null
        [Fact]
        public void HaveNoNameWhenCreated() {
            var p = new Patient();
            Assert.Null(p.FirstName);
            Assert.NotNull(p);
        }

        // 集合测试
        [Fact]
        public void HaveHadAColdBefore() {
            var p = new Patient();

            var diseases = new List<string>() {
                "Cold",
                "Fever",
                "HIV",
                "2020-Co"
            };
            p.History.Add("Cold");
            p.History.Add("Fever");
            p.History.Add("HIV");
            p.History.Add("2020-Co");

            //判断集合是否含有或者不含有某个元素
            Assert.Contains("Cold", p.History);
            Assert.DoesNotContain("YIJU", p.History);

            //判断p.History至少有一个元素，该元素以水开头
            Assert.Contains(p.History, x => x.StartsWith("C"));
            //判断集合的长度
            Assert.All(p.History, x => Assert.True(x.Length >= 2));
            //判断集合是否相等,这里测试通过，说明是比较集合元素的值，而不是比较引用
            Assert.Equal(diseases, p.History);

        }

        // 测试对象
        [Fact]
        public void BeAPerson() {
            var p = new Patient();
            var p2= new Person();
            Assert.IsNotType<Person>(p);// //测试对象是否相等，注意这里返回值为 false
            Assert.IsType<Patient>(p);

            Assert.IsAssignableFrom<Person>(p);//判断对象是否继承自Person,true
            
            //判断是否为同一个实例
            Assert.NotSame(p,p2);
            //Assert.Same(p, p2);
        }

        // 判断是否发生异常
        [Fact]
        public void ThrowException() {
            var p = new Patient();
            //判断是否返回指定类型的异常
            var ex = Assert.Throws<InvalidOperationException>(() => { p.NotAllowed(); });
            //判断异常信息是否相等
            Assert.Equal("Not able to create.",ex.Message);
            
        }

        // 判断是否触发事件
        [Fact]
        public void RaiseSleepEvent() {
            var p = new Patient();
            Assert.Raises<EventArgs>(
                handler => p.PatientSlept += handler,
                handler => p.PatientSlept -= handler,
                () => p.Sleep());
        }

        // 判断属性改变是否触发事件
        [Fact]
        public void RaisePropertyChangedEvent() {
            var p = new Patient();
            // p 第一个参数是实现了 INotifyPropertyChanged 接口的对象，第二参数是属性名
            Assert.PropertyChanged(p,nameof(p.HeartBeatRate),()=> p.IncreaseHeartBeatRate());
        }

        
        
    }
    
}