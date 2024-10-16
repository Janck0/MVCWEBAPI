using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MVCWEBAPIORGINALAPP;

namespace MVCWEBAPIORGINALAPP.API
{
    public class APIController : ApiController
    {
        // GET: api/API
        APIEntities db = new APIEntities();
        [HttpGet]
        [Route("api/API/getapi")]
        public IHttpActionResult Get()
        {
            return Ok(db.TABLE_EMPLOYEE.ToList());
        }

        // GET: api/API/5
        [HttpGet]
        [Route("api/API/getwithid/{id}")]
        public IHttpActionResult GetWithID(int id)
        {
            TABLE_EMPLOYEE tABLE_EMPLOYEE = db.TABLE_EMPLOYEE.Find(id);
            if (tABLE_EMPLOYEE == null)
            {
                return NotFound();
            }

            return Ok(tABLE_EMPLOYEE);
        }

        // POST: api/API
        [HttpPost]
        [Route("api/API/postwebapitab")]
        public IHttpActionResult Post(TABLE_EMPLOYEE Tab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.TABLE_EMPLOYEE.Add(Tab);
            db.SaveChanges();
            return Ok(200);


        }

        // PUT: api/API/5
        public void Put(int id, [FromBody] string value)
        {
        }



        [HttpGet]
        [Route("api/API/deletewithid/{id}")]
        public IHttpActionResult DeleteWithID(int id)
        {
            TABLE_EMPLOYEE tABLE_EMPLOYEE = db.TABLE_EMPLOYEE.Find(id);
            if (tABLE_EMPLOYEE == null)
            {
                return NotFound();
            }

            return Ok(tABLE_EMPLOYEE);
        }
        // DELETE: api/API/5
        [HttpDelete]
        [Route("api/API/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            TABLE_EMPLOYEE tABLE_EMPLOYEE = db.TABLE_EMPLOYEE.Find(id);
            if (tABLE_EMPLOYEE == null)
            {
                return NotFound();
            }
            db.TABLE_EMPLOYEE.Remove(tABLE_EMPLOYEE);
            db.SaveChanges();
            return Ok(tABLE_EMPLOYEE);
        }
        [HttpPut]
        [Route("api/API/Edit{id}")]
        public IHttpActionResult Edit(int id,TABLE_EMPLOYEE Tab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Entry(Tab).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(200);
            
        }
    }
}
