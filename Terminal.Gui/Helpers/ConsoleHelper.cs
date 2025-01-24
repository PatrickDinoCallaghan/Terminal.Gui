using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.Gui.Helpers;

    public static class ConsoleHelper
    {
        public static void Exit ()
        {
            Console.WriteLine ("Press any key to exit.");
            Console.ReadKey ();
        }

        public static ConsoleColor BackgroundColor
        {
            get => Console.BackgroundColor;
            set => Console.BackgroundColor = value;
        }

        public static ConsoleColor ForegroundColor
        {
            get => Console.ForegroundColor;
            set => Console.ForegroundColor = value;
        }

        public static void WriteLine (string? value) => Console.WriteLine (value);
        public static void Write (string? value) => Console.Write (value);
        public static string? ReadLine () => Console.ReadLine ();

        private static bool _spinnerActive = false;

        public static void StartSpinner (string title = "")
        {
            _spinnerActive = true;
            var spinnerChars = new [] { '/', '-', '\\', '|' };
            int counter = 0;

            ConsoleHelper.Write (title + " ");

            new Thread (() =>
            {
                while (_spinnerActive)
                {
                    ConsoleHelper.Write (spinnerChars [counter++ % spinnerChars.Length].ToString());
                    Console.SetCursorPosition (Console.CursorLeft - 1, Console.CursorTop);
                    Thread.Sleep (100);
                }
            }).Start ();
        }

        public static void StopSpinner ()
        {
            _spinnerActive = false;
        }
    }


