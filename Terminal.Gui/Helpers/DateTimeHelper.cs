using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.Gui.Helpers;
public static class DateTimeHelper
{

    public static Tuple<Nullable<DateTime>, Nullable<DateTime>> DateTimeRangeSelection (string Title = "DateTime Range")
    {
        Nullable<DateTime> Start = null;
        Nullable<DateTime> End = null;
        Application.Init ();
        var top = Application.Top;

        var win = new Window()
        {
            X = 0,
            Y = 1,
            Width = Dim.Fill (),
            Height = Dim.Fill (),
            Title = Title
        };
        top.Add (win);

        // FrameView 1
        var frameView1 = new FrameView ()
        {
            X = 0,
            Y = 0,
            Width = 25,
            Height = 7,
            Title = "Start Range"
        };

        var timePicker = new TimeField ()
        {
            X = 1, // Relative to GroupBox
            Y = 1,
            Width = 20
        };
        frameView1.Add (timePicker);

        var datePicker = new DateField (DateTime.Now)
        {
            X = 1,
            Y = 3,
            Width = 20
        };
        frameView1.Add (datePicker);

        var frameView2 = new FrameView ()
        {
            X = Pos.Right (frameView1) + 2,
            Y = 0,
            Width = 25,
            Height = 7,
            Title = "End Range"
        };
        var timePicker2 = new TimeField ()
        {
            X = 1, // Relative to GroupBox
            Y = 1,
            Width = 20
        };
        frameView2.Add (timePicker2);

        var datePicker2 = new DateField (DateTime.Now)
        {
            X = 1,
            Y = 3,
            Width = 20
        };
        frameView2.Add (datePicker2);

        win.Add (frameView1, frameView2);

        var okButton = new Button ()
        {
            X = Pos.Left (win),
            Y = 7, // Position below the date picker
            Title = "OK"
        };
        okButton.MouseClick += (object sender, MouseEventArgs e) =>
        {
            Start = new DateTime (
                datePicker.Date.Year,
                datePicker.Date.Month,
                datePicker.Date.Day,
                timePicker.Time.Hours,
                timePicker.Time.Minutes,
                timePicker.Time.Seconds
            );

            End = new DateTime (
                datePicker2.Date.Year,
                datePicker2.Date.Month,
                datePicker2.Date.Day,
                timePicker2.Time.Hours,
                timePicker2.Time.Minutes,
                timePicker2.Time.Seconds
            );

            if (Start >= End)
            {
                MessageBox.Query (50, 7, "Range Selection Error",
                    "The start date should be earlier than the end date. Please adjust your selection and try again.",
                    "Ok");
            }
            else
            {
                Application.RequestStop ();
            }
        };



        win.Add (okButton);

        timePicker2.Time = TimeSpan.Zero;
        timePicker.Time = TimeSpan.Zero;

        Application.Run ();
        Application.Shutdown ();
        Console.Clear ();

        return Tuple.Create (Start, End);

    }

}

