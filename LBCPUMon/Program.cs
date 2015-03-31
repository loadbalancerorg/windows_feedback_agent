using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using Centivus.Services;
using System.Diagnostics;



namespace LBCPUMon
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main()
		{
#if DEBUGSERVICE
			ServiceDebugger debug = new ServiceDebugger();
			debug.Service = new LBCPUMon();
			System.Windows.Forms.Application.Run(debug);
#else
			ServiceBase[] ServicesToRun;						

			// More than one user Service may run within the same process. To add
			// another service to this process, change the following line to
			// create a second service object. For example,
			//
			//   ServicesToRun = new ServiceBase[] {new Service1(), new MySecondUserService()};
			//
			ServicesToRun = new ServiceBase[] { new LBCPUMon() };
#if DEBUG
			// Debugger.Launch();
#endif
			ServiceBase.Run(ServicesToRun);
#endif

		}
	}
}