using System;
using System.IO;

namespace ArkhenManufacturing.Domain
{
    public class FileLogger : ILogger
    {
        private readonly string _filepath;

        public FileLogger(string filepath) {
            _filepath = Environment.ExpandEnvironmentVariables(filepath);
        }

        public void Log(string message) {
            using var writer = new StreamWriter(_filepath);
            writer.Write(message);
        }

        public void LogLine(string message) {
            using var writer = new StreamWriter(_filepath);
            writer.WriteLine(message);
        }
    }
}
