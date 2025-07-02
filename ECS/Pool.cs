using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Frith.ECS
{


	public class IPool
	{
		~IPool()
		{
			
		}
	}

	/// <summary>
	/// A pool is just a vector (contiguous data) of objects of type T
	/// </summary>
	/// 
	public class Pool<T> : IPool where T : struct
	{
		private T[] data;
		private int size = 0;
		public ref T this[int index]
		{
			get
			{
				if (data != null)
					return ref data[index];

				throw new NullReferenceException("data list must not be null");
			}
		}


		public Pool(int size = 100)
		{
			data = new T[size];

			Array.Resize(ref data, size);
	
		}

		public int? GetSize()
		{
			return data?.Length;
		}

		public bool isEmpty()
		{
			return data?.Length == 0;
		}

		public void Clear()
		{
			Array.Clear(data, 0, data.Length);
		}

		public void Add(T componentObject)
		{
			data[size++] = (componentObject);
		}

		public void Set(int index, T componentObject)
		{
			if (data != null)
				data[index] = componentObject;
		}

		public T Get(int index)
		{
			if (data != null)	
				return data[index];

			throw new NullReferenceException("component object can't be null");
		}
	}
}
