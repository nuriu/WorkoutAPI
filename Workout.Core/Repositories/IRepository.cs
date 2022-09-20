using Workout.Core.Entities;

namespace Workout.Core.Repositories;

public interface IRepository<T> : IBaseRepository<T, uint> where T : IBaseEntity<uint>
{
}
