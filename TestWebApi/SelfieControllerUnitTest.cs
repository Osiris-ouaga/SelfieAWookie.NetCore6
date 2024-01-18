using Microsoft.AspNetCore.Mvc;
using Moq;
using SelfieAWookie.NetCore6.Application.DTOs;
using SelfieAWookie.NetCore6.Controllers;
using SelfieAWookies.Core.Selfies.Domain;
using Selfies.AWookies.Core.Framework;

namespace TestWebApi
{
    public class SelfieControllerUnitTest
    {
        [Fact]
        public void ShouldAddOneSelfie()
        {
            //ARRANGE
            SelfieDto selfie = new SelfieDto();
            var repositoryMock = new Mock<ISelfieRepository>();
            var unit = new Mock<IUnitOfWork>();

            repositoryMock.Setup(item => item.UnitOfWork).Returns(new Mock<IUnitOfWork>().Object);
            repositoryMock.Setup(item => item.AddOne(It.IsAny<Selfie>())).Returns(new Selfie() { Id= 4 });

            //ACT
           var controller = new SelfieController(repositoryMock.Object);
            var result =controller.AddOne(selfie);

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var addedSelfie = (result as OkObjectResult).Value as SelfieDto;
            Assert.NotNull(addedSelfie);

            Assert.True(addedSelfie.Id >0);
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

            repositoryMock.Setup(item => item.GetAll(It.IsAny<int>())).Returns(expectedList);

            var controller = new SelfieController(repositoryMock.Object);

            //Act
            var result = controller.GetAll(null);

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