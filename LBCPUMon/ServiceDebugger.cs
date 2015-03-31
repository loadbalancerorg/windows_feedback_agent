using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Diagnostics;

namespace Centivus.Services
{
	public partial class ServiceDebugger : Form
	{
#if DEBUGSERVICE		
		public ServiceDebugger()
		{
			InitializeComponent();
			_form = this;		
		}

		public ServiceDebugHelper Service
		{
			get { return _service; }
			set
			{
				_service = value;
				if (_service.CanHandlePowerEvent)
					powerToolStrip.Enabled = true;
				if (_service.CanHandleSessionChangeEvent)
					sessionChangeToolStrip.Enabled = true;
				if (_service.CanPauseAndContinue)
					pauseToolStrip.Enabled = true;
				if (_service.CanShutdown)
					shutdownToolStrip.Enabled = true;
				if (_service.CanStop)
					stopToolStrip.Enabled = true;
			}
		}

		private static ServiceDebugHelper _service;
		private static ServiceDebugger _form = null;

		private void startButton_Click(object sender, EventArgs e)
		{
			try
			{
				ServiceStarting();
				_service.Start();
				ServiceStarted();
			}
			catch(Exception ex)
			{
				AddEvent("Service Debugger", "Beware of forcing cluster failover:"+ex.Message, EventLogEntryType.Error);
			}
		}

		public static void ServiceStarted()
		{
			if (_service.CanStop)
				_form.stopButton.Enabled = true;
			_form.AddEvent("Service Debugger", _service.ServiceName + " started");
		}

		private void ServiceStarting()
		{
			startButton.Enabled = false;
			AddEvent("Service Debugger", _service.ServiceName + " starting");
		}

		private void stopButton_Click(object sender, EventArgs e)
		{
			ServiceStopping();
			_service.Stop();
			ServiceStopped();
		}

		public static void ServiceStopped()
		{
            try
            {
                _form.startButton.Enabled = true;
                _form.AddEvent("Service Debugger", _service.ServiceName + " stopped");
            }
            catch { }
		}

		private void ServiceStopping()
		{
			stopButton.Enabled = false;
			AddEvent("Service Debugger", _service.ServiceName + " stopping");
		}

		private void AddEvent(string source, string message)
		{
			AddEvent(source, message, EventLogEntryType.Information, 0, 0, new byte[0]);
		}
		private void AddEvent(string source, string message,EventLogEntryType errorType)
		{
			AddEvent(source, message, errorType, 0, 0, new byte[0]);
		}		

		private void AddEvent(string source, string message, EventLogEntryType type, int eventID, short category, byte[] rawData)
		{
			ListViewItem lvi = new ListViewItem(type.ToString(), type.ToString());
			lvi.SubItems.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			lvi.SubItems.Add(message);		
			lvi.SubItems.Add(source);
			lvi.SubItems.Add(category.ToString());
			lvi.SubItems.Add(eventID.ToString());
			lvi.SubItems.Add(rawData.Length.ToString());
			lvi.ToolTipText = message;
			if (_eventLogListView.InvokeRequired)
				_eventLogListView.Invoke(new AddListItemDelegate(AddEventToList), lvi);
			else
				AddEventToList(lvi);
		}

		private delegate void AddListItemDelegate(ListViewItem lvi);
		private void AddEventToList(ListViewItem lvi)
		{
			_eventLogListView.Items.Insert(0, lvi);
			Application.DoEvents();
		}
			
#endif

		public static void WriteLogEntry(string source, string message)
		{
			EventLog.WriteEntry(source, message);
#if DEBUGSERVICE
			_form.AddEvent(source, message, EventLogEntryType.Information, 0, 0, new byte[0]);
#endif
		}
		public static void WriteLogEntry(string source, string message, EventLogEntryType type)
		{
			EventLog.WriteEntry(source, message, type);
#if DEBUGSERVICE
			_form.AddEvent(source, message, type, 0, 0, new byte[0]);
#endif
		}
		public static void WriteLogEntry(string source, string message, EventLogEntryType type, int eventID)
		{
			EventLog.WriteEntry(source, message, type, eventID);
#if DEBUGSERVICE
			_form.AddEvent(source, message, type, eventID, 0, new byte[0]);
#endif
		}
		public static void WriteLogEntry(string source, string message, EventLogEntryType type, int eventID, short category)
		{
			EventLog.WriteEntry(source, message, type, eventID, category);
#if DEBUGSERVICE
			_form.AddEvent(source, message, type, eventID, category, new byte[0]);
#endif
		}
		public static void WriteLogEntry(string source, string message, EventLogEntryType type, int eventID, short category, byte[] rawdata)
		{
			EventLog.WriteEntry(source, message, type, eventID, category, rawdata);
#if DEBUGSERVICE
			_form.AddEvent(source, message, type, eventID, category, rawdata);
#endif
		}


	}
}