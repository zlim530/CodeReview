using System;
using Demo;
using Xunit;

namespace DemoTest {
    public class ShouldAddEquals5 {
        [Fact]
        public void Test1() {
            // Arrange
            var sut = new Calculator();// sut = System Under Test
            
            // Act
            var result = sut.Add(3, 2);
            
            // Assert
            Assert.Equal(5,result);
        }
    }
}