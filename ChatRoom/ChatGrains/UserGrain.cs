using System.Threading.Tasks;
using Orleans;
using ChatGrainInterfaces;
using System;
using Orleans.Providers;

namespace ChatGrains
{
    [StorageProvider(ProviderName = "MemoryStore")]
    public class UserGrain : Grain<User>, IUserGrain
    {
        public async Task<User> Create(User usr)
        {
            State = usr;
            await WriteStateAsync();
            return State;
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
