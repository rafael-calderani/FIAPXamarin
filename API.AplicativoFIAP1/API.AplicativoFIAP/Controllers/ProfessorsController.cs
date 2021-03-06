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
using API.AplicativoFIAP;
using API.AplicativoFIAP.Models;

namespace API.AplicativoFIAP.Controllers
{
    public class ProfessorsController : ApiController
    {
        private FiapDbContext db = new FiapDbContext();

        // GET: api/Professors
        public IQueryable<Professor> GetProfessors()
        {
            return db.Professors;
        }

        // GET: api/Professors/5
        [ResponseType(typeof(Professor))]
        public IHttpActionResult GetProfessor(int id)
        {
            Professor professor = db.Professors.Find(id);
            if (professor == null)
            {
                return NotFound();
            }

            return Ok(professor);
        }

        // PUT: api/Professors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProfessor(int id, Professor professor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != professor.Id)
            {
                return BadRequest();
            }

            db.Entry(professor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfessorExists(id))
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

        // POST: api/Professors
        [ResponseType(typeof(Professor))]
        public IHttpActionResult PostProfessor(Professor professor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ProfessorExists(professor.Id))
            {
                db.Professors.Add(professor);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = professor.Id }, professor);
            }
            else
            {
                db.Entry(professor).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        // DELETE: api/Professors/5
        [ResponseType(typeof(Professor))]
        public IHttpActionResult DeleteProfessor(int id)
        {
            Professor professor = db.Professors.Find(id);
            if (professor == null)
            {
                return NotFound();
            }

            db.Professors.Remove(professor);
            db.SaveChanges();

            return Ok(professor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProfessorExists(int id)
        {
            return db.Professors.Count(e => e.Id == id) > 0;
        }
    }
}