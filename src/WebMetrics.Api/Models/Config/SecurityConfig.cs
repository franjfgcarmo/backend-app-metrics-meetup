namespace WebMetrics.Api.Models.Config
{
    public class SecurityConfig
    {
        public string Secret { get; set; }
        public int ExpirationHours { get; set; }
    }
}
