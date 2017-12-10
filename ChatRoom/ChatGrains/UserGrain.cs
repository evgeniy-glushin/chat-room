using System.Threading.Tasks;
using Orleans;
using ChatGrainInterfaces;
using System;

namespace ChatGrains
{
    public class UserGrain : Grain, IUserGrain
    {
        public Task<User> Create(User usr)
        {
            throw new NotImplementedException();
        }

        public Task<UserChatRoom> GetChatRooms()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SendMsg(Guid chatRoomId, string msg)
        {

            return true;
        }
    }
}
