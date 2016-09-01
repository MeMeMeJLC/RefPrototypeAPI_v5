using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RefPrototypeAPI_v5.Models;

namespace RefPrototypeAPI_v5.Controllers
{
    public class refereesController : ApiController
    {
        private Entities4 db = new Entities4();

        // GET: api/referees
        public IQueryable<referee> Getreferees()
        {
            return db.referees;
        }

        // GET: api/referees/5
        [ResponseType(typeof(referee))]
        public IHttpActionResult Getreferee(int id)
        {
            referee referee = db.referees.Find(id);
            if (referee == null)
            {
                return NotFound();
            }

            return Ok(referee);
        }

        // PUT: api/referees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putreferee(int id, referee referee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != referee.referee_id)
            {
                return BadRequest();
            }

            db.Entry(referee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!refereeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/referees
        [ResponseType(typeof(referee))]
        public IHttpActionResult Postreferee(referee referee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.referees.Add(referee);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = referee.referee_id }, referee);
        }

        // DELETE: api/referees/5
        [ResponseType(typeof(referee))]
        public IHttpActionResult Deletereferee(int id)
        {
            referee referee = db.referees.Find(id);
            if (referee == null)
            {
                return NotFound();
            }

            db.referees.Remove(referee);
            db.SaveChanges();

            return Ok(referee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool refereeExists(int id)
        {
            return db.referees.Count(e => e.referee_id == id) > 0;
        }
    }
}