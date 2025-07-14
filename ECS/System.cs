using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Signature = System.Collections.BitArray;

namespace Frith.ECS
{
	/// <summary>
	/// The system processes entities that contains a specific signature
	/// </summary>
	public class EcsSystem
	{
		public static int MAX_COMPONENTS = 32;
		private Signature? componentSignature = new Signature(MAX_COMPONENTS);
		private List<Entity> entities = new List<Entity>();

		

		

		public void AddEntityToSystem(Entity entity)
		{
			entities.Add(entity);
		}

		public void RemoveEntityFromSystem(Entity entity)
		{
			if (entities.Contains(entity))
			{
				entities.Remove(entity);
			} else
			{
				Logger.Warn($"Can't remove {entity} because it's not in list of entities");
			}
		}

		public List<Entity> GetSystemEntities()
		{
			return entities.Where(e => e.IsActive).ToList();
		}

		public Signature? GetComponentSignature()
		{
			return componentSignature;
		}

		public virtual void Update(GameTime gameTime)
		{

		}

		public virtual void Draw()
		{

		}

		/// <summary>
		/// Defines  the component type that entities must have to be considered by the system
		/// </summary>
		/// <typeparam name="T"></typeparam>
		protected void RequireComponent<TComponent>() where TComponent : struct
		{
			int componentId = Component<TComponent>.GetId();
			componentSignature?.Set(componentId, true);
		}

	
	}

	
}
