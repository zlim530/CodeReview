using System.Threading.Tasks;
using LearningAbpDemo.Models.TokenAuth;
using LearningAbpDemo.Web.Controllers;
using Shouldly;
using Xunit;

namespace LearningAbpDemo.Web.Tests.Controllers
{
    public class HomeController_Tests: LearningAbpDemoWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}