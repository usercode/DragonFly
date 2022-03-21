using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Content;

public abstract class ContentBase : Entity
{
    public virtual DateTime? CreatedAt { get; set; }

    public virtual DateTime? ModifiedAt { get; set; }

    public virtual DateTime? PublishedAt { get; set; }

    public virtual int Version { get; set; }

    public override string ToString()
    {
        return Id.ToString();
    }
}
