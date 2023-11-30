namespace Core.Persistence.Dynamic;

public class Filter
{
    public string Field { get; set; }//alanı 
    public string? Value { get; set; }//değeri
    public string Operator { get; set; } // == vb.
    public string? Logic { get; set; }//and ,or vb.
    public IEnumerable<Filter>? Filters { get; set; }

    public Filter()
    {
        Field=string.Empty;
        Operator=string.Empty;
    }
    public Filter(string field,string @operator)
    {
        Field=field;
        Operator=@operator;
    }
}
