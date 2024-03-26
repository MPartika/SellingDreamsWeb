using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SellingDreamsInfrastructure.Model
{
  [PrimaryKey (nameof(Id), nameof(Name), nameof(EmailAdress))]
  public class User : IDbEntity
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; } = "Name not defined";
    public string? Adress { get; set; }
    public string EmailAdress { get; set; } = "Email not defined";
    public string? PhoneNumber { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
  }
}
