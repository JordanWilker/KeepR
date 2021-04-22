using System;
using System.Collections.Generic;
using System.Linq;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
   public class VaultsService
   {
      private readonly VaultsRepository _repo;

      public VaultsService(VaultsRepository repo)
      {
         _repo = repo;
      }

      internal IEnumerable<Vault> GetAll()
      {
         {
      IEnumerable<Vault> vaults = _repo.GetAll();
      return vaults.ToList().FindAll(v => !v.isPrivate);
    }
      }
      internal Vault GetByIdforDelete(int id)
      {
         Vault data = _repo.GetByIdforDelete(id);
         if (data == null)
         {
            throw new Exception("Invalid Id Yeah");
         }
         return data;
      }
       internal Vault GetByIdYeah(int id)
      {
         Vault vaults = _repo.GetByIdforDelete(id);
         if (vaults == null)
         {
            throw new Exception("Inval Id");
         }
         return vaults;
      }
      

      internal Vault GetById(int id)
      {
         Vault vaults = _repo.GetById(id);
         if (vaults == null)
         {
            throw new Exception("Invalidalkjad;lk Id");
         }
         return vaults;
      }
      


      internal Vault Create(Vault newVault)
      {
         return _repo.Create(newVault);
      }

      internal Vault Edit(Vault editData, string userId)
      {
         Vault data = GetById(editData.Id);
         if (data.creatorId != userId) { throw new Exception("Access Denied: Cannot Edit a Restaurant You did not Create"); }

         data.Name = editData.Name != null ? editData.Name : data.Name;
         data.Description = editData.Description != null ? editData.Description : data.Description;

         return _repo.Edit(data);
      }

       internal String Delete(int id, string userId)
      {
         Vault original = GetByIdforDelete(id);
         if (original.creatorId != userId) { throw new Exception("Access Denied: Cannot Delete a Vault You did not Create"); }
         _repo.Remove(id);
         return "successfully deletorted";
      }
      internal object GetVaultsByProfileId(string id)
    {
      IEnumerable<Vault> vaults = _repo.GetByCreatorId(id);
      return vaults.ToList().FindAll(r => r.isPrivate == false);

    }
      internal object GetVaultsByAccountId(string id)
    {
      IEnumerable<Vault> vaults = _repo.GetByAccountId(id);
      return vaults;

   }
}
}