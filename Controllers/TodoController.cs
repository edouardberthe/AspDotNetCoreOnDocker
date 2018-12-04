using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MySimpleWebAPI.Models;

namespace MySimpleWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;
        private const string GetRouteName = "GetTodo";

        public TodoController(TodoContext context)
        {
            _context = context;
            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem {Name = "Item1"});
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public List<TodoItem> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        [HttpGet("{id}", Name = GetRouteName)]
        public ActionResult<TodoItem> GetById(long id)
        {
            var item = _context.TodoItems.Find(id);
            if (item == null)
                return NotFound();
            return item;
        }

        /// <summary>
        /// Creates a TodoItem
        /// </summary>
        /// <remarks>
        ///     POST /todo
        ///     {
        ///         "id": 1,
        ///         "name": "Item1",
        ///         "isComplete": false
        ///     }
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created TodoItem</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult Create(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute(GetRouteName, new {id = item.Id}, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, TodoItem item)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _context.TodoItems.Update(item);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deletes a specific TodoItem
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo == null)
                return NotFound();

            _context.TodoItems.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}