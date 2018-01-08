using ChatGrainInterfaces;
using Orleans;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class ChatController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> CreateUser(string nickname)
        {
            var id = Guid.NewGuid();
            var grain = GrainClient.GrainFactory.GetGrain<IUserGrain>(id);

            var usr = await grain.Create(new User
            {
                Id = id,
                Nickname = nickname
            });

            return Ok(usr);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetUser(Guid id)
        {
            var grain = GrainClient.GrainFactory.GetGrain<IUserGrain>(id);
            var usr = await grain.Get();
            return Ok(usr);
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
        public IHttpActionResult SendMessage([FromBody] Models.Message msg)
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
