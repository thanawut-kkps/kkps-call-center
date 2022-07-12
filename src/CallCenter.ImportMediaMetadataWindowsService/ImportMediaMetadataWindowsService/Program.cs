using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Runtime.Remoting.Contexts;
using System.IO;


namespace ImportMediaMetadataWindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            //string[] allLines = File.ReadAllLines(@"C:\SNWorking\Auto Exporting WAV Daily_export_log_3_25_2016_10_6_25.txt");
            //MediaMetaDataContext MediaMetaDataContext = new MediaMetaDataContext();
            //// start counting from index = 1 --> skipping the header (index=0)
            //for (int index = 1; index < allLines.Length; index++)
            //{
            //    string[] items = allLines[index].Split(new char[] { '\t' });

            //    MediaMetaDataContext.usp_cc_insert_media_metadata(
            //            items[0],
            //            items[1],
            //            items[2],
            //            items[3],
            //            items[4],
            //            items[5],
            //            items[6],
            //            items[7],
            //            Convert.ToDateTime(items[8] + " " + items[9]),
            //            items[11],
            //            items[12],
            //            items[13],
            //            items[14],
            //            items[15],
            //            items[16]);
            //}                

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                //new ImportingMediaMetadataService(connectionString, monitorFolder) 
                new ImportingMediaMetadataService()
            };

            ServiceBase.Run(ServicesToRun);                         
        }
    }
}
