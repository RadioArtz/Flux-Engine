using System;

namespace Flux
{
    public static class Debug
    {
        public static void Log(object obj)
        {
            Console.WriteLine("Log: " + obj);
        }
        public static void Log(object obj,ConsoleColor textFrontColor)
        {
            Console.ForegroundColor = textFrontColor;
            Console.WriteLine("Log: " + obj);
            Console.ResetColor();
        }
        public static void Log(object obj, ConsoleColor textFrontColor,ConsoleColor textBackColor = ConsoleColor.Black)
        {
            Console.ForegroundColor = textFrontColor;
            Console.BackgroundColor = textBackColor;
            Console.WriteLine("Log: " + obj);
            Console.ResetColor();
        }

        public static void LogEngine(object obj)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("EngineLog: " + obj);
            Console.ResetColor();
        }

        public static void LogError(object obj)
        {
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine("Error: " + obj);
            Console.ResetColor();
        }
    }
}