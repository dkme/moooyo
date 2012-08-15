using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBB.LocationFunctionHelper
{
    /// <summary>
    /// 经纬度范围和距离计算器
    /// </summary>
    public static class DistanceAndAroundCalculator
    {
        private static double PI = 3.14159265;
        private static double EARTH_RADIUS = 6378137;
        private static double RAD = Math.PI / 180.0;

        //@see http://snipperize.todayclose.com/snippet/php/SQL-Query-to-Find-All-Retailers-Within-a-Given-Radius-of-a-Latitude-and-Longitude--65095/ 
        //The circumference of the earth is 24,901 miles.
        //24,901/360 = 69.17 miles / degree  
        /**
         * @param raidus 单位米
         * return minLat,minLng,maxLat,maxLng
         */
        public static double[] getAround(double lat, double lon, int raidus)
        {

            Double latitude = lat;
            Double longitude = lon;

            Double degree = (24901 * 1609) / 360.0;
            double raidusMile = raidus;

            Double dpmLat = 1 / degree;
            Double radiusLat = dpmLat * raidusMile;
            Double minLat = latitude - radiusLat;
            Double maxLat = latitude + radiusLat;

            Double mpdLng = degree * Math.Cos(latitude * (PI / 180));
            Double dpmLng = 1 / mpdLng;
            Double radiusLng = dpmLng * raidusMile;
            Double minLng = longitude - radiusLng;
            Double maxLng = longitude + radiusLng;

            return new double[] { minLat, minLng, maxLat, maxLng };
        }

        /**
         * 根据两点间经纬度坐标（double值），计算两点间距离，单位为米
         * @param lng1
         * @param lat1
         * @param lng2
         * @param lat2
         * @return
         */
        public static double getDistance(double lng1, double lat1, double lng2, double lat2)
        {
            double radLat1 = lat1 * RAD;
            double radLat2 = lat2 * RAD;
            double a = radLat1 - radLat2;
            double b = (lng1 - lng2) * RAD;
            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
             Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * EARTH_RADIUS;
            s = Math.Round(s * 10000) / 10000;
            return s;
        }

        public static String getDistanceStr(double lng1, double lat1, double lng2, double lat2)
        {
            double distant = getDistance(
                       lng1,
                       lat1,
                       lng2,
                       lat2);

            int distantint = Int32.Parse(Math.Floor(distant).ToString());
            if (distantint > 1000)
            {
                double km = distantint / 1000;
                return Math.Floor(km).ToString() + " 千米";
            }
            else
            {
                return distantint + " 米";
            }
        }
    }

    
	
}
