using System;
using System.Collections.Generic;

namespace ChatGrainInterfaces
{
    public class User
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public IEnumerable<UserChatRoom> ChatRooms { get; set; } = new List<UserChatRoom>();
    }

    public class UserChatRoom
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class ChatRoom
    {
        public Guid Id { get; set; }
        public IEnumerable<Guid> Participants { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }

    public class Message
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid SenderId { get; set; }
        public string SenderNickname { get; set; }
    }
}
