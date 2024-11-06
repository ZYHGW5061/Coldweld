using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonPanelClsLib
{
    public static class WarningBox1
    {
        public static void FormShow(string title, string content, string header)
        {
            MessageBox1 myMessageBox1 = new MessageBox1();
            //myMessageBox1.OnButtonClicked += (result) =>
            //{
            //    int output = result == "confirm" ? 1 : 0;
            //    callback?.Invoke(output);
            //};
            myMessageBox1.showMessage(title, content, header);
        }
    }
}
