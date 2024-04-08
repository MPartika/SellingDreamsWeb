namespace SellingDreamsService.ContractsDto;

public interface IUserPatchDto
{
  int Id { get; set; }
  string? Address { get; set; }
  string? PhoneNumber { get; set; }
}
