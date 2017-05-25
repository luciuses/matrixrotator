using MatrixRotator.Providers;

namespace MatrixRotator.Rotators
{
    public class Rotator270 : Rotator90
    {
        public override RotateValue RotateValue
        {
            get { return RotateValue.Rotate270; }
        }

        protected  override Cell GetTargetCell(Cell cell, int[][] matrix)
        {
            var targetCell = new Cell(matrix.Length - cell.Column - 1, cell.Row);
            
            targetCell.PickUpValue(matrix);

            return targetCell;
        }
    }
}