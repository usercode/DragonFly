// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Generator.SourceBuilder;

internal class SourceBuilder
{
    private StringBuilder Builder { get; } = new StringBuilder();

    private int Tabs { get; set; }

    public void AddTab() => Tabs++;

    public void RemoveTab() => Tabs--;

    public void AppendTabs()
    {
        for (var i = 0; i < Tabs; i++)
        {
            Builder.Append("    ");
        }
    }

    public void AppendLineBreak()
    {
        Builder.AppendLine();
    }

    public void Append(string source)
    {
        Builder.Append(source);
    }

    public void AppendLine(string source)
    {
        AppendTabs();

        Builder.AppendLine(source);
    }

    public void AppendLine()
    {
        AppendTabs();

        Builder.AppendLine();
    }

    public void AppendBlock(Action<SourceBuilder> action)
    {
        AppendLine("{");
        AddTab();

        action(this);

        RemoveTab();
        AppendLine("}");
    }

    public override string ToString() => Builder.ToString();
}
