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
using SAPapp.Models;

namespace SAPapp.Controllers
{
    public class EmployeesController : ApiController
    {
        private Context db = new Context();

        // GET: api/Employees
        public IQueryable<Employees> GetEmployees()
        {
            db.Configuration.LazyLoadingEnabled = true;
            var list = db.Employees.ToList();
            return db.Employees;
        }

        // GET: api/Employees/5
        [ResponseType(typeof(Employees))]
        public IHttpActionResult GetEmployees(int id)
        {
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployees(int id, Employees employees)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employees.Id)
            {
                return BadRequest();
            }

            db.Entry(employees).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeesExists(id))
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

        // POST: api/Employees
        [ResponseType(typeof(Employees))]
        public IHttpActionResult PostEmployees(Employees employees)
        {
            db.Configuration.LazyLoadingEnabled = true;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            db.Employees.Add(employees);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employees.Id }, employees);
        }

        // DELETE: api/Employees/5
        [ResponseType(typeof(Employees))]
        public IHttpActionResult DeleteEmployees(int id)
        {
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employees);
            db.SaveChanges();

            return Ok(employees);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeesExists(int id)
        {
            return db.Employees.Count(e => e.Id == id) > 0;
        }
    }
}