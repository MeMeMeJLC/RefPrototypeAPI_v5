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
    public class game_locationController : ApiController
    {
        private Entities5 db = new Entities5();

        // GET: api/game_location
        public IQueryable<game_location> Getgame_location()
        {
            return db.game_location;
        }

        // GET: api/game_location/5
        [ResponseType(typeof(game_location))]
        public IHttpActionResult Getgame_location(int id)
        {
            game_location game_location = db.game_location.Find(id);
            if (game_location == null)
            {
                return NotFound();
            }

            return Ok(game_location);
        }

        // PUT: api/game_location/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putgame_location(int id, game_location game_location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != game_location.location_id)
            {
                return BadRequest();
            }

            db.Entry(game_location).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!game_locationExists(id))
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

        // POST: api/game_location
        [ResponseType(typeof(game_location))]
        public IHttpActionResult Postgame_location(game_location game_location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.game_location.Add(game_location);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = game_location.location_id }, game_location);
        }

        // DELETE: api/game_location/5
        [ResponseType(typeof(game_location))]
        public IHttpActionResult Deletegame_location(int id)
        {
            game_location game_location = db.game_location.Find(id);
            if (game_location == null)
            {
                return NotFound();
            }

            db.game_location.Remove(game_location);
            db.SaveChanges();

            return Ok(game_location);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool game_locationExists(int id)
        {
            return db.game_location.Count(e => e.location_id == id) > 0;
        }
    }
}