using MatrixRotator.Providers;
using MatrixRotator.Rotators;

namespace MatrixRotator
{
    public interface IApplication
    {
        void Run();
    }

    public class Application : IApplication
    {
        private readonly IMatrixHandler _matrixHandler;
        private readonly IRotatorProvider _rotatorProvider;

        public Application(IMatrixHandler matrixHandler, IRotatorProvider rotatorProvider)
        {
            _matrixHandler = matrixHandler;
            _rotatorProvider = rotatorProvider;
        }

        public void Run()
        {
            var matrix = _matrixHandler.GetMatrix();

            _matrixHandler.ShowMatrix(matrix);

            var result = _rotatorProvider.GetRotator().Rotate(matrix);

            _matrixHandler.ShowMatrix(result);

            _matrixHandler.SaveMatrix(result);
        }
    }
}