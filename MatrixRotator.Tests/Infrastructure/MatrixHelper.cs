using System;
using System.IO;
using System.Linq;
using System.Text;

namespace MatrixRotator.Tests.Infrastructure
{
    public static class MatrixHelper
    {
        public static int[][] GetMatrix()
        {
return ConvertFromText(
@"1,2,3,4,5
6,7,8,9,10
11,12,13,14,15
16,17,18,19,20
21,22,23,24,25"
);
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

        public static int[][] ConvertFromText(string text)
        {
            return text.Replace("\r\n","|")
                .Split('|')
                .Select(
                        a => a.Split(',').Select(Int32.Parse).ToArray()
                    ).ToArray();
        }
    }
}