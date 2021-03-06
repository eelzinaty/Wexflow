﻿using System.ServiceProcess;
using Wexflow.Core;
using System.Configuration;
using System.ServiceModel;

namespace Wexflow.Clients.WindowsService
{
    public partial class WexflowWindowsService : ServiceBase
    {
        public static string SettingsFile = ConfigurationManager.AppSettings["WexflowSettingsFile"];
        public static WexflowEngine WexflowEngine = new WexflowEngine(SettingsFile);

        ServiceHost _serviceHost;
        
        public WexflowWindowsService()
        {
            InitializeComponent();
            ServiceName = "Wexflow";
            WexflowEngine.Run();
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            if (_serviceHost != null)
            {
                _serviceHost.Close();
            }

            // Create a ServiceHost for the WexflowService type and 
            // provide the base address.
            _serviceHost = new ServiceHost(typeof(WexflowService));
                
            // Open the ServiceHostBase to create listeners and start 
            // listening for messages.
            _serviceHost.Open();
        }

        protected override void OnStop()
        {
            if (_serviceHost != null)
            {
                _serviceHost.Close();
                _serviceHost = null;
            }
        }
    }
}
