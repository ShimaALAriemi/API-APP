using APIapp.Context;
using APIapp.Model;
using Microsoft.AspNetCore.Http;
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

        public ActionResult GetAll()
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
        public ActionResult Get(int id)
        {
            var todo = _dbContext.ToDoList.Find(id);
            return Ok(todo);
            //return NotFound();
        }

        [HttpPost]
        
        public ActionResult Create(ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                _dbContext.ToDoList.Add(toDo);
                _dbContext.SaveChanges();
                return Ok(toDo);
            }
            return BadRequest();
        }

        [HttpPost("id:int")]
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
                return Ok(taskDone);
            }
            return BadRequest();
        }

        //[HttpPost]
        //public ActionResult Update(ToDo toDo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _dbContext.ToDoList.Update(toDo);
        //        _dbContext.SaveChanges();
        //        return Ok(toDo);
        //    }
        //    return BadRequest();
        //}
    }
}
