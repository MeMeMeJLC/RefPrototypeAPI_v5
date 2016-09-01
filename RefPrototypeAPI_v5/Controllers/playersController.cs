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
    public class playersController : ApiController
    {
        private Entities1 db = new Entities1();

        // GET: api/players
        public IQueryable<player> Getplayers()
        {
            return db.players;
        }

        // GET: api/players/5
        [ResponseType(typeof(player))]
        public IHttpActionResult Getplayer(int id)
        {
            player player = db.players.Find(id);
            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }

        // PUT: api/players/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putplayer(int id, player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != player.player_id)
            {
                return BadRequest();
            }

            db.Entry(player).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!playerExists(id))
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

        // POST: api/players
        [ResponseType(typeof(player))]
        public IHttpActionResult Postplayer(player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.players.Add(player);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (playerExists(player.player_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = player.player_id }, player);
        }

        // DELETE: api/players/5
        [ResponseType(typeof(player))]
        public IHttpActionResult Deleteplayer(int id)
        {
            player player = db.players.Find(id);
            if (player == null)
            {
                return NotFound();
            }

            db.players.Remove(player);
            db.SaveChanges();

            return Ok(player);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool playerExists(int id)
        {
            return db.players.Count(e => e.player_id == id) > 0;
        }
    }
}