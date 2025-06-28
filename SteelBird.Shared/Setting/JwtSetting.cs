namespace SteelBird.Shared.Setting;

public class JwtSetting
{
    public string Secret { get; set; }
    public string ValidIssuer { get; set; }
    public string ValidAudience { get; set; }
    public int expireInMinutes { get; set; }
}
