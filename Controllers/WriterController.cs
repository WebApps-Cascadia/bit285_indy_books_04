using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IndyBooks.Models;
using IndyBooks.ViewModels;
using System.Net.Http;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IndyBooks.Controllers
{
    [Route("api/[controller]")]
    public class WriterController : ControllerBase
    {
        private IndyBooksDataContext _db;
        public WriterController(IndyBooksDataContext db) 
        { 
            _db = db; 
        }
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            //entire list of writers (without book information)
            var writers = _db.Writers.Select(w => new {w.Id, w.Name});
            return Ok(writers);
        }


        // GET api/Writer/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            //The info for a SINGLE writer(without book information)
            if (id > 0) {
                var writers = _db.Writers.Select(w => new { w.Id, w.Name })
                    .Where(w => w.Id == id);
                return Ok(writers);
            }
            else
            {
                return NotFound();
            }
        }
        // POST api/writer/
        [HttpPost]
        public IActionResult Post([FromBody] Writer writer)
        {
            //Add a new writer to the list
            Writer newWriter = new Writer
            {
                Name = writer.Name
            };
            _db.Writers.Add(newWriter);
            _db.SaveChanges();
            return Accepted();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] Writer writer)
        {
            //Update writer's information when provided with an acceptable id
            if (id > 0)
            {
                var author = _db.Writers.Single(w => w.Id == id);
                author.Name = writer.Name;
                _db.SaveChanges();
                return Accepted();
            }

            else
            {
                return NotFound();
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            //remove a writer from the database
            if (id > 0)
            {
                var author = _db.Writers.Single(w => w.Id == id);
                _db.Writers.Remove(author);
                _db.SaveChanges();
                return Accepted();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
