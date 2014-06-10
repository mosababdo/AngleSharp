﻿namespace AngleSharp.DOM
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Html;
    using System;
    using System.Net;

    /// <summary>
    /// The HTMLDocument interface represent an HTML document.
    /// </summary>
    [DomName("HTMLDocument")]
    public interface IHtmlDocument : IDocument
    {
        HTMLCollection<HTMLAnchorElement> Anchors { get; }
        HTMLBodyElement Body { get; }
        Cookie Cookie { get; set; }
        String Domain { get; }
        HTMLCollection Embeds { get; }
        HTMLCollection<HTMLFormElement> Forms { get; }
        HTMLCollection GetElementsByName(String name);
        HTMLHeadElement Head { get; }
        HTMLCollection<HTMLImageElement> Images { get; }
        HTMLCollection Links { get; }
        void Load(String url);
        HTMLCollection<HTMLScriptElement> Scripts { get; }
        String Title { get; set; }
        void Open();
        void Close();
        void Write(String content);
        void WriteLn(String content);
    }
}
