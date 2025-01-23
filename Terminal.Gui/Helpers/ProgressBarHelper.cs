using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.Gui.Helpers;

public static class ProgressBarHelper
{
    private static int _barLength = 50;

    public static void SetBarLength (int length)
    {
        _barLength = length;
    }

    public static void DisplayProgressBar (int currentIteration, int totalIterations, string title = "")
    {
        int progress = (int)((double)currentIteration / totalIterations * _barLength);
        ConsoleHelper.Write ($"\r{title} [{new string ('#', progress)}{new string ('-', _barLength - progress)}] {currentIteration}/{totalIterations}");
    }

    public static void DisplayProgressBar (long currentIteration, long totalIterations, string title = "")
    {
        int progress = (int)((double)currentIteration / totalIterations * _barLength);
        ConsoleHelper.Write ($"\r{title} [{new string ('#', progress)}{new string ('-', _barLength - progress)}] {currentIteration}/{totalIterations}");
    }
}
