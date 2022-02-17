using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Handlers;
using TodoApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private IDbHandler _dbHandler;
        public TodoController(IDbHandler dbHandler)
        {
            _dbHandler = dbHandler;
        }

        [HttpGet("id")]
        public IActionResult GetNextId()
        {
            var nextId = _dbHandler.GetNextId();
            return Ok(nextId);
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var todos = _dbHandler.GetTodoItems();
            return Ok(todos);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] TodoModel value)
        {
            _dbHandler.InsertTodo(value);
            return Ok();
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody] TodoModel value)
        {
            _dbHandler.UpdateTodo(value);
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _dbHandler.DeleteTodo(id);
            return Ok();
        }
    }
}
