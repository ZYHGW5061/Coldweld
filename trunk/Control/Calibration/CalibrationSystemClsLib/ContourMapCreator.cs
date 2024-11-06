using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalibrationSystemClsLib
{
    public class ContourMapCreator
    {
        #region 单例
        private static ContourMapCreator Instance = new ContourMapCreator();
        private ContourMapCreator()
        { }

        public static ContourMapCreator GetHandler()
        {
            return Instance;
        }
        #endregion

        /// <summary>
        /// 激光测距仪在System坐标系中的坐标位置
        /// </summary>
        private float _laserCenterX = -81.0731f, _laserCenterY = 0.0929f;

        /// <summary>
        /// 激光测距仪.
        /// </summary>
        private ILaserDisplacement _laserDisplacement
        {
            get { return HardwareManager.GetHandler().LaserDisplacement; }
        }

        /// <summary>
        /// 定位系统
        /// </summary>
        private PositioningSystem _positioningSystem
        {
            get { return PositioningSystem.GetHandler(); }
        }

        public void SaveSamplePointsToFile(string filePath, List<List<ContourMapPoint>> pointList)
        {
            /// 保存采样点的坐标
            TextWriter tw = new StreamWriter(filePath);
            tw.WriteLine(string.Format("R{0} C{1}", pointList.Count.ToString(), pointList[0].Count.ToString()));
            for (int r = 0; r < pointList.Count; r++)
            {
                bool isTorRight = r % 2 == 0;
                var samplePositions = isTorRight ? pointList[r].OrderBy(p => p.XMapping).ToList() : pointList[r].OrderByDescending(p => p.XMapping).ToList();
                for (int c = 0; c < pointList[r].Count; c++)
                {
                    samplePositions[c].ZMapping = 0;
                    tw.Write(string.Format("({0},{1},{2}) ", samplePositions[c].XMapping, samplePositions[c].YMapping, samplePositions[c].ZMapping));
                }
                tw.WriteLine("");
                tw.Flush();
            }
            tw.Dispose();
            tw = null;
        }

        /// <summary>
        /// 从文件中读取采样点数据
        /// </summary>
        /// <param name="mappingFilePath">文件路径</param>
        /// <returns></returns>
        public List<List<ContourMapPoint>> Read3dMappingData(string mappingFilePath)
        {
            List<List<ContourMapPoint>> samplePointList = new List<List<ContourMapPoint>>();
            if (File.Exists(mappingFilePath))
            {
                samplePointList.Clear();
            }

            //读取3d mapping 校准数据
            TextReader tr = new StreamReader(mappingFilePath);
            string rowcolumns = tr.ReadLine();
            rowcolumns.Split(' ');

            double max = -2, min = 2;
            while (tr.Peek() != -1)
            {
                string data = tr.ReadLine();
                if (data.Trim() == "") { continue; }
                List<ContourMapPoint> threeDdata = new List<ContourMapPoint>();
                var rowdata = data.Split((" ").ToArray(), StringSplitOptions.RemoveEmptyEntries);
                for (int c = 0; c < rowdata.Length; c++)
                {
                    var mapping = rowdata[c].Replace("(", "").Replace(")", "").Split(',');
                    var mappingX = Convert.ToDouble(mapping[0]);
                    var mappingY = Convert.ToDouble(mapping[1]);
                    var mappingZ = Convert.ToDouble(mapping[2]);
                    if (mappingZ != 0)
                    {
                        if (mappingZ > max)
                        {
                            max = mappingZ;
                        }
                        else if (mappingZ < min)
                        {
                            min = mappingZ;
                        }
                    }
                    threeDdata.Add(new ContourMapPoint() { XMapping = mappingX, YMapping = mappingY, ZMapping = mappingZ });
                }
                samplePointList.Add(threeDdata);
            }
            tr.Dispose();
            tr = null;

            var middle = 0f;
            for (int r = 0; r < samplePointList.Count; r++)
            {
                if (samplePointList[r] != null)
                {
                    for (int c = 0; c < samplePointList[r].Count; c++)
                    {
                        if (samplePointList[r][c].ZMapping != 0)
                        {
                            samplePointList[r][c].ZMapping = (samplePointList[r][c].ZMapping - middle);
                        }
                    }
                }
            }
            return samplePointList;
        }

        /// <summary>
        /// 采集创建等高线需要的样本点并保存到文件中
        /// </summary>
        public List<List<ContourMapPoint>> PickDataforContourmap(List<List<ContourMapPoint>> pointList, string filePath, double radius = 100, EnumCoordCategory category = EnumCoordCategory.Stage)
        {
            ///
            /// 3dmapping.txt文件格式
            /// R行数 C列数
            /// R行号
            /// (X,Y,Z) (X,Y,Z) (X,Y,Z) ......
            ///
            var theta = _positioningSystem.ReadCurrentSystemCoord().Theta;
            TextWriter tw = new StreamWriter(filePath);
            tw.WriteLine(string.Format("R{0} C{1}", pointList.Count.ToString(), pointList[0].Count.ToString()));
            List<List<ContourMapPoint>> tmpPointList = new List<List<ContourMapPoint>>();
            for (int r = 0; r < pointList.Count; r++)
            {
                bool isTorRight = r % 2 == 0;
                var samplePositions = isTorRight ? pointList[r].OrderBy(p => p.XMapping).ToList() : pointList[r].OrderByDescending(p => p.XMapping).ToList();
                for (int c = 0; c < pointList[r].Count; c++)
                {
                    if (Skyverse.BIRCH.GlobalSettingClsLib.CommonProcess.JudgeInCircleArea(samplePositions[c].XMapping - _laserCenterX, samplePositions[c].YMapping - _laserCenterY, radius))
                    {
                        //蛇形运动到采样点  X=-80.9953,Y=0.5168
                        _positioningSystem.MoveToWaferCoordWithoutZAxis(new XYZTCoordinate() { X = samplePositions[c].XMapping, Y = samplePositions[c].YMapping, Z = samplePositions[c].ZMapping, Theta = theta }, EnumCoordSetType.Absolute);

                        bool isOverRangeAtPisitiveSide = false, isOverRangeAtNegetiveSide = false;
                        //采集激光测距仪数据
                        samplePositions[c].ZMapping = _laserDisplacement.GetValue(ref isOverRangeAtPisitiveSide, ref isOverRangeAtNegetiveSide)[0];
                        if (double.Equals(samplePositions[c].ZMapping, double.NaN))
                        {
                            samplePositions[c].ZMapping = 0;
                        }
                    }
                    else
                    {
                        samplePositions[c].ZMapping = 0;
                    }

                    if(category == EnumCoordCategory.Stage)
                    {
                        var stagePos = _positioningSystem.CoordConverter.ConvertWaferCoordToStageCoord(new XYZTCoordinate() { X = samplePositions[c].XMapping, Y = samplePositions[c].YMapping, Z = samplePositions[c].ZMapping }, EnumCoordSetType.Absolute);
                        //转为Stage坐标用以进行3d mapping。
                        samplePositions[c].XMapping = stagePos.X;
                        samplePositions[c].YMapping = stagePos.Y;
                    }

                    //存为文件格式用以后续校准使用
                    tw.Write(string.Format("({0},{1},{2}) ", samplePositions[c].XMapping, samplePositions[c].YMapping, samplePositions[c].ZMapping));
                }

                // 保存坐标转化后的采样点集合
                tmpPointList.Add(samplePositions);
                tw.WriteLine("");
                tw.Flush();
            }
            tw.Dispose();
            tw = null;
            return tmpPointList;
        }

        /// <summary>
        /// 获取stage坐标下的采样点集合
        /// </summary>
        /// <param name="pointListInWaferCoordinate"></param>
        /// <returns></returns>
        public List<List<ContourMapPoint>> GetSamplePointsInStageCoordinate(List<List<ContourMapPoint>> pointListInWaferCoordinate)
        {
            var pointListInStageCoord = Skyverse.BIRCH.GlobalSettingClsLib.CommonProcess.Clone(pointListInWaferCoordinate);

            foreach (var list in pointListInStageCoord)
            {
                foreach(var point in list)
                {
                    var stagePos = _positioningSystem.CoordConverter.ConvertWaferCoordToStageCoord(new XYZTCoordinate() { X = point.XMapping, Y = point.YMapping, Z = point.ZMapping }, EnumCoordSetType.Absolute);
                    point.XMapping = stagePos.X;
                    point.YMapping = stagePos.Y;
                }
            }
            return pointListInStageCoord;
        }

        /// <summary>
        /// 关闭stage的3d mapping 补偿功能
        /// </summary>
        public void Close3dMapping()
        {
            //关闭3d mapping补偿
            var stage = (HardwareManager.GetHandler().Stage as IStageController);
            if (stage != null)
            {
                stage.Close3dMapping();
            }
        }
    }
}
