using MatrixRotator.Rotators;
using MatrixRotator.Tests.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatrixRotator.Tests
{
    [TestClass]
    public class Rotator180Tests : RotatorBaseTests
    {
        public override IRotator Rotator
        {
            get { return new Rotator180(); }
        }

        public override int[][] ExpectedResult
        {
            get
            {
                return MatrixHelper.ConvertFromText(
@"25,24,23,22,21
20,19,18,17,16
15,14,13,12,11
10,9,8,7,6
5,4,3,2,1"
                    );
            }

        }
    }
}