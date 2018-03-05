using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mobil_Ogrenci_Yoklama_Uygulamasi.Controllers
{
    public class GetMetotlariController : ApiController
    {

        // GET: api/GetMetotlari
        public List<int> Get()
        {
            return null;
        }

        // GET: api/GetMetotlari/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GetMetotlari
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/GetMetotlari/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GetMetotlari/5
        public void Delete(int id)
        {
        }
    }
}
