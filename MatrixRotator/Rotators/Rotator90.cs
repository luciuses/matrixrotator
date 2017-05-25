using MatrixRotator.Providers;

namespace MatrixRotator.Rotators
{
    public class Rotator90 : IRotator
    {
        public int[][] Rotate(int[][] matrix)
        {
            var matrixSize = matrix.Length;

            for (int row = 0; row < matrixSize / 2; row++)
            {
                HandleRow(matrix, row);
            }

            return matrix;
        }

        public virtual RotateValue RotateValue
        {
            get { return RotateValue.Rotate90; }
        }

        private void HandleRow(int[][] matrix, int row)
        {
            for (int column = row; column < matrix.Length - 1 - row; column++)
            {
                SubstituateAroundMatrix(matrix, row, column);
            }
        }

        private void SubstituateAroundMatrix(int[][] matrix, int row, int column)
        {
            var cell = new Cell(row, column);
            cell.PickUpValue(matrix);

            SubstituateAroundMatrix(matrix, cell);
        }

        private void SubstituateAroundMatrix(int[][] matrix, Cell initialCell)
        {
            for (int step = 0; step < 4; step++)
            {
                initialCell = SubstituateCell(matrix, initialCell);
            }
        }

        private Cell SubstituateCell(int[][] matrix, Cell cell)
        {
            var targetCell = GetTargetCell(cell, matrix);

            matrix[targetCell.Row][targetCell.Column] = cell.Value;

            return targetCell;
        }

        protected virtual Cell GetTargetCell(Cell cell, int[][] matrix)
        {
            var targetCell = new Cell(cell.Column, matrix.Length - cell.Row - 1);
            
            targetCell.PickUpValue(matrix);

            return targetCell;
        }
    }
}