namespace SellingDreamsInfrastructure;

internal interface IDbEntity
{
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }    
}