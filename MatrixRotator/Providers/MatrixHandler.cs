using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MatrixRotator.Exceptions;

namespace MatrixRotator.Providers
{
    /// <summary>
    /// The MatrixProvider interface.
    /// </summary>
    public interface IMatrixHandler
    {
        int[][] GetMatrix();

        void SaveMatrix(int[][] matrix);

        bool ShowMatrix(int[][] matrix);
    }

    /// <summary>
    /// The provider for Matrix.
    /// </summary>
    public class MatrixHandler: IMatrixHandler
    {
        private readonly IOptions _options;
        private readonly Func<string, Stream> _streamProvider;

        public MatrixHandler(IOptions options, Func<string, Stream> streamProvider)
        {
            _options = options;
            _streamProvider = streamProvider;
        }

        /// <summary>
        /// Load matrix from csv file.
        /// </summary>
        /// <returns>Matrix as two-dimensional array</returns>
        public int[][] GetMatrix()
        {
            try
            {
                List<string> list = new List<string>();
                using (var fs = _streamProvider.Invoke(_options.InputFile))
                using (StreamReader streamReader = new StreamReader(fs))
                {
                    string str;
                    while ((str = streamReader.ReadLine()) != null)
                        list.Add(str);
                }
                return list.Select(
                        a => a.Split(',').Select(Int32.Parse).ToArray()
                    ).ToArray();;
            }
            catch (FileNotFoundException ex)
            {
                throw new MatrixRotatorException(ErrorCode.ReadFileError, ex);
            }
            catch (FormatException ex)
            {
                throw new MatrixRotatorException(ErrorCode.ParseError, ex);
            }
            catch (OverflowException ex)
            {
                throw new MatrixRotatorException(ErrorCode.ParseError, ex);
            }
        }

        public bool ShowMatrix(int[][] matrix)
        {
            if (!_options.Show)
            {
                return false;
            }

            Console.WriteLine();
            foreach (var row in matrix)
            {
                Console.WriteLine(String.Join("\t", row));
            }

            return true;
        }

        public void SaveMatrix(int[][] matrix)
        {
            try
            {
                using (var fs = _streamProvider.Invoke(_options.OutputFile))
                using (var writer = new StreamWriter(fs))
                {
                    for (var indexRow = 0; indexRow < matrix.Length; indexRow++)
                    {
                        SaveRow(matrix[indexRow], writer);
                        if(indexRow < matrix.Length - 1) writer.Write(writer.NewLine);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new MatrixRotatorException(ex.Message, ErrorCode.WriteFileError, ex);
            }
        }

        private static void SaveRow(int[] row, StreamWriter writer)
        {
            for(var indexColumn = 0; indexColumn < row.Length; indexColumn++)
            {
                writer.Write(row[indexColumn]);
                if (indexColumn < row.Length - 1) writer.Write(',');
            }
        }
    }
}