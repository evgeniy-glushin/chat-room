using Orleans;
using System;
using System.Collections.Generic;

namespace ChatGrainInterfaces
{
    public class User : IGrainState
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public List<UserChatRoom> ChatRooms { get; set; } = new List<UserChatRoom>();
        public object State { get; set; }
        public string ETag { get; set; }
    }

    public class UserChatRoom
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class ChatRoom : IGrainState
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Guid> Participants { get; set; } = new List<Guid>();
        public List<Message> Messages { get; set; } = new List<Message>();
        public object State { get; set; }
        public string ETag { get; set; }
    }

    public class Message
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid SenderId { get; set; }
        public string SenderNickname { get; set; }
    }
}
