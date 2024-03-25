namespace SellingDreamsInfrastructure.Model
{
  public class User : IDbEntity
  {
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Adress { get; set; }
    public string? EmailAdress { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
  }
}
