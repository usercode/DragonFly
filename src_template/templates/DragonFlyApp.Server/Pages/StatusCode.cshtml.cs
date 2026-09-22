// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFlyTemplate.Models;
using Microsoft.AspNetCore.WebUtilities;

namespace DragonFlyTemplate.Pages;

public class StatusCodePage : BasePageModel
{
    public int PageStatusCode { get; set; }

    public void OnGet(int statusCode)
    {
        PageStatusCode = statusCode;

        PageTitle = ReasonPhrases.GetReasonPhrase(statusCode);
    }
}
