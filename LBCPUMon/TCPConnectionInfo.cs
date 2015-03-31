using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;

namespace LBCPUMon
{
    class TCPConnectionInfo
    {
        public static int GetNumberOfLocalEstablishedConnectionsByPort(string IPAddress, int Port)
        {
            int Result = 0;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] tcpConnections = ipProperties.GetActiveTcpConnections();

            foreach (TcpConnectionInformation tcpInfo in tcpConnections)
            {
                    if (tcpInfo.State == TcpState.Established) 
                    {
                        if (tcpInfo.LocalEndPoint.Port == Port) 
                        {
                            if (IPAddress.Equals("*") || IPAddress.Equals(tcpInfo.LocalEndPoint.Address.ToString()))
                            {
                                Result++;
                            }
                        }
                    }
            }

            return Result;
        }
    }
}
