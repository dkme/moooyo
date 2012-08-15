using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBB.ExceptionHelper
{
    /// <summary>
    /// 系统错误信息处理
    /// </summary>
    public class ExpressionPaser
    {
        /// <summary>
        /// 消去冗余的错误信息
        /// </summary>
        /// <param name="message">信息</param>
        /// <returns>string</returns>
        public static string ErrTrim(string message)
        {
            if (message.Contains("XML"))
                message = "网络连接发生异常，操作失败，请重新再试...";
            if (message.Contains("未将对象引用设置到对象的实例"))
                message = "连接发生异常，请检查网络，重新登陆再进入....";
            if (message.Contains("牺牲"))
                message = "连接发生异常，请检查网络，重新登陆再进入...";
            if (message.Contains("查询处理器用尽了堆栈空间"))
                message = "数据量过大，请减少相应数据量，重新操作...";
            if (message.Contains("请求因 HTTP 状态 404 失败: Not Found"))
                message = "网络服务连接失败，请检查服务连接是否正确";
            if (message.Contains("将截断字符串或二进制数据"))
                message = "超过字段设置长度！无法继续...";
            if (message.Contains("重复键"))
                message = "您增加的数据和现有数据重复，请检查您的输入！";

            try
            {
                int start = message.LastIndexOf("->");
                if (start != -1)
                {
                    int end = message.IndexOf("\n");
                    if (end == -1) end = message.Length;
                    message = message.Substring(start + 2, end - start - 2);
                    start = message.IndexOf(':');
                    if (start != -1)
                    {
                        end = message.Length;
                        return message.Substring(start + 1, end - start - 1);
                    }
                }
                return message;
            }
            catch
            {
                return message;
            }
        }

        /// <summary>
        /// 错误处理
        /// </summary>
        /// <param name="err">错误类</param>
        /// <returns>string</returns>
        public static string ErrTrim(System.Exception err)
        {
            return ErrTrim(err.Message);
        }
    }
}
