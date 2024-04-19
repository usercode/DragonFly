// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Identity;

public class LoginResult
{
    public LoginResult(bool success)
    {
        Success = success;
    }

    public LoginResult()
    {
            
    }

    public bool Success { get; set; } = false;

    public string Username { get; set; } = string.Empty;

    public IList<ClaimItem> Claims { get; set; } = new List<ClaimItem>();
}
