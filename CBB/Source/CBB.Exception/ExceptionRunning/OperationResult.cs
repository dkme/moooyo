using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBB.ExceptionHelper
{
    /// <summary>
    /// 操作执行异常类
    /// </summary>
    public class OperationException : System.Exception
    {
        #region 类属性
        /// <summary>
        /// 错误级别
        /// </summary>
        private ErrType errType;
        public ErrType ErrType
        {
            get { return this.errType; }
            set { this.errType = value; }
        }
        /// <summary>
        /// 错误编码
        /// </summary>
        private ErrNo errNo;
        public ErrNo ErrNo
        {
            get { return this.errNo; }
            set { this.errNo = value; }
        }
        /// <summary>
        /// 系统原始错误信息
        /// </summary>
        private System.Exception err;
        public System.Exception Err
        {
            get { return this.err; }
            set { this.err = value; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// OperationException 构造函数
        /// </summary>
        /// <param name="errType">错误类别</param>
        /// <param name="errNo">错误号</param>
        public OperationException(ErrType errType, ErrNo errNo)
        {
            this.errType = errType;
            this.errNo = errNo;
            this.err = null;

            //使用log4net写入到日志文本文档
            CBB.Logger.SystemLog.LogHelper.WriteLog("\r\n错误编码：" + errNo.ToString() + "\r\n错误级别：" + errType.ToString());
        }
        /// <summary>
        /// OperationException 构造函数
        /// </summary>
        /// <param name="errType">错误类别</param>
        /// <param name="errNo">错误号</param>
        /// <param name="err">系统错误</param>
        public OperationException(ErrType errType, ErrNo errNo, System.Exception err)
        {
            this.errType = errType;
            this.errNo = errNo;
            this.err = err;

            //使用log4net写入到日志文本文档
            CBB.Logger.SystemLog.LogHelper.WriteLog("\r\n错误编码：" + errNo.ToString() + "\r\n错误级别：" + errType.ToString() + "\r\n引发当前异常的方法：" + err.TargetSite.Name + "\r\n导致错误的应用程序或对象的名称：" + err.Source + "\r\n异常信息：" + err.Message);
        }
        #endregion

        public static void ThrowDataRowIsNullError()
        {
            throw new
                CBB.ExceptionHelper.OperationException(
                CBB.ExceptionHelper.ErrType.UserErr,
                CBB.ExceptionHelper.ErrNo.DataRowIsNull);
        }

    }

}
