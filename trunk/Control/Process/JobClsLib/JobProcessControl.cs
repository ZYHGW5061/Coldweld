using CameraControllerClsLib;
using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using GlobalToolClsLib;
using PositioningSystemClsLib;
using RecipeClsLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using thinger.cn.DataConvertHelper;
using VisionControlAppClsLib;
using VisionGUI;
using WestDragon.Framework.BaseLoggerClsLib;

namespace JobClsLib
{
    public class JobProcessControl
    {
        #region Private File

        private static readonly object _lockObj = new object();
        private static volatile JobProcessControl _instance = null;


        public static JobProcessControl Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new JobProcessControl();
                        }
                    }
                }
                return _instance;
            }
        }

        private CameraConfig _TrackCameraConfig
        {
            get { return CameraManager.Instance.GetCameraConfigByID(EnumCameraType.TrackCamera); }
        }


        private JobProcessControl()
        {

        }

        private TransportControl _transportControl = TransportControl.Instance;




        private Thread productionThread;
        private AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        private volatile bool isRunning = false;
        private volatile bool isPaused = false;
        private volatile bool isStopped = false;

        



        #endregion



        #region public file

        public TransportRecipe recipe;


        public double Oven1Vacuum { get; set; } = 0;
        public double Oven2Vacuum { get; set; } = 0;
        public double BoxVacuum { get; set; } = 0;
        public double VacuumD { get; set; } = 10;
        public double VacuumC { get; set; } = 0.0001;

        public int singleDelay { get; set; } = 500;
        public int Delay { get; set; } = 120000;

        #endregion


        #region private mothed

        private int FindNextState(List<string> list, int startIndex, int state)
        {
            for (int i = startIndex + 1; i < list.Count; i++)
            {
                string state0 = list[i];

                int variableCode = int.Parse(state0.Substring(0, 3));
                int methodCode = int.Parse(state0.Substring(3, 3));
                int stateCode = int.Parse(state0.Substring(6, 3));
                if (stateCode == state)
                {
                    return i;
                }
            }
            return -1;
        }

        private void StartMothed()
        {
            if (isRunning || (productionThread != null && productionThread.IsAlive))
            {
                Task.Factory.StartNew(new Action(() =>
                {
                    DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "生产流程已经在运行，无法重复启动.";
                }));
                
                Console.WriteLine("生产流程已经在运行，无法重复启动.");
                return;
            }
            isRunning = true;
            //isPaused = false;
            isStopped = false;
            productionThread = new Thread(RunMothed);
            productionThread.Start();
        }

        private void PauseOrStepMothed()
        {
            isPaused = false;
            autoResetEvent.Set();

            Thread.Sleep(100);

            isPaused = true;

        }

        private void ContinueMothed()
        {
            isPaused = false;
            autoResetEvent.Set();
        }

        private void StopMothed()
        {
            isStopped = true;
            isPaused = false;
            //autoResetEvent.Set();
            isRunning = false;


            productionThread?.Join();
        }
        private void WaitForNext()
        {
            //while (isPaused && !isStopped)
            //{
            //    autoResetEvent.WaitOne(); // 等待继续信号  
            //}

            while (isPaused)
            {
                autoResetEvent.WaitOne(); // 等待继续信号  
            }

            if (isStopped)
            {
                Task.Factory.StartNew(new Action(() =>
                {
                    DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "生产过程已停止.";
                }));
                //throw new ThreadInterruptedException("生产过程已停止");
            }
        }

        private List<string> ProcessStateList()
        {
            List<string> statelist = new List<string>();
            try
            {

                if (_transportControl.TransportRecipe != null)
                {
                    #region 20241101


                    #region 20241101

                    statelist.Add("000029000");

                    statelist.Add("000001000");

                    bool Bin = false;
                    bool Ain = false;
                    int iOverBox1num = 0;
                    int iOverBox2num = 0;

                    if (recipe.OverBox1Param.OverBoxMaterialBoxGetInNumber > 0)
                    {
                        statelist.Add("001027000");
                    }
                    if (recipe.OverBox2Param.OverBoxMaterialBoxGetInNumber > 0)
                    {
                        statelist.Add("002027000");
                    }

                    for (int num = 0; num <= Math.Max(recipe.OverBox1Param.OverBoxMaterialBoxGetInNumber, recipe.OverBox2Param.OverBoxMaterialBoxGetInNumber); num++)
                    {
                        iOverBox1num = num;
                        iOverBox2num = num;

                        if (iOverBox1num >= recipe.OverBox1Param.OverBoxMaterialBoxGetInNumber)
                        {
                            if (Bin)
                            {
                                //B的料盘出料

                                if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber < 1)
                                {

                                }
                                else if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 1)
                                {
                                    #region 料盘1从烘箱A出去

                                    //料盒进烘箱A

                                    statelist.Add("001003010");

                                    statelist.Add("007007004");
                                    statelist.Add("007006000");
                                    statelist.Add("003007000");


                                    statelist.Add("001004000");

                                    statelist.Add("003008000");
                                    //关烘箱门
                                    statelist.Add("001009000");

                                    statelist.Add("000002000");

                                    statelist.Add("000012000");

                                    statelist.Add("001010000");

                                    statelist.Add("017026100");//出烘箱A提示,且不进料

                                    #endregion

                                }
                                else if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 2)
                                {
                                    #region 料盘2到位置4与料盘1叠加，从烘箱A出去

                                    //料盒进烘箱A

                                    //料盒2到空闲区4
                                    statelist.Add("005007010");
                                    statelist.Add("005006000");
                                    statelist.Add("009007000");
                                    statelist.Add("009008004");

                                    statelist.Add("001003004");

                                    statelist.Add("007007000");
                                    statelist.Add("007006000");
                                    statelist.Add("003007000");


                                    statelist.Add("001004000");

                                    statelist.Add("003008000");
                                    //关烘箱门
                                    statelist.Add("001009000");

                                    statelist.Add("000002000");

                                    statelist.Add("000012000");

                                    statelist.Add("001010000");

                                    statelist.Add("017026100");//出烘箱A提示,且不进料

                                    #endregion
                                }

                                Bin = false;
                            }
                            Ain = false;
                        }
                        else
                        {
                            if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber < 1)
                            {
                                if (Bin)
                                {
                                    //B的料盘出料

                                    if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber < 1)
                                    {

                                    }
                                    else if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 1)
                                    {
                                        #region 料盘1从烘箱A出去

                                        //料盒进烘箱A

                                        statelist.Add("001003010");

                                        statelist.Add("007007004");
                                        statelist.Add("007006000");
                                        statelist.Add("003007000");


                                        statelist.Add("001004000");

                                        statelist.Add("003008000");
                                        //关烘箱门
                                        statelist.Add("001009000");

                                        statelist.Add("000002000");

                                        statelist.Add("000012000");

                                        statelist.Add("001010000");

                                        statelist.Add("017026100");//出烘箱A提示,且不进料

                                        #endregion

                                    }
                                    else if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 2)
                                    {
                                        #region 料盘2到位置4与料盘1叠加，从烘箱A出去

                                        //料盒进烘箱A

                                        //料盒2到空闲区4
                                        statelist.Add("005007010");
                                        statelist.Add("005006000");
                                        statelist.Add("009007000");
                                        statelist.Add("009008004");

                                        statelist.Add("001003004");

                                        statelist.Add("007007000");
                                        statelist.Add("007006000");
                                        statelist.Add("003007000");


                                        statelist.Add("001004000");

                                        statelist.Add("003008000");
                                        //关烘箱门
                                        statelist.Add("001009000");

                                        statelist.Add("000002000");

                                        statelist.Add("000012000");

                                        statelist.Add("001010000");

                                        statelist.Add("017026100");//出烘箱A提示,且不进料

                                        #endregion
                                    }

                                    Bin = false;
                                }

                                Ain = false;

                            }
                            else if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber == 1)
                            {


                                #region 料盘1焊接，到达位置4

                                //料盒钩爪到避让位置
                                statelist.Add("000024000");
                                //物料钩爪到避让位置
                                statelist.Add("000025000");

                                //料盒出烘箱
                                statelist.Add("000002001");

                                statelist.Add("001003007");//先烘箱开门

                                statelist.Add("001005000");//料盒钩爪先到等待抓起料盘的位置，料盘再移动出来

                                statelist.Add("001004000");
                                statelist.Add("003006000");
                                //关烘箱门
                                statelist.Add("001009000");

                                //料盒到出烘箱位置
                                statelist.Add("004007000");
                                statelist.Add("004008000");

                                statelist.Add("001010000");//后关烘箱门

                                //料盒到焊接位置
                                statelist.Add("004007000");
                                statelist.Add("004006000");
                                statelist.Add("006007000");
                                statelist.Add("006008000");
                                statelist.Add("000002002");
                                //料盒钩爪到避让位置
                                statelist.Add("000024100");

                                if (Bin)
                                {
                                    //B的料盘出料

                                    if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber < 1)
                                    {

                                    }
                                    else if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 1)
                                    {
                                        #region 料盘1从烘箱A出去

                                        //料盒进烘箱A

                                        statelist.Add("001003010");

                                        statelist.Add("007007004");
                                        statelist.Add("007006000");
                                        statelist.Add("003007000");


                                        statelist.Add("001004000");

                                        statelist.Add("003008000");
                                        //关烘箱门
                                        statelist.Add("001009000");

                                        statelist.Add("000002000");

                                        statelist.Add("000012000");

                                        statelist.Add("001010000");

                                        statelist.Add("017026100");//出烘箱A提示,且不进料

                                        #endregion

                                    }
                                    else if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 2)
                                    {
                                        #region 料盘2到位置4与料盘1叠加，从烘箱A出去

                                        //料盒进烘箱A

                                        //料盒2到空闲区4
                                        statelist.Add("005007010");
                                        statelist.Add("005006000");
                                        statelist.Add("009007000");
                                        statelist.Add("009008004");

                                        statelist.Add("001003004");

                                        statelist.Add("007007000");
                                        statelist.Add("007006000");
                                        statelist.Add("003007000");


                                        statelist.Add("001004000");

                                        statelist.Add("003008000");
                                        //关烘箱门
                                        statelist.Add("001009000");

                                        statelist.Add("000002000");

                                        statelist.Add("000012000");

                                        statelist.Add("001010000");

                                        statelist.Add("017026100");//出烘箱A提示,且不进料

                                        #endregion
                                    }

                                    Bin = false;
                                }
                                else
                                {
                                    if (iOverBox1num + 1 < recipe.OverBox1Param.OverBoxMaterialBoxGetInNumber)
                                    {
                                        statelist.Add("001027000");
                                    }
                                }


                                //料盒焊接
                                statelist.Add("000011011");
                                statelist.Add("000012000");
                                statelist.Add("000014000");
                                statelist.Add("000013000");
                                //statelist.Add("000002000");
                                statelist.Add("011023000");
                                //物料钩爪到避让位置
                                statelist.Add("000025000");
                                //料盒到空闲区2
                                statelist.Add("006007000");
                                statelist.Add("006006000");
                                statelist.Add("007007001");
                                statelist.Add("007008100");


                                #endregion


                                Ain = true;
                            }
                            else if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber == 2)
                            {


                                #region 料盘1焊接，到达位置4 料盘2焊接，到达位置2

                                //料盒钩爪到避让位置
                                statelist.Add("000024000");
                                //物料钩爪到避让位置
                                statelist.Add("000025000");

                                //料盒出烘箱
                                statelist.Add("000002001");

                                statelist.Add("001003007");//先开烘箱门

                                statelist.Add("001005000");

                                statelist.Add("001004000");
                                statelist.Add("003006000");
                                //关烘箱门
                                statelist.Add("001009000");


                                statelist.Add("004007000");
                                statelist.Add("004008000");

                                statelist.Add("001010000");//后关烘箱门

                                //料盒1到焊接区
                                statelist.Add("015007000");
                                statelist.Add("015006000");
                                statelist.Add("006007000");
                                statelist.Add("006008000");
                                statelist.Add("000002002");
                                //料盒钩爪到避让位置
                                statelist.Add("000024100");

                                if (Bin)
                                {
                                    //B的料盘出料

                                    if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber < 1)
                                    {

                                    }
                                    else if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 1)
                                    {
                                        #region 料盘1从烘箱A出去

                                        //料盒进烘箱A

                                        statelist.Add("001003010");

                                        statelist.Add("007007004");
                                        statelist.Add("007006000");
                                        statelist.Add("003007000");


                                        statelist.Add("001004000");

                                        statelist.Add("003008000");
                                        //关烘箱门
                                        statelist.Add("001009000");

                                        statelist.Add("000002000");

                                        statelist.Add("000012000");

                                        statelist.Add("001010000");

                                        statelist.Add("017026100");//出烘箱A提示,且不进料

                                        #endregion

                                    }
                                    else if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 2)
                                    {
                                        #region 料盘2到位置4与料盘1叠加，从烘箱A出去

                                        //料盒进烘箱A

                                        //料盒2到空闲区4
                                        statelist.Add("005007010");
                                        statelist.Add("005006000");
                                        statelist.Add("009007000");
                                        statelist.Add("009008004");

                                        statelist.Add("001003004");

                                        statelist.Add("007007000");
                                        statelist.Add("007006000");
                                        statelist.Add("003007000");


                                        statelist.Add("001004000");

                                        statelist.Add("003008000");
                                        //关烘箱门
                                        statelist.Add("001009000");

                                        statelist.Add("000002000");

                                        statelist.Add("000012000");

                                        statelist.Add("001010000");

                                        statelist.Add("017026100");//出烘箱A提示,且不进料

                                        #endregion
                                    }

                                    Bin = false;
                                }
                                else
                                {
                                    if (iOverBox1num + 1 < recipe.OverBox1Param.OverBoxMaterialBoxGetInNumber)
                                    {
                                        statelist.Add("001027000");
                                    }
                                }

                                //料盒1焊接
                                statelist.Add("000011011");
                                statelist.Add("000012000");
                                statelist.Add("000014000");
                                statelist.Add("000013000");
                                //statelist.Add("000002000");
                                statelist.Add("011023000");
                                //物料钩爪到避让位置
                                statelist.Add("000025000");
                                //料盒1到空闲区2
                                statelist.Add("006007000");
                                statelist.Add("006006000");
                                statelist.Add("007007000");
                                statelist.Add("007008001");

                                //料盒到焊接位置
                                statelist.Add("004007000");
                                statelist.Add("004006000");
                                statelist.Add("006007000");
                                statelist.Add("006008000");
                                statelist.Add("000002003");
                                //料盒钩爪到避让位置
                                statelist.Add("000024000");
                                //料盒2焊接
                                statelist.Add("000011000");
                                statelist.Add("000012000");
                                statelist.Add("000014000");
                                statelist.Add("000013000");
                                //statelist.Add("000002000");
                                statelist.Add("012023000");
                                //物料钩爪到避让位置
                                statelist.Add("000025000");
                                //料盒2到空闲区2
                                statelist.Add("006007000");
                                statelist.Add("006006000");
                                statelist.Add("005007001");
                                statelist.Add("005008100");

                                #endregion


                                Ain = true;
                            }

                        }



                        if (iOverBox2num >= recipe.OverBox2Param.OverBoxMaterialBoxGetInNumber)
                        {
                            if (Ain)
                            {
                                //B的料盘出料

                                if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber < 1)
                                {

                                }
                                else if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber == 1)
                                {
                                    #region 料盘1从烘箱B出去

                                    //料盒进烘箱B
                                    statelist.Add("002003008");//先开烘箱门

                                    statelist.Add("007007001");
                                    statelist.Add("007006000");
                                    statelist.Add("010007000");


                                    statelist.Add("002004000");

                                    statelist.Add("010008000");
                                    //关烘箱门
                                    statelist.Add("002009000");

                                    statelist.Add("000002000");

                                    statelist.Add("000012000");

                                    statelist.Add("002010000");//后关烘箱门

                                    statelist.Add("019026100");//出烘箱B提示,且不进料

                                    #endregion

                                }
                                else if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber == 2)
                                {
                                    #region 料盘2到位置4与料盘1叠加，从烘箱B出去

                                    //料盒进烘箱B

                                    //料盒2到空闲区4
                                    statelist.Add("005007008");
                                    statelist.Add("005006000");
                                    statelist.Add("009007000");
                                    statelist.Add("009008001");

                                    //料盒进烘箱B
                                    statelist.Add("002003001");//先开烘箱门

                                    statelist.Add("007007000");
                                    statelist.Add("007006000");
                                    statelist.Add("010007000");


                                    statelist.Add("002004000");

                                    statelist.Add("010008000");
                                    //关烘箱门
                                    statelist.Add("002009000");

                                    statelist.Add("000002000");

                                    statelist.Add("000012000");

                                    statelist.Add("002010000");//后关烘箱门

                                    statelist.Add("019026100");//出烘箱B提示,且不进料

                                    #endregion
                                }

                                Ain = false;
                            }
                            Bin = false;
                        }
                        else
                        {
                            if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber < 1)
                            {
                                if (Ain)
                                {
                                    //B的料盘出料

                                    if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber < 1)
                                    {

                                    }
                                    else if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber == 1)
                                    {
                                        #region 料盘1从烘箱B出去

                                        //料盒进烘箱B
                                        statelist.Add("002003008");//先开烘箱门

                                        statelist.Add("007007001");
                                        statelist.Add("007006000");
                                        statelist.Add("010007000");


                                        statelist.Add("002004000");

                                        statelist.Add("010008000");
                                        //关烘箱门
                                        statelist.Add("002009000");

                                        statelist.Add("000002000");

                                        statelist.Add("000012000");

                                        statelist.Add("002010000");//后关烘箱门

                                        statelist.Add("019026100");//出烘箱B提示,且不进料

                                        #endregion

                                    }
                                    else if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber == 2)
                                    {
                                        #region 料盘2到位置4与料盘1叠加，从烘箱B出去

                                        //料盒进烘箱B

                                        //料盒2到空闲区4
                                        statelist.Add("005007008");
                                        statelist.Add("005006000");
                                        statelist.Add("009007000");
                                        statelist.Add("009008001");

                                        //料盒进烘箱B
                                        statelist.Add("002003001");//先开烘箱门

                                        statelist.Add("007007000");
                                        statelist.Add("007006000");
                                        statelist.Add("010007000");


                                        statelist.Add("002004000");

                                        statelist.Add("010008000");
                                        //关烘箱门
                                        statelist.Add("002009000");

                                        statelist.Add("000002000");

                                        statelist.Add("000012000");

                                        statelist.Add("002010000");//后关烘箱门

                                        statelist.Add("019026100");//出烘箱B提示,且不进料

                                        #endregion
                                    }

                                    Ain = false;
                                }

                                Bin = false;
                            }
                            else if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 1)
                            {


                                #region 料盘1焊接，到达位置4

                                //料盒钩爪到避让位置
                                statelist.Add("000024000");
                                //物料钩爪到避让位置
                                statelist.Add("000025000");

                                //料盒出烘箱
                                statelist.Add("000002004");

                                statelist.Add("002003009");

                                statelist.Add("002005000");

                                statelist.Add("002004000");
                                statelist.Add("010006000");
                                //关烘箱门
                                statelist.Add("002009000");


                                statelist.Add("004007000");
                                statelist.Add("004008000");

                                statelist.Add("002010000");

                                //料盒到焊接位置
                                statelist.Add("004007000");
                                statelist.Add("004006000");
                                statelist.Add("006007000");
                                statelist.Add("006008000");
                                statelist.Add("000002005");
                                //料盒钩爪到避让位置
                                statelist.Add("000024100");

                                if (Ain)
                                {
                                    //B的料盘出料

                                    if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber < 1)
                                    {

                                    }
                                    else if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber == 1)
                                    {
                                        #region 料盘1从烘箱B出去

                                        //料盒进烘箱B
                                        statelist.Add("002003008");//先开烘箱门

                                        statelist.Add("007007001");
                                        statelist.Add("007006000");
                                        statelist.Add("010007000");


                                        statelist.Add("002004000");

                                        statelist.Add("010008000");
                                        //关烘箱门
                                        statelist.Add("002009000");

                                        statelist.Add("000002000");

                                        statelist.Add("000012000");

                                        statelist.Add("002010000");//后关烘箱门

                                        statelist.Add("019026100");//出烘箱B提示,且不进料

                                        #endregion

                                    }
                                    else if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber == 2)
                                    {
                                        #region 料盘2到位置4与料盘1叠加，从烘箱B出去

                                        //料盒进烘箱B

                                        //料盒2到空闲区4
                                        statelist.Add("005007008");
                                        statelist.Add("005006000");
                                        statelist.Add("009007000");
                                        statelist.Add("009008001");

                                        //料盒进烘箱B
                                        statelist.Add("002003001");//先开烘箱门

                                        statelist.Add("007007000");
                                        statelist.Add("007006000");
                                        statelist.Add("010007000");


                                        statelist.Add("002004000");

                                        statelist.Add("010008000");
                                        //关烘箱门
                                        statelist.Add("002009000");

                                        statelist.Add("000002000");

                                        statelist.Add("000012000");

                                        statelist.Add("002010000");//后关烘箱门

                                        statelist.Add("019026100");//出烘箱B提示,且不进料

                                        #endregion
                                    }

                                    Ain = false;
                                }
                                else
                                {
                                    if (iOverBox2num + 1 < recipe.OverBox2Param.OverBoxMaterialBoxGetInNumber)
                                    {
                                        statelist.Add("002027000");
                                    }
                                }

                                //料盒焊接
                                statelist.Add("000011012");
                                statelist.Add("000012000");
                                statelist.Add("000014000");
                                statelist.Add("000013000");
                                //statelist.Add("000002000");
                                statelist.Add("013023000");
                                //物料钩爪到避让位置
                                statelist.Add("000025000");
                                //料盒到空闲区2
                                statelist.Add("006007000");
                                statelist.Add("006006000");
                                statelist.Add("007007000");
                                statelist.Add("007008100");

                                #endregion

                                Bin = true;
                            }
                            else if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 2)
                            {


                                #region 料盘1焊接，到达位置4 料盘2焊接，到达位置2

                                //料盒钩爪到避让位置
                                statelist.Add("000024000");
                                //物料钩爪到避让位置
                                statelist.Add("000025000");

                                //料盒出烘箱
                                statelist.Add("000002004");

                                statelist.Add("002003009");

                                statelist.Add("002005000");

                                statelist.Add("002004000");
                                statelist.Add("010006000");
                                //关烘箱门
                                statelist.Add("002009000");


                                statelist.Add("004007000");
                                statelist.Add("004008000");

                                statelist.Add("002010000");
                                //料盒1到焊接区
                                statelist.Add("015007000");
                                statelist.Add("015006000");
                                statelist.Add("006007000");
                                statelist.Add("006008000");
                                statelist.Add("000002002");
                                //料盒钩爪到避让位置
                                statelist.Add("000024100");

                                if (Ain)
                                {
                                    //B的料盘出料

                                    if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber < 1)
                                    {

                                    }
                                    else if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber == 1)
                                    {
                                        #region 料盘1从烘箱B出去

                                        //料盒进烘箱B
                                        statelist.Add("002003008");//先开烘箱门

                                        statelist.Add("007007001");
                                        statelist.Add("007006000");
                                        statelist.Add("010007000");


                                        statelist.Add("002004000");

                                        statelist.Add("010008000");
                                        //关烘箱门
                                        statelist.Add("002009000");

                                        statelist.Add("000002000");

                                        statelist.Add("000012000");

                                        statelist.Add("002010000");//后关烘箱门

                                        statelist.Add("019026100");//出烘箱B提示,且不进料

                                        #endregion

                                    }
                                    else if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber == 2)
                                    {
                                        #region 料盘2到位置4与料盘1叠加，从烘箱B出去

                                        //料盒进烘箱B

                                        //料盒2到空闲区4
                                        statelist.Add("005007008");
                                        statelist.Add("005006000");
                                        statelist.Add("009007000");
                                        statelist.Add("009008001");

                                        //料盒进烘箱B
                                        statelist.Add("002003001");//先开烘箱门

                                        statelist.Add("007007000");
                                        statelist.Add("007006000");
                                        statelist.Add("010007000");


                                        statelist.Add("002004000");

                                        statelist.Add("010008000");
                                        //关烘箱门
                                        statelist.Add("002009000");

                                        statelist.Add("000002000");

                                        statelist.Add("000012000");

                                        statelist.Add("002010000");//后关烘箱门

                                        statelist.Add("019026100");//出烘箱B提示,且不进料

                                        #endregion
                                    }

                                    Ain = false;
                                }
                                else
                                {
                                    if (iOverBox2num + 1 < recipe.OverBox2Param.OverBoxMaterialBoxGetInNumber)
                                    {
                                        statelist.Add("002027000");
                                    }
                                }

                                //料盒1焊接
                                statelist.Add("000011012");
                                statelist.Add("000012000");
                                statelist.Add("000014000");
                                statelist.Add("000013000");
                                //statelist.Add("000002000");
                                statelist.Add("013023000");
                                //物料钩爪到避让位置
                                statelist.Add("000025000");
                                //料盒1到空闲区2
                                statelist.Add("006007000");
                                statelist.Add("006006000");
                                statelist.Add("007007000");
                                statelist.Add("007008004");
                                //料盒到焊接位置
                                statelist.Add("004007000");
                                statelist.Add("004006000");
                                statelist.Add("006007000");
                                statelist.Add("006008000");
                                statelist.Add("000002003");
                                //料盒钩爪到避让位置
                                statelist.Add("000024000");
                                //料盒2焊接
                                statelist.Add("000011000");
                                statelist.Add("000012000");
                                statelist.Add("000014000");
                                statelist.Add("000013000");
                                //statelist.Add("000002000");
                                statelist.Add("014023000");
                                //物料钩爪到避让位置
                                statelist.Add("000025000");
                                //料盒2到空闲区2
                                statelist.Add("006007000");
                                statelist.Add("006006000");
                                statelist.Add("005007004");
                                statelist.Add("005008100");

                                #endregion
                                Bin = true;
                            }

                        }

                        //if(Bin == false && Ain == false)
                        //{
                        //    break;
                        //}

                    }
                    //物料钩爪到避让位置
                    statelist.Add("000025000");
                    //料盒钩爪到空闲位置
                    statelist.Add("000002000");
                    //物料钩爪到空闲位置
                    statelist.Add("000012000");


                    #endregion


                    #endregion


                    #region 20241103

                    //#region 20241103


                    //statelist.Add("000001000");

                    //bool Bin = false;
                    //bool Ain = false;
                    //int iOverBox1num = 0;
                    //int iOverBox2num = 0;

                    //if (recipe.OverBox1Param.OverBoxMaterialBoxGetInNumber > 0)
                    //{
                    //    statelist.Add("001027000");
                    //}
                    //if (recipe.OverBox2Param.OverBoxMaterialBoxGetInNumber > 0)
                    //{
                    //    statelist.Add("002027000");
                    //}

                    //for (int num = 0; num <= Math.Max(recipe.OverBox1Param.OverBoxMaterialBoxGetInNumber, recipe.OverBox2Param.OverBoxMaterialBoxGetInNumber); num++)
                    //{
                    //    iOverBox1num = num;
                    //    iOverBox2num = num;

                    //    if (iOverBox1num >= recipe.OverBox1Param.OverBoxMaterialBoxGetInNumber)
                    //    {
                    //        if (Bin)
                    //        {
                    //            //B的料盘出料

                    //            if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber < 1)
                    //            {

                    //            }
                    //            else if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 1)
                    //            {
                    //                #region 料盘1从烘箱A出去

                    //                //料盒进烘箱A

                    //                statelist.Add("001003010");

                    //                statelist.Add("005007010");
                    //                statelist.Add("005006000");
                    //                statelist.Add("003007000");


                    //                statelist.Add("001004000");

                    //                statelist.Add("003008000");
                    //                //关烘箱门
                    //                statelist.Add("001009000");

                    //                statelist.Add("000002000");

                    //                statelist.Add("000012000");

                    //                statelist.Add("001010000");

                    //                statelist.Add("017026100");//出烘箱A提示,且不进料

                    //                #endregion

                    //            }
                    //            else if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 2)
                    //            {
                    //                #region 料盘2到位置4与料盘1叠加，从烘箱A出去

                    //                //4到1
                    //                statelist.Add("007007000");
                    //                statelist.Add("007006000");
                    //                statelist.Add("004007000");
                    //                statelist.Add("004008000");

                    //                //料盒进烘箱A

                    //                //料盒2到空闲区4
                    //                statelist.Add("005007010");
                    //                statelist.Add("005006000");
                    //                statelist.Add("015007000");
                    //                statelist.Add("015008004");

                    //                statelist.Add("001003004");

                    //                statelist.Add("004007000");
                    //                statelist.Add("004006000");
                    //                statelist.Add("003007000");


                    //                statelist.Add("001004000");

                    //                statelist.Add("003008000");
                    //                //关烘箱门
                    //                statelist.Add("001009000");

                    //                statelist.Add("000002000");

                    //                statelist.Add("000012000");

                    //                statelist.Add("001010000");

                    //                statelist.Add("017026100");//出烘箱A提示,且不进料

                    //                #endregion
                    //            }

                    //            Bin = false;
                    //        }
                    //        Ain = false;
                    //    }
                    //    else
                    //    {
                    //        if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber < 1)
                    //        {
                    //            if (Bin)
                    //            {
                    //                //B的料盘出料

                    //                if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber < 1)
                    //                {

                    //                }
                    //                else if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 1)
                    //                {
                    //                    #region 料盘1从烘箱A出去

                    //                    //料盒进烘箱A

                    //                    statelist.Add("001003010");

                    //                    statelist.Add("005007010");
                    //                    statelist.Add("005006000");
                    //                    statelist.Add("003007000");


                    //                    statelist.Add("001004000");

                    //                    statelist.Add("003008000");
                    //                    //关烘箱门
                    //                    statelist.Add("001009000");

                    //                    statelist.Add("000002000");

                    //                    statelist.Add("000012000");

                    //                    statelist.Add("001010000");

                    //                    statelist.Add("017026100");//出烘箱A提示,且不进料

                    //                    #endregion

                    //                }
                    //                else if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 2)
                    //                {
                    //                    #region 料盘2到位置4与料盘1叠加，从烘箱A出去

                    //                    //4到1
                    //                    statelist.Add("007007000");
                    //                    statelist.Add("007006000");
                    //                    statelist.Add("004007000");
                    //                    statelist.Add("004008000");

                    //                    //料盒进烘箱A

                    //                    //料盒2到空闲区4
                    //                    statelist.Add("005007010");
                    //                    statelist.Add("005006000");
                    //                    statelist.Add("015007000");
                    //                    statelist.Add("015008004");

                    //                    statelist.Add("001003004");

                    //                    statelist.Add("004007000");
                    //                    statelist.Add("004006000");
                    //                    statelist.Add("003007000");


                    //                    statelist.Add("001004000");

                    //                    statelist.Add("003008000");
                    //                    //关烘箱门
                    //                    statelist.Add("001009000");

                    //                    statelist.Add("000002000");

                    //                    statelist.Add("000012000");

                    //                    statelist.Add("001010000");

                    //                    statelist.Add("017026100");//出烘箱A提示,且不进料

                    //                    #endregion
                    //                }

                    //                Bin = false;
                    //            }

                    //            Ain = false;

                    //        }
                    //        else if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber == 1)
                    //        {


                    //            #region 料盘1焊接，到达位置2

                    //            //料盒钩爪到避让位置
                    //            statelist.Add("000024000");
                    //            //物料钩爪到避让位置
                    //            statelist.Add("000025000");

                    //            //料盒出烘箱
                    //            statelist.Add("000002001");

                    //            statelist.Add("001003007");//先烘箱开门

                    //            statelist.Add("001005000");//料盒钩爪先到等待抓起料盘的位置，料盘再移动出来

                    //            statelist.Add("001004000");
                    //            statelist.Add("003006000");
                    //            //关烘箱门
                    //            statelist.Add("001009000");

                    //            //料盒到出烘箱位置
                    //            statelist.Add("004007000");
                    //            statelist.Add("004008000");

                    //            statelist.Add("001010000");//后关烘箱门

                    //            //料盒到焊接位置
                    //            statelist.Add("004007000");
                    //            statelist.Add("004006000");
                    //            statelist.Add("006007000");
                    //            statelist.Add("006008000");
                    //            statelist.Add("000002002");
                    //            //料盒钩爪到避让位置
                    //            statelist.Add("000024100");



                    //            if (Bin)
                    //            {
                    //                //B的料盘出料

                    //                if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber < 1)
                    //                {

                    //                }
                    //                else if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 1)
                    //                {
                    //                    #region 料盘1从烘箱A出去

                    //                    //料盒进烘箱A

                    //                    statelist.Add("001003010");

                    //                    statelist.Add("005007010");
                    //                    statelist.Add("005006000");
                    //                    statelist.Add("003007000");


                    //                    statelist.Add("001004000");

                    //                    statelist.Add("003008000");
                    //                    //关烘箱门
                    //                    statelist.Add("001009000");

                    //                    statelist.Add("000002000");

                    //                    statelist.Add("000012000");

                    //                    statelist.Add("001010000");

                    //                    statelist.Add("017026100");//出烘箱A提示,且不进料

                    //                    #endregion

                    //                }
                    //                else if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 2)
                    //                {
                    //                    #region 料盘2到位置4与料盘1叠加，从烘箱A出去

                    //                    //4到1
                    //                    statelist.Add("007007000");
                    //                    statelist.Add("007006000");
                    //                    statelist.Add("004007000");
                    //                    statelist.Add("004008000");

                    //                    //料盒进烘箱A

                    //                    //料盒2到空闲区4
                    //                    statelist.Add("005007010");
                    //                    statelist.Add("005006000");
                    //                    statelist.Add("015007000");
                    //                    statelist.Add("015008004");

                    //                    statelist.Add("001003004");

                    //                    statelist.Add("004007000");
                    //                    statelist.Add("004006000");
                    //                    statelist.Add("003007000");


                    //                    statelist.Add("001004000");

                    //                    statelist.Add("003008000");
                    //                    //关烘箱门
                    //                    statelist.Add("001009000");

                    //                    statelist.Add("000002000");

                    //                    statelist.Add("000012000");

                    //                    statelist.Add("001010000");

                    //                    statelist.Add("017026100");//出烘箱A提示,且不进料

                    //                    #endregion
                    //                }

                    //                Bin = false;
                    //            }
                    //            else
                    //            {
                    //                if (iOverBox1num + 1 < recipe.OverBox1Param.OverBoxMaterialBoxGetInNumber)
                    //                {
                    //                    statelist.Add("001027000");
                    //                }
                    //            }


                    //            //料盒焊接
                    //            statelist.Add("000011011");
                    //            statelist.Add("000012000");
                    //            statelist.Add("000014000");
                    //            statelist.Add("000013000");
                    //            //statelist.Add("000002000");
                    //            statelist.Add("011023000");
                    //            //物料钩爪到避让位置
                    //            statelist.Add("000025000");
                    //            //料盒到空闲区2
                    //            statelist.Add("006007000");
                    //            statelist.Add("006006000");
                    //            statelist.Add("005007001");
                    //            statelist.Add("005008100");


                    //            #endregion


                    //            Ain = true;
                    //        }
                    //        else if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber == 2)
                    //        {


                    //            #region 料盘1焊接，到达位置4 料盘2焊接，到达位置2

                    //            //料盒钩爪到避让位置
                    //            statelist.Add("000024000");
                    //            //物料钩爪到避让位置
                    //            statelist.Add("000025000");

                    //            //料盒出烘箱
                    //            statelist.Add("000002001");

                    //            statelist.Add("001003007");//先开烘箱门

                    //            statelist.Add("001005000");

                    //            statelist.Add("001004000");
                    //            statelist.Add("003006000");
                    //            //关烘箱门
                    //            statelist.Add("001009000");


                    //            statelist.Add("004007000");
                    //            statelist.Add("004008000");

                    //            statelist.Add("001010000");//后关烘箱门

                    //            //料盒1到焊接区
                    //            statelist.Add("015007000");
                    //            statelist.Add("015006000");
                    //            statelist.Add("006007000");
                    //            statelist.Add("006008000");
                    //            statelist.Add("000002002");
                    //            //料盒钩爪到避让位置
                    //            statelist.Add("000024100");


                    //            //倒料
                    //            if (Bin)
                    //            {
                    //                if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber < 1)
                    //                {
                    //                    //1到4
                    //                    statelist.Add("004007000");
                    //                    statelist.Add("004006000");
                    //                    statelist.Add("007007000");
                    //                    statelist.Add("007008000");
                    //                }
                    //                else if(recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 1)
                    //                {
                    //                    //1到4
                    //                    statelist.Add("004007000");
                    //                    statelist.Add("004006000");
                    //                    statelist.Add("007007000");
                    //                    statelist.Add("007008000");
                    //                }
                    //                else if(recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 2)
                    //                {
                    //                    //1到4的上方
                    //                    statelist.Add("004007000");
                    //                    statelist.Add("004006000");
                    //                    statelist.Add("009007000");
                    //                    statelist.Add("009008000");

                    //                    //1和4都到1
                    //                    statelist.Add("007007000");
                    //                    statelist.Add("007006000");
                    //                    statelist.Add("004007000");
                    //                    statelist.Add("004008000");

                    //                    //1的上方到4
                    //                    statelist.Add("015007000");
                    //                    statelist.Add("015006000");
                    //                    statelist.Add("007007000");
                    //                    statelist.Add("007008000");
                    //                }

                    //            }
                    //            else
                    //            {
                    //                //1到4
                    //                statelist.Add("004007000");
                    //                statelist.Add("004006000");
                    //                statelist.Add("007007000");
                    //                statelist.Add("007008000");
                    //            }

                    //            //statelist.Add("000002002");
                    //            ////料盒钩爪到避让位置
                    //            //statelist.Add("000024100");

                    //            if (Bin)
                    //            {
                    //                //B的料盘出料

                    //                if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber < 1)
                    //                {

                    //                }
                    //                else if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 1)
                    //                {
                    //                    #region 料盘1从烘箱A出去

                    //                    //料盒进烘箱A

                    //                    statelist.Add("001003010");

                    //                    statelist.Add("007007004");
                    //                    statelist.Add("007006000");
                    //                    statelist.Add("003007000");


                    //                    statelist.Add("001004000");

                    //                    statelist.Add("003008000");
                    //                    //关烘箱门
                    //                    statelist.Add("001009000");

                    //                    statelist.Add("000002000");

                    //                    statelist.Add("000012000");

                    //                    statelist.Add("001010000");

                    //                    statelist.Add("017026100");//出烘箱A提示,且不进料

                    //                    #endregion

                    //                }
                    //                else if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 2)
                    //                {
                    //                    #region 料盘2到位置4与料盘1叠加，从烘箱A出去

                    //                    //料盒进烘箱A

                    //                    //料盒2到空闲区4
                    //                    statelist.Add("005007010");
                    //                    statelist.Add("005006000");
                    //                    statelist.Add("009007000");
                    //                    statelist.Add("009008004");

                    //                    statelist.Add("001003004");

                    //                    statelist.Add("007007000");
                    //                    statelist.Add("007006000");
                    //                    statelist.Add("003007000");


                    //                    statelist.Add("001004000");

                    //                    statelist.Add("003008000");
                    //                    //关烘箱门
                    //                    statelist.Add("001009000");

                    //                    statelist.Add("000002000");

                    //                    statelist.Add("000012000");

                    //                    statelist.Add("001010000");

                    //                    statelist.Add("017026100");//出烘箱A提示,且不进料

                    //                    #endregion
                    //                }

                    //                Bin = false;
                    //            }
                    //            else
                    //            {
                    //                if (iOverBox1num + 1 < recipe.OverBox1Param.OverBoxMaterialBoxGetInNumber)
                    //                {
                    //                    statelist.Add("001027000");
                    //                }
                    //            }

                    //            statelist.Add("000002002");
                    //            //料盒钩爪到避让位置
                    //            statelist.Add("000024100");

                    //            //料盒1焊接
                    //            statelist.Add("000011011");
                    //            statelist.Add("000012000");
                    //            statelist.Add("000014000");
                    //            statelist.Add("000013000");
                    //            //statelist.Add("000002000");
                    //            statelist.Add("011023000");
                    //            //物料钩爪到避让位置
                    //            statelist.Add("000025000");
                    //            //料盒1到位置2
                    //            statelist.Add("006007000");
                    //            statelist.Add("006006000");
                    //            statelist.Add("005007000");
                    //            statelist.Add("005008001");

                    //            //料盒2到焊接位置
                    //            statelist.Add("007007000");
                    //            statelist.Add("007006000");
                    //            statelist.Add("006007000");
                    //            statelist.Add("006008000");
                    //            statelist.Add("000002003");
                    //            //料盒钩爪到避让位置
                    //            statelist.Add("000024000");
                    //            //料盒2焊接
                    //            statelist.Add("000011000");
                    //            statelist.Add("000012000");
                    //            statelist.Add("000014000");
                    //            statelist.Add("000013000");
                    //            //statelist.Add("000002000");
                    //            statelist.Add("012023000");
                    //            //物料钩爪到避让位置
                    //            statelist.Add("000025000");
                    //            //料盒2到位置1
                    //            statelist.Add("006007000");
                    //            statelist.Add("006006000");
                    //            statelist.Add("004007001");
                    //            statelist.Add("004008100");

                    //            statelist.Add("000002002");

                    //            #endregion


                    //            Ain = true;
                    //        }

                    //    }



                    //    if (iOverBox2num >= recipe.OverBox2Param.OverBoxMaterialBoxGetInNumber)
                    //    {
                    //        if (Ain)
                    //        {
                    //            //B的料盘出料

                    //            if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber < 1)
                    //            {

                    //            }
                    //            else if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber == 1)
                    //            {
                    //                #region 料盘1从烘箱B出去

                    //                //料盒进烘箱B
                    //                statelist.Add("002003008");//先开烘箱门

                    //                statelist.Add("005007001");
                    //                statelist.Add("005006000");
                    //                statelist.Add("010007000");


                    //                statelist.Add("002004000");

                    //                statelist.Add("010008000");
                    //                //关烘箱门
                    //                statelist.Add("002009000");

                    //                statelist.Add("000002000");

                    //                statelist.Add("000012000");

                    //                statelist.Add("002010000");//后关烘箱门

                    //                statelist.Add("019026100");//出烘箱B提示,且不进料

                    //                #endregion

                    //            }
                    //            else if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber == 2)
                    //            {
                    //                #region 料盘2到位置4与料盘1叠加，从烘箱B出去

                    //                //1到4
                    //                statelist.Add("004007000");
                    //                statelist.Add("004006000");
                    //                statelist.Add("007007000");
                    //                statelist.Add("007008000");

                    //                //料盒进烘箱B

                    //                //料盒2到空闲区4
                    //                statelist.Add("005007008");
                    //                statelist.Add("005006000");
                    //                statelist.Add("009007000");
                    //                statelist.Add("009008001");

                    //                //料盒进烘箱B
                    //                statelist.Add("002003001");//先开烘箱门

                    //                statelist.Add("007007000");
                    //                statelist.Add("007006000");
                    //                statelist.Add("010007000");


                    //                statelist.Add("002004000");

                    //                statelist.Add("010008000");
                    //                //关烘箱门
                    //                statelist.Add("002009000");

                    //                statelist.Add("000002000");

                    //                statelist.Add("000012000");

                    //                statelist.Add("002010000");//后关烘箱门

                    //                statelist.Add("019026100");//出烘箱B提示,且不进料

                    //                #endregion
                    //            }

                    //            Ain = false;
                    //        }
                    //        Bin = false;
                    //    }
                    //    else
                    //    {
                    //        if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber < 1)
                    //        {
                    //            if (Ain)
                    //            {
                    //                //B的料盘出料

                    //                if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber < 1)
                    //                {

                    //                }
                    //                else if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber == 1)
                    //                {
                    //                    #region 料盘1从烘箱B出去

                    //                    //料盒进烘箱B
                    //                    statelist.Add("002003008");//先开烘箱门

                    //                    statelist.Add("005007001");
                    //                    statelist.Add("005006000");
                    //                    statelist.Add("010007000");


                    //                    statelist.Add("002004000");

                    //                    statelist.Add("010008000");
                    //                    //关烘箱门
                    //                    statelist.Add("002009000");

                    //                    statelist.Add("000002000");

                    //                    statelist.Add("000012000");

                    //                    statelist.Add("002010000");//后关烘箱门

                    //                    statelist.Add("019026100");//出烘箱B提示,且不进料

                    //                    #endregion

                    //                }
                    //                else if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber == 2)
                    //                {
                    //                    #region 料盘2到位置4与料盘1叠加，从烘箱B出去

                    //                    //1到4
                    //                    statelist.Add("004007000");
                    //                    statelist.Add("004006000");
                    //                    statelist.Add("007007000");
                    //                    statelist.Add("007008000");

                    //                    //料盒进烘箱B

                    //                    //料盒2到空闲区4
                    //                    statelist.Add("005007008");
                    //                    statelist.Add("005006000");
                    //                    statelist.Add("009007000");
                    //                    statelist.Add("009008001");

                    //                    //料盒进烘箱B
                    //                    statelist.Add("002003001");//先开烘箱门

                    //                    statelist.Add("007007000");
                    //                    statelist.Add("007006000");
                    //                    statelist.Add("010007000");


                    //                    statelist.Add("002004000");

                    //                    statelist.Add("010008000");
                    //                    //关烘箱门
                    //                    statelist.Add("002009000");

                    //                    statelist.Add("000002000");

                    //                    statelist.Add("000012000");

                    //                    statelist.Add("002010000");//后关烘箱门

                    //                    statelist.Add("019026100");//出烘箱B提示,且不进料

                    //                    #endregion
                    //                }

                    //                Ain = false;
                    //            }

                    //            Bin = false;
                    //        }
                    //        else if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 1)
                    //        {


                    //            #region 料盘1焊接，到达位置4

                    //            //料盒钩爪到避让位置
                    //            statelist.Add("000024000");
                    //            //物料钩爪到避让位置
                    //            statelist.Add("000025000");

                    //            //料盒出烘箱
                    //            statelist.Add("000002004");

                    //            statelist.Add("002003009");

                    //            statelist.Add("002005000");

                    //            statelist.Add("002004000");
                    //            statelist.Add("010006000");
                    //            //关烘箱门
                    //            statelist.Add("002009000");


                    //            statelist.Add("004007000");
                    //            statelist.Add("004008000");

                    //            statelist.Add("002010000");

                    //            //料盒到焊接位置
                    //            statelist.Add("004007000");
                    //            statelist.Add("004006000");
                    //            statelist.Add("006007000");
                    //            statelist.Add("006008000");
                    //            statelist.Add("000002005");
                    //            //料盒钩爪到避让位置
                    //            statelist.Add("000024100");

                    //            if (Ain)
                    //            {
                    //                //B的料盘出料

                    //                if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber < 1)
                    //                {

                    //                }
                    //                else if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber == 1)
                    //                {
                    //                    #region 料盘1从烘箱B出去

                    //                    //料盒进烘箱B
                    //                    statelist.Add("002003008");//先开烘箱门

                    //                    statelist.Add("005007001");
                    //                    statelist.Add("005006000");
                    //                    statelist.Add("010007000");


                    //                    statelist.Add("002004000");

                    //                    statelist.Add("010008000");
                    //                    //关烘箱门
                    //                    statelist.Add("002009000");

                    //                    statelist.Add("000002000");

                    //                    statelist.Add("000012000");

                    //                    statelist.Add("002010000");//后关烘箱门

                    //                    statelist.Add("019026100");//出烘箱B提示,且不进料

                    //                    #endregion

                    //                }
                    //                else if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber == 2)
                    //                {
                    //                    #region 料盘2到位置4与料盘1叠加，从烘箱B出去

                    //                    //1到4
                    //                    statelist.Add("004007000");
                    //                    statelist.Add("004006000");
                    //                    statelist.Add("007007000");
                    //                    statelist.Add("007008000");

                    //                    //料盒进烘箱B

                    //                    //料盒2到空闲区4
                    //                    statelist.Add("005007008");
                    //                    statelist.Add("005006000");
                    //                    statelist.Add("009007000");
                    //                    statelist.Add("009008001");

                    //                    //料盒进烘箱B
                    //                    statelist.Add("002003001");//先开烘箱门

                    //                    statelist.Add("007007000");
                    //                    statelist.Add("007006000");
                    //                    statelist.Add("010007000");


                    //                    statelist.Add("002004000");

                    //                    statelist.Add("010008000");
                    //                    //关烘箱门
                    //                    statelist.Add("002009000");

                    //                    statelist.Add("000002000");

                    //                    statelist.Add("000012000");

                    //                    statelist.Add("002010000");//后关烘箱门

                    //                    statelist.Add("019026100");//出烘箱B提示,且不进料

                    //                    #endregion
                    //                }

                    //                Ain = false;
                    //            }
                    //            else
                    //            {
                    //                if (iOverBox2num + 1 < recipe.OverBox2Param.OverBoxMaterialBoxGetInNumber)
                    //                {
                    //                    statelist.Add("002027000");
                    //                }
                    //            }

                    //            //料盒焊接
                    //            statelist.Add("000011012");
                    //            statelist.Add("000012000");
                    //            statelist.Add("000014000");
                    //            statelist.Add("000013000");
                    //            //statelist.Add("000002000");
                    //            statelist.Add("013023000");
                    //            //物料钩爪到避让位置
                    //            statelist.Add("000025000");
                    //            //料盒到空闲区2
                    //            statelist.Add("006007000");
                    //            statelist.Add("006006000");
                    //            statelist.Add("005007000");
                    //            statelist.Add("005008100");

                    //            #endregion

                    //            Bin = true;
                    //        }
                    //        else if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 2)
                    //        {


                    //            #region 料盘1焊接，到达位置4 料盘2焊接，到达位置2

                    //            //料盒钩爪到避让位置
                    //            statelist.Add("000024000");
                    //            //物料钩爪到避让位置
                    //            statelist.Add("000025000");

                    //            //料盒出烘箱
                    //            statelist.Add("000002004");

                    //            statelist.Add("002003009");

                    //            statelist.Add("002005000");

                    //            statelist.Add("002004000");
                    //            statelist.Add("010006000");
                    //            //关烘箱门
                    //            statelist.Add("002009000");


                    //            statelist.Add("007007000");
                    //            statelist.Add("007008000");

                    //            statelist.Add("002010000");
                    //            //料盒1到焊接区
                    //            statelist.Add("009007000");
                    //            statelist.Add("009006000");
                    //            statelist.Add("006007000");
                    //            statelist.Add("006008000");
                    //            statelist.Add("000002002");
                    //            //料盒钩爪到避让位置
                    //            statelist.Add("000024100");


                    //            //倒料
                    //            if (Ain)
                    //            {
                    //                if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber < 1)
                    //                {
                    //                    //1到4
                    //                    statelist.Add("004007000");
                    //                    statelist.Add("004006000");
                    //                    statelist.Add("007007000");
                    //                    statelist.Add("007008000");
                    //                }
                    //                else if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 1)
                    //                {
                    //                    //1到4
                    //                    statelist.Add("004007000");
                    //                    statelist.Add("004006000");
                    //                    statelist.Add("007007000");
                    //                    statelist.Add("007008000");
                    //                }
                    //                else if (recipe.OverBox2Param.OverBoxMaterialBoxLayerNumber == 2)
                    //                {
                    //                    //1到4的上方
                    //                    statelist.Add("004007000");
                    //                    statelist.Add("004006000");
                    //                    statelist.Add("009007000");
                    //                    statelist.Add("009008000");

                    //                    //1和4都到1
                    //                    statelist.Add("007007000");
                    //                    statelist.Add("007006000");
                    //                    statelist.Add("004007000");
                    //                    statelist.Add("004008000");

                    //                    //1的上方到4
                    //                    statelist.Add("015007000");
                    //                    statelist.Add("015006000");
                    //                    statelist.Add("007007000");
                    //                    statelist.Add("007008000");
                    //                }

                    //            }
                    //            else
                    //            {
                    //                //1到4
                    //                statelist.Add("004007000");
                    //                statelist.Add("004006000");
                    //                statelist.Add("007007000");
                    //                statelist.Add("007008000");
                    //            }

                    //            //statelist.Add("000002002");
                    //            ////料盒钩爪到避让位置
                    //            //statelist.Add("000024100");


                    //            if (Ain)
                    //            {
                    //                //B的料盘出料

                    //                if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber < 1)
                    //                {

                    //                }
                    //                else if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber == 1)
                    //                {
                    //                    #region 料盘1从烘箱B出去

                    //                    //料盒进烘箱B
                    //                    statelist.Add("002003008");//先开烘箱门

                    //                    statelist.Add("007007001");
                    //                    statelist.Add("007006000");
                    //                    statelist.Add("010007000");


                    //                    statelist.Add("002004000");

                    //                    statelist.Add("010008000");
                    //                    //关烘箱门
                    //                    statelist.Add("002009000");

                    //                    statelist.Add("000002000");

                    //                    statelist.Add("000012000");

                    //                    statelist.Add("002010000");//后关烘箱门

                    //                    statelist.Add("019026100");//出烘箱B提示,且不进料

                    //                    #endregion

                    //                }
                    //                else if (recipe.OverBox1Param.OverBoxMaterialBoxLayerNumber == 2)
                    //                {
                    //                    #region 料盘2到位置4与料盘1叠加，从烘箱B出去

                    //                    //料盒进烘箱B

                    //                    //料盒2到空闲区4
                    //                    statelist.Add("005007008");
                    //                    statelist.Add("005006000");
                    //                    statelist.Add("009007000");
                    //                    statelist.Add("009008001");

                    //                    //料盒进烘箱B
                    //                    statelist.Add("002003001");//先开烘箱门

                    //                    statelist.Add("007007000");
                    //                    statelist.Add("007006000");
                    //                    statelist.Add("010007000");


                    //                    statelist.Add("002004000");

                    //                    statelist.Add("010008000");
                    //                    //关烘箱门
                    //                    statelist.Add("002009000");

                    //                    statelist.Add("000002000");

                    //                    statelist.Add("000012000");

                    //                    statelist.Add("002010000");//后关烘箱门

                    //                    statelist.Add("019026100");//出烘箱B提示,且不进料

                    //                    #endregion
                    //                }

                    //                Ain = false;
                    //            }
                    //            else
                    //            {
                    //                if (iOverBox2num + 1 < recipe.OverBox2Param.OverBoxMaterialBoxGetInNumber)
                    //                {
                    //                    statelist.Add("002027000");
                    //                }
                    //            }

                    //            statelist.Add("000002002");
                    //            //料盒钩爪到避让位置
                    //            statelist.Add("000024100");

                    //            //料盒1焊接
                    //            statelist.Add("000011012");
                    //            statelist.Add("000012000");
                    //            statelist.Add("000014000");
                    //            statelist.Add("000013000");
                    //            //statelist.Add("000002000");
                    //            statelist.Add("013023000");
                    //            //物料钩爪到避让位置
                    //            statelist.Add("000025000");
                    //            //料盒1到空闲区2
                    //            statelist.Add("006007000");
                    //            statelist.Add("006006000");
                    //            statelist.Add("005007000");
                    //            statelist.Add("005008004");
                    //            //料盒到焊接位置
                    //            statelist.Add("004007000");
                    //            statelist.Add("004006000");
                    //            statelist.Add("006007000");
                    //            statelist.Add("006008000");
                    //            statelist.Add("000002003");
                    //            //料盒钩爪到避让位置
                    //            statelist.Add("000024000");
                    //            //料盒2焊接
                    //            statelist.Add("000011000");
                    //            statelist.Add("000012000");
                    //            statelist.Add("000014000");
                    //            statelist.Add("000013000");
                    //            //statelist.Add("000002000");
                    //            statelist.Add("014023000");
                    //            //物料钩爪到避让位置
                    //            statelist.Add("000025000");
                    //            //料盒2到空闲区2
                    //            statelist.Add("006007000");
                    //            statelist.Add("006006000");
                    //            statelist.Add("007007004");
                    //            statelist.Add("007008100");

                    //            statelist.Add("000002002");

                    //            #endregion
                    //            Bin = true;
                    //        }

                    //    }

                    //    //if(Bin == false && Ain == false)
                    //    //{
                    //    //    break;
                    //    //}

                    //}
                    ////物料钩爪到避让位置
                    //statelist.Add("000025000");
                    ////料盒钩爪到空闲位置
                    //statelist.Add("000002000");
                    ////物料钩爪到空闲位置
                    //statelist.Add("000012000");


                    //#endregion



                    #endregion



                    DataModel.Instance.ProcessTable = new DataTable();
                    DataModel.Instance.ProcessTable.Columns.Add("Status", typeof(ProcessTaskStatus));
                    DataModel.Instance.ProcessTable.Columns.Add("Index", typeof(int));
                    DataModel.Instance.ProcessTable.Columns.Add("Name", typeof(string));

                    for (int i = 0; i < statelist.Count; i++)
                    {
                        string state = statelist[i];

                        int variableCode = int.Parse(state.Substring(0, 3));
                        int methodCode = int.Parse(state.Substring(3, 3));
                        int stateCode = int.Parse(state.Substring(6, 3));

                        ProcessTaskStatus status = ProcessTaskStatus.NotExecuted;
                        int index = i;
                        string name = "";

                        int Ovennum = 0, Positionnum = 0, Layersnum = 0, rowNum = 0, columnNum = 0, WeldPostionNum = 0, speed = 0;
                        switch (variableCode)
                        {
                            case 0:
                                break;
                            case 1:
                                Ovennum = 1;
                                break;
                            case 2:
                                Ovennum = 2;
                                break;
                            case 3:
                                Positionnum = 1;
                                Ovennum = 1;
                                break;
                            case 4:
                                Positionnum = 1;
                                Layersnum = 1;
                                break;
                            case 5:
                                Positionnum = 2;
                                Layersnum = 1;
                                break;
                            case 6:
                                Positionnum = 3;
                                Layersnum = 1;
                                break;
                            case 7:
                                Positionnum = 4;
                                Layersnum = 1;
                                break;
                            case 8:
                                Positionnum = 2;
                                Layersnum = 2;
                                break;
                            case 9:
                                Positionnum = 4;
                                Layersnum = 2;
                                break;
                            case 10:
                                Positionnum = 2;
                                Ovennum = 2;
                                break;
                            case 11:
                                Layersnum = 1;
                                Ovennum = 1;
                                break;
                            case 12:
                                Layersnum = 2;
                                Ovennum = 1;
                                break;
                            case 13:
                                Layersnum = 1;
                                Ovennum = 2;
                                break;
                            case 14:
                                Layersnum = 2;
                                Ovennum = 2;
                                break;
                            case 15:
                                Positionnum = 3;
                                Layersnum = 2;
                                break;
                            case 16:
                                Ovennum = 1;
                                break;
                            case 17:
                                Ovennum = 1;
                                break;
                            case 18:
                                Ovennum = 2;
                                break;
                            case 19:
                                Ovennum = 2;
                                break;
                            case 20:
                                speed = 1;
                                break;
                            case 21:
                                speed = 2;
                                break;
                            case 22:
                                speed = 3;
                                break;
                            default:
                                throw new InvalidOperationException($"Unsupported generic parameter code: {variableCode}");
                        }

                        switch (methodCode)
                        {
                            case 0:
                                name = "模拟方法";
                                break;
                            case 1:
                                name = "初始化";
                                break;
                            case 2:
                                name = "料盒钩爪移动到空闲位置";
                                break;
                            case 3:
                                name = $"料盒搬送：打开烘箱{Ovennum}内门";
                                break;
                            case 4:
                                name = $"料盒搬送：烘箱{Ovennum}出料";
                                break;
                            case 5:
                                name = $"料盒搬送：钩爪到烘箱{Ovennum}出料盒上方";
                                break;
                            case 6:
                                name = "料盒搬送：钩爪拾取料盒";
                                break;
                            case 7:
                                name = $"料盒搬送：钩爪移动到目标位置{Positionnum}.{Layersnum}层";
                                break;
                            case 8:
                                name = "料盒搬送：钩爪放下料盒";
                                break;
                            case 9:
                                name = $"料盒搬送：方舱向烘箱{Ovennum}进料";
                                break;
                            case 10:
                                name = $"料盒搬送：关闭烘箱{Ovennum}内门";
                                break;
                            case 11:
                                name = "物料搬送：初始化压机";
                                break;
                            case 12:
                                name = "物料搬送：物料钩爪移动到安全位置";
                                break;
                            case 13:
                                name = "物料搬送：压机升降台顶升";
                                break;
                            case 14:
                                name = "物料搬送：压机升降台下降";
                                break;
                            case 15:
                                name = "物料焊接：物料钩爪移动到物料上方";
                                break;
                            case 16:
                                name = "物料焊接：物料钩爪拾取物料";
                                break;
                            case 17:
                                name = $"物料焊接：物料钩爪到：{rowNum}行{columnNum}列物料上方";
                                break;
                            case 18:
                                name = "物料焊接：物料钩爪放下物料";
                                break;
                            case 19:
                                name = "物料焊接：焊接";
                                break;
                            case 20:
                                name = "物料焊接：搬送相机识别料盘";
                                break;
                            case 21:
                                name = "物料焊接：搬送相机识别物料";
                                break;
                            case 22:
                                name = "物料焊接：焊台相机识别物料";
                                break;
                            case 23:
                                name = "物料焊接：焊接流程";
                                break;
                            case 24:
                                name = "料盒搬送：料盒钩爪移动到避让位置";
                                break;
                            case 25:
                                name = "物料搬送：物料钩爪移动到避让位置";
                                break;
                            case 26:
                                name = $"料盒搬送：烘箱{Ovennum}出料";
                                break;
                            case 27:
                                name = $"料盒搬送：烘箱{Ovennum}进料";
                                break;
                            case 28:
                                name = $"物料搬送：设置速度{speed}";
                                break;
                            case 29:
                                name = "初始化数据";
                                break;
                            default:
                                throw new InvalidOperationException($"Unsupported generic parameter code: {methodCode}");
                        }


                        DataModel.Instance.ProcessTable.Rows.Add(status, index, name);

                    }


                    DataModel.Instance.ProcessIndex = 0;

                    DataModel.Instance.OnPropertyChanged(nameof(DataModel.Instance.ProcessTable));


                }



            }
            catch
            {

            }


            return statelist;
        }


        private EnumTrainsportMaterialboxParam WeldMothed(EnumTrainsportMaterialboxParam param)
        {
            string state = "000000000";
            XktResult<string> result = new XktResult<string>();

            try
            {
                if (_transportControl.TransportRecipe != null)
                {
                    Console.WriteLine("料盒搬送：料盒焊接.");

                    #region 料盒焊接

                    Task.Factory.StartNew(new Action(() =>
                    {
                        DataModel.Instance.MaterialMat = param.MaterialMat;

                        DataModel.Instance.Materialnum = 0;
                        DataModel.Instance.Materialrow = 0;
                        DataModel.Instance.Materialcol = 0;
                    }));


                    int toweldnum = 0;
                    List<XYZTCoordinateConfig> weldpositions = new List<XYZTCoordinateConfig>();
                    List<List<int>> weldnum = new List<List<int>>();
                    List<int> weldnum2 = new List<int>();

                    if (param.Iswelded == false)
                    {
                        for (int i = 0; i < param.MaterialRowNumber; i++)
                        {
                            for (int j = 0; j < param.MaterialColNumber; j++)
                            {
                                if (isStopped)
                                {
                                    LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                                    Task.Factory.StartNew(new Action(() =>
                                    {
                                        DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                                    }));


                                    isRunning = false;
                                    return null;
                                }

                                if (param.MaterialMat[param.MaterialRowNumber - i - 1][j].Materialstate == EnumMaterialstate.Unwelded)
                                {
                                    CameraWindowGUI.Instance.ClearGraphicDraw();
                                    CameraWindowGUI.Instance.SelectCamera(0);

                                    DataModel.Instance.Materialnum = i * param.MaterialColNumber + j;
                                    DataModel.Instance.Materialcol = j;
                                    DataModel.Instance.Materialrow = i;

                                    Console.WriteLine("物料搬送：设置轴运行速度为高速");
                                    state = "022028000";
                                    result = ProcessStateMachineControl.ExecuteState(state);
                                    if (result.IsSuccess == false) { LogRecorder.RecordLog(EnumLogContentType.Warn, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null; }
                                    WaitForNext();

                                    if (isStopped)
                                    {
                                        LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                                        Task.Factory.StartNew(new Action(() =>
                                        {
                                            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                                        }));


                                        isRunning = false;
                                        return null;
                                    }


                                    state = "000021000";

                                    PointF pixelSize = new PointF()
                                    {
                                        X = _TrackCameraConfig.WidthPixelSize,
                                        Y = _TrackCameraConfig.HeightPixelSize,
                                    };

                                    param.MaterialParam.TrackCameraIdentifyMaterialMatch.SearchRoi = new RectangleFV()
                                    {
                                        X = ((float)param.MaterialMat[i][j].MaterialPosition.X - (float)param.MaterialParam.MaterialSize.X / 2) / pixelSize.X - 10,
                                        Y = ((float)param.MaterialMat[i][j].MaterialPosition.Y - (float)param.MaterialParam.MaterialSize.Y / 2) / pixelSize.Y - 10,
                                        Width = ((float)param.MaterialParam.MaterialSize.X) / pixelSize.X + 20,
                                        Height = ((float)param.MaterialParam.MaterialSize.Y) / pixelSize.Y + 20,
                                    };
                                    param.MaterialParam.TrackCameraIdentifyMaterialMatch.MaterialRow = i;
                                    param.MaterialParam.TrackCameraIdentifyMaterialMatch.MaterialCol = j;

                                    result = ProcessStateMachineControl.ExecuteState(state, param.MaterialParam.TrackCameraIdentifyMaterialMatch);

                                    if (result.IsSuccess == false)
                                    {
                                        PauseOrStepMothed();
                                        Task.Factory.StartNew(new Action(() =>
                                        {
                                            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 搬送相机识别物料失败.";
                                        }));

                                        param.MaterialMat[i][j].MaterialPosition = TransportControl.Instance.TransportRecipe.OverBox1Param.MaterialboxParam[0].MaterialMat[i][j].MaterialPosition;

                                        int Done = VisionControlclass.Instance.ShowMessage("识别错误", "搬送相机未识别到物料，是否跳料", "警告");
                                        if (Done == 1)
                                        {
                                            Task.Factory.StartNew(new Action(() =>
                                            {
                                                DataModel.Instance.MaterialMat[i][j].Materialstate = EnumMaterialstate.Jumping;
                                                DataModel.Instance.OnPropertyChanged(nameof(DataModel.MaterialMat));
                                                //DataModel.Instance.MaterialMat[i][j] = new EnumMaterialproperties() { MaterialPosition = DataModel.Instance.MaterialMat[i][j].MaterialPosition, Materialstate = EnumMaterialstate.Totheweldingstation };
                                            }));

                                            continue;
                                        }
                                        else
                                        {

                                            return null;
                                        }
                                        


                                        //LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null;

                                        

                                    }
                                    WaitForNext();

                                    Console.WriteLine("料盒焊接：物料钩爪移动到物料上方");

                                    state = "000015000";
                                    result = ProcessStateMachineControl.ExecuteState(state, new ProcessTargetPositionParam(param.MaterialMat[i][j].MaterialPosition, _transportControl.TransportRecipe.MaterialHookUp, 0, i, j));

                                    if (result.IsSuccess == false) { LogRecorder.RecordLog(EnumLogContentType.Warn, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null; }
                                    WaitForNext();

                                    if (isStopped)
                                    {
                                        LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                                        Task.Factory.StartNew(new Action(() =>
                                        {
                                            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                                        }));


                                        isRunning = false;
                                        return null;
                                    }

                                    Console.WriteLine("物料搬送：设置轴运行速度为中速");
                                    state = "021028000";
                                    result = ProcessStateMachineControl.ExecuteState(state);
                                    if (result.IsSuccess == false) { LogRecorder.RecordLog(EnumLogContentType.Warn, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null; }
                                    WaitForNext();

                                    if (isStopped)
                                    {
                                        LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                                        Task.Factory.StartNew(new Action(() =>
                                        {
                                            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                                        }));


                                        isRunning = false;
                                        return null;
                                    }


                                    Console.WriteLine("料盒焊接：物料钩爪拾取物料");

                                    state = "000016000";
                                    result = ProcessStateMachineControl.ExecuteState(state, new ProcessTargetPositionParam(param.MaterialMat[i][j].MaterialPosition, _transportControl.TransportRecipe.MaterialHookUp, 0, i, j));

                                    if (result.IsSuccess == false) { LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null; }
                                    WaitForNext();

                                    if (isStopped)
                                    {
                                        LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                                        Task.Factory.StartNew(new Action(() =>
                                        {
                                            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                                        }));


                                        isRunning = false;
                                        return null;
                                    }

                                    CameraWindowGUI.Instance.ClearGraphicDraw();
                                    CameraWindowGUI.Instance.SelectCamera(1);

                                    Console.WriteLine("料盒焊接：物料钩爪到目标位置");

                                    state = "000017000";
                                    result = ProcessStateMachineControl.ExecuteState(state, new ProcessTargetPositionParam(_transportControl.TransportRecipe.MaterialHooktoTargetPosition[toweldnum], _transportControl.TransportRecipe.MaterialHookUp2, 0, toweldnum));

                                    if (result.IsSuccess == false) { LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null; }

                                    WaitForNext();

                                    if (isStopped)
                                    {
                                        LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                                        Task.Factory.StartNew(new Action(() =>
                                        {
                                            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                                        }));


                                        isRunning = false;
                                        return null;
                                    }

                                    Console.WriteLine("料盒焊接：物料钩爪放下物料");

                                    state = "000018000";
                                    result = ProcessStateMachineControl.ExecuteState(state, new ProcessTargetPositionParam(_transportControl.TransportRecipe.MaterialHooktoTargetPosition[toweldnum], _transportControl.TransportRecipe.MaterialHookUp2, 0, toweldnum));

                                    DataModel.Instance.MaterialMat[param.MaterialRowNumber - i - 1][j].Materialstate = EnumMaterialstate.Totheweldingstation;
                                    DataModel.Instance.OnPropertyChanged(nameof(DataModel.MaterialMat));


                                    if (result.IsSuccess == false) { LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null; }
                                    WaitForNext();

                                    if (isStopped)
                                    {
                                        LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                                        Task.Factory.StartNew(new Action(() =>
                                        {
                                            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                                        }));


                                        isRunning = false;
                                        return null;
                                    }


                                    

                                    weldpositions.Add(param.MaterialMat[i][j].MaterialPosition);

                                    List<int> weldnumi = new List<int>();
                                    weldnumi.Add(i);
                                    weldnumi.Add(j);
                                    weldnum.Add(weldnumi);
                                    
                                    weldnum2.Add(i * param.MaterialColNumber + j);

                                    toweldnum++;

                                    
                                }

                                if (toweldnum == _transportControl.TransportRecipe.WeldNum || (toweldnum > 0 && i == param.MaterialRowNumber - 1 && j == param.MaterialColNumber - 1))
                                {
                                    Console.WriteLine("物料搬送：设置轴运行速度为高速");
                                    state = "022028000";
                                    result = ProcessStateMachineControl.ExecuteState(state);
                                    if (result.IsSuccess == false) { LogRecorder.RecordLog(EnumLogContentType.Warn, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null; }
                                    WaitForNext();

                                    if (isStopped)
                                    {
                                        LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                                        Task.Factory.StartNew(new Action(() =>
                                        {
                                            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                                        }));


                                        isRunning = false;
                                        return null;
                                    }

                                    Console.WriteLine("料盒焊接：物料钩爪到空闲位置");

                                    state = "000012000";
                                    result = ProcessStateMachineControl.ExecuteState(state);

                                    if (result.IsSuccess == false) { LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null; }

                                    WaitForNext();

                                    if (isStopped)
                                    {
                                        LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                                        Task.Factory.StartNew(new Action(() =>
                                        {
                                            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                                        }));


                                        isRunning = false;
                                        return null;
                                    }

                                    Console.WriteLine("料盒焊接：焊接相机识别物料状态");

                                    //state = "000022000";
                                    //result = ProcessStateMachineControl.ExecuteState(state, param.MaterialParam.WeldCameraIdentifyMaterialMatch);

                                    //if (result.IsSuccess == false)
                                    //{
                                    //    PauseOrStepMothed();
                                    //    Task.Factory.StartNew(new Action(() =>
                                    //    {
                                    //        DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 物料识别失败.";
                                    //    }));

                                    //    LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null;
                                    //}
                                    //WaitForNext();




                                    if (isStopped)
                                    {
                                        LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                                        Task.Factory.StartNew(new Action(() =>
                                        {
                                            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                                        }));


                                        isRunning = false;
                                        return null;
                                    }


                                    Console.WriteLine("物料搬送：设置轴运行速度为中速");
                                    state = "021028000";
                                    result = ProcessStateMachineControl.ExecuteState(state);
                                    if (result.IsSuccess == false) { LogRecorder.RecordLog(EnumLogContentType.Warn, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null; }
                                    WaitForNext();

                                    if (isStopped)
                                    {
                                        LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                                        Task.Factory.StartNew(new Action(() =>
                                        {
                                            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                                        }));


                                        isRunning = false;
                                        return null;
                                    }

                                    Console.WriteLine("料盒焊接：升降轴降");

                                    state = "000014000";
                                    result = ProcessStateMachineControl.ExecuteState(state);
                                    if (result.IsSuccess == false) { LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null; }
                                    WaitForNext();

                                    if (isStopped)
                                    {
                                        LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                                        Task.Factory.StartNew(new Action(() =>
                                        {
                                            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                                        }));


                                        isRunning = false;
                                        return null;
                                    }

                                    Console.WriteLine("料盒焊接：升降轴升");

                                    state = "000013000";
                                    result = ProcessStateMachineControl.ExecuteState(state);

                                    if (result.IsSuccess == false) { LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null; }
                                    WaitForNext();

                                    if (isStopped)
                                    {
                                        LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                                        Task.Factory.StartNew(new Action(() =>
                                        {
                                            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                                        }));


                                        isRunning = false;
                                        return null;
                                    }

                                    Console.WriteLine("料盒焊接：升降轴降");

                                    state = "000014000";
                                    result = ProcessStateMachineControl.ExecuteState(state);
                                    if (result.IsSuccess == false) { LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null; }
                                    WaitForNext();

                                    if (isStopped)
                                    {
                                        LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                                        Task.Factory.StartNew(new Action(() =>
                                        {
                                            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                                        }));


                                        isRunning = false;
                                        return null;
                                    }

                                    for (int i_w = 0; i_w < toweldnum; i_w++)
                                    {
                                        state = "000022000";
                                        result = ProcessStateMachineControl.ExecuteState(state, param.MaterialParam.WeldCameraIdentifyMaterialMatchs[i_w]);

                                        if (result.IsSuccess == false)
                                        {
                                            PauseOrStepMothed();
                                            Task.Factory.StartNew(new Action(() =>
                                            {
                                                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 物料识别失败.";
                                            }));

                                            LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n");
                                        }
                                        WaitForNext();
                                    }

                                    CameraWindowGUI.Instance.ClearGraphicDraw();
                                    CameraWindowGUI.Instance.SelectCamera(1);

                                    Console.WriteLine("料盒焊接：焊接物料");

                                    state = "000019000";
                                    result = ProcessStateMachineControl.ExecuteState(state);

                                    if (result.IsSuccess == false) { LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null; }
                                    WaitForNext();

                                    

                                    if (isStopped)
                                    {
                                        LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                                        Task.Factory.StartNew(new Action(() =>
                                        {
                                            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                                        }));


                                        isRunning = false;
                                        return null;
                                    }

                                    for (int i_w = 0; i_w < toweldnum; i_w++)
                                    {
                                        state = "000022000";
                                        result = ProcessStateMachineControl.ExecuteState(state, param.MaterialParam.WeldCameraIdentifyMaterialMatchs[i_w]);

                                        if (result.IsSuccess == false)
                                        {
                                            PauseOrStepMothed();
                                            Task.Factory.StartNew(new Action(() =>
                                            {
                                                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 物料识别失败.";
                                            }));

                                            LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n");
                                        }
                                        WaitForNext();
                                    }

                                    CameraWindowGUI.Instance.ClearGraphicDraw();
                                    CameraWindowGUI.Instance.SelectCamera(1);

                                    Console.WriteLine("料盒焊接：升降轴升");

                                    state = "000013000";
                                    result = ProcessStateMachineControl.ExecuteState(state);

                                    if (result.IsSuccess == false) { LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null; }
                                    WaitForNext();

                                    if (isStopped)
                                    {
                                        LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                                        Task.Factory.StartNew(new Action(() =>
                                        {
                                            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                                        }));


                                        isRunning = false;
                                        return null;
                                    }


                                    for (int i0 = 0; i0 < toweldnum; i0++)
                                    {
                                        CameraWindowGUI.Instance.ClearGraphicDraw();
                                        CameraWindowGUI.Instance.SelectCamera(1);

                                        Console.WriteLine("物料搬送：设置轴运行速度为高速");
                                        state = "022028000";
                                        result = ProcessStateMachineControl.ExecuteState(state);
                                        if (result.IsSuccess == false) { LogRecorder.RecordLog(EnumLogContentType.Warn, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null; }
                                        WaitForNext();

                                        if (isStopped)
                                        {
                                            LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                                            Task.Factory.StartNew(new Action(() =>
                                            {
                                                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                                            }));


                                            isRunning = false;
                                            return null;
                                        }

                                        //DataModel.Instance.Materialnum = weldnum2[toweldnum - i0 - 1];
                                        DataModel.Instance.Materialcol = weldnum[i0][1];
                                        DataModel.Instance.Materialrow = weldnum[i0][0];

                                        

                                        Console.WriteLine("料盒焊接：物料钩爪到目标位置");

                                        state = "000017000";
                                        result = ProcessStateMachineControl.ExecuteState(state, new ProcessTargetPositionParam(_transportControl.TransportRecipe.MaterialHooktoTargetPosition[toweldnum - i0 - 1], _transportControl.TransportRecipe.MaterialHookUp2, 0, toweldnum - i0 - 1));

                                        if (result.IsSuccess == false) { LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null; }
                                        WaitForNext();

                                        if (isStopped)
                                        {
                                            LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                                            Task.Factory.StartNew(new Action(() =>
                                            {
                                                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                                            }));


                                            isRunning = false;
                                            return null;
                                        }

                                        Console.WriteLine("物料搬送：设置轴运行速度为中速");
                                        state = "021028000";
                                        result = ProcessStateMachineControl.ExecuteState(state);
                                        if (result.IsSuccess == false) { LogRecorder.RecordLog(EnumLogContentType.Warn, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null; }
                                        WaitForNext();

                                        if (isStopped)
                                        {
                                            LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                                            Task.Factory.StartNew(new Action(() =>
                                            {
                                                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                                            }));


                                            isRunning = false;
                                            return null;
                                        }

                                        Console.WriteLine("料盒焊接：物料钩爪拾取物料");

                                        state = "000016000";
                                        result = ProcessStateMachineControl.ExecuteState(state, new ProcessTargetPositionParam(_transportControl.TransportRecipe.MaterialHooktoTargetPosition[toweldnum - i0 - 1], _transportControl.TransportRecipe.MaterialHookUp2, 0, toweldnum - i0 - 1));

                                        if (result.IsSuccess == false) { LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null; }
                                        WaitForNext();

                                        if (isStopped)
                                        {
                                            LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                                            Task.Factory.StartNew(new Action(() =>
                                            {
                                                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                                            }));


                                            isRunning = false;
                                            return null;
                                        }

                                        CameraWindowGUI.Instance.ClearGraphicDraw();
                                        CameraWindowGUI.Instance.SelectCamera(0);

                                        Console.WriteLine("料盒焊接：物料钩爪移动到物料上方");

                                        state = "000015000";
                                        result = ProcessStateMachineControl.ExecuteState(state, new ProcessTargetPositionParam(weldpositions[toweldnum - i0 - 1], _transportControl.TransportRecipe.MaterialHookUp, 0, weldnum[toweldnum - i0 - 1][0], weldnum[toweldnum - i0 - 1][1]));

                                        if (result.IsSuccess == false) { LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null; }
                                        WaitForNext();

                                        if (isStopped)
                                        {
                                            LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                                            Task.Factory.StartNew(new Action(() =>
                                            {
                                                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                                            }));


                                            isRunning = false;
                                            return null;
                                        }


                                        Console.WriteLine("料盒焊接：物料钩爪放下物料");

                                        state = "000018000";
                                        result = ProcessStateMachineControl.ExecuteState(state, new ProcessTargetPositionParam(weldpositions[toweldnum - i0 - 1], _transportControl.TransportRecipe.MaterialHookUp2, 0, weldnum[toweldnum - i0 - 1][0], weldnum[toweldnum - i0 - 1][1]));


                                        DataModel.Instance.MaterialMat[param.MaterialRowNumber - weldnum[toweldnum - i0 - 1][0] - 1][weldnum[toweldnum - i0 - 1][1]].Materialstate = EnumMaterialstate.Welded;
                                        DataModel.Instance.OnPropertyChanged(nameof(DataModel.MaterialMat));
                                        DataModel.Instance.WeldMaterialNumber++;
                                        SystemConfiguration.Instance.StatisticalDataConfig.WeldMaterialNumber = DataModel.Instance.WeldMaterialNumber;



                                        if (result.IsSuccess == false) { LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null; }
                                        WaitForNext();

                                        if (isStopped)
                                        {
                                            LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                                            Task.Factory.StartNew(new Action(() =>
                                            {
                                                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                                            }));


                                            isRunning = false;
                                            return null;
                                        }


                                        
                                        
                                    }
                                    weldnum2.Clear();
                                    weldpositions.Clear();
                                    weldnum.Clear();
                                    toweldnum = 0;

                                    Console.WriteLine("物料搬送：设置轴运行速度为高速");
                                    state = "022028000";
                                    result = ProcessStateMachineControl.ExecuteState(state);

                                    if (result.IsSuccess == false) { LogRecorder.RecordLog(EnumLogContentType.Warn, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null; }
                                    WaitForNext();

                                    if (isStopped)
                                    {
                                        LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                                        Task.Factory.StartNew(new Action(() =>
                                        {
                                            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                                        }));


                                        isRunning = false;
                                        return null;
                                    }

                                    Console.WriteLine("料盒焊接：物料钩爪到空闲位置");

                                    state = "000012000";
                                    result = ProcessStateMachineControl.ExecuteState(state);

                                    if (result.IsSuccess == false) { LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null; }
                                    WaitForNext();

                                    if (isStopped)
                                    {
                                        LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                                        Task.Factory.StartNew(new Action(() =>
                                        {
                                            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                                        }));


                                        isRunning = false;
                                        return null;
                                    }
                                }

                                if (i == param.MaterialRowNumber - 1 && j == param.MaterialColNumber - 1)
                                {
                                    param.Iswelded = true;
                                }
                            }
                        }



                    }

                    #endregion

                    CameraWindowGUI.Instance.ClearGraphicDraw();
                    CameraWindowGUI.Instance.SelectCamera(0);

                    Console.WriteLine("物料搬送：设置轴运行速度为中速");
                    state = "021028000";
                    result = ProcessStateMachineControl.ExecuteState(state);

                    if (result.IsSuccess == false) { LogRecorder.RecordLog(EnumLogContentType.Warn, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n"); return null; }
                    WaitForNext();

                    if (isStopped)
                    {
                        LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                        Task.Factory.StartNew(new Action(() =>
                        {
                            DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                        }));


                        isRunning = false;
                        return null;
                    }


                    Console.WriteLine("料盒搬送：物料焊接完成.");

                    DataModel.Instance.Materialnum = 0;
                    DataModel.Instance.Materialrow = 0;
                    DataModel.Instance.Materialcol = 0;


                    int wnum = 0;
                    while (!param.Iswelded == true)
                    {
                        Thread.Sleep(singleDelay);

                        if ((wnum * singleDelay) > Delay)
                        {
                            Console.WriteLine("料盒搬送：物料焊接异常，焊接未完成");
                            return null;
                        }
                    }

                }


            }
            catch (Exception ex)
            {

            }

            return param;

        }

        private void UpdateNum(int positionCode)
        {
            object parameter = ProcessStateMachineControl.GetPositionByCode(positionCode);

            if(positionCode < 1)
            {
                LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:位置未加载: \n");

                DataModel.Instance.State = EnumOvenBoxState.None;

               isRunning = false;
                return;
            }

            if (parameter == null)
            {
                LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:位置未加载: \n");

                DataModel.Instance.State = EnumOvenBoxState.None;

                isRunning = false;
                return;
            }
            if(positionCode < 7)
            {
                if (((EnumPosition)parameter).Num == EnumOvenBoxNum.Oven1)
                {
                    Task.Factory.StartNew(new Action(() =>
                    {
                        DataModel.Instance.Ovennum = 1;
                    }));
                }
                else if (((EnumPosition)parameter).Num == EnumOvenBoxNum.Oven2)
                {
                    Task.Factory.StartNew(new Action(() =>
                    {
                        DataModel.Instance.Ovennum = 2;
                    }));
                }
                if (((EnumPosition)parameter).MaterialBoxNum == 1)
                {
                    Task.Factory.StartNew(new Action(() =>
                    {
                        DataModel.Instance.Materialboxnum = 1;
                    }));
                }
                else if (((EnumPosition)parameter).MaterialBoxNum == 2)
                {
                    Task.Factory.StartNew(new Action(() =>
                    {
                        DataModel.Instance.Materialboxnum = 2;
                    }));
                }
                else if (((EnumPosition)parameter).MaterialBoxNum == 0)
                {
                    Task.Factory.StartNew(new Action(() =>
                    {
                        DataModel.Instance.Materialboxnum = 0;
                    }));
                }

            }
            else if(positionCode > 6 && positionCode < 12)
            {
                DataModel.Instance.State = ((EnumOvenStates)parameter).State;

            }


        }

        private void RunMothed()
        {
            try 
            {
                bool JumpMaterialbox = false;
                Task.Factory.StartNew(new Action(() =>
                {
                    DataModel.Instance.Ovennum = 0;
                    DataModel.Instance.Materialboxnum = 0;
                    DataModel.Instance.Materialnum = 0;
                    DataModel.Instance.Materialrow = 0;
                    DataModel.Instance.Materialcol = 0;

                    List<List<EnumMaterialproperties>> param = new List<List<EnumMaterialproperties>>();

                    if(_transportControl.TransportRecipe?.OverBox1Param?.MaterialboxParam.Count > 0)
                    {
                        param = _transportControl.TransportRecipe.OverBox1Param.MaterialboxParam[0].MaterialMat;
                    }
                    else if(_transportControl.TransportRecipe?.OverBox2Param?.MaterialboxParam.Count > 0)
                    {
                        param = _transportControl.TransportRecipe.OverBox1Param.MaterialboxParam[0].MaterialMat;
                    }

                    DataModel.Instance.MaterialMat = param;
                }));
                if (_transportControl.TransportRecipe != null)
                {
                    List<string> statelist = ProcessStateList();
                    XktResult<string> result = new XktResult<string>();

                    for (int i = 0; i < statelist.Count; i++)
                    {
                        DataModel.Instance.ProcessIndex = i;

                        WaitForNext();

                        i = DataModel.Instance.ProcessIndex;

                        string state = statelist[i];

                        int variableCode = int.Parse(state.Substring(0, 3));
                        int methodCode = int.Parse(state.Substring(3, 3));
                        int stateCode = int.Parse(state.Substring(6, 3));

                        UpdateNum(stateCode);

                        ProcessTaskStatus status = ProcessTaskStatus.InProgress;
                        DataModel.Instance.ProcessTable.Rows[i]["Status"] = status;
                        DataModel.Instance.OnPropertyChanged(nameof(DataModel.Instance.ProcessTable));


                        if (isStopped)
                        {
                            LogRecorder.RecordLog(EnumLogContentType.Info, $"流程终止: \n");
                            Task.Factory.StartNew(new Action(() =>
                            {
                                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程终止.";
                            }));
                            

                            isRunning = false;
                            return;
                        }

                        if (state.Length != 9)
                            throw new ArgumentException("State must be a 9-character string.");


                        #region 焊接流程

                        


                        if (methodCode == 20)
                        {
                            

                            object parameter = ProcessStateMachineControl.GetParameterByCode(variableCode);

                            if (parameter == null)
                            {
                                LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:焊接错误，未正确加载参数: \n");
                                isRunning = false;
                                return;
                            }

                            result = ProcessStateMachineControl.ExecuteState(state, ((ProcessTrainsportMaterialboxParam)parameter).param.TrackCameraIdentifyMaterialBoxMatch);


                            if (result.IsSuccess == false)
                            {
                                int Done = VisionControlclass.Instance.ShowMessage("识别错误", "搬送相机未识别到料盘，是否跳料盘", "警告");
                                if (Done == 1)
                                {
                                    JumpMaterialbox = true;
                                    continue;
                                }
                                else
                                {
                                    int Done1 = VisionControlclass.Instance.ShowMessage("识别错误", "搬送相机未识别到料盘，是否继续焊接", "警告");

                                    if (Done == 1)
                                    {
                                        JumpMaterialbox = false;
                                        continue;
                                    }

                                    return;
                                }

                                //LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n");
                                //isRunning = false;
                                //return;
                            }



                            LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:|{state}| 信息:{result.ErrorCode}|{result.Content}|{result.Message} \n");

                            

                        }

                        if (methodCode == 23)
                        {
                            if(JumpMaterialbox)
                            {
                                JumpMaterialbox = false;

                                continue;
                            }

                            object parameter = ProcessStateMachineControl.GetParameterByCode(variableCode);

                            if (parameter == null)
                            {
                                LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:焊接错误，未正确加载参数: \n");
                                isRunning = false;
                                return;
                            }

                            EnumTrainsportMaterialboxParam param = WeldMothed(((ProcessTrainsportMaterialboxParam)parameter).param);

                            if (param == null)
                            {
                                LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:焊接错误: \n");
                                isRunning = false;
                                return;
                            }
                        }

                        #endregion

                        #region 搬送流程



                        if(methodCode != 23 && methodCode != 20)
                        {
                            result = ProcessStateMachineControl.ExecuteState(state);

                            if (result.IsSuccess == false)
                            {
                                LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:|{state}| 错误:{result.ErrorCode}|{result.Content}|{result.Message} \n");
                                isRunning = false;
                                return;
                            }



                            LogRecorder.RecordLog(EnumLogContentType.Info, $"当前状态机:|{state}| 信息:{result.ErrorCode}|{result.Content}|{result.Message} \n");

                            
                        }




                        #endregion


                        if (!result.IsSuccess)
                        {
                            Task.Factory.StartNew(new Action(() =>
                            {
                                DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 流程错误，流程已经暂停.";
                            }));
                            PauseOrStepMothed();
                            WaitForNext();
                        }

                        DataModel.Instance.ProcessTable.Rows[i]["Status"] = ProcessTaskStatus.Completed;
                        DataModel.Instance.OnPropertyChanged(nameof(DataModel.Instance.ProcessTable));

                    }

                    LogRecorder.RecordLog(EnumLogContentType.Info, $"生产流程结束: \n");
                    Task.Factory.StartNew(new Action(() =>
                    {
                        DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 生产流程结束.";
                    }));

                }
                else
                {
                    Task.Factory.StartNew(new Action(() =>
                    {
                        DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "配方为空，无法执行.";
                    }));

                    

                    Console.WriteLine("配方为空，无法执行.");
                    isRunning = false;
                }

            }
            catch(ThreadInterruptedException)
            {
                Task.Factory.StartNew(new Action(() =>
                {
                    DataModel.Instance.JobLogText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "流程异常.";
                }));
                

                isRunning = false;
            }


            
        }





        #endregion


        #region public mothed

        public void SetRecipe(TransportRecipe recipe)
        {
            try
            {
                this.recipe = recipe;
                _transportControl.SetTransportParam(recipe);
                _transportControl.Oven1Vacuum = this.Oven1Vacuum;
                _transportControl.Oven2Vacuum = this.Oven2Vacuum;
                _transportControl.BoxVacuum = this.BoxVacuum;
                _transportControl.VacuumD = this.VacuumD;
                _transportControl.VacuumC = this.VacuumC;
                _transportControl.singleDelay = this.singleDelay;
                _transportControl.Delay = this.Delay;

                ProcessStateList();
            }
            catch(Exception ex)
            {

            }
        }

        public void Run()
        {
            try
            {
                StartMothed();
            }
            catch (Exception ex)
            {

            }
        }

        public void PausedOrSingle()
        {
            try
            {
                PauseOrStepMothed();
            }
            catch (Exception ex)
            {

            }
        }

        public void Continue()
        {
            try
            {
                ContinueMothed();
            }
            catch (Exception ex)
            {

            }
        }

        public void Stop()
        {
            try
            {
                Task.Run(() =>
                {
                    StopMothed();
                });
                
            }
            catch (Exception ex)
            {

            }
        }


        #endregion


    }

}
