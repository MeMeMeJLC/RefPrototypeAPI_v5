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
    public class gamesController : ApiController
    {
        private Entities2 db = new Entities2();

        // GET: api/games
        public IQueryable<game> Getgames()
        {
            return db.games;
        }

        // GET: api/games/5
        [ResponseType(typeof(game))]
        public IHttpActionResult Getgame(int id)
        {
            game game = db.games.Find(id);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        // PUT: api/games/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putgame(int id, game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != game.game_id)
            {
                return BadRequest();
            }

            db.Entry(game).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!gameExists(id))
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

        // POST: api/games
        [ResponseType(typeof(game))]
        public IHttpActionResult Postgame(game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.games.Add(game);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = game.game_id }, game);
        }

        // DELETE: api/games/5
        [ResponseType(typeof(game))]
        public IHttpActionResult Deletegame(int id)
        {
            game game = db.games.Find(id);
            if (game == null)
            {
                return NotFound();
            }

            db.games.Remove(game);
            db.SaveChanges();

            return Ok(game);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool gameExists(int id)
        {
            return db.games.Count(e => e.game_id == id) > 0;
        }
    }
}