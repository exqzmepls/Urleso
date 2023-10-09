using Urleso.Domain.Primitives;

namespace Urleso.Domain.ShortenedUrls;

public sealed record ShortenedUrlId(Guid Value) : EntityId(Value);