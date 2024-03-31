namespace SellingDreamsService.ContractsDto;

public interface IUserPatchDto
{
  int Id { get; set; }
  string? Name { get; set; }
  string? Address { get; set; }
  string? EmailAddress { get; set; }
  string? PhoneNumber { get; set; }
}
