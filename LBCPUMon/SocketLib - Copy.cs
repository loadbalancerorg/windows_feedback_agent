using System;
using System.Net.Sockets;
using System.Threading;
using System.Management;
using System.IO;
using System.Diagnostics;
using System.ServiceProcess;
using System.Net;
using Microsoft.Win32;
using Centivus.Services;
using System.Collections.Generic;

namespace LBCPUMon
{
    /// <summary>
    /// call from a new thread to avoid the blocking of Socket.AcceptSocket
    /// </summary>
    public class SocketLib
    {
        public SocketLib()
        {            
            aborted = false;
            Port = 3333;

            ReturnIdle = true;
            Mode = Mode.Normal;

            listen = new TcpListener(IPAddress.Any, Port);
            listen.Start();
        }

        private bool aborted { get; set; }
        public int Port { get; set; }

        public Action<SocketLib> Alive { get; set; }
        public Action<Exception> Error { get; set; }

        public bool ReturnIdle { get; set; }
        public Mode Mode { get; set; }

        private TcpListener listen;

        private class TCPService
        {
            public string serviceName = "HTTP";
            public string serviceIPAddress = "*";
            public int servicePort = 80;
            public int serviceMaxConnections = 0;
            public float serviceImportance = 0;
        }

        private float cpuImportance = 1.0f;
        private int cpuThresholdValue = 100;
        private float ramImportance = 0;
        private int ramThresholdValue = 100;
        private List<TCPService> myTCPServices = new List<TCPService>();

        private bool readAgentStatusFromConfig = false;
        private int readAgentStatusFromConfigInterval = 5;
        private string agentStatus = "Normal";

        string configFilePath = "";

        DateTime lastExecutionTime = DateTime.MinValue;

        public void Stop()
        {
            aborted = true;
        }

        public void Start()
        {

            // Get configuration file
            string companyPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "LoadBalancer.org");
            string productPath = Path.Combine(companyPath, "LoadBalancer");
            configFilePath = Path.Combine(productPath, "config.xml");

            try
            {
                if (!System.IO.Directory.Exists(companyPath)) { System.IO.Directory.CreateDirectory(companyPath); }
                if (!System.IO.Directory.Exists(productPath)) { System.IO.Directory.CreateDirectory(productPath); }
                try
                {
                    ReadConfiguration();
                }
                catch
                {
                    WriteConfiguration();
                }
            }
            catch { }

            while (!aborted)
            {
                if (listen.Pending())
                {
                    try
                    {
                        HandleConnection(listen.AcceptSocket());
                    }
                    catch { }
                }
                else
                {
                    Thread.Sleep(1000);
                }

            }
        }

        private void WriteConfiguration()
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

            myXmlcfg.Save(configFilePath);
            myXmlcfg.Dispose();

        }

        private void ReadConfiguration()
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

            
            if (agentStatus.Length == 0)
            {
                readAgentStatusFromConfig = false;
                readAgentStatusFromConfigInterval = 5;
                agentStatus = "Normal";
                throw new Exception("Configuration not complete.");
            }
            
            myXmlcfg.Dispose();
        }

        private void HandleConnection(Socket acceptedClient)
        {

            Socket socketForClient = acceptedClient;

            if (socketForClient != null && socketForClient.Connected)
            {

                NetworkStream networkStream = new NetworkStream(socketForClient);
                StreamWriter streamWriter = new System.IO.StreamWriter(networkStream);

                string response = GetResponseForMode(Mode);

                streamWriter.Write(response);
                streamWriter.Flush();

                streamWriter.Close();
                networkStream.Close();
                socketForClient.Close();

            }
        }

        private static double GetCPULoad()
        {
            //Find CPU Usage
            ObjectQuery objQuery = new ObjectQuery("select LoadPercentage from Win32_Processor");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(objQuery);
            ManagementObjectCollection cpus = searcher.Get();

            double loadTotal = 0;
            int cpuCount = 0;
            foreach (ManagementObject cpu in cpus)
            {
                loadTotal += double.Parse(cpu["LoadPercentage"].ToString());
                cpuCount++;
            }
            double avgLoadPerCPU = loadTotal / cpuCount;
            return avgLoadPerCPU;
        }

        private static double GetSessionsUtilized(string IPAddress, int servicePort, int maxNumberOfSessionsPerService)
        {
            double Result = 0.00;

            int NumberOfEstablishedConnections = TCPConnectionInfo.GetNumberOfLocalEstablishedConnectionsByPort(IPAddress, servicePort);

            if (NumberOfEstablishedConnections > 0 && maxNumberOfSessionsPerService > 0)
            {
                Result = (double)maxNumberOfSessionsPerService / (double)NumberOfEstablishedConnections * 100;
            }
            else
            {
                Result = 0.00;
            }

            return Result;
        }

        public string GetResponseForMode(Mode myMode)
        {
            if (readAgentStatusFromConfig)
            {
                if (DateTime.Compare(lastExecutionTime.AddSeconds(readAgentStatusFromConfigInterval), DateTime.Now) <= 0 || DateTime.Compare(lastExecutionTime, DateTime.MinValue) == 0)
                {
                    ReadConfiguration();
                    lastExecutionTime = DateTime.Now;
                    if (agentStatus.ToLower().Equals("normal")) { myMode = Mode.Normal; }
                    if (agentStatus.ToLower().Equals("down")) { myMode = Mode.Halt; }
                    if (agentStatus.ToLower().Equals("drain")) { myMode = Mode.Drain; }
                }
            }

            string Response = "";

            switch (myMode)
            {
                case Mode.Normal:
                    double cpuLoad = GetCPULoad();
                    double cpuFree = 100 - cpuLoad;
                    double ramOccupied = PerformanceInfo.GetPercentOccupiedMemory();
                    double ramFree = 100 - ramOccupied;

                    int divider = 0;

                    double utilization = 0.0;

                    // If any resource is important and utilized 100% then everything else is not important
                    if ((cpuLoad > cpuThresholdValue && cpuThresholdValue > 0) || (ramOccupied > ramThresholdValue && ramThresholdValue > 0))
                    {
                        Response = "0%\n"; break;
                    }
                    
                    utilization = utilization + cpuLoad * cpuImportance;
                    if (cpuImportance > 0) { divider++; }

                    utilization = utilization + ramOccupied * ramImportance;
                    if (ramImportance > 0) { divider++; }

                    foreach (TCPService myTCPService in myTCPServices)
                    {
                        double sessionOccupied = GetSessionsUtilized(myTCPService.serviceIPAddress, myTCPService.servicePort, myTCPService.serviceMaxConnections);
                        
                        utilization = utilization + sessionOccupied * myTCPService.serviceImportance;
                        if (myTCPService.serviceImportance > 0) { divider++; }

                        if (sessionOccupied > 99 && myTCPService.serviceImportance == 1)
                        {
                            Response = "0%\n"; break;
                        }
                    }

                    utilization = utilization / divider;

                    if (utilization < 0) { utilization = 0; }
                    if (utilization > 100) { utilization = 100; }

                    if (ReturnIdle)
                    {
                        Response = (100 - (int)utilization).ToString() + "%\n";
                    }
                    else
                    {
                        Response = ((int)utilization).ToString() + "%\n";
                    }
                    return Response;

                case Mode.Halt:
                    Response = "down\n";
                    break;
                case Mode.Drain:
                    Response = "drain\n";
                    break;
                default:
                    Response = string.Empty;
                    break;
            }

            return Response;
        }
    }
}