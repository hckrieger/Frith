using Frith.ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Signature = System.Collections.BitArray;

namespace Frith.ECS
{
	public class Registry
	{
		private int numEntities = 0;

		/// <summary>
		/// List of component pools, each pool contains all the data for a certain type
		/// The list index = component type
		/// Pool index = entity id
		/// </summary>
		private IPool[] componentPools = new IPool[1];

		/// <summary>
		/// List of component signatures perentity, saying which component is turned on for a given entity
		/// list index = entity id
		/// </summary>

		private List<Signature> entityComponentSignatures = new List<Signature>();


		private List<EcsSystem> systems = [];

		private HashSet<Entity> entitiesToBeAdded = new HashSet<Entity>();
		private HashSet<Entity> entitiesToBeRemoved = new HashSet<Entity>();

		private LinkedList<int> freeIds = new LinkedList<int>();

		private Dictionary<string, Entity> entityPerTag = new Dictionary<string, Entity>();
		private Dictionary<int, string> tagPerEntity = new Dictionary<int, string>();

		private Dictionary<string, HashSet<Entity>> entitiesPerGroup = new Dictionary<string, HashSet<Entity>>();
		private Dictionary<int, string> groupPerEntities = new Dictionary<int, string>();

		public void RemoveEntity(Entity entity)
		{
			entitiesToBeRemoved.Add(entity);
		}

		public Entity CreateEntity()
		{
			int entityId;

			if (freeIds.Count == 0)
			{
				entityId = numEntities++;
				if (entityId >= entityComponentSignatures.Count)
				{
					entityComponentSignatures.Add(new Signature(EcsSystem.MAX_COMPONENTS));
				}
			} else
			{
				entityId = freeIds.First();
				freeIds.Remove(entityId);
			}



			Entity entity = new Entity(entityId)
			{
				registry = this
			};

			entitiesToBeAdded.Add(entity);

			Logger.Info("Entity created with id = " + entityId);

			return entity;	
		}

		public void AddEntityToSystems(Entity entity)
		{
			var entityId = entity.GetId();

			Signature? entityComponentSignature = entityComponentSignatures?[entityId];


			foreach (EcsSystem system in systems)
			{
				Signature? systemComponentSignature = system.GetComponentSignature();
				if (systemComponentSignature != null && entityComponentSignature != null)
				{
					Signature resultingSignature = new Signature(entityComponentSignature).And(systemComponentSignature);
					bool isInterested = resultingSignature.BitsetsMatch(systemComponentSignature);
					if (isInterested)
					{
						system.AddEntityToSystem(entity);
					}
				}
					
			}
		}

		public void RemoveEntityFromSystems(Entity entity)
		{
			foreach (EcsSystem system in systems)
			{
				system.RemoveEntityFromSystem(entity);
			}
		}



		public void AddComponent<TComponent>(Entity entity, TComponent component) where TComponent : struct
		{
			var componentId = Component<TComponent>.GetId();
			var entityId = entity.GetId();

			//If the component id is higher than the component count then 
			//Add a new pool to the list of component pools
			
			if (componentPools.Length <= componentId)
			{
				Array.Resize(ref componentPools, componentId + 1);
			}

			if (componentPools[componentId] == null)
			{
				componentPools[componentId] = new Pool<TComponent>();
			}

		
			if (componentPools == null)
				return;

			Pool<TComponent>? componentPool = (Pool<TComponent>?)componentPools[componentId];

			//Set the component whose index is the entityId
			componentPool?.Set(entityId, component);

			//Set the the index of the bitset that matches the component id to true
			entityComponentSignatures?[entityId]?.Set(componentId, true);

			Logger.Info($"Component id = {componentId} was added to entity id {entityId}");
		}

		public void RemoveComponent<TComponent>(Entity entity) where TComponent : struct
		{
			var componentId = Component<TComponent>.GetId();
			var entityId = entity.GetId();

			entityComponentSignatures?[entityId]?.Set(componentId, false);

			Logger.Info($"Component id = {componentId} was removed from entity id {entityId}");
		}

