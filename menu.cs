using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;

namespace WebScraper
{
    class menu
    {
        public static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to webscraper!");
            Thread.Sleep(1000);
            Console.WriteLine("To begin scraping please press any key.");
            Console.ReadKey();
            DirectorySelecter.ChoosePath();
            //directory and url selected done

            //starts scraping
            Scrapper.begin();
        }
    }
}
