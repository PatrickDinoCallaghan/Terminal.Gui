using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.Gui.Helpers;

public static class ProgressBarHelper
{
    private static int _barLength = 50;
    private static DateTime _startTime;
    private static long _lastTotal = -1;

    /// <summary>
    /// Adjust the character‐width of the progress bar.
    /// </summary>
    public static void SetBarLength (int length)
    {
        if (length > 0) _barLength = length;
    }

    /// <summary>
    /// Display a progress bar with ETA for int counts.
    /// </summary>
    public static void DisplayProgressBar (int currentIteration, int totalIterations, string title = "")
        => DisplayProgressBar ((long)currentIteration, (long)totalIterations, title);

    /// <summary>
    /// Display a progress bar with ETA for long counts.
    /// </summary>
    public static void DisplayProgressBar (long currentIteration, long totalIterations, string title = "")
    {
        // Reset on new run or first tick
        if (totalIterations != _lastTotal || currentIteration == 0)
        {
            _startTime = DateTime.UtcNow;
            _lastTotal = totalIterations;
        }

        // Clamp progress
        double fraction = totalIterations > 0
            ? (double)currentIteration / totalIterations
            : 0d;
        int filled = (int)(fraction * _barLength);
        if (filled < 0) filled = 0;
        if (filled > _barLength) filled = _barLength;

        string bar = new string ('#', filled)
                   + new string ('-', _barLength - filled);

        // Compute ETA
        TimeSpan eta = TimeSpan.Zero;
        if (currentIteration > 0 && totalIterations > 0)
        {
            var elapsed = DateTime.UtcNow - _startTime;
            double secsPerItem = elapsed.TotalSeconds / currentIteration;
            double remainingSecs = secsPerItem * (totalIterations - currentIteration);
            eta = TimeSpan.FromSeconds (Math.Max (0, remainingSecs));
        }

        string etaStr = FormatTimeSpan (eta);

        ConsoleHelper.Write (
            $"\r{title} [{bar}] {currentIteration}/{totalIterations}  ETA: {etaStr}"
        );
    }

    /// <summary>
    /// Render a TimeSpan as "Xd Xh Xm Xs", omitting any zero‐value units except seconds.
    /// </summary>
    private static string FormatTimeSpan (TimeSpan ts)
    {
        var parts = new List<string> ();
        if (ts.Days > 0) parts.Add ($"{ts.Days}d");
        if (ts.Hours > 0) parts.Add ($"{ts.Hours}h");
        if (ts.Minutes > 0) parts.Add ($"{ts.Minutes}m");
        parts.Add ($"{ts.Seconds}s");
        return string.Join (" ", parts);
    }
} 