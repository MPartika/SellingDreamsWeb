using System.ComponentModel.DataAnnotations.Schema;

namespace SellingDreamsInfrastructure.Model;

public class Role
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string RoleName { get; set; } = "User";
}
