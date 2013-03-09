using System;
using Autofac;
using MediaImporter.Framework;

namespace MediaImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = BuildContainer();

            var mediaProcessor = container.Resolve<IMediaProcessor>();

            mediaProcessor.ImportFiles();

            Console.ReadLine();
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterAssemblyModules(typeof(IMediaProcessor).Assembly);

            return builder.Build();
        }
    }
}
