using System.Collections.Generic;
using System.Linq;
using MatrixRotator.Rotators;

namespace MatrixRotator.Providers
{
    public interface IRotatorProvider
    {
        IRotator GetRotator();
    }

    public class RotatorProvider : IRotatorProvider
    {
        private readonly IOptions _options;
        private readonly IEnumerable<IRotator> _rotators;

        public RotatorProvider(IOptions options, IEnumerable<IRotator> rotators )
        {
            _options = options;
            _rotators = rotators;
        }

        public IRotator GetRotator()
        {
            return _rotators.First(r => r.RotateValue.Equals(_options.Rotate));
        }
    }
}