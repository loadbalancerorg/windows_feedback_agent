using System;
using System.Configuration.Install;
using System.Diagnostics;
using System.ComponentModel;

namespace LBCPUMon
{
    [RunInstaller(true)]
    public class LBCPUMonEventLogInstaller : Installer
    {
        private EventLogInstaller eventLogInstaller;

        public LBCPUMonEventLogInstaller()
        {
            // Create an instance of an EventLogInstaller.
            eventLogInstaller = new EventLogInstaller();

            // Set the source name of the event log.
            eventLogInstaller.Source = LBCPUMon.Name;

            // Set the event log that the source writes entries to.
            eventLogInstaller.Log = "Application";

            // Add myEventLogInstaller to the Installer collection.
            Installers.Add(eventLogInstaller);
        }
    }
}