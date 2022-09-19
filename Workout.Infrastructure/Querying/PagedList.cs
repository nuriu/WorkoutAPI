using Workout.Core.Querying;

namespace Workout.Infrastructure.Querying;

public sealed class PagedList<T> : IPagedList<T>
{
    public PagedList(int index, int size, IEnumerable<T> items)
    {
        PageIndex = index < 1 ? 1 : index;
        PageSize = size < 1 ? 10 : size;
        Items = items;
    }

    public PagedList(PagingArgs pagingArgs, IEnumerable<T> items)
    {
        PageIndex = pagingArgs.Index < 1 ? 1 : pagingArgs.Index;
        PageSize = pagingArgs.Size < 1 ? 10 : pagingArgs.Size;
        Items = items;
    }

    public int PageIndex { get; }

    public int PageSize { get; }

    public IEnumerable<T> Items { get; }
}
