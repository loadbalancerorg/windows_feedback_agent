using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceProcess;

namespace Centivus.Services
{
    public class ServiceDebugHelper : ServiceBase
    {
#if DEBUGSERVICE
        public void Start(string[] args)
        {
            OnStart(args);
        }

        public void Start()
        {
            OnStart(new string[0]);
        }

        public new void Stop()
        {
            OnStop();
            ServiceDebugger.ServiceStopped();
        }
#endif
    }
}
