using BHF.MS.test22.Controllers;
using BHF.MS.test22.Models;
using BHF.MS.test22.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace BHF.MS.test22.Tests.Controllers
{
    public class test22ControllerTests
    {
        private readonly Mock<ILogger<test22Controller>> _loggerMock = new();
        private readonly Mock<IExampleService> _exampleServiceMock = new();
        private readonly test22Controller _sut;

        public test22ControllerTests()
        {
            _sut = new test22Controller(_loggerMock.Object, _exampleServiceMock.Object);
        }

        [Fact]
        public async Task Get_LogsWarning()
        {
            // Arrange
            var message = new HttpResponseMessage();
            _exampleServiceMock.Setup(x => x.GetSomething()).ReturnsAsync(message);

            // Act
            var result = await _sut.Get();

            // Assert
            _loggerMock.VerifyLog(x => x.LogWarning("Responses {Response} are invalid!", message), Times.Once);
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task Post_LogsWarning()
        {
            // Arrange
            var message = new HttpResponseMessage();
            var model = new ExampleModel { Title = "title", Email = "abc@abc.com", Phone = "123123123" };
            _exampleServiceMock.Setup(x => x.PostSomething(model)).ReturnsAsync(message);

            // Act
            var result = await _sut.Post(model);

            // Assert
            _loggerMock.VerifyLog(x => x.LogWarning("Responses {Response} are invalid!", message), Times.Once);
            result.Should().BeOfType<OkResult>();
        }
    }
}