		public bool HasComponent<TComponent>(Entity entity) where TComponent : struct
		{
			var entityId = entity.GetId();


			return entityComponentSignatures[entityId].HasAnySet();
		}

		public ref TComponent GetComponent<TComponent>(Entity entity) where TComponent : struct
		{
			var componentId = Component<TComponent>.GetId();

			var entityId = entity.GetId();

			var componentPool = (Pool<TComponent>?)componentPools?[componentId] ?? throw new InvalidOperationException("component pool can't be null");

			return ref componentPool[entityId];

			
		}

		public void Update()
		{ 
			foreach (var entity in entitiesToBeAdded)
			{
				AddEntityToSystems(entity);
			}

			entitiesToBeAdded.Clear();

			foreach (var entity in entitiesToBeRemoved)
			{
				RemoveEntityFromSystems(entity);

				entityComponentSignatures[entity.GetId()].SetAll(false);

				freeIds.AddLast(entity.GetId());

				RemoveEntityFromTag(entity);
				RemoveEntityFromGroup(entity);
			}
			entitiesToBeRemoved.Clear();

			
		}

		public void TagEntity(Entity entity, string tag)
		{
			entityPerTag[tag] = entity;
			tagPerEntity[entity.GetId()] = tag;
		}

		public bool EntityHasTag(Entity entity, string tag)
		{
			return entityPerTag.ContainsKey(tag);	
		}

		public Entity GetEntityByTag(string tag)
		{
			return entityPerTag[tag];
		}

		public void RemoveEntityFromTag(Entity entity)
		{
			if (!tagPerEntity.ContainsKey(entity.GetId()))
			{
				Logger.Warn($"Tried to remove entity with key {entity.GetId} but it doesn't exist");
				return;
			}

			var taggedEntity = tagPerEntity[entity.GetId()];
			entityPerTag.Remove(taggedEntity);
			tagPerEntity.Remove(entity.GetId());
		}


		public void GroupEntity(Entity entity, string group)
		{
			if (!entitiesPerGroup.TryGetValue(group, out var entityPerGroup))
			{
				entitiesPerGroup[group] = new HashSet<Entity>();
			}

			entitiesPerGroup[group].Add(entity);
			groupPerEntities[entity.GetId()] = group;
		}

		public bool EntityBelongsToGroup(Entity entity, string group)
		{
			if (entitiesPerGroup.TryGetValue(group, out var groupEntities))
			{
				return groupEntities.Contains(entity);
			}

			return false;
		}

		public void RemoveEntityFromGroup(Entity entity)
		{
			if (!groupPerEntities.ContainsKey(entity.GetId()))
				return;

			var groupedEntity = groupPerEntities[entity.GetId()];
			var group = entitiesPerGroup[groupedEntity];

			if (!group.Contains(entity))
				return;

			if (group != null)
			{
				Entity? entityInGroup = group.FirstOrDefault(e => Equals(e, entity));

				group.Remove(entityInGroup ?? throw new NullReferenceException("Entity in group is null"));

			}
			groupPerEntities.Remove(entity.GetId());
		}

		public void AddSystem<TSystem>(TSystem system) where TSystem : EcsSystem
		{
			systems.Add(system);
		}

		public void RemoveSystem<TSystem>() where TSystem : EcsSystem
		{
			var systemToRemove = systems.Find(t => t is TSystem);
			if (systemToRemove != null) 
				systems.Remove(systemToRemove);
		}

		public bool HasSystem<TSystem>()
		{
			return systems.Find(t => t is TSystem) != null;
		}

		public EcsSystem GetSystem<TSystem>()
		{
			var system = systems.Find(t => t is TSystem);
			if (system != null)
				return system;
			else
				throw new Exception("System doesn't exist");
		}


		public List<EcsSystem> GetAllSystems()
		{
			return systems;
		}
	}
}
