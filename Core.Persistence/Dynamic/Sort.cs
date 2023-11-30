
namespace Core.Persistence.Dynamic;

public class Sort
{
    public string Field { get; set; }
    public string Dir { get; set; }//Direction (ASC/DESC)

    public Sort()
    {
        Field = string.Empty;
        Dir = string.Empty;
    }
    public Sort(string field, string dir)
    {
        Field = field;
        Dir = dir;
    }
}
