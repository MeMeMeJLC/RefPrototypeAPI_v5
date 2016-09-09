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
    public class incidentsController : ApiController
    {
        private Entities3 db = new Entities3();

        // GET: api/incidents
        public IQueryable<incident> Getincidents()
        {
            return db.incidents;
        }

        // GET: api/incidents/{incident_type}
        [ResponseType(typeof(incident))]
        public IHttpActionResult Getincident(string id)
        {
            incident incident = db.incidents.Find(id);
            if (incident == null)
            {
                return NotFound();
            }

            return Ok(incident);
        }

        // GET: api/incidents?incident_description={incident_description}
        //needs exact match of string
        public IEnumerable<incident> GetIncidentByDescription(string incident_description)
        {
            var incidents = db.incidents.Where(t => t.incident_description == incident_description).ToList();
            if (incidents == null || !incidents.Any())
            {
                //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                return incidents;
            }

            return incidents;
        }

        // PUT: api/incidents/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putincident(string id, incident incident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != incident.incident_type)
            {
                return BadRequest();
            }

            db.Entry(incident).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!incidentExists(id))
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

        // POST: api/incidents
        [ResponseType(typeof(incident))]
        public IHttpActionResult Postincident(incident incident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.incidents.Add(incident);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (incidentExists(incident.incident_type))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = incident.incident_type }, incident);
        }

        // DELETE: api/incidents/5
        [ResponseType(typeof(incident))]
        public IHttpActionResult Deleteincident(string id)
        {
            incident incident = db.incidents.Find(id);
            if (incident == null)
            {
                return NotFound();
            }

            db.incidents.Remove(incident);
            db.SaveChanges();

            return Ok(incident);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool incidentExists(string id)
        {
            return db.incidents.Count(e => e.incident_type == id) > 0;
        }
    }
}