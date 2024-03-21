using Urleso.Application.Abstractions.Services;

namespace Urleso.Infrastructure.Services;

internal sealed class SystemClock : IClock
{
    public DateTime GetUtcNow() => DateTime.UtcNow;
}