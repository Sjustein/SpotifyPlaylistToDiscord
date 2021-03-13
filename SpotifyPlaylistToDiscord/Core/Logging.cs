using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyPlaylistToDiscord.Core
{
    /// <summary>
    ///     Class to quickly write log messages to the log output
    /// </summary>
    public static class Logging
    {
        /// <summary>
        ///     Log a message that will be printed in red to the console
        /// </summary>
        /// <param name="Source">The source that this log message is coming from</param>
        /// <param name="Message">The message that should be logged</param>
        /// <param name="Exception">Optional - extra exception details. This will only be printed if debugging is enabled</param>
        public static void LogError(string Source, string Message, string Exception = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (Storage.Debugging && Exception != null)
            {
                Console.WriteLine($"{Source} at {DateTime.Now}] {Message}");
                Console.WriteLine(Exception);
            }
            else
                Console.WriteLine($"{Source} at {DateTime.Now}] {Message}");
            Console.ResetColor();
        }

        /// <summary>
        ///     Log a message that will be printed in green to the console
        /// </summary>
        /// <param name="Source">The source that this log message is coming from</param>
        /// <param name="Message">The message that should be logged</param>
        /// <param name="Exception">Optional - extra exception details. This will only be printed if debugging is enabled</param>
        public static void LogSucces(string Source, string Message, string Exception = null)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (Storage.Debugging && Exception != null)
            {
                Console.WriteLine($"{Source} at {DateTime.Now}] {Message}");
                Console.WriteLine(Exception);
            }
            else
                Console.WriteLine($"{Source} at {DateTime.Now}] {Message}");
            Console.ResetColor();
        }

        /// <summary>
        ///     Log a message that will just be printed to the console
        /// </summary>
        /// <param name="Source">The source that this log message is coming from</param>
        /// <param name="Message">The message that should be logged</param>
        /// <param name="Exception">Optional - extra exception details. This will only be printed if debugging is enabled</param>
        public static void Log(string Source, string Message, string Exception = null)
        {
            if (Storage.Debugging && Exception != null)
            {
                Console.WriteLine($"{Source} at {DateTime.Now}] {Message}");
                Console.WriteLine(Exception);
            }
            else
                Console.WriteLine($"{Source} at {DateTime.Now}] {Message}");
        }


        /// <summary>
        ///     Log an error message in red in the console and exit the program with the specified exit code
        /// </summary>
        /// <param name="Source">The source that this log message is coming from</param>
        /// <param name="Message">The message that should be logged</param>
        /// <param name="Exception">Optional - extra exception details. This will only be printed if debugging is enabled</param>
        /// <param name="ExitCode">The exitcode to exit the program with</param>
        public static void LogErrorAndExit(string Source, string Message, string Exception, int ExitCode)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (Storage.Debugging && Exception != null)
            {
                Console.WriteLine($"{Source} at {DateTime.Now}] {Message}");
                Console.WriteLine(Exception);
            }
            else
                Console.WriteLine($"{Source} at {DateTime.Now}] {Message}");
            Console.ResetColor();

            Environment.Exit(ExitCode);
        }

        /// <summary>
        ///     Log anh error message is red in the console and exit the program with the specified exit code
        /// </summary>
        /// <param name="Source">The source that this log message is coming from</param>
        /// <param name="Message">The message that should be logged</param>
        /// <param name="ExitCode">The exitcode to exit the program with</param>
        public static void LogErrorAndExit(string Source, string Message, int ExitCode)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{Source} at {DateTime.Now}] {Message}");
            Console.ResetColor();

            Environment.Exit(ExitCode);
        }
    }
}
