using Workout.Core.Entities;

namespace Workout.Core.Repositories;

public interface IRepository<T> : IBaseRepository<T, int> where T : IBaseEntity<int>
{
}
