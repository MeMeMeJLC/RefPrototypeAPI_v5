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
    public class teamsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/teams
        public IQueryable<team> Getteams()
        {
            return db.teams;
        }

        // GET: api/teams/5
        [ResponseType(typeof(team))]
        public IHttpActionResult Getteam(int id)
        {
            team team = db.teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        // GET: api/teams?team_name={team_name}
        public IEnumerable<team> GetTeamsByTeamName(string team_name)
        {
            var teams = db.teams.Where(t => t.team_name == team_name).ToList();
            if (teams == null || !teams.Any())
            {
                //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                return teams;
            }

            return teams;
        }

        // GET: api/teams?team_coach_last_name={team_coach_last_name}
        public IEnumerable<team> GetTeamsByCoachLastname(string team_coach_lastname)
        {
            var teams = db.teams.Where(t => t.team_coach_last_name == team_coach_lastname).ToList();
            if (teams == null || !teams.Any())
            {
                //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                return teams;
            }

            return teams;
        }

        // PUT: api/teams/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putteam(int id, team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != team.team_id)
            {
                return BadRequest();
            }

            db.Entry(team).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!teamExists(id))
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

        // POST: api/teams
        [ResponseType(typeof(team))]
        public IHttpActionResult Postteam(team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.teams.Add(team);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = team.team_id }, team);
        }

        // DELETE: api/teams/5
        [ResponseType(typeof(team))]
        public IHttpActionResult Deleteteam(int id)
        {
            team team = db.teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            db.teams.Remove(team);
            db.SaveChanges();

            return Ok(team);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool teamExists(int id)
        {
            return db.teams.Count(e => e.team_id == id) > 0;
        }
    }
}