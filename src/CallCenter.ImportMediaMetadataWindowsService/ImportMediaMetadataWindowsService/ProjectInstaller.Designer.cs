namespace ImportMediaMetadataWindowsService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ImportingMediaMetadataServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.ImportingMediaMetadataServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // ImportingMediaMetadataServiceProcessInstaller
            // 
            this.ImportingMediaMetadataServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.ImportingMediaMetadataServiceProcessInstaller.Password = null;
            this.ImportingMediaMetadataServiceProcessInstaller.Username = null;
            // 
            // ImportingMediaMetadataServiceInstaller
            // 
            this.ImportingMediaMetadataServiceInstaller.Description = "Importing Media Metadata Service";
            this.ImportingMediaMetadataServiceInstaller.DisplayName = "Importing Media Metadata Service";
            this.ImportingMediaMetadataServiceInstaller.ServiceName = "ImportingMediaMetadataService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ImportingMediaMetadataServiceProcessInstaller,
            this.ImportingMediaMetadataServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ImportingMediaMetadataServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller ImportingMediaMetadataServiceInstaller;
    }
}