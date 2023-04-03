using System;
using System.Collections.Generic;
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
        [Route("api/Roles/GetAllRoles")]
        public IHttpActionResult GetAllRoles()
        {
            return Ok(connection.Role.Select(r => new
            { r.Id, r.Name}));
        }

        [HttpGet]
        [Route("api/Roles/{RoleName}")]
        public IHttpActionResult GetRole(string RoleName)
        {
            var foundedRole = connection.Role.FirstOrDefault(x => x.Name == RoleName);

            if (foundedRole == null)
            {
                return BadRequest("Такого пользователя не существует!");
            }

            return Ok(new
            {
                foundedRole.Id,
                foundedRole.Name
            });
        }

        [HttpGet]
        [Route("api/Users/{Name}")]
        public IHttpActionResult GetUser(string Name)
        {
            var foundedUser = connection.User.FirstOrDefault(x => x.Name == Name);

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
    }
}
