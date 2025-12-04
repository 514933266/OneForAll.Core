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
        /// <typeparam name="T">目标类型（必须为引用类型）</typeparam>
        /// <returns>转换后的对象，若类型不匹配则返回 null</returns>
        public T GetData<T>() where T : class
        {
            return Data as T;
        }

        /// <summary>
        /// 设置为成功状态
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <returns>当前对象</returns>
        public BaseMessage Success(string msg = "success")
        {
            Status = true;
            Message = msg;
            ErrType = BaseErrType.Success;
            return this;
        }

        /// <summary>
        /// 设置为失败状态
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <returns>当前对象</returns>
        public BaseMessage Fail(string msg = "fail")
        {
            Status = false;
            Message = msg;
            ErrType = BaseErrType.Fail;
            return this;
        }

        /// <summary>
        /// 设置为失败状态（指定错误类型）
        /// </summary>
        /// <param name="type">错误类型</param>
        /// <param name="msg">消息内容</param>
        /// <returns>当前对象</returns>
        public BaseMessage Fail(BaseErrType type, string msg = "fail")
        {
            Status = false;
            Message = msg;
            ErrType = type;
            return this;
        }

        /// <summary>
        /// 创建一个成功的消息对象
        /// </summary>
        /// <param name="msg">成功消息</param>
        /// <param name="data">附加数据（可选）</param>
        /// <returns>成功状态的 BaseMessage 实例</returns>
        public static BaseMessage Success(string msg = "success", object data = null)
        {
            return new BaseMessage
            {
                Status = true,
                Message = msg,
                ErrType = BaseErrType.Success,
                Data = data
            };
        }

        /// <summary>
        /// 创建一个失败的消息对象（通用失败）
        /// </summary>
        /// <param name="msg">失败消息</param>
        /// <param name="data">附加数据（可选）</param>
        /// <returns>失败状态的 BaseMessage 实例</returns>
        public static BaseMessage Fail(string msg = "fail", object data = null)
        {
            return new BaseMessage
            {
                Status = false,
                Message = msg,
                ErrType = BaseErrType.Fail,
                Data = data
            };
        }

        /// <summary>
        /// 创建一个失败的消息对象（指定错误类型）
        /// </summary>
        /// <param name="type">错误类型</param>
        /// <param name="msg">失败消息</param>
        /// <param name="data">附加数据（可选）</param>
        /// <returns>失败状态的 BaseMessage 实例</returns>
        public static BaseMessage Fail(BaseErrType type, string msg = "fail", object data = null)
        {
            return new BaseMessage
            {
                Status = false,
                Message = msg,
                ErrType = type,
                Data = data
            };
        }
    }
}
