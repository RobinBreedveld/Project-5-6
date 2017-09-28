using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using assignment3.Models;

namespace assignment3.Controllers
{
    //This is the default route of the API.
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private readonly MovieContext _context;
        
        public MoviesController (MovieContext context){
            _context = context;
        }
           
        
        // GET api/values
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            Console.Write("test");
            return  _context.Movies.ToList();
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var movie = _context.Movies.FirstOrDefault (t=>t.Id ==id);
            if (movie == null){
                return NotFound();
            }
            return new ObjectResult (movie);
        }

        // POST api/values
        [HttpPost]
        public void Post ([FromBody]Movie value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Movie value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id){}
    }
}


