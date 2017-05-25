using MatrixRotator.Providers;

namespace MatrixRotator.Rotators
{
    public class Rotator180 : IRotator
    {
        public int[][] Rotate(int[][] matrix)
        {
            var matrixSize = matrix.Length;

            for (int row = 0; row < matrixSize / 2 + matrixSize%2; row++)
            {
                HandleRow(matrix, row);
            }

            return matrix;
        }

        public RotateValue RotateValue
        {
            get { return RotateValue.Rotate180; }
        }

        private void HandleRow(int[][] matrix, int row)
        {
            var matrixSize = matrix.Length;

            var isMiddleRow = row == matrixSize / 2;

            for (int column = 0; column < matrix.Length / (isMiddleRow ? 2 : 1); column++)
            {
                SwapCells(matrix, row, column);
            }
        }

        private void SwapCells(int[][] matrix, int row, int column)
        {
            var targetRow = matrix.Length - 1 - row;
            var targetCol = matrix.Length - 1 - column;
            var targetValue = matrix[targetRow][targetCol];

            matrix[targetRow][targetCol] = matrix[row][column];
            matrix[row][column] = targetValue;
        }
    }
}