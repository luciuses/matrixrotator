using System;
using System.IO;
using System.Text;
using MatrixRotator.Exceptions;
using MatrixRotator.Providers;
using MatrixRotator.Rotators;
using MatrixRotator.Tests.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MatrixRotator.Tests
{
    [TestClass]
    public abstract class RotatorBaseTests
    {
        private int[][] _matrixSource;
        
        public abstract IRotator Rotator { get; }
        public abstract int[][] ExpectedResult { get; }

        [TestInitialize()]
        public void RotatorBaseTestsInitialize()
        {
            _matrixSource = MatrixHelper.GetMatrix();
        }

        [TestMethod]
        public void Rotate_Success()
        {
            // Arrange
            
            // Act
            var result = Rotator.Rotate(_matrixSource);
            
            // Assert
            MatrixAssert.AreEqualValues(ExpectedResult, result);
        }
    }
}