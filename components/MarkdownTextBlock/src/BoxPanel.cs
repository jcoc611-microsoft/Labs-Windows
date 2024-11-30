// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace CommunityToolkit.WinUI.Controls.MarkdownTextBlock;

/// <summary>
/// Single-child panel that arranges its child to fill the panel. Useful as the child of a InlineUIContainer for full-width content.
/// </summary>
internal partial class BoxPanel : Panel
{
    public BoxPanel(UIElement child)
    {
        Children.Add(child);
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        ValidateChildren();
        UIElement child = Children[0];

        child.Measure(availableSize);
        double width = !double.IsInfinity(availableSize.Width) ? availableSize.Width : child.DesiredSize.Width;
        double height = !double.IsInfinity(availableSize.Height) ? availableSize.Height : child.DesiredSize.Height;
        return new Size(width, height);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        ValidateChildren();
        Children[0].Arrange(new Rect(new Point(0, 0), finalSize));
        return base.ArrangeOverride(finalSize);
    }

    private void ValidateChildren()
    {
        if (Children.Count != 1)
        {
            throw new Exception("BoxPanel can only have one child");
        }
    }
}
