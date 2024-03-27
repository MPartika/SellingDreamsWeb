namespace SellingDreamsService.ContractsDto;

public interface IUserLoginPatchDto
{
  public int Id { get; set; }
  public string? UserName { get; set; }
  public byte[]? Password { get; set; }
  public byte[]? Salt { get; set; }
  public int? UserId { get; set; }
}
