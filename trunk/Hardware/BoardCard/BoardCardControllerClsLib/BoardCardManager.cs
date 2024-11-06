using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardCardControllerClsLib
{
    public class BoardCardManager
    {
        #region 单例
        private static readonly object _lockObj = new object();
        private static volatile BoardCardManager _instance = null;
        public static BoardCardManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new BoardCardManager();
                        }
                    }
                }
                return _instance;
            }
        }
        private BoardCardManager()
        {
        }
        #endregion

        IBoardCardController _CurrentBoardControl = null;
        public void Initialize()
        {
            _CurrentBoardControl = new GoogolBoardCardController();
            _CurrentBoardControl.Connect();
            _CurrentBoardControl.MotioParaInit();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Shutdown()
        {

        }

        public IBoardCardController GetCurrentController()
        {
            if (_CurrentBoardControl == null)
            {
            }
            return _CurrentBoardControl;
        }




    }
}
