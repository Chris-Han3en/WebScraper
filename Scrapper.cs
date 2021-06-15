using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Mime;
using System.Web;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

namespace WebScraper
{
    class Scrapper
    {
        public static void begin()
        {
            var t = new Thread(start);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            void start()
            {
                bool run = false;
                bool run2 = false;
                int pointless = 0;
                while (!DirectorySelecter.start)
                {
                    pointless++;
                }

                WebClient wcleint = new WebClient();

                if (!run)
                {
                    string data = string.Empty;
                    try
                    {
                        data = wcleint.DownloadString(DirectorySelecter.SelectedURL);
                    }
                    catch
                    {
                        MessageBox.Show("No such host is known. Please double check spelling and that you got the url correct.");
                        Console.WriteLine("No such host is known. Please double check spelling and that you got the url correct.\nThe program will now close\nPlease press any key to continue.");
                        Console.ReadKey();
                        string dllFileLocation = Assembly.GetEntryAssembly().Location;
                        string exeFileLocation = dllFileLocation.Replace("WebScraper.dll", "WebScraper.exe");
                        Process.Start(exeFileLocation);
                        Environment.Exit(1);
                    }

                    
                    try
                    {
                        StreamWriter writer = new StreamWriter(DirectorySelecter.folder + "/index.html");
                        writer.Write(data);
                    }
                    catch
                    {
                        MessageBox.Show($"There was a error trying to save to the directory '{DirectorySelecter.folder}'. Please run the program as administrator so it can have access to the folder.");
                    }
                    run = true;
                    //gets css file

                    WebBrowser browser1 = new WebBrowser();
                    browser1.ScriptErrorsSuppressed = true;
                    browser1.Navigate(DirectorySelecter.SelectedURL);
                    browser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(cssDownloader);
                    MessageBox.Show("Scraping css file");

                    void cssDownloader(object sender, WebBrowserDocumentCompletedEventArgs e)
                    {
                        HtmlElementCollection collection;

                        if (browser1 != null)
                        {
                            if (browser1.Document != null)
                            {
                                if (!run2)
                                {
                                    collection = browser1.Document.GetElementsByTagName("link");
                                    if (collection != null)
                                    {
                                        int i = 0;
                                        string[] href =
                                        {
                                            ""
                                        };

                                        foreach (HtmlElement thing in collection)
                                        {
                                            string test = collection[i].GetAttribute("rel");
                                            if (test == "stylesheet")
                                            {
                                                href[0] = collection[i].GetAttribute("href");
                                            }
                                            i++;
                                        }

                                        WebClient cssCleint = new WebClient();
                                        string cssCode = string.Empty;
                                        try
                                        {
                                            cssCode = cssCleint.DownloadString(DirectorySelecter.SelectedURL + "//" + href[0]);
                                        }
                                        catch
                                        {
                                            cssCode = cssCleint.DownloadString(href[0]);
                                            MessageBox.Show("The website you want to scrape uses external links to host their stylesheets. This means if you click the index.html the website should look the exact same however the external file has also been downloaded.");
                                        }

                                        try
                                        {
                                            StreamWriter cssWriter = new StreamWriter(DirectorySelecter.folder + "/main.css");
                                            cssWriter.Write(cssCode);
                                        }
                                        catch
                                        {
                                            MessageBox.Show($"Website {DirectorySelecter.SelectedURL} has been scraped.");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
