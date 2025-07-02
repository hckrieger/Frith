using Frith.ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.ECS
{
	public class IComponent
	{
		protected static int nextId = 0;
	}

	/// <summary>
	/// Used to assign a unique id to a component type
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Component<T> : IComponent
	{
		private static int id = -1;
		public static int GetId()
		{
			if (id == -1)
			{
				id = nextId++;
			}

			return id;
		}
	}

	public class Entity(int id)
	{
		private int id = id;


		public Registry? registry;

		public int GetId()
		{
			
			return id;
		}

		public void AddComponent<TComponent>(TComponent component) where TComponent : struct
		{
			registry?.AddComponent(this, component);
		}

		public void RemoveComponent<TComponent>() where TComponent : struct
		{
			registry?.RemoveComponent<TComponent>(this);	
		}

		public bool HasComponent<TComponent>() where TComponent : struct
		{
			if (registry == null)
				return false;
			return registry.HasComponent<TComponent>(this);
		}

		public ref TComponent GetComponent<TComponent>() where TComponent : struct
		{
			if (registry == null)
				throw new InvalidOperationException("Registry can't be null");
			return ref registry.GetComponent<TComponent>(this);
		}

		public void RemoveSelf()
		{
			registry?.RemoveEntity(this);
		}

		public void Tag(string tag)
		{
			registry?.TagEntity(this, tag);
		}

		public bool HasTag(string tag)
		{
			if (registry != null)
				return registry.EntityHasTag(this, tag);

			return false;
		}	

		public void Group(string group)
		{
			registry?.GroupEntity(this, group);
		}

		public bool? BelongsToGroup(string group)
		{
			return registry?.EntityBelongsToGroup(this, group);
		}

		//public static bool operator =(Entity entity, Entity other) = default;
		

		public static bool operator ==(Entity? entity, Entity? other)
		{
			return entity?.id == other?.id;
		}

		public static bool operator !=(Entity entity, Entity other)
		{
			return entity.id != other.id;
		}

		public static bool operator >(Entity entity, Entity other)
		{
			return entity.id > other.id;
		}

		public static bool operator <(Entity entity, Entity other)
		{
			return entity.id < other.id;
		}

		public override bool Equals(object? obj)
		{
			return obj is Entity entity && id == entity.id;
		}

		public override int GetHashCode()
		{
			return id.GetHashCode();
		}
	}



}
