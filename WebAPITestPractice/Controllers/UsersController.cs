using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Web.Http;
using WebAPITestPractice.Models;

namespace WebAPITestPractice.Controllers
{
    public class UsersController : ApiController
    {
        WebAPIPracticeDBEntities connection = new WebAPIPracticeDBEntities();

        [HttpGet]
        [Route("api/Users/GetAllUsers")]
        public IHttpActionResult GetAllUsers()
        {
            return Ok(connection.User.Select(u => new
            { u.Id, u.Name, u.Age, RoleName = u.Role.Name }));
        }

        [HttpGet]
        [Route("api/Users/{id}")]
        public IHttpActionResult GetUser(int id)
        {
            var foundedUser = connection.User.FirstOrDefault(x => x.Id == id);

            if(foundedUser == null)
            {
                return BadRequest("Такого пользователя не существует!");
            }

            return Ok(new 
            {
                foundedUser.Id,
                foundedUser.Name,
                foundedUser.Age,
                RoleName = foundedUser.Role.Name
            });
        }

        [HttpPost]
        [Route("api/Users/Add")]
        public IHttpActionResult PostUser(User user)
        {
            if(user != null)
            {
                connection.User.Add(user);
                connection.SaveChanges();
            }
            return Ok();
        }

        [HttpPut]
        [Route("api/Users/Edit/{id}")]
        public IHttpActionResult PutUser(int id, User user) 
        {
            var foundedUser = connection.User.FirstOrDefault(u => u.Id == id);

            if(foundedUser == null)
            {
                return BadRequest("Пользователь не найден");
            }

            try
            {
                foundedUser.Name = user.Name;
                foundedUser.Age = user.Age;
                foundedUser.Role_Id = user.Role_Id;

                connection.SaveChanges();
            }
            catch
            {
                return BadRequest("Произошла ошибка");
            }
            return Ok($"Пользователь {foundedUser.Name} изменен");
        }

        [HttpDelete]
        [Route("api/Users/Delete/{name}")]
        public IHttpActionResult DeleteUser(string name)
        {
            var user = connection.User.FirstOrDefault(u => u.Name == name);

            if (user == null)
            {
                return BadRequest("Пользователь не найден");
            }

            connection.User.Remove(user);
            connection.SaveChanges();

            return Ok($"Пользователь {name} удален");
        }
    }
}
