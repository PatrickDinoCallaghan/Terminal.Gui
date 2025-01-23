using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.Gui.Helpers;
public static class InputHelper
{
    public static bool AskYesNoQuestion (string question)
    {
        while (true)
        {
            ConsoleHelper.ForegroundColor = ConsoleColor.DarkRed;
            ConsoleHelper.BackgroundColor = ConsoleColor.Green;
            ConsoleHelper.WriteLine (question + " (yes/no):");
            ConsoleHelper.ResetColor ();

            string? response = ConsoleHelper.ReadLine ()?.Trim ().ToLower ();

            if (response == "yes" || response == "y")
                return true;
            if (response == "no" || response == "n")
                return false;

            ConsoleHelper.WriteLine ("Invalid input. Please answer with 'yes' or 'no'.");
        }
    }

    public static string AskForFolder (string question = "Please enter the path to the folder: ")
    {
        while (true)
        {
            ConsoleHelper.Write (question);
            string? folderPath = ConsoleHelper.ReadLine ();

            if (Directory.Exists (folderPath))
            {
                ConsoleHelper.WriteLine ("Valid directory detected.");
                return folderPath;
            }
            else
            {
                ConsoleHelper.WriteLine ("Directory does not exist. Please try again.");
            }
        }
    }

    public static string AskForFile (string question = "Please enter the path to the file: ")
    {
        while (true)
        {
            ConsoleHelper.Write (question);
            string? filePath = ConsoleHelper.ReadLine ();

            if (File.Exists (filePath))
            {
                ConsoleHelper.WriteLine ("Valid file detected.");
                return filePath;
            }
            else
            {
                ConsoleHelper.WriteLine ("File does not exist. Please try again.");
            }
        }
    }

    public static int SelectOptionFromList (List<string> options)
    {
        while (true)
        {
            ConsoleHelper.WriteLine ("Please select an option:");
            for (int i = 0; i < options.Count; i++)
            {
                ConsoleHelper.WriteLine ($"{i + 1}. {options [i]}");
            }

            ConsoleHelper.Write ("Enter the number of your choice: ");
            if (int.TryParse (ConsoleHelper.ReadLine (), out int choice) && choice > 0 && choice <= options.Count)
            {
                return choice - 1;
            }

            ConsoleHelper.WriteLine ("Invalid input, please try again.");
        }
    }
}
