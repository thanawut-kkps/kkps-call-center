using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Configuration;


namespace ImportMediaMetadataWindowsService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string connectionString = appSettings["connectionString"] ?? "Not Found";
                string monitoringFolder = appSettings["monitoringFolder"] ?? "Not Found";

                string parameter = String.Format("{0}\" \"{1}", connectionString, monitoringFolder);
                Context.Parameters["assemblypath"] = "\"" + Context.Parameters["assemblypath"] + "\" \"" + parameter + "\"";
                base.OnBeforeInstall(savedState);            

            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }            
        }   
    }    
}
