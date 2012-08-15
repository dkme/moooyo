using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBB.LocationFunctionHelper
{
    /// <summary>
    /// 经纬度
    /// </summary>
    public class Coordinate
    {
        private double longitude;
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude
        {
            get { return this.longitude; }
            set { this.longitude = value; }
        }

        private double latitude;
        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude
        {
            get { return this.latitude; }
            set { this.latitude = value; }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        public Coordinate(double longitude, double latitude)
        {
            this.longitude = longitude;
            this.latitude = latitude;
        }
        public Coordinate()
        { }
    }
}
