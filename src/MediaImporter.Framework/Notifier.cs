using System;

namespace MediaImporter.Framework
{
    public class Notifier : INotifier
    {
        public void Notify(string message)
        {
            Console.WriteLine(message);
        }
    }
}