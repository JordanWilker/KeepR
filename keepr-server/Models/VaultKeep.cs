namespace keepr_server.Models
{
    public class VaultKeep
    {
    public VaultKeep()
      {
      }
    public int Id { get; set; }
    public int VaultId { get; set; }
    public int KeepId { get; set; }
    public string CreatorId { get; set; }
    }
}