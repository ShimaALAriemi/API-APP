using APIapp.Context;
using APIapp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace APIapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public ToDoController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [ProducesResponseType(statusCode:200)]
        [ProducesResponseType(statusCode:404)]

        public ActionResult<IEnumerable<ToDo>> GetAll()
        {
            var todos = _dbContext.ToDoList.ToList();
            if (todos.Count > 0)
            {
                return Ok(todos);
            }
            //var todo = new List<ToDo>()
            //{
            //    new ToDo{ID = 1, Name = "Update User LogIn", Description = "I have to Add other Action"},
            //    new ToDo{ID = 2, Name = "Teem Meeting", Description = "I have meet them at 3 pm"},
            //    new ToDo{ID = 3, Name = "Project Task", Description = "I have done my tasks"}


            //};
            //return todo;
            return NotFound();
        }

        [HttpGet("id:int")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 404)]
        [ProducesResponseType(statusCode: 400)]

        public ActionResult<ToDo> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
             var todo = _dbContext.ToDoList.Find(id);
            
            if (todo == null) 
                {
                return NotFound();
                }
            return Ok(todo);

            //return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 404)]
        [ProducesResponseType(statusCode: 400)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult Create(ToDo toDo)
        {
            if (toDo == null)
            {
                return BadRequest(toDo);
            }

            if (toDo.ID > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }

            if (ModelState.IsValid)
            {
                _dbContext.ToDoList.Add(toDo);
                _dbContext.SaveChanges();
                return Ok(toDo);
            }
            return BadRequest();
        }

        [HttpDelete("id:int")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 404)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var taskDone = _dbContext.ToDoList.Where(o => o.ID == id).FirstOrDefault();
            if (taskDone != null) {
                _dbContext.ToDoList.Remove(taskDone);
                _dbContext.SaveChanges();
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut("id:int")]
        [ProducesResponseType(statusCode: 404)]
        [ProducesResponseType(statusCode: 400)]
        public ActionResult Update(int id, ToDo toDo)
        {
            if (id == 0 || id != toDo.ID || toDo == null)
            {
                return BadRequest();
            }
            var taskUpdated = _dbContext.ToDoList.Where(o => o.ID == id).FirstOrDefault();

            if (taskUpdated != null) if (ModelState.IsValid)
            {
                    taskUpdated.Name = toDo.Name;
                    taskUpdated.Description = toDo.Description;
                    

                _dbContext.ToDoList.Update(taskUpdated);
                _dbContext.SaveChanges();
                    return NoContent();
                }
            return BadRequest();
        }

        [HttpPatch("id:int")]
        [ProducesResponseType(statusCode: 404)]
        [ProducesResponseType(statusCode: 400)]
        public ActionResult Update(int id, JsonPatchDocument<ToDo> document)
        {
            if (id == 0 || document == null)
            {
                return BadRequest();
            }

            var taskUpdated = _dbContext.ToDoList.Where(o => o.ID == id).FirstOrDefault();
            if (taskUpdated != null)
            {
                document.ApplyTo(taskUpdated, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
