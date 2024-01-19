using Core.Persistence.Repositories;

namespace Core.Security.Entities;

public class OperationClaim:Entity<int>
{
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<UserOperationClaim> UserOperationClaims { get; set; } = null;    
    public OperationClaim()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

    public OperationClaim(int id,string name, string description):base(id)
    {
        Name=name;
        Description=description;
    }
}
