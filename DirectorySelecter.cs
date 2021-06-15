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
    class DirectorySelecter
    {
        public static string folder = string.Empty;
        public static string SelectedURL = string.Empty;
        public static bool start = false;

        public static void ChoosePath()
        {
            Console.Clear();
            Console.WriteLine("Please select a folder to which you want the files to be stored to.");
            Thread.Sleep(1000);
            var t = new Thread(FileExplorer);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            static void FileExplorer()
            {
                FolderBrowserDialog diag = new FolderBrowserDialog();

                if (diag.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        folder = diag.SelectedPath;  //selected folder path
                    }
                    catch
                    {
                        MessageBox.Show($"directory chosen for stored files cannot be found. Entered folder was {folder}.Please select the folder again or choose another folder\nPress any key to continue.");
                        Console.ReadKey();
                        FileExplorer();
                    }

                }
                Console.WriteLine("\n" + folder + " has been chosen for the stored files.\n");
                Thread.Sleep(1000);
                Console.WriteLine("Please enter the full url of the site you want to scrape. Example: https://example.com\n");
                SelectedURL = Console.ReadLine();
                bool error = false;

                if (!SelectedURL.Contains("http"))
                {
                    Console.Clear();
                    Console.WriteLine($"You did not enter http or https.\n{SelectedURL} is not a valid url; please try again.\nPress any key to continue.");
                    Console.ReadKey();
                    ChoosePath();
                    error = true;
                }

                if (!SelectedURL.Contains('.') && !error)
                {
                    Console.Clear();
                    Console.WriteLine($"You did not enter a '.' in the url.\n{SelectedURL} is not a valid url; please try again.\nPress any key to continue.");
                    Console.ReadKey();
                    ChoosePath();
                    error = true;
                }

                if (!error)
                {
                    Console.WriteLine("\n" + SelectedURL + " chosen for scraping\nPlease press any key to begin.");
                    Console.ReadKey();
                    Console.Clear();
                    start = true;
                }
            }
        }
    }
}
