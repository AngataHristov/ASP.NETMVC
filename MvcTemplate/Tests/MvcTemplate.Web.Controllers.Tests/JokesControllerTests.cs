namespace MvcTemplate.Web.Controllers.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using TestStack.FluentMVCTesting;

    using Data.Models;
    using Infrastructure.Mapping;
    using Moq;
    using Services.Data;
    using ViewModels.Home;

    [TestClass]
    public class JokesControllerTests
    {
        private Mock jokesServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(JokesController).Assembly);
            this.jokesServiceMock = new Mock<IJokesService>();
        }

        [TestMethod]
        public void ByIdShouldWorkCorrectly()
        {
            const string Content = "SomeContent";
            const string CategoryName = "Funny";

            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(JokesController).Assembly);

            var jokesServiceMock = new Mock<IJokesService>();
            jokesServiceMock
                .Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(new Joke()
                {
                    Content = Content,
                    Category = new JokeCategory()
                    {
                        Name = CategoryName
                    }
                });

            var controller = new JokesController(jokesServiceMock.Object);

            controller.WithCallTo(jc => jc.ById("adafadsfa"))
                .ShouldRenderView("ById")
                .WithModel<JokeViewModel>(viewModel =>
                    {
                        Assert.AreEqual(Content, viewModel.Content);
                    }
                )
                .AndNoModelErrors();
        }
    }
}
