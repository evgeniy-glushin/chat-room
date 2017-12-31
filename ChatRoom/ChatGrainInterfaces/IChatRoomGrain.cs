using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGrainInterfaces
{
    public interface IChatRoomGrain : IGrainWithGuidKey
    {
        Task<ChatRoom> Create(ChatRoom chatRoom);
        Task<UserChatRoom[]> Join(params IUserGrain[] participant);
        Task<IEnumerable<Message>> GetMessages();
        Task<Message> AddMessage(Message msg);
    }
}
