using System.Diagnostics;

namespace ArkhenManufacturing.Domain
{
    public class DebugLogger : ILogger
    {
        public void Log(string message) {
            Debug.Write(message);
        }

        public void LogLine(string message) {
            Debug.WriteLine(message);
        }
    }
}
