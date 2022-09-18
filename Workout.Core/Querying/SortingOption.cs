namespace Workout.Core.Querying;

/// <summary>
/// Represents sorting operations on queries.
/// </summary>
public sealed class SortingOption
{
    /// <summary>
    /// Kinds of sorting.
    /// </summary>
    public enum SortingDirection
    {
        ASCENDING,
        DESCENDING
    }

    /// <summary>
    /// Target field to sort by.
    /// </summary>
    public string Field { get; set; }

    /// <summary>
    /// Kind of sorting to perform.
    /// </summary>
    public SortingDirection Direction { get; set; }

    public SortingOption(string field, SortingDirection direction)
    {
        Field = field;
        Direction = direction;
    }
}
