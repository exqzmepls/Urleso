namespace Urleso.Application.Abstractions.Services;

public interface IClock
{
    public DateTime GetUtcNow();
}