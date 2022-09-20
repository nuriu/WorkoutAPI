using Workout.Core.Querying;

namespace Workout.Infrastructure.Querying;

public sealed class PagedList<T> : IPagedList<T>
{
    public PagedList(uint index, uint size, IEnumerable<T> items)
    {
        PageIndex = index < 1 ? 1 : index;
        PageSize = size < 1 ? 10 : size;
        Items = items;
    }

    public PagedList(PagingArgs pagingArgs, uint totalItemCount, IEnumerable<T> items)
    {
        PageIndex = pagingArgs.Index < 1 ? 1 : pagingArgs.Index;
        PageSize = pagingArgs.Size < 1 ? 10 : pagingArgs.Size;
        TotalItemCount = totalItemCount;

        TotalPageCount = TotalItemCount / PageSize;
        if (TotalItemCount % PageSize > 0)
            TotalPageCount++;

        Items = items;
    }

    public uint PageIndex { get; }

    public uint PageSize { get; }

    public uint TotalItemCount { get; }

    public uint TotalPageCount { get; }

    public bool HasPreviousPage => PageIndex > 1;

    public bool HasNextPage => PageIndex < TotalPageCount;

    public IEnumerable<T> Items { get; }
}
