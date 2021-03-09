using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OneForAll.Core
{
    /// <summary>
    /// 基础消息类
    /// </summary>
    public class BaseMessage
    {
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 错误类型
        /// </summary>
        public virtual BaseErrType ErrType { get; set; } = BaseErrType.Fail;

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 获取数据并转换格式
        /// </summary>
        /// <typeparam name="T">格式</typeparam>
        /// <returns>结果</returns>
        public virtual T GetData<T>() where T : class
        {
            return Data as T;
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <returns>当前对象</returns>
        public virtual BaseMessage Success(string msg = "success")
        {
            Status = true;
            Message = msg;
            ErrType = BaseErrType.Success;
            return this;
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <returns>当前对象</returns>
        public virtual BaseMessage Fail(string msg = "fail")
        {
            Status = false;
            Message = msg;
            return this;
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="type">错误类型</param>
        /// <param name="msg">消息内容</param>
        /// <returns>当前对象</returns>
        public virtual BaseMessage Fail(BaseErrType type, string msg = "fail")
        {
            Status = false;
            Message = msg;
            ErrType = type;
            return this;
        }
    }
}
