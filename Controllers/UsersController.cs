//Importações - São 'bibliotecas' que contém funcionalidades e classes necessárias para o código funcionar.
using Microsoft.AspNetCore.Mvc; //Contém classes e interfaces para criar controladores e manipular requisições e respostas \HTTP em um projeto ASP.NET Core
using System.Collections.Generic;
using System.Linq;

namespace ApiCore.Controllers
{
    public class User
    {
        // Definição da classe User
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }

    [ApiController]
    [Route("api/users")]
    public class UsersController:ControllerBase
    {
        //Mock Jason de usuários
        //private static List<User> users = new List<User>
        //{
        //    new User { Id = 1, Name = "Bárbara Marcello", Email = "barbaramarcello@example.com"},
        //    new User { Id = 2, Name = "Rafael Marcello", Email = "rafaelmarcello@example.com"}

        //};

        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        //Método Get para listar todos os usuários
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        //Método GET para buscar um usuário por ID
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        //Metódo POST para criar um novo usuário
        [HttpPost]
        public IActionResult CreateUser([FromBody] User newUser)
        {
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        //Método PUT para atualizar um usuário
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updateUser)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = updateUser.Name;
            user.Email = updateUser.Email;

            _context.SaveChanges();

            return NoContent();
        }

        //Método DELETE para remover usuário
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return NoContent();
        }
    }
}