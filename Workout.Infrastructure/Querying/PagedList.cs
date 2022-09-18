using Workout.Core.Querying;

namespace Workout.Infrastructure.Querying;

public sealed class PagedList<T> : IPagedList<T>
{
    public PagedList(int index, int size, IEnumerable<T> items)
    {
        Index = index < 1 ? 1 : index;
        Size = size < 1 ? 10 : size;
        Items = items;
    }

    public PagedList(PagingArgs pagingArgs, IEnumerable<T> items)
    {
        Index = pagingArgs.Index < 1 ? 1 : pagingArgs.Index;
        Size = pagingArgs.Size < 1 ? 10 : pagingArgs.Size;
        Items = items;
    }

    public int Index { get; }

    public int Size { get; }

    public IEnumerable<T> Items { get; }
}
