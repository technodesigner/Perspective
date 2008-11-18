using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace Perspective.Core.Automation
{
    /// <summary>
    /// A helper class for UI Automation.
    /// </summary>
    public static class AutomationHelper
    {
        /// <summary>
        /// Start an UI Automation server.
        /// </summary>
        /// <param name="exePath">Full path of the executable to launch.</param>
        /// <returns>The root AutomationElement.</returns>
        public static AutomationElement StartServer(string exePath)
        {
            AutomationElement root = null;
            if (!File.Exists(exePath))
            {
                throw new FileNotFoundException("File not found", exePath);
            }
            else
            {
                Process process = Process.Start(exePath);
                Thread.Sleep(2000);
                if (process.MainWindowHandle != null)
                {
                    root = AutomationElement.FromHandle(process.MainWindowHandle);
                }
            }
            return root;
        }
    }
}
