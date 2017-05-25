using System;
using MatrixRotator.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatrixRotator.Tests
{
    public static class MatrixAssert
    {
        public static void AreEqualValues(int[][] expected, int[][] actual)
        {
            Assert.AreEqual(expected.ToText(), actual.ToText());
        }

        public static void Throws<T>(ErrorCode expectedCode, Action func) where T : MatrixRotatorException
        {
            var exceptionThrown = false;
            try
            {
                func.Invoke();
            }
            catch (T ex)
            {
                if (expectedCode.Equals(ex.ErrorCode))
                {
                    exceptionThrown = true;
                }
                else
                {
                    throw new AssertFailedException(
                        String.Format("An exception of type {0} with code {1} was expected, but thrown with code {2}", typeof(T), expectedCode, ex.ErrorCode)
                   );
                }
            }

            if (!exceptionThrown)
            {
                throw new AssertFailedException(
                        String.Format("An exception of type {0} with code {1} was expected, but not thrown", typeof(T), expectedCode)
                    );
            }
        }
    }
} 
