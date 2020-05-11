using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace SelfService.Lib
{
    public class Logger
    {
		private static ILog m_FlowLog = null;
		private static ILog m_ExceptionLog = null;
		private static bool m_IsInitialized = false;
		private static object m_Lock = new object();

		private static void InitializeLog()
		{
			lock (m_Lock)
			{
				if (!m_IsInitialized)
				{
					m_FlowLog = log4net.LogManager.GetLogger("FlowLog");
					m_ExceptionLog = log4net.LogManager.GetLogger("ExceptionLog");

					m_IsInitialized = true;
				}
			}
		}

		public static ILog FlowLog
		{
			get
			{
				if (!m_IsInitialized)
				{
					InitializeLog();
				}
				return m_FlowLog;
			}
		}

		public static ILog ExceptionLog
		{
			get
			{
				if (!m_IsInitialized)
				{
					InitializeLog();
				}
				return m_ExceptionLog;
			}
		}

		public static void Flow(string message)
		{
			FlowLog?.Info(message);
		}

		public static void Error(string message)
		{
			FlowLog?.Error(message);
			ExceptionLog?.Error(message);
		}

		public static void FlowFormat(string message, params string[] arguments)
		{
			Flow(string.Format(message, arguments));
		}

		public static void ErrorFormat(string message, params string[] arguments)
		{
			Error(string.Format(message, arguments));
		}
	}
}
