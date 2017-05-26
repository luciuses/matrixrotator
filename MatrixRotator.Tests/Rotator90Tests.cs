using MatrixRotator.Rotators;
using MatrixRotator.Tests.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatrixRotator.Tests
{
    [TestClass]
    public class Rotator90Tests : RotatorBaseTests
    {
        public override IRotator Rotator
        {
            get { return new Rotator90(); }
        }

        public override int[][] ExpectedResult
        {
            get
            {
                return MatrixHelper.ConvertFromText(
@"21,16,11,6,1
22,17,12,7,2
23,18,13,8,3
24,19,14,9,4
25,20,15,10,5"
                    );
            }

        }
    }
}