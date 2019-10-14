using BlazorCRUD.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCRUD.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PeopleController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // All persons
        [HttpGet]
        public async Task<ActionResult<List<Person>>> Get()
        {
            return await context.People.ToListAsync();
        }

        [HttpGet("{id}", Name = "GetPerson")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            // filter by id
            return await context.People.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Person person)
        {
            context.Add(person);
            await context.SaveChangesAsync();
            // 
            return new CreatedAtRouteResult("GetPerson", new { id = person.Id }, person);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Person person)
        {
            context.Entry(person).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var person = new Person { Id = id };
            context.Remove(person);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
