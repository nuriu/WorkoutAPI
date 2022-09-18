namespace Workout.Core.Querying;

/// <summary>
/// Represent general search arguments when querying entities.
/// </summary>
public sealed class SearchArgs
{
    /// <summary>
    /// Filtering arguments.
    /// </summary>
    public List<FilteringOption> FilteringOptions { get; set; }

    /// <summary>
    /// Sorting arguments.
    /// </summary>
    public List<SortingOption> SortingOptions { get; set; }

    public SearchArgs(List<FilteringOption> filteringOptions,
                      List<SortingOption> sortingOptions)
    {
        FilteringOptions = filteringOptions;
        SortingOptions = sortingOptions;
    }
}
