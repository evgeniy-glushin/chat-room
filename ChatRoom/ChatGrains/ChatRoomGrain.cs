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
            IEnumerable<Guid> newParticipants = (await Task.WhenAll(participants.Select(x => x.Get())))
                .Select(x => x.Id)
                .Except(State.Participants)
                .ToList();

            State.Participants.AddRange(newParticipants);
            await WriteStateAsync();

            return await Task.WhenAll(participants.Select(AddChatRoom));

            Task<UserChatRoom> AddChatRoom(IUserGrain user) =>
                user.AddChatRoom(new UserChatRoom { Id = State.Id, Name = State.Name });
        }

        public Task<List<Message>> GetMessages()
        {
            return Task.FromResult(State.Messages);
        }


        public async Task<Message> AddMessage(Message msg)
        {
            // TODO: make sure the participant in the room
            // TODO: get notified the other participants

            State.Messages.Add(msg);
            await WriteStateAsync();
            _subscriptionManager.Notify(x => x.Notify(msg, State.Participants));
            return msg;
        }

        ObserverSubscriptionManager<IMessageHub> _subscriptionManager;
        public override Task OnActivateAsync()
        {
            _subscriptionManager = new ObserverSubscriptionManager<IMessageHub>();
            return base.OnActivateAsync();
        }

        public Task Subscribe(IMessageHub observer)
        {
            _subscriptionManager.Subscribe(observer);
            return Task.CompletedTask;
        }

        public Task Unsubscribe(IMessageHub observer)
        {
            _subscriptionManager.Unsubscribe(observer);
            return Task.CompletedTask;
        }
    }
}
