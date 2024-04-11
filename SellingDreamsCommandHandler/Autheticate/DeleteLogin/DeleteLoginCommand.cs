using System.ComponentModel.DataAnnotations;

namespace SellingDreamsCommandHandler.Authenticate;

public class DeleteLoginCommand : ICommand
{
    [Required]
    public int Id { get; set; }
}
