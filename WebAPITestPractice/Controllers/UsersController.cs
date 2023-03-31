using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            return Ok(connection.Users.Select(u => new
            { u.Id, u.Lastname, u.Firstname, u.Patronymic, u.Age, u.Role_Id }));
        }

        [HttpGet]
        [Route("api/Users/{Lastname}")]
        public IHttpActionResult GetUser(string Lastname)
        {
            var foundedUser = connection.Users.FirstOrDefault(x => x.Lastname == Lastname);

            if(foundedUser == null)
            {
                return BadRequest("Такого пользователя не существует!");
            }

            return Ok(new 
            {
                foundedUser.Id,
                foundedUser.Lastname,
                foundedUser.Firstname,
                foundedUser.Patronymic,
                foundedUser.Age,
                foundedUser.Role_Id
            });
        }
    }
}
