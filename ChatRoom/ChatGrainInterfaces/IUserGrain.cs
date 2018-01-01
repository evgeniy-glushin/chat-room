using System.Threading.Tasks;
using Orleans;
using System;

namespace ChatGrainInterfaces
{
    public interface IUserGrain : IGrainWithGuidKey
    {
        Task<UserChatRoom> AddChatRoom(UserChatRoom chatRoom);
        Task<UserChatRoom> GetChatRooms();
        Task<User> Create(User usr);
        Task<User> Get();
        Task<Message> SendMsg(string msg, IChatRoomGrain chatRoomGrain);
    }

}
