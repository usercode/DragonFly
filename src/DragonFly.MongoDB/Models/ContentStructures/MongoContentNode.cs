using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.MongoDB;

/// <summary>
/// MongoContentNode
/// </summary>
public class MongoContentNode : MongoContentBase
{
    public MongoContentNode()
    {
    }

    /// <summary>
    /// Structure
    /// </summary>
    public Guid? Structure { get; set; }

    /// <summary>
    /// Parent
    /// </summary>
    public Guid? Parent { get; set; }

    /// <summary>
    /// Target
    /// </summary>
    public IContentNodeTarget? Target { get; set; }
}
