using System;
using System.Collections.Generic;
using ChatGrainInterfaces;
using Orleans;

namespace ChatGrains
{
    public class MessageHub : IMessageHub
    {
        private ChatRoom _chatRoom;

        public MessageHub(ChatRoom chatRoom)
        {
            _chatRoom = chatRoom;
        }

        public void Notify(Message msg, List<Guid> targetsId)
        {
            Console.WriteLine($"{msg.SenderNickname} has sent a message to {_chatRoom.Name} - {msg.Content}");
        }
    }
}
