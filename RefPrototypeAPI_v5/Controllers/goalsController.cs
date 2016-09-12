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
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using Newtonsoft.Json;

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

        // GET: api/goals?game_goals_id={game_goals_id}
        public  async System.Threading.Tasks.Task<Score> GetGoalsByTeamAndGame(int game_goals_id)
        {
            var url = "http://localhost:50588/api/players";
            var goals = db.goals.Where(t => t.game_id == game_goals_id).ToList();
            int? team_one_id = 0;
            int? team_two_id = 0;
            int total_goals = 0;
            int team_one_goals = 0;
            int team_two_goals = 0;
            int num_of_teams = 0;
            int? playerteam = 0;
            //var int team_two;
            foreach (var goal in goals)
            {
                int? player_id = goal.player_id;
                var url_param = url + "/" + player_id;
                //run a get request for team
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);

                //Add accept header for JSON format
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //list data response
               // HttpResponseMessage response = client.GetAsync(url_param).Result; //blocking call
                HttpResponseMessage response = client.GetAsync(url_param).Result; //blocking call

                if (response.IsSuccessStatusCode)
                {
                    player player = await response.Content.ReadAsAsync<player>();
                    playerteam = player.team_id;
                    if (playerteam == team_one_id)
                    {
                        if (goal.is_own_goal == true)
                        {
                            team_two_goals += 1;
                        }
                        else
                        {
                            team_one_goals += 1;
                        }
                    }
                    else if (playerteam == team_two_id)
                    {
                        if (goal.is_own_goal == true)
                        {
                            team_one_goals += 1;
                        }
                        else
                        {
                            team_two_goals += 1;
                        }
                    }
                    else if (team_one_id == 0)
                    {
                        team_one_id = playerteam;
                        if (goal.is_own_goal == true)
                        {
                            team_two_goals += 1;
                        }
                        else
                        {
                            team_one_goals += 1;
                        }
                    }
                    else
                    {
                        team_two_id = playerteam;
                        if (goal.is_own_goal == true)
                        {
                            team_one_goals += 1;
                        }
                        else
                        {
                            team_two_goals += 1;
                        }
                    }
                }
            }

            Score score = new Score(team_one_id, team_one_goals, team_two_id, team_two_goals);
            return score;
       
            //return "team one id = " + team_one_id + " goals = " + team_one_goals + ", team two id = " + team_two_id + " goals = " + team_two_goals;
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

[DataContract]
public class Score
{
    [DataMember]
    public int? Team_one_id { get; set; }
    [DataMember]
    public int Team_one_number_of_goals { get; set; }
    [DataMember]
    public int? Team_two_id { get; set; }
    [DataMember]
    public int Team_two_number_of_goals { get; set; }

    public Score( int? team_one_id, int team_one_number_of_goals, int? team_two_id, int team_two_number_of_goals)
    {
        Team_one_id = team_one_id;
        Team_one_number_of_goals = team_one_number_of_goals;
        Team_two_id = team_two_id;
        Team_two_number_of_goals = team_two_number_of_goals;
    }
}