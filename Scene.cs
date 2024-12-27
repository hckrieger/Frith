using Frith.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith
{


	public abstract class Scene 
	{

		private readonly Registry registry;
		protected Game game;

		protected static int nextId = 0;
		private int id = -1;
		public bool HasBeenVisited { get; set; } = false;


		public int GetId
		{
			get
			{
				if (id == -1)
				{
					id = nextId++;
				}

				return id;
			}
		}

		

		public Scene(Game game)
		{
			registry = Globals<Registry>.Instance();
			this.game = game;
		}

		public virtual void OnCreate()
		{
			foreach (var system in registry.GetAllSystems())
			{
				system.Initialize();
			}

	
		}

		public virtual void OnFirstEnter()
		{
			HasBeenVisited = true;
			OnEnterConditions();
		}

		public virtual void OnEnter()
		{
			OnEnterConditions();


		}

		private void OnEnterConditions()
		{
			foreach (var entity in registry.GetAllEntities)
			{
				switch (entity.EntityLifeCycle)
				{
					case Entity.LifeCycle.Volatile when entity.SceneId != GetId:
						entity.RemoveSelf();
						break;
					case Entity.LifeCycle.Isolated when entity.SceneId == GetId:
						registry.ReassignReservedEntity(entity, GetId);
						break;
					case Entity.LifeCycle.Isolated when entity.SceneId != GetId:
					case Entity.LifeCycle.Reserved:
						registry.ReserveEntity(entity);
						break;
					case Entity.LifeCycle.Persistant:
						entity.SceneId = GetId;
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		public virtual void OnExit()
		{
			foreach (var entity in registry.GetAllEntities)
			{
				if (entity.EntityLifeCycle is Entity.LifeCycle.Isolated or Entity.LifeCycle.Reserved)
				{
					
					registry.ReserveEntity(entity);
				}
			}
		}

		public virtual void Update(GameTime gameTime)
		{
			foreach (var system in registry.GetAllSystems())
			{
				system.Update(gameTime);
			}

			
		}


		public virtual void Draw(SpriteBatch spriteBatch)
		{
			foreach (var system in registry.GetAllSystems())
			{
				system.Draw(spriteBatch);
			}
		}

	}
}
