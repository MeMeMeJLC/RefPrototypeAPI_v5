﻿using System;
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
    public class game_incidentController : ApiController
    {
        private Entities6 db = new Entities6();

        // GET: api/game_incident
        public IQueryable<game_incident> Getgame_incident()
        {
            return db.game_incident;
        }

        // GET: api/game_incident/5
        [ResponseType(typeof(game_incident))]
        public IHttpActionResult Getgame_incident(int id)
        {
            game_incident game_incident = db.game_incident.Find(id);
            if (game_incident == null)
            {
                return NotFound();
            }

            return Ok(game_incident);
        }

        // PUT: api/game_incident/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putgame_incident(int id, game_incident game_incident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != game_incident.game_incident_id)
            {
                return BadRequest();
            }

            db.Entry(game_incident).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!game_incidentExists(id))
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

        // POST: api/game_incident
        [ResponseType(typeof(game_incident))]
        public IHttpActionResult Postgame_incident(game_incident game_incident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.game_incident.Add(game_incident);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = game_incident.game_incident_id }, game_incident);
        }

        // DELETE: api/game_incident/5
        [ResponseType(typeof(game_incident))]
        public IHttpActionResult Deletegame_incident(int id)
        {
            game_incident game_incident = db.game_incident.Find(id);
            if (game_incident == null)
            {
                return NotFound();
            }

            db.game_incident.Remove(game_incident);
            db.SaveChanges();

            return Ok(game_incident);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool game_incidentExists(int id)
        {
            return db.game_incident.Count(e => e.game_incident_id == id) > 0;
        }
    }
}