using System;
using keepr.Models;
using keepr.Repositories;
using keepr_server.Models;

namespace keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;

    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

    internal VaultKeep Create(VaultKeep newWLP)
    {
      //TODO if they are creating a vaultkeep, make sure they are the creator of the list
      return _repo.Create(newWLP);

    }

    internal String Delete(int id, string userId)
      {
         VaultKeep original = GetById(id);
         if (original.CreatorId != userId) { throw new Exception("Access Denied: Cannot Delete a Vault You did not Create"); }
         _repo.Remove(id);
         return "successfully deletorted";
      }

        private VaultKeep GetById(int id)
        {
           VaultKeep data = _repo.GetById(id);
         if (data == null)
         {
            throw new Exception("Invalid Id");
         }
         return data;
        }
    }
}