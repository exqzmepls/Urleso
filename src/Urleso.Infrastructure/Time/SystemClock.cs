using Urleso.Application.Abstractions.Time;

namespace Urleso.Infrastructure.Time;

internal sealed class SystemClock : IClock
{
    public DateTime GetUtcNow() => DateTime.UtcNow;
}