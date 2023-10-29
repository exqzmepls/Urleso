namespace Urleso.Application.Abstractions.Time;

public interface IClock
{
    public DateTime GetUtcNow();
}