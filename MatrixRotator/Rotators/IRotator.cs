using MatrixRotator.Providers;

namespace MatrixRotator.Rotators
{
    public interface IRotator
    {
        int[][] Rotate(int[][] matrix);

        RotateValue RotateValue { get; }
    }
}