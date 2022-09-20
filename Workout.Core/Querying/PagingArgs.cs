namespace Workout.Core.Querying;

/// <summary>
/// Pagination arguments.
/// </summary>
public sealed class PagingArgs
{
    /// <summary>
    /// Page index.
    /// </summary>
    public uint Index { get; set; }

    /// <summary>
    /// Page size.
    /// </summary>
    public uint Size { get; set; }

    public PagingArgs(uint index, uint size)
    {
        Index = index > 0 ? index : 1;
        Size = size > 0 ? size : 10;
    }
}
