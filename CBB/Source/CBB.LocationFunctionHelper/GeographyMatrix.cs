using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBB.LocationFunctionHelper
{
    /// <summary>
    /// 地理矩阵相关方法
    /// </summary>
    public class GeographyMatrix
    {
        /// <summary>
        /// 地理矩阵级别
        /// </summary>
        public static int MatrixLevel = 12;

        /// <summary>
        /// 获取一个经纬度范围的地理矩阵列表
        /// </summary>
        /// <param name="coordinateObj">经纬度</param>
        /// <param name="raidus">范围（单位米）</param>
        /// <returns>矩阵编码列表</returns>
        public static IList<String> GetGeographyMatrix(Coordinate coordinateObj, int raidus)
        {
            //获取范围内最大和最小的经纬度值
            // return minLat,minLng,maxLat,maxLng
            double[] minAndMaxLatLng = DistanceAndAroundCalculator.getAround(coordinateObj.Latitude, coordinateObj.Longitude, raidus);
            
            //获取范围顶端的坐标
            Coordinate topCoord = new Coordinate(coordinateObj.Longitude, minAndMaxLatLng[2]);
            //获取范围底端的坐标
            Coordinate bottomCoord = new Coordinate(coordinateObj.Longitude, minAndMaxLatLng[0]);
            //获取范围最左边的坐标
            Coordinate leftCoord = new Coordinate(minAndMaxLatLng[1], coordinateObj.Latitude);
            //获取范围最右边的坐标
            Coordinate RightCoord = new Coordinate(minAndMaxLatLng[3], coordinateObj.Latitude);

            //获取中心、上、下、左、右各点矩阵
            String centerMatrix = GetQuadtreeAddress(coordinateObj.Longitude, coordinateObj.Latitude);
            String topMatrix = GetQuadtreeAddress(topCoord.Longitude, topCoord.Latitude);
            String bottomMatrix = GetQuadtreeAddress(bottomCoord.Longitude, bottomCoord.Latitude);
            String leftMatrix = GetQuadtreeAddress(leftCoord.Longitude, leftCoord.Latitude);
            String rightMatrix = GetQuadtreeAddress(RightCoord.Longitude, RightCoord.Latitude);

            //将不同的矩阵编码记录进字典
            List<String> martixList = new List<string>();
            martixList.Add(centerMatrix);
            if (topMatrix != centerMatrix) martixList.Add(topMatrix);
            if (bottomMatrix != centerMatrix) martixList.Add(bottomMatrix);
            if (leftMatrix != centerMatrix) martixList.Add(leftMatrix);
            if (rightMatrix != centerMatrix) martixList.Add(rightMatrix);
            //上矩阵和右矩阵都超边距，需要取回右上矩阵编码 （maxLng,maxLat）
            if ((topMatrix != centerMatrix) & (rightMatrix != centerMatrix))
            {
                String topRightMartix = GetQuadtreeAddress(minAndMaxLatLng[3], minAndMaxLatLng[2]);
                martixList.Add(topRightMartix);
            }
            //下矩阵和右矩阵都超边距，需要取回右下矩阵编码（maxLng,minLat）
            if ((bottomMatrix != centerMatrix) & (rightMatrix != centerMatrix))
            {
                String bottomRightMartix = GetQuadtreeAddress(minAndMaxLatLng[3], minAndMaxLatLng[0]);
                martixList.Add(bottomRightMartix);
            }
            //下矩阵和左矩阵都超边距，需要取回左下矩阵编码 （minLng,minLat）
            if ((bottomMatrix != centerMatrix) & (leftMatrix != centerMatrix))
            {
                String bottomLeftMartix = GetQuadtreeAddress(minAndMaxLatLng[1], minAndMaxLatLng[0]);
                martixList.Add(bottomLeftMartix);
            }
            //下矩阵和左矩阵都超边距，需要取回左下矩阵编码 (minLng,maxLat）
            if ((topMatrix != centerMatrix) & (leftMatrix != centerMatrix))
            {
                String topLeftMartix = GetQuadtreeAddress(minAndMaxLatLng[1], minAndMaxLatLng[2]);
                martixList.Add(topLeftMartix);
            }

            return martixList;
        }
        /// <summary>
        /// 获取一个经纬度的矩阵地址编码
        /// </summary>
        /// <param name="coorddinateObj">经纬度</param>
        /// <returns>矩阵地址编码</returns>
        public static String GetQuadtreeAddress(Coordinate coorddinateObj)
        {
            return GetQuadtreeAddress(coorddinateObj.Longitude, coorddinateObj.Latitude);
        }
        /// <summary>
        /// 获取一个经纬度的矩阵地址编码
        /// </summary>
        /// <param name="lng">纬度</param>
        /// <param name="lat">经度</param>
        /// <returns>矩阵地址编码</returns>
        private static String GetQuadtreeAddress(double lng, double lat)
        {
            // now convert to normalized square coordinates
            // use standard equations to map into mercator projection
            double x = (180.0 + lng) / 360.0;
            double y = MercatorToNormal(lat);

	        String quad = "t";	// google addresses start with t
            String lookup = "qrts";	// tl tr bl br

            //level 12 = 每个矩阵边长9783米
            for (int digits = 0; digits < MatrixLevel; digits++)
            {
                // make sure we only look at fractional part
                x -= Math.Floor(x);
                y -= Math.Floor(y);

                quad = quad + lookup.Substring((x >= 0.5 ? 1 : 0) + (y >= 0.5 ? 2 : 0), 1);

                // now descend into that square
                x *= 2;
                y *= 2;
            }

            return quad;
        }

        private static double MercatorToNormal(double y)
        {
            y = -y * Math.PI / 180;	// convert to radians
            y = Math.Sin(y);
            y = (1 + y) / (1 - y);
            y = 0.5 * Math.Log(y);
            y *= 1.0 / (2 * Math.PI);	// scale factor from radians to normalized 
            y += 0.5;	// and make y range from 0 - 1
            return y;
        }

        private static double NormalToMercator(double y)
        {
            y -= 0.5;
            y *= 2 * Math.PI;
            y = Math.Exp(y * 2);
            y = (y - 1) / (y + 1);
            y = Math.Asin(y);
            y = y * -180 / Math.PI;
            return y;
        }
    }
}
