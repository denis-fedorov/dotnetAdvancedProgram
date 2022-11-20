using System.Runtime.Serialization;
using SharedKernel;

namespace WebApi.Settings.Model;

public abstract class ResourceBase
{
    private readonly LinkGenerator _linkGenerator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly List<ResourceLink> _links = new();

    protected ResourceBase(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
    {
        _linkGenerator = NullGuard.ThrowIfNull(linkGenerator);
        _httpContextAccessor = NullGuard.ThrowIfNull(httpContextAccessor);
    }

    [DataMember(Order = 10000)]
    public IEnumerable<ResourceLink> Links => _links;

    public ResourceBase AddGet(string relation, string routeName, object values)
    {
        _links.Add(CreateLink(
            HttpMethod.Get.Method,
            relation,
            routeName,
            values));

        return this;
    }

    public ResourceBase AddPost(string relation, string routeName)
    {
        _links.Add(CreateLink(
            HttpMethod.Post.Method,
            relation,
            routeName,
            new { }));

        return this;
    }

    public ResourceBase AddDelete(string relation, string routeName, object values)
    {
        _links.Add(CreateLink(
            HttpMethod.Delete.Method,
            relation,
            routeName,
            values));

        return this;
    }
    
    private ResourceLink CreateLink(string method, string relation, string routeName, object values)
    {
        return new ResourceLink(
            href: _linkGenerator.GetPathByRouteValues(_httpContextAccessor.HttpContext!, routeName, values)!,
            rel: relation,
            method: method);
    }
}