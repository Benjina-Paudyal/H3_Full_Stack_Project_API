using Xunit;
using Moq;
using MovieManagementSystem.API.Controllers;
using MovieManagementSystem.API.Repositories.Interfaces;
using MovieManagementSystem.API.Data.Domain;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MovieManagementSystem.API.DTO;
using NPOI.SS.Formula.Functions;

namespace MovieManagementSystem.Test.ControllersTest
{
    public class AwardControllerTest
    {
        private readonly AwardController _controller; // instance of award controller i.e controller under test

        /* mock object of the IawardRepo, used to simulate the behaviour of a real repo. Mock is created using the Mock<T> 
        class which is part of a mocking library (e.g.Moq) which allows to set up expected behaviours for method calls 
        on the mocked object during testing */
        private readonly Mock<IAwardRepo> _repo = new Mock<IAwardRepo>();

        public AwardControllerTest()
        {
            /* initializes the controller field by creating a new instance of the AwardController and passing the mocked 
             repository(repo.Object) to it which ensures that the controller uses the mocked repo during test*/
            // EXPOSES the MOCKED OBJECT INSTANCE
            _controller = new AwardController(_repo.Object);

        }

        [Fact]

        public async Task GetAll_ShouldReturn200_WhenDataExists()
        {
            // Arrange
            List<Award> awards = new List<Award>();
            awards.Add(new Award());
            awards.Add(new Award());

            _repo
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(awards);

            // Act
            var result = await _controller.GetAll();

            // Assert


            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okObjectResult.StatusCode);

            var awardsOutput = Assert.IsType<List<AwardOutputDto>>(okObjectResult.Value);
            Assert.Equal(awards.Count, awardsOutput.Count);
        }


        [Fact]
        public async Task GetAll_ShouldReturn204_WhenNoDataExists()
        {
            // Arrange
            List<Award> awards = new List<Award>();

            _repo
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(awards);

            // Act
            var result = await _controller.GetAll();

            // Assert

            var statusCodeResult = Assert.IsType<NoContentResult>(result.Result);
            Assert.Equal(204, statusCodeResult.StatusCode);

        }

        [Fact]
        public async Task GetAll_ShouldReturn204_WhenAwardIsNull()
        {
            // Arrange
            List<Award> awards = null;

            _repo
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(awards);

            //Act
            var result = await _controller.GetAll();

            // Assert
            var statusCodeResult = Assert.IsType<NoContentResult>(result.Result);
            Assert.Equal(204, statusCodeResult.StatusCode);

        }

        [Fact]
        public async Task Create_ShouldReturnStatus400_WhenGenreSubmittedIsNull()
        {
            // Arrange
            AwardInputDto nullAwardDto = null;

            // Act
            var result = await _controller.Create(nullAwardDto);

            // Assert
            var statusCodeResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, statusCodeResult.StatusCode);


        }

        //[Fact]
        //public async Task Create_ShouldReturnOk_WhenAwardCreatedSuccessfully()
        //{
        //    // Arrange
        //    var validAwardDto = new AwardInputDto { AwardName = "Valid Award", MovieId = 101 };

        //    _repo
        //        .Setup(s => s.CreateAsync(It.IsAny<Award>()))
        //                  .ReturnsAsync(new Award { AwardId = 1, AwardName = validAwardDto.AwardName, MovieId = validAwardDto.MovieId });

        //    // Act
        //    var result = await _controller.Create(validAwardDto);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    Assert.Equal(200, okResult.StatusCode);

        //    var createdDto = Assert.IsType<AwardOutputDto>(okResult.Value);
        //    Assert.Equal(1, createdDto.AwardId); 
        //    Assert.Equal(validAwardDto.AwardName, createdDto.AwardName);
        //    Assert.NotNull(createdDto.Movie);
        //    Assert.Equal(validAwardDto.MovieId, createdDto.Movie.MovieId);
        //}






    }



}



