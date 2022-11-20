namespace WebApi.Settings.Model;

public class ResourceLink
{
    public string Href { get; }
    public string Rel { get; }
    public string Method { get; }
    
    public ResourceLink(string href, string rel, string method)
    {
        Href = href;
        Rel = rel;
        Method = method;
    }
}