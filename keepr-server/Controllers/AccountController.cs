using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using keepr.Models;
using keepr.Services;
using System.Collections.Generic;

namespace keepr.Controllers
{
    
    
        
  [ApiController]
  [Route("[controller]")]

  // REVIEW this tag enforces the user must be logged in
  [Authorize]
  public class AccountController : ControllerBase
  {
    private readonly ProfilesService _pservice;
    private readonly VaultsService _vservice;
    private readonly KeepsService _kservice;

    public AccountController(ProfilesService pservice, VaultsService vservice, KeepsService kservice)
    {
      _pservice = pservice;
      _vservice = vservice;
      _kservice = kservice;
    }

    [HttpGet]
    // REVIEW async calls must return a System.Threading.Tasks, this is equivalent to a promise in JS
    public async Task<ActionResult<Profile>> GetAsync()
    {
      try
      {
        // REVIEW how to get the user info from the request token
        // same as to req.userInfo
        //MAKE SURE TO BRING IN codeworks.auth0provider
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_pservice.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("vaults")]
    public async Task<ActionResult<IEnumerable<Vault>>> GetVaultsByAccountId()
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_vservice.GetVaultsByAccountId(userInfo.Id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("keeps")]
    public async Task<ActionResult<IEnumerable<Vault>>> GetKeepsByAccountId()
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_kservice.GetKeepsByAccountId(userInfo.Id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
} 
    
