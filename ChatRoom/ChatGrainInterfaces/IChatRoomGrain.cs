using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatGrainInterfaces
{
    public interface IChatRoomGrain : IGrainWithGuidKey
    {
        Task<ChatRoom> Create(ChatRoom chatRoom);
        Task<UserChatRoom[]> Join(params IUserGrain[] participant);
        Task<List<Message>> GetMessages();
        Task<Message> AddMessage(Message msg);

        Task Subscribe(IMessageHub observer);
        Task Unsubscribe(IMessageHub observer);
    }
}
