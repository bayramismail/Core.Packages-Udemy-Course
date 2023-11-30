namespace Core.Persistence.Paging;

public abstract class BasePageableModel
{
    public int Size { get; set; }
    public int Index { get; set; }
    public long Count { get; set; }
    public int Pages { get; set; }
    public bool HasPrevious { get; set; }
    public bool HasNext {get; set; }
}
