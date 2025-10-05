using System;

public class GlbUrlValidator
{
    public bool IsValid(string url)
    {
        if (string.IsNullOrEmpty(url)) return false;

        return Uri.TryCreate(url, UriKind.Absolute, out _) && url.EndsWith(".glb", StringComparison.OrdinalIgnoreCase);
    }
}
