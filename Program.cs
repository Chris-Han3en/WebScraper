using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Mime;
using System.Net;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;

namespace WebScraper
{
    class Program
    {
        internal static class NativeMethods
        {
            [DllImport("kernel32.dll")]
            internal static extern Boolean AllocConsole();
        }

        static void Main(string[] args)
        {
            NativeMethods.AllocConsole();
            Console.Title = "Web Scraper";
            menu.MainMenu();
        }
    }
}
