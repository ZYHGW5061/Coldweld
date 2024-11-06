using GlobalDataDefineClsLib;
using HardwareManagerClsLib;
using IOUtilityClsLib;
using PowerControllerManagerClsLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerControlGUI
{
    public partial class PowerControlPanel : UserControl
    {
        private PowerParam param = new PowerParam();
        private bool StarRead = false;
        private Thread ReadStaThd;
        private IPowerController _PowerController
        {
            get { return HardwareManager.Instance.PowerController; }
        }

        public PowerControlPanel()
        {
            InitializeComponent();

            ReadStaThd = new Thread(ReadSta);
        }

        private void button_Read_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    bool Value = IOUtilityHelper.Instance.IsEctecticComplete();
            //    if (Value)
            //    {
            //        textBox_IsCom.Text = "1";
            //    }
            //    else
            //    {
            //        textBox_IsCom.Text = "0";
            //    }

            //    Value = IOUtilityHelper.Instance.IsEctecticFault();
            //    if (Value)
            //    {
            //        textBox_ERRC.Text = "1";
            //    }
            //    else
            //    {
            //        textBox_ERRC.Text = "0";
            //    }

            //    if (numericUpDown_GP.Value > -1 && numericUpDown_GP.Value < 21)
            //    {
            //        _PowerController.Write(PowerAdd.GP, (short)numericUpDown_GP.Value);
            //    }
            //    else
            //    {
            //        return;
            //    }

            //    param = _PowerController.ReadAll();

            //    if(param != null)
            //    {
            //        numericUpDown_T.Value = param.T0;
            //        numericUpDown_T1.Value = param.T1;
            //        numericUpDown_T2.Value = param.T2;
            //        numericUpDown_T3.Value = param.T3;
            //        numericUpDown_T4.Value = param.T4;
            //        numericUpDown_T5.Value = param.T5;
            //        numericUpDown_time1.Value = param.t1;
            //        numericUpDown_time2.Value = param.t2;
            //        numericUpDown_time3.Value = param.t3;
            //        numericUpDown_time4.Value = param.t4;
            //        numericUpDown_time5.Value = param.t5;
            //        numericUpDown_time6.Value = param.t6;
            //        textBox_TMC.Text = param.TMC.ToString();
            //        textBox_TM1.Text = param.TM1.ToString();
            //        textBox_TM2.Text = param.TM2.ToString();
            //        textBox_TM3.Text = param.TM3.ToString();
            //        textBox_TM4.Text = param.TM4.ToString();
            //        textBox_CNTL.Text = param.CNTL.ToString();
            //        textBox_ERRC.Text = param.ERRC.ToString();

            //        numericUpDown_G1.Value = param.G1;
            //        numericUpDown_G2.Value = param.G2;
            //        numericUpDown_G3.Value = param.G3;
            //        numericUpDown_G4.Value = param.G4;
            //    }

            //}
            //catch
            //{

            //}
        }

        private void button_Write_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    //if (numericUpDown_GP.Value > -1 && numericUpDown_GP.Value < 21)
            //    //{
            //    //    _PowerController.Write(PowerAdd.GP, (short)numericUpDown_GP.Value);
            //    //}
            //    //else
            //    //{
            //    //    return;
            //    //}

            //    if(numericUpDown_T.Value > -1 && numericUpDown_T.Value < 999)
            //    {
            //        param.T0 = (short)numericUpDown_T.Value;
            //    }
            //    if (numericUpDown_T1.Value > -1 && numericUpDown_T1.Value < 999)
            //    {
            //        param.T1 = (short)numericUpDown_T1.Value;
            //    }
            //    if (numericUpDown_T2.Value > -1 && numericUpDown_T2.Value < 999)
            //    {
            //        param.T2 = (short)numericUpDown_T2.Value;
            //    }
            //    if (numericUpDown_T3.Value > -1 && numericUpDown_T3.Value < 999)
            //    {
            //        param.T3 = (short)numericUpDown_T3.Value;
            //    }
            //    if (numericUpDown_T4.Value > -1 && numericUpDown_T4.Value < 999)
            //    {
            //        param.T4 = (short)numericUpDown_T4.Value;
            //    }
            //    if (numericUpDown_T5.Value > -1 && numericUpDown_T5.Value < 999)
            //    {
            //        param.T5 = (short)numericUpDown_T5.Value;
            //    }

            //    if (numericUpDown_time1.Value > -1 && numericUpDown_time1.Value < 999)
            //    {
            //        param.t1 = (short)numericUpDown_time1.Value;
            //    }
            //    if (numericUpDown_time2.Value > -1 && numericUpDown_time2.Value < 999)
            //    {
            //        param.t2 = (short)numericUpDown_time2.Value;
            //    }
            //    if (numericUpDown_time3.Value > -1 && numericUpDown_time3.Value < 999)
            //    {
            //        param.t3 = (short)numericUpDown_time3.Value;
            //    }
            //    if (numericUpDown_time4.Value > -1 && numericUpDown_time4.Value < 999)
            //    {
            //        param.t4 = (short)numericUpDown_time4.Value;
            //    }
            //    if (numericUpDown_time5.Value > -1 && numericUpDown_time5.Value < 999)
            //    {
            //        param.t5 = (short)numericUpDown_time5.Value;
            //    }
            //    if (numericUpDown_time6.Value > -1 && numericUpDown_time6.Value < 999)
            //    {
            //        param.t6 = (short)numericUpDown_time6.Value;
            //    }

            //    if (numericUpDown_G1.Value > -1 && numericUpDown_G1.Value < 999)
            //    {
            //        param.G1 = (short)numericUpDown_G1.Value;
            //    }
            //    if (numericUpDown_G2.Value > -1 && numericUpDown_G2.Value < 999)
            //    {
            //        param.G2 = (short)numericUpDown_G2.Value;
            //    }
            //    if (numericUpDown_G3.Value > -1 && numericUpDown_G3.Value < 999)
            //    {
            //        param.G3 = (short)numericUpDown_G3.Value;
            //    }
            //    if (numericUpDown_G4.Value > -1 && numericUpDown_G4.Value < 999)
            //    {
            //        param.G4 = (short)numericUpDown_G4.Value;
            //    }

            //    if (param != null)
            //    {
            //        //_PowerController.Write(PowerAdd.T0, param.T0);
            //        Thread.Sleep(1000);

            //        _PowerController.WriteAll(param);
            //    }

            //}
            //catch
            //{

            //}
        }

        private void button_Run_Click(object sender, EventArgs e)
        {
            try
            {
                //IOUtilityHelper.Instance.OpenEctectic();
                //_PowerController.Write(PowerAdd.IOUT, 0);
            }
            catch
            {

            }
        }

        private void button_Stop_Click(object sender, EventArgs e)
        {
            try
            {
                //IOUtilityHelper.Instance.CloseEctectic();
                //_PowerController.Write(PowerAdd.IOUT, 1);
                //IOUtilityHelper.Instance.ResetEctecticComplete();
            }
            catch
            {

            }
        }

        private void checkBox_Read_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if(checkBox_Read.Checked)
            //    {
            //        if (ReadStaThd != null && !ReadStaThd.IsAlive)
            //        {
            //            StarRead = false;
            //            ReadStaThd.Abort();
            //        }

            //        ReadStaThd = new Thread(ReadSta);
            //        StarRead = true;
            //        ReadStaThd.Start();
            //    }
            //    else
            //    {
            //        if (ReadStaThd != null && !ReadStaThd.IsAlive)
            //        {
            //            StarRead = false;
            //            ReadStaThd.Abort();
            //        }
            //    }

                
            //}
            //catch
            //{

            //}
        }

        private void ReadSta()
        {
            try
            {

                //while(StarRead)
                //{
                //    bool Value = IOUtilityHelper.Instance.IsEctecticComplete();
                //    if (Value)
                //    {
                //        textBox_IsCom.Text = "1";
                //    }
                //    else
                //    {
                //        textBox_IsCom.Text = "0";
                //    }

                //    Value = IOUtilityHelper.Instance.IsEctecticFault();
                //    if (Value)
                //    {
                //        textBox_ERRC.Text = "1";
                //    }
                //    else
                //    {
                //        textBox_ERRC.Text = "0";
                //    }

                //    param = _PowerController.ReadAll();

                //    if (param != null)
                //    {
                //        textBox_TMC.Text = param.TMC.ToString();
                //        textBox_TM1.Text = param.TM1.ToString();
                //        textBox_TM2.Text = param.TM2.ToString();
                //        textBox_TM3.Text = param.TM3.ToString();
                //        textBox_TM4.Text = param.TM4.ToString();
                //        textBox_CNTL.Text = param.CNTL.ToString();
                //        textBox_ERRC.Text = param.ERRC.ToString();


                        

                //    }

                //    Thread.Sleep(1000);
                //}
            }
            catch
            {

            }
        }

        private void button_Reset_Click(object sender, EventArgs e)
        {
            try
            {
                //IOUtilityHelper.Instance.CloseEctectic();
                //Thread.Sleep(100);
                //IOUtilityHelper.Instance.ResetEctectic();
                //_PowerController.Write(PowerAdd.IOUT, 0);
            }
            catch
            {

            }
        }
    }
}
