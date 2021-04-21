using System;
using Microsoft.AspNetCore.Mvc;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;

namespace keepr.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class KeepsController : ControllerBase
   {
      private readonly KeepsService _service;
      public KeepsController(KeepsService service)
      {
         _service = service;
      }

      [HttpGet]
      public ActionResult<Keep> Get()
      {
         try
         {
            return Ok(_service.GetAll());
         }
         catch (Exception e)
         {
            return BadRequest(e.Message);
         }
      }

      [HttpGet("{id}")]
      public ActionResult<Keep> GetAll(int id)
      {
         try
         {
            return Ok(_service.GetById(id));
         }
         catch (Exception e)
         {
            return BadRequest(e.Message);
         }
      }

      [HttpPost]
      [Authorize]
      public async Task<ActionResult<Keep>> Post([FromBody] Keep newKeep)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newKeep.creatorId = userInfo.Id;
        newKeep.Creator = userInfo;
        Keep created = _service.Create(newKeep);

        return Ok(created);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }     
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> Edit(int id, [FromBody] Keep editData)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        editData.Id = id;
        editData.Creator = userInfo;
        editData.creatorId = userInfo.Id;
        return Ok(_service.Edit(editData, userInfo.Id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
      [HttpDelete("{id}")]
      [Authorize]
      public async Task<ActionResult<string>> Delete(int id)
      {
         try
         {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
            return Ok(_service.Delete(id, userInfo.Id));
         }
         catch (Exception e)
         {
            return BadRequest(e.Message);
         }
      }
   }
}