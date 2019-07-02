using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WardenCore.Models;

namespace WardenCore.Controllers
{
    //[Route("api/[controller]")]
    public class TournamentController : ApiController
    {
        // GET: api/Tournament
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Tournament/5
        [HttpGet]
        [ActionName("getTournament")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Tournament/createTournament
        [HttpPost]
        [ActionName("createTournament")]
        public void Post(Tournament value)
        {
        }

        // PUT: api/Tournament/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Tournament/5
        public void Delete(int id)
        {
        }


    }
}
