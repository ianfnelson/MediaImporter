using SystemWrapper.IO;
using Autofac;

namespace MediaImporter.Framework
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigurationHelper>().As<IConfigurationHelper>().SingleInstance();
            builder.RegisterType<Notifier>().As<INotifier>().SingleInstance();
            builder.RegisterType<MediaProcessor>().As<IMediaProcessor>().SingleInstance();

            builder.RegisterType<DirectoryWrap>().As<IDirectoryWrap>().SingleInstance();
            builder.RegisterType<PathWrap>().As<IPathWrap>().SingleInstance();
            builder.RegisterType<FileWrap>().As<IFileWrap>().SingleInstance();

            base.Load(builder);
        }
    }
}
