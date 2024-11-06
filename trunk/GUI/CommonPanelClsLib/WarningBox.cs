using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonPanelClsLib
{
    public static class WarningBox
    {
        public static int FormShow(string 标题,string 内容,string 大标题="警告")
        {
            try
            {
                int REF = -1;
                using (MessageBox0 myMessageBox1 = new MessageBox0())
                {
                    string hh = myMessageBox1.showMessage(标题, 内容, 大标题);
                    if (hh== "confirm")
                    {
                        REF = 1;
                    }
                    else
                    {
                        REF = 0;
                    }
                }
                return REF;
            }
            catch { return -1; }
            
        }
    }
}
