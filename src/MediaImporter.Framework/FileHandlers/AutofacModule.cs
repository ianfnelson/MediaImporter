using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace MediaImporter.Framework.FileHandlers
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IgnoreFileHandler>().As<IFileHandler>();
            builder.RegisterType<ImageFileHandler>().As<IFileHandler>();
            builder.RegisterType<UnknownFileHandler>().As<IFileHandler>();
            builder.RegisterType<VideoFileHandler>().As<IFileHandler>();

            base.Load(builder);
        }
    }
}
