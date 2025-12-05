using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OneForAll.Core
{
    /// <summary>
    /// 泛型基础消息类，用于封装 API 或服务调用的响应结果。
    /// 支持强类型数据、成功/失败状态、自定义错误类型和消息。
    /// 属性可写，便于对象初始化、反序列化及动态构建。
    /// </summary>
    /// <typeparam name="T">业务数据的具体类型（可以是引用类型或值类型）</typeparam>
    public class BaseMessage<T>
    {
        /// <summary>
        /// 获取或设置操作是否成功。
        /// <para>通常：<c>true</c> 表示成功，<c>false</c> 表示失败。</para>
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 获取或设置错误类型，用于区分不同类型的失败原因。
        /// <para>默认值为 <see cref="BaseErrType.Fail"/>。</para>
        /// </summary>
        public BaseErrType ErrType { get; set; } = BaseErrType.Fail;

        /// <summary>
        /// 获取或设置人类可读的消息内容，用于描述操作结果或错误详情。
        /// </summary>
        public string Message { get; set; } = "fail";

        /// <summary>
        /// 获取或设置业务数据，类型由泛型参数 <typeparamref name="T"/> 指定。
        /// <para>例如：用户信息、ID、列表等。</para>
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 将当前消息对象设置为“成功”状态。
        /// </summary>
        /// <param name="msg">成功时的消息文本，默认为 "success"。</param>
        /// <returns>当前对象实例，支持链式调用。</returns>
        public BaseMessage<T> Success(string msg = "success")
        {
            Status = true;
            Message = msg;
            ErrType = BaseErrType.Success;
            return this;
        }

        /// <summary>
        /// 将当前消息对象设置为“通用失败”状态。
        /// </summary>
        /// <param name="msg">失败时的消息文本，默认为 "fail"。</param>
        /// <returns>当前对象实例，支持链式调用。</returns>
        public BaseMessage<T> Fail(string msg = "fail")
        {
            Status = false;
            Message = msg;
            ErrType = BaseErrType.Fail;
            return this;
        }

        /// <summary>
        /// 将当前消息对象设置为“指定错误类型”的失败状态。
        /// </summary>
        /// <param name="type">具体的错误类型枚举值。</param>
        /// <param name="msg">失败时的消息文本，默认为 "fail"。</param>
        /// <returns>当前对象实例，支持链式调用。</returns>
        public BaseMessage<T> Fail(BaseErrType type, string msg = "fail")
        {
            Status = false;
            Message = msg;
            ErrType = type;
            return this;
        }

        /// <summary>
        /// 创建一个表示“成功”的消息对象。
        /// </summary>
        /// <param name="msg">成功消息，默认为 "success"。</param>
        /// <param name="data">附加的业务数据，可为 null 或默认值。</param>
        /// <returns>配置好的成功状态 <see cref="BaseMessage{T}"/> 实例。</returns>
        public static BaseMessage<T> Success(string msg = "success", T data = default)
        {
            return new BaseMessage<T>
            {
                Status = true,
                Message = msg,
                ErrType = BaseErrType.Success,
                Data = data
            };
        }

        /// <summary>
        /// 创建一个表示“通用失败”的消息对象。
        /// </summary>
        /// <param name="msg">失败消息，默认为 "fail"。</param>
        /// <param name="data">附加的业务数据（如调试信息），可为 null 或默认值。</param>
        /// <returns>配置好的失败状态 <see cref="BaseMessage{T}"/> 实例。</returns>
        public static BaseMessage<T> Fail(string msg = "fail", T data = default)
        {
            return new BaseMessage<T>
            {
                Status = false,
                Message = msg,
                ErrType = BaseErrType.Fail,
                Data = data
            };
        }

        /// <summary>
        /// 创建一个表示“指定错误类型失败”的消息对象。
        /// </summary>
        /// <param name="type">具体的错误类型。</param>
        /// <param name="msg">失败消息，默认为 "fail"。</param>
        /// <param name="data">附加的业务数据，可为 null 或默认值。</param>
        /// <returns>配置好的失败状态 <see cref="BaseMessage{T}"/> 实例。</returns>
        public static BaseMessage<T> Fail(BaseErrType type, string msg = "fail", T data = default)
        {
            return new BaseMessage<T>
            {
                Status = false,
                Message = msg,
                ErrType = type,
                Data = data
            };
        }
    }

    /// <summary>
    /// 非泛型基础消息类，适用于无明确数据类型、动态数据或兼容旧代码的场景。
    /// 内部基于 <see cref="BaseMessage{T}"/> 实现，其中 <c>T = object</c>。
    /// </summary>
    public class BaseMessage : BaseMessage<object>
    {
        /// <summary>
        /// 将当前消息对象设置为“成功”状态。
        /// </summary>
        /// <param name="msg">成功时的消息文本，默认为 "success"。</param>
        /// <returns>当前对象实例，支持链式调用。</returns>
        public BaseMessage Success(string msg = "success")
        {
            Status = true;
            Message = msg;
            ErrType = BaseErrType.Success;
            return this;
        }

        /// <summary>
        /// 将当前消息对象设置为“通用失败”状态。
        /// </summary>
        /// <param name="msg">失败时的消息文本，默认为 "fail"。</param>
        /// <returns>当前对象实例，支持链式调用。</returns>
        public BaseMessage Fail(string msg = "fail")
        {
            Status = false;
            Message = msg;
            ErrType = BaseErrType.Fail;
            return this;
        }

        /// <summary>
        /// 将当前消息对象设置为“指定错误类型”的失败状态。
        /// </summary>
        /// <param name="type">具体的错误类型枚举值。</param>
        /// <param name="msg">失败时的消息文本，默认为 "fail"。</param>
        /// <returns>当前对象实例，支持链式调用。</returns>
        public BaseMessage Fail(BaseErrType type, string msg = "fail")
        {
            Status = false;
            Message = msg;
            ErrType = type;
            return this;
        }

        /// <summary>
        /// 创建一个表示“成功”的非泛型消息对象。
        /// </summary>
        /// <param name="msg">成功消息，默认为 "success"。</param>
        /// <param name="data">任意类型的附加数据，通常为匿名对象或字典。</param>
        /// <returns>配置好的成功状态 <see cref="BaseMessage"/> 实例。</returns>
        public new static BaseMessage Success(string msg = "success", object data = null)
        {
            return new BaseMessage
            {
                Status = true,
                Message = msg,
                Data = data,
                ErrType = BaseErrType.Success
            };
        }

        /// <summary>
        /// 创建一个表示“通用失败”的非泛型消息对象。
        /// </summary>
        /// <param name="msg">失败消息，默认为 "fail"。</param>
        /// <param name="data">任意类型的附加数据。</param>
        /// <returns>配置好的失败状态 <see cref="BaseMessage"/> 实例。</returns>
        public new static BaseMessage Fail(string msg = "fail", object data = null)
        {
            return new BaseMessage
            {
                Status = false,
                Message = msg,
                Data = data,
                ErrType = BaseErrType.Fail
            };
        }

        /// <summary>
        /// 创建一个表示“指定错误类型失败”的非泛型消息对象。
        /// </summary>
        /// <param name="type">具体的错误类型。</param>
        /// <param name="msg">失败消息，默认为 "fail"。</param>
        /// <param name="data">任意类型的附加数据。</param>
        /// <returns>配置好的失败状态 <see cref="BaseMessage"/> 实例。</returns>
        public new static BaseMessage Fail(BaseErrType type, string msg = "fail", object data = null)
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