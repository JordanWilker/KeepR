using System;
using Microsoft.AspNetCore.Mvc;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using System.Collections.Generic;

namespace keepr.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class VaultsController : ControllerBase
   {
      private readonly VaultsService _service;
      private readonly KeepsService _kservice;
      public VaultsController(VaultsService service, KeepsService kservice)
      {
         _service = service;
         _kservice = kservice;
      }

      [HttpGet]
      public ActionResult<Vault> Get()
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
      public ActionResult<Vault> GetAll(int id)
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
      public async Task<ActionResult<Vault>> Post([FromBody] Vault newVault)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newVault.creatorId = userInfo.Id;
        newVault.Creator = userInfo;
        Vault created = _service.Create(newVault);
        return Ok(created);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }     


      [HttpPut("{id}")]
      [Authorize]
    public async Task<ActionResult<Vault>> Edit(int id, [FromBody] Vault editData)
      {
         try
         {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
            editData.Id = id;
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
      
      [HttpGet("{id}/keeps")]  // NOTE '{}' signifies a var parameter
      public ActionResult<IEnumerable<VaultKeepsViewModel>> GetKeepsByVaultId(int id)
      {
         try
         {
            return Ok(_kservice.GetKeepsByVaultId(id));
         }
         catch (Exception e)
         {
            return BadRequest(e.Message);
         }
      }
  }
}

   
