using Urleso.Domain.Results;

namespace Urleso.Application.Abstractions.Data;

public static class Errors
{
    public static readonly Error DbUpdateError =
        new("Db.Update", "An error is encountered while saving to the database.");
}