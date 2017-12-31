using System.Threading.Tasks;
using Orleans;
using System;

namespace ChatGrainInterfaces
{
    public interface IUserGrain : IGrainWithGuidKey
    {
        Task<bool> SendMsg(Guid chatRoomId, string msg);
        Task<UserChatRoom> AddChatRoom(UserChatRoom chatRoom);
        Task<UserChatRoom> GetChatRooms();
        Task<User> Create(User usr);
        Task<User> Get();
        
    }

}
