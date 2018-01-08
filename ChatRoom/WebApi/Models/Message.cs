using System;

namespace WebApi.Models
{
    public class Message
    {
        public Guid FromUserId { get; set; }
        public Guid ToRoomId { get; set; }  
        public string Content { get; set; }
    }
}