// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DragonFly.Client;

public static class QuillInterop
{
    #region Constants

    private const string strCreateQuill = "QuillFunctions.createQuill";
    private const string strGetText = "QuillFunctions.getQuillText";
    private const string strGetHTML = "QuillFunctions.getQuillHTML";
    private const string strSetHTML = "QuillFunctions.setQuillHTML";
    private const string strGetContent = "QuillFunctions.getQuillContent";
    private const string strLoadQuillContent = "QuillFunctions.loadQuillContent";
    private const string strEnableQuillEditor = "QuillFunctions.enableQuillEditor";
    private const string strInsertImage = "QuillFunctions.insertQuillImage";

    #endregion Constants

    /// <summary>
    /// Creates the quill.
    /// </summary>
    /// <param name="jsRuntime">The js runtime.</param>
    /// <param name="quillElement">The quill element.</param>
    /// <param name="toolbar">The toolbar.</param>
    /// <param name="readOnly">if set to <c>true</c> [read only].</param>
    /// <param name="placeholder">The placeholder.</param>
    /// <param name="theme">The theme.</param>
    /// <param name="debugLevel">The debug level.</param>
    /// <returns>ValueTask&lt;System.Object&gt;.</returns>
    internal static ValueTask<object> CreateQuill(
                                                    IJSRuntime jsRuntime,
                                                    ElementReference quillElement,
                                                    ElementReference toolbar,
                                                    bool readOnly,
                                                    string placeholder,
                                                    string theme,
                                                    string debugLevel)
    {
        return jsRuntime.InvokeAsync<object>(
            strCreateQuill,
            quillElement, toolbar, readOnly,
            placeholder, theme, debugLevel);
    }

    /// <summary>
    /// Gets the text.
    /// </summary>
    /// <param name="jsRuntime">The js runtime.</param>
    /// <param name="quillElement">The quill element.</param>
    /// <returns>ValueTask&lt;System.String&gt;.</returns>
    internal static ValueTask<string> GetText(
                                                IJSRuntime jsRuntime,
                                                ElementReference quillElement)
    {
        return jsRuntime.InvokeAsync<string>(
            strGetText,
            quillElement);
    }

    /// <summary>
    /// Gets the HTML.
    /// </summary>
    /// <param name="jsRuntime">The js runtime.</param>
    /// <param name="quillElement">The quill element.</param>
    /// <returns>ValueTask&lt;System.String&gt;.</returns>
    internal static ValueTask<string> GetHTML(
                                                IJSRuntime jsRuntime,
                                                ElementReference quillElement)
    {
        return jsRuntime.InvokeAsync<string>(
            strGetHTML,
            quillElement);
    }

    internal static ValueTask<object> SetHTML(
                                                IJSRuntime jsRuntime,
                                                ElementReference quillElement,
                                                string html)
    {
        return jsRuntime.InvokeAsync<object>(
            strSetHTML,
            quillElement,
            html);
    }

    /// <summary>
    /// Gets the content.
    /// </summary>
    /// <param name="jsRuntime">The js runtime.</param>
    /// <param name="quillElement">The quill element.</param>
    /// <returns>ValueTask&lt;System.String&gt;.</returns>
    internal static ValueTask<string> GetContent(
                                                IJSRuntime jsRuntime,
                                                ElementReference quillElement)
    {
        return jsRuntime.InvokeAsync<string>(
            strGetContent,
            quillElement);
    }

    /// <summary>
    /// Loads the content of the quill.
    /// </summary>
    /// <param name="jsRuntime">The js runtime.</param>
    /// <param name="quillElement">The quill element.</param>
    /// <param name="Content">The content.</param>
    /// <returns>ValueTask&lt;System.Object&gt;.</returns>
    internal static ValueTask<object> LoadQuillContent(
                                                        IJSRuntime jsRuntime,
                                                        ElementReference quillElement,
                                                        string Content)
    {
        return jsRuntime.InvokeAsync<object>(
            strLoadQuillContent,
            quillElement, 
            Content);
    }

    /// <summary>
    /// Enables the quill editor.
    /// </summary>
    /// <param name="jsRuntime">The js runtime.</param>
    /// <param name="quillElement">The quill element.</param>
    /// <param name="mode">if set to <c>true</c> [mode].</param>
    /// <returns>ValueTask&lt;System.Object&gt;.</returns>
    internal static ValueTask<object> EnableQuillEditor(
                                                        IJSRuntime jsRuntime,
                                                        ElementReference quillElement,
                                                        bool mode)
    {
        return jsRuntime.InvokeAsync<object>(
            strEnableQuillEditor,
            quillElement, mode);
    }

    internal static ValueTask<object> InsertQuillImage(
                                                        IJSRuntime jsRuntime,
                                                        ElementReference quillElement,
                                                        string imageURL)
    {
        return jsRuntime.InvokeAsync<object>(
            strInsertImage,
            quillElement, imageURL);
    }
}
