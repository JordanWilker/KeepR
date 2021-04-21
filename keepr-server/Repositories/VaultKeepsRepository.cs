using System;
using System.Data;
using keepr.Models;
using Dapper;
using keepr_server.Models;
using System.Linq;

namespace keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal VaultKeep Create(VaultKeep newWLP)
    {
      string sql = @"
      INSERT INTO vaultkeep
      (vaultId, keepId, creatorId) 
      VALUES 
      (@VaultId, @KeepId, @CreatorId);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newWLP);
      newWLP.Id = id;
      return newWLP;
    }

    internal void Remove(int id)
    {
      string sql = "DELETE FROM vaultkeep WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });

    }
    internal VaultKeep GetById(int id)
       {
      string sql = @" 
      SELECT 
      vaultkeep.*,
      prof.*
      FROM vaultkeep vaultkeep
      JOIN profiles prof ON vaultkeep.CreatorId = prof.id
      WHERE vaultkeep.id = @id";
      return _db.Query<VaultKeep, Profile, VaultKeep>(sql, (VaultKeep, profile) =>
      {
        return VaultKeep;
      }, new { id }, splitOn: "id").FirstOrDefault();
    }
  }
}