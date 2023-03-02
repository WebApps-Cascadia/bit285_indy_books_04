using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IndyBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace IndyBooks.Controllers
{
    [Route("api/writers")] //The base route for all calls to this controller
    public class WriterAPIController : Controller
    {
        private IndyBooksDbContext _db;
        public WriterAPIController(IndyBooksDbContext db) { _db = db; }

        /**
         * READ: Retrievs a collection of writers
         * uses a GET verb with the URL pattern "api/writers"
         */
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_db.Writers);
        }
        /**
         * DELETE: Removes a particular writer with the given {id}
         * uses a DELETE verb with the URL pattern "api/writers/37"
         */
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            //TODO: Search for record using Any(); if missing -> return NotFound();
            if (!_db.Writers.Any(w => w.Id == id)) { return NotFound(); }
            Writer writer = _db.Writers.Single(w => w.Id == id);
            //TODO: Make changes to DbContext, save to Database -> return Accepted();
            _db.Writers.Remove(writer);
            _db.SaveChanges();
            return Accepted();
        }
        /**
         * CREATE: Add a new writer to the collection
         * uses a POST verb with the URL pattern "api/writers"
         */
        [HttpPost]
        public IActionResult Post([FromBody]Writer writer)
        {
            //TODO: Test for an invalid ModelState -> return BadRequest();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //TODO: Make changes to DbContext, save to Database -> return Accepted(writer);

            _db.Writers.Add(writer);
            _db.SaveChanges();
            return Accepted(writer);

        }
        /**
         * READ: Retrieves a particular writer with the given {id}
         * uses a GET verb with the URL pattern "api/writers/41"
         */
        [HttpGet("{id}")] //TODO: uncomment this annotation and create the method
        public IActionResult Get(long id)
        {
            //TODO: Test for missing record using Any() -> return NotFound();
            if (!_db.Writers.Any(w => w.Id == id)) { return NotFound(); }
            Writer writer = _db.Writers.Single(w => w.Id == id);
            //TODO: Make changes to DbContext, save to Database -> return Ok(writer);
            return Ok(writer);
        }

        /** 
         * UPDATE: Modify a given writer with id and [FromBody]Writer writer parameters
         * uses a PUT verb with the URL pattern "api/writers/16"
         */
        [HttpPut("{id}")] //TODO: uncomment this annotation and create the method
        public IActionResult Put(long id, [FromBody] Writer writer)
        {
            //TODO: Test for an invalid ModelState -> return BadRequest();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!_db.Writers.Any(w => w.Id == id)) { return NotFound(); }

            //TODO: Test for missing record using Any() -> return NotFound();
            
            writer.Id = id;
            _db.Update(writer);
            _db.SaveChanges();
            return Accepted(writer);

        //TODO: Make changes to DbContext, save to Database -> return Accepted(writer);

        }
}
}

