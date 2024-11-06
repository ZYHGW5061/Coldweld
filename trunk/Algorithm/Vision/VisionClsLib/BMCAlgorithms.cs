using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionClsLib
{
    public class BMCAlgorithms
    {

        #region Private Method

        static double CalculateMean(double[] data)
        {
            double sum = 0;
            foreach (double value in data)
            {
                sum += value;
            }
            return sum / data.Length;
        }

        static double CalculateStandardDeviation(double[] data, double mean)
        {
            double sumOfSquares = 0;
            foreach (double value in data)
            {
                sumOfSquares += Math.Pow(value - mean, 2);
            }
            return Math.Sqrt(sumOfSquares / data.Length);
        }


        #endregion



        /// <summary>
        /// 计算3σ-99.73
        /// </summary>
        /// <param name="Data">数据源</param>
        /// <param name="Mean">平均值</param>
        /// <param name="StandardDeviation">标准差</param>
        /// <param name="MaxNum">最大值</param>
        /// <param name="MinNum">最小值</param>
        /// <param name="Outliers">3sigma以外的个数</param>
        /// <returns></returns>
        public static bool NormalDistributionCal(float[] Data, ref double Mean, ref double StandardDeviation, ref double MaxNum, ref double MinNum, ref int Outliers)
        {
            try
            {
                double[] geneticData = Data.Select(f => (double)f).ToArray();

                double mean = CalculateMean(geneticData);
                double standardDeviation = CalculateStandardDeviation(geneticData, mean);
                double maxnum = Data.Max();
                double minnum = Data.Min();

                double lowerBound = mean - 3 * standardDeviation;
                double upperBound = mean + 3 * standardDeviation;

                int outliers = 0;
                foreach (double value in geneticData)
                {
                    if (value < lowerBound || value > upperBound)
                    {
                        outliers++;
                    }
                }
                Mean = mean;
                StandardDeviation = standardDeviation;
                Outliers = outliers;

                MaxNum = maxnum;
                MinNum = minnum;
                if (outliers>0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
                
            }
            catch
            {
                return false;
            }
        }
    }
}
