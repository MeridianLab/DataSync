namespace Harvest.Bridge.WindowsService
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
            this.HarvestBridgeProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.HarvestBridgeServiceInsaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // HarvestBridgeProcessInstaller
            // 
            this.HarvestBridgeProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.HarvestBridgeProcessInstaller.Password = null;
            this.HarvestBridgeProcessInstaller.Username = null;
            // 
            // HarvestBridgeServiceInsaller
            // 
            this.HarvestBridgeServiceInsaller.Description = "HarvestBridgeService";
            this.HarvestBridgeServiceInsaller.DisplayName = "Harvest Bridge Data Sync Service";
            this.HarvestBridgeServiceInsaller.ServiceName = "HarvestBridgeService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.HarvestBridgeProcessInstaller,
            this.HarvestBridgeServiceInsaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller HarvestBridgeProcessInstaller;
        private System.ServiceProcess.ServiceInstaller HarvestBridgeServiceInsaller;
    }
}