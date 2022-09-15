using ArconsSystem.Api.Models;
using ArconsSystem.Api.Models.Users;
using ArconsSystem.Contract.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArconsSystem.Api.Controllers;

public class RolesController : ApiController
{
    private readonly DataContext _db;

    public RolesController(DataContext db)
    {
        _db = db;
    }

    [HttpGet]
    [Authorize(Policy = "Permission_GetRoles")]
    public IActionResult GetRoles()
    {
        var roles = _db.Roles;
        return Ok(roles);
    }

    [HttpPost]
    [Authorize(Policy = "Permission_MakeRoles")]
    public IActionResult CreateRole(CreateRoleRequest request)
    {
        var role = new Role() {Name = request.Name};
        _db.Roles.Add(role);
        _db.SaveChanges();
        return Created(HttpContext.Request.Host + HttpContext.Request.Path + "/" + role.Id, role);
    }
}