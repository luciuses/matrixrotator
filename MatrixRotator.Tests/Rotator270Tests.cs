using MatrixRotator.Rotators;
using MatrixRotator.Tests.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatrixRotator.Tests
{
    [TestClass]
    public class Rotator270Tests : RotatorBaseTests
    {
        public override IRotator Rotator
        {
            get { return new Rotator270(); }
        }

        public override int[][] ExpectedResult
        {
            get
            {
                return MatrixHelper.ConvertFromText(
@"5,10,15,20,25
4,9,14,19,24
3,8,13,18,23
2,7,12,17,22
1,6,11,16,21"
                    );
            }

        }
    }
}