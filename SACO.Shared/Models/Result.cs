namespace SACO.Shared.Models;

public class Result
{
    public bool Succeeded { get; set; }
    public string[] Errors { get; set; } = [];

    public static Result Success() => new() { Succeeded = true };
    public static Result Failure(IEnumerable<string> errors) => new() { Succeeded = false, Errors = errors.ToArray() };
}