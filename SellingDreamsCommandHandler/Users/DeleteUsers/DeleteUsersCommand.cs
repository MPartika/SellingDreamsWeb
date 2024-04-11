using System.ComponentModel.DataAnnotations;

namespace SellingDreamsCommandHandler.Users;

public class DeleteUsersCommand : ICommand
{
    [Required]
    public int Id { get; set; }
}
