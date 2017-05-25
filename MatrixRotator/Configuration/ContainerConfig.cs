using System;
using System.IO;
using System.Reflection;
using Autofac;
using MatrixRotator.Providers;
using MatrixRotator.Rotators;

namespace MatrixRotator.Configuration
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            var assembly = Assembly.GetExecutingAssembly();

            builder
                .RegisterAssemblyTypes(assembly)
                .AssignableTo<IRotator>()
                .AsImplementedInterfaces();

            builder.RegisterType<Application>().AsImplementedInterfaces();
            builder.RegisterType<MatrixHandler>().AsImplementedInterfaces();
            builder.RegisterType<Options>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<RotatorProvider>().AsImplementedInterfaces();
            builder.Register(r => new Func<string, Stream>((s) => new FileStream(s, FileMode.Open, FileAccess.ReadWrite)))
                .As<Func<string, Stream>>();

            return builder.Build();
        }
    }
}