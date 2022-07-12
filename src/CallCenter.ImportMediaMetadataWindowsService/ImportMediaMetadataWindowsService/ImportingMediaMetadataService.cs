using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Security.Permissions;
using System.IO;

namespace ImportMediaMetadataWindowsService
{
    public partial class ImportingMediaMetadataService : ServiceBase
    {        
        private string _monitorFolder;        

        public ImportingMediaMetadataService()
        {
            InitializeComponent();
            _monitorFolder = global::ImportMediaMetadataWindowsService.Properties.Settings.Default.monitoringFolder;

            eventLog = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("ImportingMediaMetadataServiceSource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "ImportingMediaMetadataServiceSource", "ImportingMediaMetadataServiceLog");
            }

            eventLog.Source = "ImportingMediaMetadataServiceSource";
            eventLog.Log = "ImportingMediaMetadataServiceLog";            
        }

        protected override void OnStart(string[] args)
        {
            eventLog.WriteEntry("In OnStart");
            
            MonitorFile();      
        }

        protected override void OnStop()    
        {
            eventLog.WriteEntry("In OnStop");                        
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void MonitorFile()
        {
            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();         
            watcher.Path = _monitorFolder;

            /* Watch for changes in LastAccess and LastWrite times, and
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            // Only watch text files.
            watcher.Filter = "*.txt";

            // Add event handlers.            
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);

            // Begin watching.
            watcher.EnableRaisingEvents = true;                        
        }

        // Define the event handlers.
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            //Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);            

            if (e.ChangeType == WatcherChangeTypes.Changed && e.FullPath.IndexOf("export_log") >= 0)
            {
                eventLog.WriteEntry("Importing file : " + e.FullPath);

                // read the lines from the text file
                string[] allLines = File.ReadAllLines(e.FullPath);

                MediaMetaDataContext MediaMetaDataContext = new MediaMetaDataContext();
                
                // start counting from index = 1 --> skipping the header (index=0)
                for (int index = 1; index < allLines.Length; index++)
                {                                                                                                    
                    string[] items = allLines[index].Split(new char[] { '\t' });

                    MediaMetaDataContext.usp_cc_insert_media_metadata(
                            items[0],
                            items[1],
                            items[2],
                            items[3],
                            items[4],
                            items[5],
                            items[6],
                            items[7],
                            Convert.ToDateTime(items[8] + " " + items[9]),                            
                            items[11],
                            items[12],
                            items[13],
                            items[14],
                            items[15],
                            items[16]);
                }                
            }
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            //Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);            
        }
    }
}
