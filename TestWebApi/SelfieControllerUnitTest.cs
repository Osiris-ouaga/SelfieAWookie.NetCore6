using Microsoft.AspNetCore.Mvc;
using Moq;
using SelfieAWookie.NetCore6.Application.DTOs;
using SelfieAWookie.NetCore6.Controllers;
using SelfieAWookies.Core.Selfies.Domain;

namespace TestWebApi
{
    public class SelfieControllerUnitTest
    {
        [Fact]
        public void ShouldAddOneSelfie()
        {
            //ARRANGE
            Selfie selfie = new Selfie();
            var repositoryMock = new Mock<ISelfieRepository>();

            //ACT
            var controller = new SelfieController(repositoryMock.Object);
            var result =controller.AddOne(selfie);

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var addedSelfie = (result as OkObjectResult).Value as SelfieDto;
            Assert.NotNull(addedSelfie);
        }

        [Fact]
        public void ShouldReturnListOfSelfies()
        {
            //Arrange
            var expectedList = new List<Selfie>()
            {
                new Selfie(){Wookie = new Wookie()},
                new Selfie() {Wookie = new Wookie()},
            };

            var repositoryMock = new Mock<ISelfieRepository>();

            repositoryMock.Setup(item => item.GetAll()).Returns(expectedList);

            var controller = new SelfieController(repositoryMock.Object);

            //Act
            var result = controller.TestAMoi();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            OkObjectResult? okObjectResult = result as OkObjectResult;

            Assert.IsType<List<SelfieResumeDto>>(okObjectResult.Value);
            Assert.NotNull(okObjectResult.Value); 

            List<SelfieResumeDto> list = okObjectResult.Value as List<SelfieResumeDto>;
            Assert.True(list.Count == expectedList.Count);
        }
    }
}