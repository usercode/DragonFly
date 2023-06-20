// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator.SourceBuilder;

namespace DragonFly.Generator;

internal class ParameterList
{
    /// <summary>
    /// Empty
    /// </summary>
    public static ParameterList New => new ParameterList();

    public ParameterList()
    {
            
    }

    /// <summary>
    /// Parameters
    /// </summary>
    private IList<Parameter> Parameters { get; set; } = new List<Parameter>();

    /// <summary>
    /// Adds a parameter.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public ParameterList Add(string type, string name)
    {
        Parameters.Add(new Parameter(name, type));

        return this;
    }

    public override string ToString()
    {
        return $"({string.Join(", ", Parameters)})";
    }
}
