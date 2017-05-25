using MatrixRotator.Exceptions;
using MatrixRotator.Providers;
using MatrixRotator.Rotators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MatrixRotator.Tests
{
    [TestClass]
    public class ApplicationTests
    {
        private Mock<IRotatorProvider> _rotationProviderMock;
        private Mock<IMatrixHandler> _matrixHandlerMock;
        private Mock<IRotator> _rotatorMock;
        private int[][] _matrixSource;
        private int[][] _matrixResult;

        [TestInitialize()]
        public void ApplicationTestsInitialize()
        {
            _matrixSource = MatrixHelper.GetMatrix(5);
            _matrixResult = MatrixHelper.GetMatrix(5);

            _rotationProviderMock = new Mock<IRotatorProvider>();
            _rotatorMock = new Mock<IRotator>();
            _matrixHandlerMock = new Mock<IMatrixHandler>();

            _matrixHandlerMock.Setup(m => m.GetMatrix()).Returns(_matrixSource).Verifiable();
            _rotatorMock.Setup(r => r.Rotate(It.IsAny<int[][]>())).Returns(_matrixResult).Verifiable();
            _rotationProviderMock.Setup(r => r.GetRotator()).Returns(_rotatorMock.Object).Verifiable();
        }

        [TestMethod]
        public void Run_Success()
        {
            // Arrange
            var application = new Application(_matrixHandlerMock.Object, _rotationProviderMock.Object);

            // Act
            application.Run();
            
            // Assert
            _matrixHandlerMock.Verify(m => m.GetMatrix(), Times.Once);
            _rotationProviderMock.Verify(r => r.GetRotator(), Times.Once);
            _rotatorMock.Verify(r => r.Rotate(_matrixSource), Times.Once);
            _matrixHandlerMock.Verify(m => m.SaveMatrix(_matrixResult), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(MatrixRotatorException))]
        public void Run_Error()
        {
            // Arrange
            _matrixHandlerMock.Setup(m => m.GetMatrix()).Throws<MatrixRotatorException>();
            var application = new Application(_matrixHandlerMock.Object, _rotationProviderMock.Object);

            // Act
            application.Run();

            // Assert
            
        }
    }
}
