using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DragonFlyTemplate.Models;

public class EntityPageModel<T> : BasePageModel
{
    public T Entity { get; }

}
