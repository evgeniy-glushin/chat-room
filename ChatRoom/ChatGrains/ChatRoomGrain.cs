using ChatGrainInterfaces;
using Orleans;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Orleans.Providers;

namespace ChatGrains
{
    [StorageProvider(ProviderName = "DynamoDBStorage")]
    public class ChatRoomGrain : Grain<ChatRoom>, IChatRoomGrain
    {
        public async Task<ChatRoom> Create(ChatRoom chatRoom)
        {
            if (State.Id == Guid.Empty)
            {
                State = chatRoom;
                await WriteStateAsync();
            }

            return State;
        }

        public Task<ChatRoom> Get() => Task.FromResult(State);

        public async Task<UserChatRoom[]> Join(params IUserGrain[] participants)
        {
            State.Participants.AddRange(
                (await Task.WhenAll(participants.Select(x => x.Get()))).Select(x => x.Id)
            );

            await WriteStateAsync();

            return await Task.WhenAll(participants.Select(AddChatRoom));

            Task<UserChatRoom> AddChatRoom(IUserGrain user) =>
                user.AddChatRoom(new UserChatRoom { Id = State.Id, Name = State.Name });
        }

        public Task<IEnumerable<Message>> GetMessages()
        {
            throw new NotImplementedException();
        }


        public Task<Message> AddMessage(Message msg)
        {
            // TODO: make sure the participant in the room
            // TODO: get notified the other participants
            throw new NotImplementedException();
        }
    }
}
