using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
   public class VaultsRepository
   {
      private readonly IDbConnection _db;

      public VaultsRepository(IDbConnection db)
      {
         _db = db;
      }

      internal IEnumerable<Vault> GetAll()
       {
      string sql = @"
      SELECT 
      vault.*,
      prof.*
      FROM vault vault
      JOIN profiles prof ON vault.creatorId = prof.id";
      return _db.Query<Vault, Profile, Vault>(sql, (Vault, profile) =>
      {
        Vault.Creator = profile;
        return Vault;
      }, splitOn: "id");
    }

      internal Vault GetById(int id)
       {
      string sql = @" 
      SELECT 
      vault.*,
      prof.*
      FROM vault vault
      JOIN profiles prof ON vault.creatorId = prof.id
      WHERE vault.id = @id AND vault.isPrivate = false;";
      return _db.Query<Vault, Profile, Vault>(sql, (Vault, profile) =>
      {
        Vault.Creator = profile;
        return Vault;
      }, new { id }, splitOn: "id").FirstOrDefault();
    }

      internal Vault Create(Vault newVault)
      {
         string sql = @"
         INSERT INTO vault
         (name, description, isPrivate, creatorId)
         VALUES
         (@name, @description, @isPrivate, @creatorId);
         SELECT LAST_INSERT_ID();";
         int id = _db.ExecuteScalar<int>(sql, newVault);
         newVault.Id = id;
         return newVault;
      }

      internal Vault Edit(Vault data)
      {
         string sql = @"
         UPDATE vault
         SET
            name = @name,
            description = @description
         WHERE id = @id;
         SELECT * FROM vault WHERE id = @id;";
         Vault returnVault = _db.QueryFirstOrDefault<Vault>(sql, data);
         return returnVault;
      }

      internal void Remove(int id)
      {
         string sql = "DELETE FROM vault WHERE Id = @id LIMIT 1";
         _db.Execute(sql, new { id });
      }

      internal IEnumerable<Vault> GetByCreatorId(string id)
    {
      string sql = @"
      SELECT 
      vault.*,
      profile.*
      FROM vault vault
      JOIN profiles profile ON vault.creatorId = profile.id
      WHERE vault.creatorId = @id;";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
      {
        vault.Creator = profile;
        return vault;
      }, new { id }, splitOn: "id");
    }
     internal IEnumerable<Vault> GetByAccountId(string id)
    {
      string sql = @"
      SELECT 
      vault.*,
      profile.*
      FROM vault vault
      JOIN profiles profile ON vault.creatorId = profile.id
      WHERE vault.creatorId = @id;";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
      {
        vault.Creator = profile;
        return vault;
      }, new { id }, splitOn: "id");
    }
   }
}