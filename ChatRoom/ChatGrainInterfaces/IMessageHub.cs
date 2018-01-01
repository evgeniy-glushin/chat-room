using System;
using System.Collections.Generic;
using Orleans;

namespace ChatGrainInterfaces
{
    public interface IMessageHub : IGrainObserver
    {
        void Notify(Message msg, List<Guid> targetsId);
    }
}
