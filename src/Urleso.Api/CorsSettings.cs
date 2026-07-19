using System.ComponentModel.DataAnnotations;

namespace Urleso.Api;

internal sealed class CorsSettings
{
    public const string ConfigurationSection = "Cors";

    [Required]
    public required string AllowedOrigins { get; init; } // ";"-separated list of origins
}
