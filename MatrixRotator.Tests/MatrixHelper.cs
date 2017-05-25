using System;
using System.IO;
using System.Linq;
using System.Text;

namespace MatrixRotator.Tests
{
    public static class MatrixHelper
    {
        public static int[][] GetMatrix(int size)
        {
            var matrix = new int[size][];
            for (var row = 0; row < size; row++)
            {
                matrix[row] = new int[size];
                for (var column = 0; column < size; column++)
                {
                    matrix[row][column] = row + size * column;
                }
            }

            return matrix;
        }

        public static string ToText(this int[][] matrix)
        {
            return String.Join("\r\n", matrix.Select(m => String.Join(",", m)));
        }

        public static MemoryStream ToStream(this int[][] matrix)
        {
            return new MemoryStream(Encoding.ASCII.GetBytes(ToText(matrix)));
        }

        public static string ToText(this MemoryStream stream)
        {
            return new StreamReader(stream).ReadToEnd();
        }
    }
}