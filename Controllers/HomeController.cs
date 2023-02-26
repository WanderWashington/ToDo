using Microsoft.AspNetCore.Mvc;
using Todo.Data;

namespace ToDo.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("/")]
        public IActionResult Get([FromServices] AppDbContext context)
        { 
            return Ok(context.Todos.ToList());
        }

        [HttpPost]
        [Route("/")]
        public IActionResult Post(
            [FromBody]TodoModel todo,
            [FromServices] AppDbContext context)
        { 
            context.Todos.Add(todo);
            context.SaveChanges();

            return Created($"{todo.Id}", todo);
        }

        [HttpGet]
        [Route("/{id:int}")]
        public IActionResult GetById(
            [FromRoute]int id,
            [FromServices] AppDbContext context)
        { 
            var todos = context.Todos.FirstOrDefault(x=> x.Id == id);
            if(todos == null)
                return NotFound();
            return Ok(todos);
        }

        [HttpPut]
        [Route("/{id:int}")]
        public IActionResult Put(
            [FromRoute] int id,
            [FromBody] TodoModel todo,
            [FromServices] AppDbContext context)
        { 
            var model = context.Todos.FirstOrDefault(x=> x.Id == id);
            if (model == null)
                return NotFound();

            model.Title = todo.Title;
            model.Done = todo.Done;
            
            context.Todos.Update(model);
            context.SaveChanges();
            return Ok(model);
        }

        [HttpDelete]
        [Route("/{id:int}")]
        public IActionResult Delete(
            [FromRoute] int id,
            [FromServices] AppDbContext context)
        { 
            var model = context.Todos.FirstOrDefault(x=> x.Id == id);
            if (model == null )
                return NotFound();;

           
            context.Todos.Remove(model);
            context.SaveChanges();
            return Ok("Successfully deleted");
        }

    }
}