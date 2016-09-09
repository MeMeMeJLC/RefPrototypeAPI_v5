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

        // GET: api/goals?game_id={game_id}
        public IEnumerable<goal> GetGoalsByGame(int game_id)
        {
            var goals = db.goals.Where(t => t.game_id == game_id).ToList();
            if (goals == null || !goals.Any())
            {
                //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                return goals;
            }

            return goals;
        }

        // GET: api/goals?player_id={player_id}
        public IEnumerable<goal> GetGoalsByPlayerId(int player_id)
        {
            var goals = db.goals.Where(t => t.player_id == player_id).ToList();
            if (goals == null || !goals.Any())
            {
                //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                return goals;
            }

            return goals;
        }

        // GET: api/goals?is_own_goal={is_own_goal}
        public IEnumerable<goal> GetGoalsByIsOwnGoal(bool is_own_goal)
        {
            var goals = db.goals.Where(t => t.is_own_goal == is_own_goal).ToList();
            if (goals == null || !goals.Any())
            {
                //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                return goals;
            }

            return goals;
        }

        // GET: api/goals?game_id={game_id}
        public /*IEnumerable<goal>*/ string GetGoalsByTeamAndGame(int game_goals_id)
        {
            var url = "http://localhost:50588/api/teams";
            var url_param = "?game_id=1";
            var goals = db.goals.Where(t => t.game_id == game_goals_id).ToList();
            var total_goals = 0;
            var team_one_goals = 0;
            var team_two_goals = 0;
            var num_of_teams = 0;
            //var int team_two;
            foreach (var goal in goals)
            {
                //run a get request for team
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);

                //Add accept header for JSON format
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //list data response
                HttpResponseMessage response = client.GetAsync(url_param).Result; //blocking call
                if (response.IsSuccessStatusCode)
                {
                    //parse the response body. blocking!
                    var teams = response.Content.ReadAsAsync<IEnumerable<team>>().Result;
                    foreach (var d in teams)
                    {
                        num_of_teams += 1; //i want this to return 2, currently returning 27
                    }
                }

       
                //total_goals += 1;
            }

            if (goals == null || !goals.Any())
            {
                //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                //return goals;
            }

            //return goals;
            return "number of teams in game " + game_goals_id + " = " + num_of_teams;
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