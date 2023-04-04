using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPITestPractice.Models;

namespace WebAPITestPractice.Controllers
{
    public class RolesController : ApiController
    {
        WebAPIPracticeDBEntities connection = new WebAPIPracticeDBEntities();

        [HttpGet]
        [Route("api/Roles/GetAllRoles")]
        public IHttpActionResult GetAllRoles()
        {
            return Ok(connection.Role.Select(r => new
            { r.Id, r.Name }));
        }

        [HttpGet]
        [Route("api/Roles/{RoleName}")]
        public IHttpActionResult GetRole(string RoleName)
        {
            var foundedRole = connection.Role.FirstOrDefault(x => x.Name == RoleName);

            if (foundedRole == null)
            {
                return BadRequest("Такой роли не существует!");
            }

            return Ok(new
            {
                foundedRole.Id,
                foundedRole.Name
            });
        }
    }
}
