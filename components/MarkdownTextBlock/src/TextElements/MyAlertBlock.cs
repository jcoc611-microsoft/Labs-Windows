// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Markdig.Extensions.Alerts;
using Markdig.Syntax;
using Microsoft.UI.Xaml.Controls;
using CommunityToolkit.WinUI.Controls.MarkdownTextBlock;

namespace CommunityToolkit.Labs.WinUI.MarkdownTextBlock.TextElements;

internal class MyAlertBlock : IAddChild
{
    private Paragraph _paragraph;
    private MyFlowDocument _flowDocument;

    public TextElement TextElement
    {
        get => _paragraph;
    }
    public MyAlertBlock(AlertBlock alertBlock)
    {
        InfoBarSeverity severity = alertBlock.Kind.ToString().ToUpperInvariant() switch
        {
            "NOTE" => InfoBarSeverity.Success,
            "TIP" => InfoBarSeverity.Informational,
            "IMPORTANT" => InfoBarSeverity.Informational,
            "WARNING" => InfoBarSeverity.Warning,
            "CAUTION" => InfoBarSeverity.Error,
            _ => InfoBarSeverity.Informational
        };
        _flowDocument = new MyFlowDocument(alertBlock);
        _flowDocument.RichTextBlock.Padding = new Thickness(left: 0, top: 0, right: 12, bottom: 12);

        var infoBar = new InfoBar
        {
            Severity = severity,
            Title = alertBlock.Kind.ToString(),
            Content = _flowDocument.RichTextBlock,
            IsClosable = false,
            IsOpen = true,
            Margin = new Thickness(left: 0, top: 12, right: 0, bottom: 12),
        };

        var inlineUIContainer = new InlineUIContainer();
        inlineUIContainer.Child = new BoxPanel(infoBar);

        _paragraph = new Paragraph();
        _paragraph.Inlines.Add(inlineUIContainer);
    }

    public void AddChild(IAddChild child)
    {
        _flowDocument.AddChild(child);
    }
}
