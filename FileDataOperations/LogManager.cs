using System;

namespace FileDataOperations
{
    public class LogManager
    {
        public static void Output(string message)
        {
            Logger.GetLogger().Log(message);
        }
    }
}
