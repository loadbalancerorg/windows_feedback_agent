    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Interop;
using System.ServiceProcess;
using System.IO;


namespace Monitor
{
    public partial class MainWindow : Window
    {

        #region Globals

        public const string serviceName = "LBCPUMon";
        public ServiceController service = null;
        public TimeSpan timeout = TimeSpan.FromMilliseconds(15000);

        public enum Mode
        {
            Normal = 0,
            Down = 1,
            Drain = 2
        }

        private class TCPService
        {
            public string serviceName = "HTTP";
            public string serviceIPAddress = "*";
            public int servicePort = 80;
            public int serviceMaxConnections = 0;
            public float serviceImportance = 0;
        }

        private int interval = 10;

        private float cpuImportance = 1.0f;
        private int cpuThresholdValue = 100;
        private float ramImportance = 0;
        private int ramThresholdValue = 100;
        private List<TCPService> myTCPServices = new List<TCPService>();

        private bool readAgentStatusFromConfig = false;
        private int readAgentStatusFromConfigInterval = 5;
        private string agentStatus = "Normal";

        #endregion

        #region Starting Routines

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            HwndTarget hwndTarget = hwndSource.CompositionTarget;
            hwndTarget.RenderMode = RenderMode.SoftwareOnly;

            string configFilePath = this.GetConfigurationFilePath();
            InitConfiguration(configFilePath);

            sb_Exceptions.Foreground = Brushes.Red;
            sb_Status.Foreground = Brushes.Green;

            try
            {
                service = new ServiceController(serviceName);
            }
            catch (Exception ex)
            {
                txt_Exceptions.Text = ex.Message;
            }

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            this.cbo_Mode.Items.Add(Mode.Normal.ToString());
            this.cbo_Mode.Items.Add(Mode.Down.ToString());
            this.cbo_Mode.Items.Add(Mode.Drain.ToString());

            switch (agentStatus.ToLower())
            {
                case "normal":
                    cbo_Mode.SelectedIndex = (int)Mode.Normal;
                    break;
                case "halt":
                    cbo_Mode.SelectedIndex = (int)Mode.Down;
                    break;
                case "down":
                    cbo_Mode.SelectedIndex = (int)Mode.Down;
                    break;
                case "drain":
                    cbo_Mode.SelectedIndex = (int)Mode.Drain;
                    break;
                default:
                    this.cbo_Mode.SelectedIndex = 0;
                    break;
            }

            dispatcherTimer_Tick(sender, new EventArgs());

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (service != null)
            {
                switch (service.Status)
                {
                    case ServiceControllerStatus.Running:
                        txt_Status.Text = "Running";
                        btn_Start.IsEnabled = false;
                        btn_Stop.IsEnabled = true;
                        btn_Restart.IsEnabled = true;
                        break;
                    case ServiceControllerStatus.Stopped:
                        txt_Status.Text = "Stopped";
                        btn_Start.IsEnabled = true;
                        btn_Stop.IsEnabled = false;
                        btn_Restart.IsEnabled = false;
                        break;
                    case ServiceControllerStatus.Paused:
                        txt_Status.Text = "Paused";
                        btn_Start.IsEnabled = true;
                        btn_Stop.IsEnabled = false;
                        btn_Restart.IsEnabled = false;
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region User Triggered Events

        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {

            txt_Exceptions.Text = "";
            txt_Status.Text = "Starting...";

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch (Exception ex)
            {
                txt_Exceptions.Text = ex.Message;
            }

            Mouse.OverrideCursor = null;

        }

        private void btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            txt_Exceptions.Text = "";
            txt_Status.Text = "Stoping...";

            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            }
            catch (Exception ex)
            {
                txt_Exceptions.Text = ex.Message;
            }

            Mouse.OverrideCursor = null;

        }

        private void btn_Restart_Click(object sender, RoutedEventArgs e)
        {
            btn_Stop_Click(sender, e);
            btn_Start_Click(sender, e);
        }

        private void btn_Configuration_Click(object sender, RoutedEventArgs e)
        {
            string companyPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "LoadBalancer.org");
            string productPath = System.IO.Path.Combine(companyPath, "LoadBalancer");
            string configFilePath = System.IO.Path.Combine(productPath, "config.xml");

            try
            {
                if (!System.IO.Directory.Exists(companyPath)) { System.IO.Directory.CreateDirectory(companyPath); }
                if (!System.IO.Directory.Exists(productPath)) { System.IO.Directory.CreateDirectory(productPath); }
            }
            catch { }

            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "notepad.exe";
            p.StartInfo.Arguments = "\"" + configFilePath + "\"";
            p.StartInfo.UseShellExecute = true;
            p.Start();
        }

        private void cbo_Mode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            switch (cbo_Mode.SelectedIndex)
            {
                case (int)Mode.Normal:
                    agentStatus = "Normal";
                    break;
                case (int)Mode.Down:
                    agentStatus = "Down";
                    break;
                case (int)Mode.Drain:
                    agentStatus = "Drain";
                    break;
                default:
                    break;
            }

