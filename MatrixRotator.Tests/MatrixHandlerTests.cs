using System;
using System.IO;
using System.Text;
using MatrixRotator.Exceptions;
using MatrixRotator.Providers;
using MatrixRotator.Tests.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MatrixRotator.Tests
{
    [TestClass]
    public class MatrixHandlerTests
    {
        private int[][] _matrixSource;
        private Mock<IOptions> _optionsMock;
        private string _inputFileName = "test.csv";
        private Mock<Func<string, Stream>> _streamProviderMock;
        private MatrixHandler _matrixHandler;
        private string _outputFileName = "result.csv";

        [TestInitialize()]
        public void MatrixHandlerTestsInitialize()
        {
            _matrixSource = MatrixHelper.GetMatrix();
            _optionsMock = new Mock<IOptions>();
            _optionsMock.SetupGet(o => o.InputFile).Returns(_inputFileName);
            _optionsMock.SetupGet(o => o.OutputFile).Returns(_outputFileName);
            _streamProviderMock = new Mock<Func<string, Stream>>();
            _streamProviderMock.Setup(s => s(_inputFileName)).Returns(_matrixSource.ToStream()).Verifiable();
            _matrixHandler = new MatrixHandler(_optionsMock.Object, _streamProviderMock.Object);
        }

        [TestMethod]
        public void GetMatrix_Success()
        {
            // Arrange
            
            // Act
            var result = _matrixHandler.GetMatrix();
            
            // Assert
            Assert.AreNotEqual(_matrixSource, result);
            MatrixAssert.AreEqualValues(_matrixSource, result);
            _optionsMock.VerifyGet(o => o.InputFile, Times.Once);
            _streamProviderMock.Verify(s => s(_inputFileName), Times.Once);
        }

        [TestMethod]
        public void GetMatrix_FileNotFound_Exception()
        {
            // Arrange
            _streamProviderMock.Setup(s => s(_inputFileName))
                .Throws<FileNotFoundException>();
            
            MatrixAssert.Throws<MatrixRotatorException>(ErrorCode.ReadFileError, () => _matrixHandler.GetMatrix());
        }

        [TestMethod]
        public void GetMatrix_Format_Exception()
        {
            // Arrange
            _streamProviderMock.Setup(s => s(_inputFileName))
                .Throws<FormatException>();

            MatrixAssert.Throws<MatrixRotatorException>(ErrorCode.ParseError, () => _matrixHandler.GetMatrix());
        }

        [TestMethod]
        public void GetMatrix_Overflow_Exception()
        {
            // Arrange
            _streamProviderMock.Setup(s => s(_inputFileName))
                .Throws<OverflowException>();

            MatrixAssert.Throws<MatrixRotatorException>(ErrorCode.ParseError, () => _matrixHandler.GetMatrix());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetMatrix_Exception()
        {
            // Arrange
            _streamProviderMock.Setup(s => s(_inputFileName))
                .Throws<Exception>();

            _matrixHandler.GetMatrix();
        }

        [TestMethod]
        public void ShowMatrix_Enabled()
        {
            // Arrange
            _optionsMock.SetupGet(o => o.Show).Returns(true).Verifiable();

            // Act
            var result = _matrixHandler.ShowMatrix(_matrixSource);

            // Assert
            Assert.IsTrue(result, "Matrix was not shown.");
            _optionsMock.VerifyGet(o => o.Show, Times.Once);
        }

        [TestMethod]
        public void ShowMatrix_Disabled()
        {
            // Arrange
            _optionsMock.SetupGet(o => o.Show).Returns(false).Verifiable();

            // Act
            var result = _matrixHandler.ShowMatrix(_matrixSource);

            // Assert
            Assert.IsFalse(result, "Matrix was shown.");
            _optionsMock.VerifyGet(o => o.Show, Times.Once);
        }

        [TestMethod]
        public void SaveMatrix_Success()
        {
            // Arrange
            var resultStream = new MemoryStream(Encoding.UTF8.GetBytes(_matrixSource.ToText()));
            resultStream.Position = 0;
            _streamProviderMock.Setup(s => s(_outputFileName)).Returns(resultStream).Verifiable();

            // Act
            _matrixHandler.SaveMatrix(_matrixSource);

            // Assert
            _streamProviderMock.Verify(s => s(_outputFileName), Times.Once);
        }

        [TestMethod]
        public void SaveMatrix_Exception()
        {
            // Arrange
            _streamProviderMock.Setup(s => s(_outputFileName)).Throws<Exception>();

            MatrixAssert.Throws<MatrixRotatorException>(ErrorCode.WriteFileError, () => _matrixHandler.SaveMatrix(_matrixSource));
        }
    }
}