using System;
using System.Collections.Generic;
using System.Linq;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
   public class KeepsService
   {
      private readonly KeepsRepository _repo;
      private readonly VaultsRepository _vrepo;

      public KeepsService(KeepsRepository repo, VaultsRepository vrepo)
      {
         _repo = repo;
         _vrepo = vrepo;
      }

      internal IEnumerable<Keep> GetAll()
      {
         return _repo.GetAll();
      }

      internal Keep GetById(int id)
      {
         Keep data = _repo.GetById(id);
         if (data == null)
         {
            throw new Exception("Invalid Id");
         }
         return data;
      }

      internal Keep Create(Keep newKeep)
      {
         return _repo.Create(newKeep);
      }

     internal Keep Edit(Keep editData, string userId)
    {
      Keep original = GetById(editData.Id);
      if (original.creatorId != userId) { throw new Exception("Access Denied: Cannot Edit a Restaurant You did not Create"); }
      editData.Name = editData.Name == null ? original.Name : editData.Name;
      editData.Description = editData.Description == null ? original.Description : editData.Description;

      return _repo.Edit(editData);

    }

      internal String Delete(int id, string userId)
      {
         Keep original = GetById(id);
         if (original.creatorId != userId) { throw new Exception("Access Denied: Cannot Delete a Keep You did not Create"); }
         _repo.Remove(id);
         return "successfully deleted";
      }
      internal IEnumerable<VaultKeepsViewModel> GetKeepsByVaultId(int id)
    {
      Vault data = _vrepo.GetById(id);
      if( data.isPrivate == true){throw new Exception("bad");}
      return _repo.GetKeepsByVaultId(id);
    }

    internal object GetKeepsByProfileId(string id, bool isPrivate)
    {
      IEnumerable<Keep> keeps = _repo.GetByCreatorId(id);
      if (isPrivate == true){throw new Exception("Bad");}
      return keeps;
    }
    internal object GetKeepsByAccountId(string id)
    {
      IEnumerable<Keep> keeps = _repo.GetByAccountId(id);
      return keeps;

   }
   }
}