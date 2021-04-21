using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
   public class KeepsRepository
   {
      private readonly IDbConnection _db;

      public KeepsRepository(IDbConnection db)
      {
         _db = db;
      }

      internal IEnumerable<Keep> GetAll()
      {
         string sql = @"
      SELECT 
      keep.*,
      prof.*
      FROM keep keep
      JOIN profiles prof ON keep.creatorId = prof.id";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }, splitOn: "id");
      }

      internal Keep GetById(int id)
       {
      string sql = @" 
      SELECT 
      keep.*,
      prof.*
      FROM keep keep
      JOIN profiles prof ON keep.creatorId = prof.id
      WHERE keep.id = @id";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }, new { id }, splitOn: "id").FirstOrDefault();
    }

      internal Keep Create(Keep newKeep)
      {
         string sql = @"
         INSERT INTO keep
         (name, description, img, views, shares, keeps, creatorId )
         VALUES
         (@name, @description, @img, 0,0,0, @creatorId);
            SELECT LAST_INSERT_ID();";
         int id = _db.ExecuteScalar<int>(sql, newKeep);
         newKeep.Id = id;
         return newKeep;
      }

      internal Keep Edit(Keep updated)
    {
      string sql = @"
        UPDATE keep
        SET
         name = @Name,
         description = @Description
        WHERE id = @Id;";
      _db.Execute(sql, updated);
      return updated;
    }

      internal void Remove(int id)
      {
         string sql = "DELETE FROM keep WHERE Id = @id LIMIT 1";
         _db.Execute(sql, new { id });
      }

   internal IEnumerable<VaultKeepsViewModel> GetKeepsByVaultId(int id)
    {
      string sql = @"SELECT 
      k.*,
      vk.id AS VaultKeepId
      FROM vaultkeep vk
      JOIN keep k ON k.id = vk.keepId
      WHERE vaultId = @id;";
      return _db.Query<VaultKeepsViewModel>(sql, new { id });
    }

     internal IEnumerable<Keep> GetByCreatorId(string id)
    {
      string sql = @"
      SELECT 
      keep.*,
      profile.*
      FROM keep keep
      JOIN profiles profile ON keep.creatorId = profile.id
      WHERE keep.creatorId = @id;";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }, new { id }, splitOn: "id");
    }
     internal IEnumerable<Keep> GetByAccountId(string id)
    {
      string sql = @"
      SELECT 
      keep.*,
      profile.*
      FROM keep keep
      JOIN profiles profile ON keep.creatorId = profile.id
      WHERE keep.creatorId = @id;";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }, new { id }, splitOn: "id");
    }
   }

   }
