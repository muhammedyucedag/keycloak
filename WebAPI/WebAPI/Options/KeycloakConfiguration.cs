namespace WebAPI.Options;

public sealed class KeycloakConfiguration
{
    public string HostName { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string Realm { get; set; }  = null!;
    public string ClientSecret { get; set; } = null!;
    public string ClientUuıd { get; set; }  = null!;
    public string GrantType { get; set; }  = null!;
}