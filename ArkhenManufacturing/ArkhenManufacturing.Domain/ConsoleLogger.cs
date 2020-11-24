﻿using System;

namespace ArkhenManufacturing.Domain
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message) {
            Console.Write(message);
        }

        public void LogLine(string message) {
            Console.WriteLine(message);
        }
    }
}
