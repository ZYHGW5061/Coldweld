using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainGUI.UserControls.Utils
{
    public partial class LogWin : UserControl
    {
        public LogWin()
        {
            InitializeComponent();
        }

        public void Info(string log)
        {
            // 移动到RichTextBox的末尾
            rtbLog.AppendText(Environment.NewLine);
            // 将光标置于末尾
            rtbLog.SelectionStart = rtbLog.Text.Length;
            rtbLog.SelectionLength = 0;

            SetTextColor("[INFO ]:" + log, Color.Black);
            //SetTextColor(log, Color.Black);
        }

        public void Warn(string log)
        {
            // 移动到RichTextBox的末尾
            rtbLog.AppendText(Environment.NewLine);
            // 将光标置于末尾
            rtbLog.SelectionStart = rtbLog.Text.Length;
            rtbLog.SelectionLength = 0;

            SetTextColor("[WARN ]:" + log, Color.Blue);
            //SetTextColor(log, Color.Blue);
        }

        public void Error(string log)
        {
            // 移动到RichTextBox的末尾
            rtbLog.AppendText(Environment.NewLine);
            // 将光标置于末尾
            rtbLog.SelectionStart = rtbLog.Text.Length;
            rtbLog.SelectionLength = 0;

            SetTextColor("[ERROR]:" + log, Color.Red);
            //SetTextColor(log, Color.Red);
        }

        public void Clear()
        {
            rtbLog.Clear();
        }

        private void SetTextColor(string text, Color color)
        {            
            // 设置选中文本的颜色
            rtbLog.SelectionColor = color;
            // 添加文本
            rtbLog.AppendText(text);
            // 将光标置于添加的文本之后
            rtbLog.SelectionStart = rtbLog.Text.Length;
            rtbLog.SelectionLength = 0;
        }

        private void rtbLog_TextChanged(object sender, EventArgs e)
        {
            rtbLog.ScrollToCaret();
        }
    }
}
