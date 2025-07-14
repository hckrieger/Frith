using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.ECS
{
	public class EntityPool<T> : Pool<T> where T : Entity
	{

		public T? Grab()
		{
			foreach (var entity in data)
			{
				if (entity != null && !entity.IsActive)
				{
					entity.IsActive = true;
					return entity;
				}
			}

			Logger.Warn($"Tried to call {typeof(T)} entity from entity Pool but this pool is empty");
			return null;
		}

		public void Deposit(T entity)
		{
			entity.IsActive = false;
		}

		public override void Add(T entity)
		{
			base.Add(entity);
			entity.IsActive = false;
		}


		public void ForEachActive(Action<T> action)
		{
			foreach (var entity in data)
			{
				if (entity != null && entity.IsActive)
					action(entity);
				
			}
		}

		public void AddEach(int quantity, Func<T> createEntity, Action<int, T> action)
		{
			for (int i = 0; i < quantity; i++)
			{
				var entity = createEntity();
				action(i, entity);
				Add(entity);
			}
		}

	}
}
