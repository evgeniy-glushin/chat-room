using System.Threading.Tasks;
using Orleans;
using ChatGrainInterfaces;
using System;
using Orleans.Providers;

namespace ChatGrains
{
    [StorageProvider(ProviderName = "DynamoDBStorage")]
    public class UserGrain : Grain<User>, IUserGrain
    {
        public override Task OnActivateAsync()
        {
            return base.OnActivateAsync();
        }

        public async Task<User> Create(User usr)
        {
            if (State.Id == Guid.Empty)
            {
                State = usr;
                await WriteStateAsync();
            }
            return State;
        }

        public Task<User> Get() => Task.FromResult(State);

        public async Task<UserChatRoom> AddChatRoom(UserChatRoom chatRoom)
        {
            State.ChatRooms.Add(chatRoom);
            await WriteStateAsync();
            return chatRoom;
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
