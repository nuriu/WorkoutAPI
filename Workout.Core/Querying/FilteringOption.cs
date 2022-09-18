namespace Workout.Core.Querying;

/// <summary>
/// Represents filtering operation on queries.
/// </summary>
public sealed class FilteringOption
{
    /// <summary>
    /// Kind of operators we support to perform filtering with.
    /// </summary>
    public enum FilteringOperator
    {
        EQUALS
    }

    /// <summary>
    /// Field to perform filtering on.
    /// </summary>
    public string Field { get; set; }

    /// <summary>
    /// Filtering operator.
    /// </summary>
    public FilteringOperator Operator { get; set; }

    /// <summary>
    /// Value to filter field against.
    /// </summary>
    public object Value { get; set; }

    public FilteringOption(string field, FilteringOperator filteringOperator, object value)
    {
        Field = field;
        Operator = filteringOperator;
        Value = value;
    }
}


