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
    public class goalsController : ApiController
    {
        private Entities7 db = new Entities7();

        // GET: api/goals
        public IQueryable<goal> Getgoals()
        {
            return db.goals;
        }

        // GET: api/goals/5
        [ResponseType(typeof(goal))]
        public IHttpActionResult Getgoal(int id)
        {
            goal goal = db.goals.Find(id);
            if (goal == null)
            {
                return NotFound();
            }

            return Ok(goal);
        }

        // PUT: api/goals/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putgoal(int id, goal goal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != goal.goal_id)
            {
                return BadRequest();
            }

            db.Entry(goal).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!goalExists(id))
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

        // POST: api/goals
        [ResponseType(typeof(goal))]
        public IHttpActionResult Postgoal(goal goal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.goals.Add(goal);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = goal.goal_id }, goal);
        }

        // DELETE: api/goals/5
        [ResponseType(typeof(goal))]
        public IHttpActionResult Deletegoal(int id)
        {
            goal goal = db.goals.Find(id);
            if (goal == null)
            {
                return NotFound();
            }

            db.goals.Remove(goal);
            db.SaveChanges();

            return Ok(goal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool goalExists(int id)
        {
            return db.goals.Count(e => e.goal_id == id) > 0;
        }
    }
}