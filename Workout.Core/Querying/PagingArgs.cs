namespace Workout.Core.Querying;

/// <summary>
/// Pagination arguments.
/// </summary>
public sealed class PagingArgs
{
    /// <summary>
    /// Page index.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Page size.
    /// </summary>
    public int Size { get; set; }
}
