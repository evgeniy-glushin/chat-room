using ChatGrainInterfaces;
using Orleans;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ChatGrains
{
    public class ChatRoomGrain : Grain, IChatRoomGrain
    {        
        public Task<ChatRoom> Create(ChatRoom chatRoom)
        {
            throw new NotImplementedException();
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
        
        public Task<ChatRoom> Join(params User[] participant)
        {
            throw new NotImplementedException();
        }
    }
}
