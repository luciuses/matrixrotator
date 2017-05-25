using System.IO;
namespace MatrixRotator.Providers
{
    public class FileProvider : IFileProvider
    {
        public Stream GetFile(string fileName)
        {
            return new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
        }
    }

    public interface IFileProvider
    {
        Stream GetFile(string fileName);
    }
}
