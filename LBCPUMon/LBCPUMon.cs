using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using Centivus.Services;
using Microsoft.Win32;
using System.Configuration;


namespace LBCPUMon
{
    public partial class LBCPUMon : ServiceDebugHelper
    {
        public const string Name = "LBCPUMon";
        public const string RegKeyName = @"Software\LoadBalancer.org\LBCPUMon";

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        // worker thread
        Thread m_WorkerThread;
        object _lockObject = new object();
        SocketLib _socket;

        public LBCPUMon()
        {
            // This call is required by the Windows.Forms Component Designer.
            InitializeComponent();
        }

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ServiceName = Name;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Set things in motion so your service can do its work.
        /// </summary>
        protected override void OnStart(string[] args)
        {
            StartThread();
        }

        public void StartThread()
        {

            // create worker thread instance
            m_WorkerThread = new Thread(new ThreadStart(this.WorkerThreadFunction));
            m_WorkerThread.Start();
        }

        /// <summary>
        /// Stop this service.
        /// </summary>
        protected override void OnStop()
        {
            lock (_lockObject)
            {
                if (_socket != null)
                {
                    _socket.Stop();
                    _socket = null;
                }
            }
            ServiceDebugger.WriteLogEntry(Name, "Stopping.");
        }

        //private void StopThread()
        //{
        //    m_WorkerThread.Abort();
        //}

        private void WorkerThreadFunction()
        {
            try
            {
                lock (_lockObject)
                {
                    bool returnIdle = true;
                    ////bool.TryParse(ConfigurationManager.AppSettings["ReturnIdle"], out returnIdle);

                    Mode mode = Mode.Normal;
                    string modeStr = ConfigurationManager.AppSettings["Mode"];
                    mode = string.Equals(Mode.Drain.ToString(), modeStr, StringComparison.OrdinalIgnoreCase) ? Mode.Drain :
                        (string.Equals(Mode.Halt.ToString(), modeStr, StringComparison.OrdinalIgnoreCase) || string.Equals("down", modeStr, StringComparison.OrdinalIgnoreCase) ? Mode.Halt : Mode.Normal);

                    _socket = new SocketLib()
                    {
                        Alive = new Action<SocketLib>(socketLib =>
                            {
                                if (ReadRegKeyInt(RegKeyName, "AliveLogging", 0) > 0)
                                {
                                    //Write to event log IF should
                                    ServiceDebugger.WriteLogEntry(Name, String.Format("Listening on port {0}", socketLib.Port));
                                }
                            }),
                        Error = new Action<Exception>(exception =>
                            {
                                if (ReadRegKeyInt(RegKeyName, "ExceptionLogging", 0) > 0)
                                {
                                    ServiceDebugger.WriteLogEntry(Name, exception.ToString());
                                }
                            }),
                        ReturnIdle = returnIdle,
                        Mode = mode
                    };
                }
                _socket.Start();
            }
            catch (Exception ex)
            {
                ServiceDebugger.WriteLogEntry(Name, ex.ToString());
            }
            finally
            {
                //Tell OS we've stopped
                Stop();
            }
        }

        private static int ReadRegKeyInt(string keyName, string valueName, int defaultValue)
        {
            //Read registry safely
            RegistryKey key = Registry.LocalMachine.OpenSubKey(keyName);
            if (key != null)
            {
                object keyValue = key.GetValue(valueName, defaultValue);
                int parsedValue = defaultValue;
                if (int.TryParse(keyValue.ToString(), out parsedValue))
                    return parsedValue;
            }
            return defaultValue;
        }

    }
}
