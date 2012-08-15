using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBB.ExceptionHelper
{
    /// <summary>
    /// 错误号
    /// </summary>
    public enum ErrNo
    {
        #region 系统错误类
        /// 系统错误类，错误号0-999
        /// <summary>
        /// 数据库操作错误
        /// </summary>
        DBOperationError=0001,
        /// <summary>
        /// 文件上传错误
        /// </summary>
        FileUpload = 0002,
        #endregion

        #region 用户错误类

        /// 用户类错误
        /// 错误号 1000+
        /// <summary>
        /// 用户对象为空
        /// </summary>
        UserObjectIsNull = 1000,
        /// <summary>
        /// 用户信息不完整
        /// </summary>
        UserInfomationNotCompleted = 1001,
        /// <summary>
        /// 必须提供用户昵称或姓名
        /// </summary>
        UserNickNameMustBeSupply = 1002,
        /// <summary>
        /// 用户ID无效
        /// </summary>
        UserIDNotVaild = 1003,
        /// <summary>
        /// 用户密码必须提供
        /// </summary>
        UserPasswordMustBeSupply = 1004,
        /// <summary>
        /// 用户旧密码必须提供
        /// </summary>
        UserOldPasswordMustBeSupply = 1005,
        /// <summary>
        /// 用户手机号码必须提供
        /// </summary>
        UserMobileNoMustBeSupply = 1006,
        /// <summary>
        /// 用户未找到或密码不正确
        /// </summary>
        UserNotFindOrPasswordIsWrong = 1007,
        /// <summary>
        /// 用户未找到
        /// </summary>
        UserNotFind = 1008,
        /// <summary>
        /// DataRow为Null
        /// </summary>
        DataRowIsNull = 1009,
        /// <summary>
        /// DataRow数据解析错误
        /// </summary>
        DataRowPaserError = 1010,
        #endregion

        #region 抱团错误类
        /// <summary>
        /// 抱团对象为空
        /// </summary>
        GroupObjectIsNull=2001,
        /// <summary>
        /// 抱团ID无效
        /// </summary>
        GroupIDNotVaild = 2002,
        #endregion

        #region 标量赋值类
        /// <summary>
        /// 标量Distance列未找到
        /// </summary>
        ColumnDistanceNotFind = 1010,
        #endregion

        #region 地理位置类
        /// <summary>
        /// 经度或纬度值无效
        /// </summary>
        LatOrLngNotVaild = 5001,
        #endregion

        #region 系统级别错误
        /// <summary>
        /// 
        /// </summary>
        SqlTranstionIsNull = 9001,
        #endregion
    }
}
