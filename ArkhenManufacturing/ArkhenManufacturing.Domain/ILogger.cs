namespace ArkhenManufacturing.Domain
{
    /// <summary>
    /// Interface that provides the ability to log a string to a method of output
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs a message without a newline/carriage-return at the end of the string
        /// </summary>
        /// <param name="message">The message to log to output</param>
        void Log(string message);

        /// <summary>
        /// Logs a message with a newline/carriage-return at the end of the string
        /// </summary>
        /// <param name="message">The message to log to output</param>
        void LogLine(string message);
    }
}
