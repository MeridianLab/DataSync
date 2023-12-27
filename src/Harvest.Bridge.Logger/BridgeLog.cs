using log4net.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Harvest.Bridge.Logger
{

    public class BridgeLog
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static RichTextBox _rtf;

        public static void SetRTFReference(RichTextBox rtf)
        {
            _rtf = rtf;
        }

        public static void Debug(string message)
        {
            _log.Debug(message);
            WriteMessageToRTF(0, message);
        }

        public static void Info(string message)
        {
            _log.Info(message);
            WriteMessageToRTF(1, message);
        }

        public static void Warning(string message)
        {
            _log.Warn(message);
            WriteMessageToRTF(3, message);
        }

        public static void Error(string message)
        {
            _log.Error(message);
            WriteMessageToRTF(4, message);
        }

        public static void Error(string message, Exception ex)
        {
            _log.Error(message, ex);
            WriteMessageToRTF(4, message);
        }

        private static void WriteMessageToRTF(int level, string message)
        {
            if (_rtf != null)
            {
                if(message.Length > 200)
                {
                    message= message.Substring(0, 200) + "...";
                }
                message = "[" + DateTime.Now.ToString() + "] " + message + Environment.NewLine;
                _rtf.BeginInvoke((MethodInvoker)delegate
                {
                    if (level == 0)
                    {
                        return;
                        //_rtf.SelectionStart = _rtf.TextLength;
                        //_rtf.SelectionLength = 0;
                        //_rtf.SelectionColor = Color.RoyalBlue;
                        //_rtf.AppendText(message);
                        //_rtf.SelectionColor = _rtf.ForeColor;
                    }
                    else if (level == 1)
                    {
                        _rtf.SelectionStart = _rtf.TextLength;
                        _rtf.SelectionLength = 0;
                        _rtf.SelectionColor = Color.ForestGreen;
                        _rtf.AppendText(message);
                        _rtf.SelectionColor = _rtf.ForeColor;
                    }
                    else if (level == 2)
                    {
                        _rtf.SelectionStart = _rtf.TextLength;
                        _rtf.SelectionLength = 0;
                        _rtf.SelectionColor = Color.DarkOrange;
                        _rtf.AppendText(message);
                        _rtf.SelectionColor = _rtf.ForeColor;
                    }
                    else if (level == 3)
                    {
                        _rtf.SelectionStart = _rtf.TextLength;
                        _rtf.SelectionLength = 0;
                        _rtf.SelectionColor = Color.DarkRed;
                        _rtf.AppendText(message);
                        _rtf.SelectionColor = _rtf.ForeColor;
                    }
                    else if (level == 4)
                    {
                        _rtf.SelectionStart = _rtf.TextLength;
                        _rtf.SelectionLength = 0;
                        _rtf.SelectionColor = Color.Crimson;
                        _rtf.AppendText(message);
                        _rtf.SelectionColor = _rtf.ForeColor;
                    }
                    else
                    {
                        _rtf.AppendText(message);
                    }
                    _rtf.SelectionStart = _rtf.Text.Length;
                    // scroll it automatically
                    _rtf.ScrollToCaret();
                });
            }
        }
    }
}
