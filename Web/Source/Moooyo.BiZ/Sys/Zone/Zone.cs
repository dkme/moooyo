using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Moooyo.BiZ.Sys
{
    /// <summary>
    /// 区域类
    /// </summary>
    public class Zone
    {
        /// <summary>
        /// id
        /// </summary>
        [System.Web.Script.Serialization.ScriptIgnore]
        public ObjectId _id;
        /// <summary>
        /// 省
        /// </summary>
        public String Province
        {
            get { return this.province; }
            set { this.province = value; }
        }
        private String province;
        /// <summary>
        /// 市
        /// </summary>
        public String City
        {
            get { return this.city; }
            set { this.city = value; }
        }
        private String city;
        /// <summary>
        /// 区
        /// </summary>
        public String Area
        {
            get { return this.area; }
            set { this.area = value; }
        }
        private String area;
        /// <summary>
        /// 中心纬度
        /// </summary>
        public double CenterLng
        {
            get { return this.centerLng; }
            set { this.centerLng = value; }
        }
        private double centerLng;
        /// <summary>
        /// 中心经度
        /// </summary>
        public double CenterLat
        {
            get { return this.centerLat; }
            set { this.centerLat = value; }
        }
        private double centerLat;

        public Zone(DataRow dr)
        {
            try
            {
                this.Province = dr["prov"].ToString().Trim();
                this.City = dr["city"].ToString().Trim();
                this.CenterLng = double.Parse(dr["lng"].ToString().Trim());
                this.CenterLat = double.Parse(dr["lat"].ToString().Trim());
            }
            catch (Exception err)
            {
                throw new
                    CBB.ExceptionHelper.OperationException(
                        CBB.ExceptionHelper.ErrType.UserErr,
                        CBB.ExceptionHelper.ErrNo.DataRowIsNull,
                        err);
            }

        }
        public Zone()
        { }
    }
}