            string configFilePath = this.GetConfigurationFilePath();
            Xmlconfig myXmlcfg = new Xmlconfig(configFilePath, false);
            myXmlcfg.Settings["AgentStatus"].Value = agentStatus;
            myXmlcfg.Save(configFilePath);
            myXmlcfg.Dispose();

        }

        #endregion

        #region Helper Functions

        public string GetConfigurationFilePath()
        {
            string companyPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "LoadBalancer.org");
            string productPath = System.IO.Path.Combine(companyPath, "LoadBalancer");
            return System.IO.Path.Combine(productPath, "config.xml");
        }

        #endregion

        #region Configuration

        private void InitConfiguration(string configFilePath)
        {
            try
            {

                // Get configuration file
                string companyPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "LoadBalancer.org");
                string productPath = Path.Combine(companyPath, "LoadBalancer");

                if (!System.IO.Directory.Exists(companyPath)) { System.IO.Directory.CreateDirectory(companyPath); }
                if (!System.IO.Directory.Exists(productPath)) { System.IO.Directory.CreateDirectory(productPath); }

                try
                {
                    ReadConfiguration(configFilePath);
                }
                catch
                {
                    WriteConfiguration(configFilePath);
                }
            }
            catch { }
        }

        private void WriteConfiguration(string configFilePath)
        {

            // Backup config file
            try
            {
                File.Move(configFilePath, configFilePath + ".bak");
            }
            catch { }

            Xmlconfig myXmlcfg = new Xmlconfig(configFilePath, true);

            myXmlcfg.Settings["Cpu"]["ImportanceFactor"].floatValue = cpuImportance;
            myXmlcfg.Settings["Cpu"]["ThresholdValue"].intValue = cpuThresholdValue;
            myXmlcfg.Settings["Ram"]["ImportanceFactor"].floatValue = ramImportance;
            myXmlcfg.Settings["Ram"]["ThresholdValue"].floatValue = ramThresholdValue;

            if (myTCPServices.Count == 0)
            {
                // Demo TCPService
                TCPService myTCPService = new TCPService();
                myXmlcfg.Settings["TCPService"]["Name"].Value = myTCPService.serviceName;
                myXmlcfg.Settings["TCPService"]["IPAddress"].Value = myTCPService.serviceIPAddress;
                myXmlcfg.Settings["TCPService"]["Port"].intValue = myTCPService.servicePort;
                myXmlcfg.Settings["TCPService"]["MaxConnections"].intValue = myTCPService.serviceMaxConnections;
                myXmlcfg.Settings["TCPService"]["ImportanceFactor"].floatValue = myTCPService.serviceImportance;
            }
            else
            {
                foreach (TCPService myTCPService in myTCPServices)
                {
                    myXmlcfg.Settings["TCPService"]["Name"].Value = myTCPService.serviceName;
                    myXmlcfg.Settings["TCPService"]["IPAddress"].Value = myTCPService.serviceIPAddress;
                    myXmlcfg.Settings["TCPService"]["Port"].intValue = myTCPService.servicePort;
                    myXmlcfg.Settings["TCPService"]["MaxConnections"].intValue = myTCPService.serviceMaxConnections;
                    myXmlcfg.Settings["TCPService"]["ImportanceFactor"].floatValue = myTCPService.serviceImportance;
                }
            }

            myXmlcfg.Settings["ReadAgentStatusFromConfig"].boolValue = readAgentStatusFromConfig;
            myXmlcfg.Settings["ReadAgentStatusFromConfigInterval"].intValue = readAgentStatusFromConfigInterval;
            myXmlcfg.Settings["AgentStatus"].Value = agentStatus;

            myXmlcfg.Settings["Interval"].intValue = interval;

            myXmlcfg.Save(configFilePath);
            myXmlcfg.Dispose();

        }

        private void ReadConfiguration(string configFilePath)
        {

            Xmlconfig myXmlcfg = new Xmlconfig(configFilePath, false);

            cpuImportance = myXmlcfg.Settings["Cpu"]["ImportanceFactor"].floatValue;
            cpuThresholdValue = myXmlcfg.Settings["Cpu"]["ThresholdValue"].intValue;
            ramImportance = myXmlcfg.Settings["Ram"]["ImportanceFactor"].floatValue;
            ramThresholdValue = myXmlcfg.Settings["Ram"]["ThresholdValue"].intValue;

            myTCPServices.Clear();

            if (myXmlcfg.Settings.GetNamedChildrenCount("TCPService") > 0)
            {
                foreach (ConfigSetting cs in myXmlcfg.Settings.GetNamedChildren("TCPService"))
                {
                    TCPService myTCPService = new TCPService();
                    myTCPService.serviceName = cs["Name"].Value;
                    myTCPService.serviceIPAddress = cs["IPAddress"].Value;
                    myTCPService.servicePort = cs["Port"].intValue;
                    myTCPService.serviceMaxConnections = cs["MaxConnections"].intValue;
                    myTCPService.serviceImportance = cs["ImportanceFactor"].floatValue;
                    myTCPServices.Add(myTCPService);
                }
            }

            readAgentStatusFromConfig = myXmlcfg.Settings["ReadAgentStatusFromConfig"].boolValue;
            readAgentStatusFromConfigInterval = myXmlcfg.Settings["ReadAgentStatusFromConfigInterval"].intValue;
            agentStatus = myXmlcfg.Settings["AgentStatus"].Value;

            if (myXmlcfg.Settings["Interval"].intValue.ToString().Trim() != "0")
            {
                interval = myXmlcfg.Settings["Interval"].intValue;
            }
            else
            {
                WriteConfiguration(configFilePath);
            }
            

            if (agentStatus.Length == 0)
            {
                readAgentStatusFromConfig = false;
                readAgentStatusFromConfigInterval = 5;
                agentStatus = "Normal";
                throw new Exception("Configuration not complete.");
            }

            //myXmlcfg.Dispose();
        }

        #endregion

    }
}