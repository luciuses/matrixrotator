namespace MatrixRotator.Rotators
{
    public struct Cell
    {
        public Cell(int row, int column)
            : this()
        {
            Row = row;
            Column = column;
        }

        public int Row { get; private set; }
        public int Column { get; private set; }
        public int Value { get; private set; }

        public void PickUpValue(int[][] matrix)
        {
            Value = matrix[Row][Column];
        }
    }
}