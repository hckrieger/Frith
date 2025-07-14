using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Frith.ECS
{


	public interface IPool
	{
		public void RemoveEntityFromPool(int entityId);
	}

	/// <summary>
	/// A pool is just a vector (contiguous data) of objects of type T
	/// </summary>
	/// 
	public class Pool<T> : IPool
	{
		protected T[] data;
		private int size = 0;


		private Dictionary<int, int> entityIdToIndex = [];
		private Dictionary<int, int> indexToEntityId = [];

		public ref T this[int index]
		{
			get
			{
				if (data != null)
					return ref data[index];

				throw new NullReferenceException("data list must not be null");
			}
		}


		public Pool(int capacity = 100)
		{
			size = 0;
			data = new T[size];

			Array.Resize(ref data, capacity);
	
		}

		public int? GetSize()
		{
			return size;
		}

		public bool isEmpty()
		{
			return size == 0;
		}

		public void Resize(int n)
		{
			Array.Resize(ref data, n);
		}

		public void Clear()
		{
			Array.Clear(data, 0, data.Length);
			size = 0;
		}

		public virtual void Add(T componentObject)
		{
			data[size++] = (componentObject);
		}

		public void Set(int entityId, T componentObject)
		{
			if (entityIdToIndex.ContainsKey(entityId))
			{
				int index = entityIdToIndex[entityId];
				data[index] = componentObject;
			} else
			{
				int index = size;
				entityIdToIndex[entityId] = index;
				indexToEntityId[index] = entityId;

				if (index >= data.Length)
					Array.Resize(ref data, size * 2);

				data[index] = componentObject;
				size++;
			}
		}

		public void Remove(int entityId)
		{
			int indexOfRemoved = entityIdToIndex[entityId];
			int indexOfLast = size - 1;
			data[indexOfRemoved] = data[indexOfLast];

			int entityIdOfLastElement = indexToEntityId[indexOfLast];
			entityIdToIndex[entityIdOfLastElement] = indexOfRemoved;
			indexToEntityId[indexOfRemoved] = indexOfLast;

			entityIdToIndex.Remove(entityId);
			indexToEntityId.Remove(indexOfLast);

			size--;
		}

		public ref T Get(int entityId) 
		{
			var index = entityIdToIndex[entityId];
			if (data != null)	
				return ref data[index];

			throw new NullReferenceException("component object can't be null");
		}

		public void RemoveEntityFromPool(int entityId)
		{
			if (entityIdToIndex.ContainsKey(entityId))
				Remove(entityId);
		}
	}
}
