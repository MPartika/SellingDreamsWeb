namespace SellingDreamsService.ContractsDto;

public interface IUserPatchDto
{
  int Id { get; set; }
  string? Name { get; set; }
  string? Adress { get; set; }
  string? EmailAdress { get; set; }
  string? PhoneNumber { get; set; }
}
