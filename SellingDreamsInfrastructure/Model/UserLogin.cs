using System.ComponentModel.DataAnnotations.Schema;

namespace SellingDreamsInfrastructure.Model;

public class UserLogin : IDbEntity
{
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id { get; set; }
  public string UserName { get; set; } = UserNameDefault;
  public byte[] Password { get; set; } = Array.Empty<byte>();
  public byte[] Salt {get; set; } = Array.Empty<byte>();
  public int? UserId { get; set; }
  public DateTime Created { get; set; }
  public DateTime Updated { get; set; }

  public const string UserNameDefault = "Not Defined";
}
