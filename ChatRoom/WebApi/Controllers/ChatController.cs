using System;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ChatController : ApiController
    {
        [HttpPost]
        public IHttpActionResult CreateUser(string nickname)
        {

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult CreateChat(string name)
        {

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult JoinParticipants(Guid[] ids)
        {

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult SendMessage([FromBody] Message msg)
        {

            return Ok();
        }     
        
        [HttpGet]
        public IHttpActionResult GetMessages(Guid userId)
        {

            return Ok();
        }
    }
}
