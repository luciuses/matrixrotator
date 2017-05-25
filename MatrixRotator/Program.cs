using System;
using Autofac;
using MatrixRotator.Configuration;
using MatrixRotator.Exceptions;
using MatrixRotator.Helpers;
using MatrixRotator.Providers;

namespace MatrixRotator
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var options = scope.Resolve<IOptions>();
                options.Parse(args);

                while (string.IsNullOrEmpty(options.InputFile))
                {
                    Console.WriteLine("Please enter path to source csv file:");
                    options.InputFile = Console.ReadLine();
                }

                var app = scope.Resolve<IApplication>();
                try
                {
                    app.Run();
                    Console.WriteLine("Rotating to {0} degrees is completed, the result is saved to the file - {1}", (int)options.Rotate, options.OutputFile);
                }
                catch (MatrixRotatorException ex)
                {
                    Console.WriteLine(ex.ErrorCode.GetDescription());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
