using System;
using System.Threading.Tasks;

namespace OneForAll.Core.Events
{
    /// <summary>
    /// 领域事件标记契约（进程内 / 可跨模块复用）。
    /// 纯抽象：不绑定任何具体事件总线（MediatR / 自研派发器均由宿主层实现），
    /// 以保持 OneForAll.Core 作为通用基础库零 CQRS 依赖。
    /// 使用方（如 Base.Domain）可让具体事件同时实现本接口与所选总线的通知契约
    /// （例如 MediatR 的 INotification）。
    /// </summary>
    public interface IDomainEvent
    {
        /// <summary>
        /// 事件唯一标识
        /// </summary>
        Guid EventId { get; }

        /// <summary>
        /// 事件发生时间
        /// </summary>
        DateTime OccurredOn { get; }
    }

    /// <summary>
    /// 领域事件派发器端口：领域层只依赖本抽象发布事件，具体实现（进程内 MediatR / 
    /// 事务性 Outbox 等）由宿主层（Host）提供并注入。
    /// </summary>
    public interface IEventDispatcher
    {
        /// <summary>
        /// 发布一个领域事件
        /// </summary>
        /// <param name="domainEvent">领域事件</param>
        Task PublishAsync(IDomainEvent domainEvent);
    }
}
