//using Frith.Components;
using Frith.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Signature = System.Collections.BitArray;


namespace Frith
{
	public static class EcsConstants
    {
        private const int MaxComponents = 32;

        // Signature
        // We use a bitset (1s and 0s) to keep track of which components an entity has,
        // and also helps keep track of which entities a system is interested in
        public static Signature CreateSignature() => new Signature(MaxComponents);
    }

    //IComponent and Component
    //Component only needs an id
    public class Component
    {
        protected static int nextId;
    }

    public abstract class Component<T> : Component
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



    // Entity
    // Needs an id and operator overloading. 
    public class Entity(int id)
    {
        private readonly int id = id;

        public enum LifeCycle
        {
            Persistant,
            Volatile,
            Isolated,
            Reserved
        }

        public int? SceneId { get; set; }

		public Registry registry;

        public LifeCycle EntityLifeCycle { get; set; } = LifeCycle.Isolated;

        public void RemoveSelf()
        {
            registry.RemoveEntity(this);
        }
         
        public int GetId() => id;


        //public static bool operator ==(Entity a, Entity b) => a.id == b.id;
        //public static bool operator !=(Entity a, Entity b) => a.id != b.id;
        //public static bool operator >(Entity a, Entity b) => a.id > b.id;
        //public static bool operator <(Entity a, Entity b) => a.id < b.id;


        

        public override bool Equals(object? obj)
        {
            return obj is Entity entity && id == entity.id;
        }

        public override int GetHashCode() => id.GetHashCode();

        public void AddComponent<TComponent>(TComponent component) where TComponent : struct
        {   
            if (!HasComponent<TComponent>())
                registry.AddComponent(this, component);

            return;
        }

        public void RemoveComponent<TComponent>() where TComponent: struct
        {
            registry.RemoveComponent<TComponent>(this);
        }

        public bool HasComponent<TComponent>() where TComponent : struct
        {
            return registry.HasComponent<TComponent>(this);
        }

        public ref TComponent GetComponent<TComponent>() where TComponent : struct
        { 
            return ref registry.GetComponent<TComponent>(this);
        }




        public void Tag(string tag)
        {
            registry.TagEntity(this, tag);
        }

        public bool HasTag(string tag)
        {
            return registry.EntityHasTag(this, tag);
        }

        public void Group(string group)
        {
            registry.GroupEntity(this, group);
        }

        public bool BelongsToGroup(string group)
        {
            return registry.EntityBelongsToGroup(this, group);
        }

		public override string ToString()
		{
            return $"Entity {id}";
		}
	}


    // System
    // The System processes entities that contain a specific signature

    //Contents:
    // 1: componentSignature
    // 2: entities list
    // 3: add entities to system if there's component match
    // 4: remove entities from system
    // 5: Require a component by setting its bitset in the system to true
    public class System
    {
        // Which components an entity must have for the system to consider the entity
        private readonly Signature componentSignature = EcsConstants.CreateSignature();

        // list of all entities that the system is interested in
        private readonly List<Entity> entities = new List<Entity>();

        public void AddEntityToSystem(Entity entity)
        {
            entities.Add(entity);
        }

        public void RemoveEntityFromSystem(Entity entity)
        {
            entities.RemoveAll(e => Equals(e, entity));
        }

        public List<Entity> GetSystemEntities() => entities;

        public Signature GetComponentSignature() => componentSignature;

        public void RequireComponent<TComponent>()
        {
            int componentId = Component<TComponent>.GetId();
            componentSignature.Set(componentId, true);
        }

        public virtual void Initialize() { }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(SpriteBatch spriteBatch) { }
    }

    public interface IPool
    {
        public void RemoveEntityFromPool(int entityId)
        { }
    }


    // Pool 

    public class Pool<T> : IPool where T : struct
    {
        private T[] data;
        private int size;

        private readonly Dictionary<int, int> entityIdToIndex = new Dictionary<int, int>();
        private readonly Dictionary<int, int> indexToEntityId = new Dictionary<int, int>();

        private int capacity;



        public Pool(int initialCapacity = 100)
        {
            this.capacity = initialCapacity;
            size = 0;
            data = new T[capacity];
        }

		private void AddCapacity()
		{
            capacity *= 2;
            var newData = new T[capacity];
            Array.Copy(data, newData, size);
            data = newData;
		}


		public void RemoveEntityFromPool(int entityId)
        {
            if (entityIdToIndex.ContainsKey(entityId))
            {
                Remove(entityId);
            }
        }

        public bool IsEmpty() => size == 0;

        public int GetSize() => size;





        public void Clear()
        {
            Array.Clear(data, 0, size);

            entityIdToIndex.Clear();
            indexToEntityId.Clear();

            size = 0;
        }

        public void Add(T component) 
        {
            if (size >= capacity)
            {
                AddCapacity();
            }

            data[size++] = component;
        }

        public void Set(int entityId, T component)
        {
            if (entityIdToIndex.TryGetValue(entityId, out var index)) 
            {
                data[index] = component;
            }
            else
            {
                if (size >= capacity)
                {
                    AddCapacity();
                }

                int newIndex = size;
                entityIdToIndex[entityId] = newIndex;
                indexToEntityId[size] = entityId;

                data[size++] = component;
               
            }
        }

        public void Remove(int entityId)
        {
            if (!entityIdToIndex.TryGetValue(entityId, out var indexOfRemoved))
            {
                throw new InvalidOperationException($"Entity {entityId} does not exist in the pool");   
            }

            var indexOfLast = size - 1;
            data[indexOfRemoved] = data[indexOfLast];

            var entityIdOfLastElement = indexToEntityId[indexOfLast];
            entityIdToIndex[entityIdOfLastElement] = indexOfRemoved;
            indexToEntityId[indexOfRemoved] = entityIdOfLastElement;

            entityIdToIndex.Remove(entityId);
            indexToEntityId.Remove(indexOfLast);

            data[indexOfLast] = default;

            size--;
        }

        public ref T Get(int entityId)
        {
            if (!entityIdToIndex.TryGetValue(entityId, out var index))
            {
                throw new KeyNotFoundException($"Entity {entityId} does not have a component of type {typeof(T).Name}.");
            }
            return ref data[index]; 
        }

        public T this[int index]
        {
            get => data[index];
            set => data[index] = value;
        }
    }


    //Registry
    //The registry manages the creation and destruction of entities, add systems, and components
    public class Registry
    {
        private int numEntities = 0;

        // List of component pools, each pool contains all the data for a certain component type
        private readonly List<IPool> componentPools = new List<IPool>();

        private readonly List<Signature?> entityComponentSignatures = new List<Signature?>();

        private readonly Dictionary<Type, System> systems = new Dictionary<Type, System>();

        private readonly HashSet<Entity> entitiesToBeAdded = new HashSet<Entity>();
        private readonly HashSet<Entity> entitiesToBeRemoved = new HashSet<Entity>();

        private readonly Dictionary<string, Entity?> entityPerTag = new Dictionary<string, Entity?>();
        private readonly Dictionary<int, string> tagPerEntity = new Dictionary<int, string>();

        private readonly Dictionary<string, HashSet<Entity?>> entitiesPerGroup = new Dictionary<string, HashSet<Entity?>>();
        private readonly Dictionary<int, string> groupPerEntity = new Dictionary<int, string>();

        private readonly HashSet<Entity> allEntities = new HashSet<Entity>();

        public HashSet<Entity> GetAllEntities => allEntities;

        private readonly HashSet<Entity> reservedEntities = new HashSet<Entity>();

		public void ReserveEntity(Entity entity)
        {
            
            reservedEntities.Add(entity);
            RemoveEntityFromSystems(entity);

           
        }

        public void ReassignReservedEntity(Entity entity, int sceneId)
        {
            reservedEntities.Remove(entity);
            AddEntityToSystems(entity);
            entity.SceneId = sceneId;
        }

		public void ReassignReservedEntity(Entity entity, Scene scene)
		{
			reservedEntities.Remove(entity);
			AddEntityToSystems(entity);
			entity.SceneId = scene.GetId;
            entity.EntityLifeCycle = Entity.LifeCycle.Isolated;
		}

		public HashSet<System> GetAllSystems()
        {
            return systems.Values.ToHashSet();
        }

        


        public void TagEntity(Entity entity, string tag)
        {
            entityPerTag[tag] = entity;
            tagPerEntity[entity.GetId()] = tag;
        }

        public bool EntityHasTag(Entity? entity, string tag)
        {
            return tagPerEntity[entity.GetId()] == null && entityPerTag[tag].Equals(entity);
        }

        public Entity? GetEntityByTag(string tag)
        {
            return entityPerTag[tag]; 
        }

        public void RemoveEntityTag(Entity? entity)
        {
            if (!tagPerEntity.ContainsKey(entity.GetId()))
                return;

            var taggedEntity = tagPerEntity[entity.GetId()];
            entityPerTag.Remove(taggedEntity); 
            tagPerEntity.Remove(entity.GetId());
        }

        public void GroupEntity(Entity? entity, string group)
        {
            if (!entitiesPerGroup.TryGetValue(group, out var entityPerGroup))
            {
				entitiesPerGroup[group] = new HashSet<Entity?>();
			}
            
            entitiesPerGroup[group].Add(entity);
            groupPerEntity[entity.GetId()] = group;
        }

        public bool EntityBelongsToGroup(Entity? entity, string group)
        {
            if (entitiesPerGroup.TryGetValue(group, out var groupEntities))
            {
                return groupEntities.Contains(entity);
            }

            return false;
        }

        public HashSet<Entity?> GetEntitiesByGroup(string group)
        {
            return entitiesPerGroup[group];
        }

        public void RemoveEntityGroup(Entity? entity)
        {
            if (!groupPerEntity.ContainsKey(entity.GetId()))
                 return; 

            var groupedEntity = groupPerEntity[entity.GetId()];
            var group = entitiesPerGroup[groupedEntity];

            if (!group.Contains(entity))
                return;

            if (group != null)
            {
                var entityInGroup = group.FirstOrDefault(e => Equals(e, entity));
                    
                group.Remove(entityInGroup);
                    
            }
            groupPerEntity.Remove(entity.GetId());
        }


        private readonly LinkedList<int>? freeIds = [];

        public Entity CreateEntity(Scene? associatedScene = null)
        {
            int entityId;

            if (freeIds?.Count == 0)
            {
				entityId = numEntities++;
				if (entityId >= entityComponentSignatures.Count)
				{
					entityComponentSignatures.Add(EcsConstants.CreateSignature());
				}
			}
			else
			{
				entityId = (freeIds ?? throw new InvalidOperationException()).First();
				freeIds.RemoveFirst();
			}


			var entity = new Entity(entityId)
            {
                registry = this
            };
            entitiesToBeAdded.Add(entity);

            if (associatedScene != null)
                entity.SceneId = associatedScene.GetId;
            else 
                entity.EntityLifeCycle = Entity.LifeCycle.Persistant;

            allEntities.Add(entity);

            Logger.Info($"Entity created with id = {entityId}");



            return entity;
        }

        public void RemoveEntity(Entity entity)
        {
            entitiesToBeRemoved.Add(entity);
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
                freeIds?.AddLast(entity.GetId());
                entityComponentSignatures[entity.GetId()]?.SetAll(false);

                foreach (var pool in componentPools)
                {
                    pool?.RemoveEntityFromPool(entity.GetId());
                }
                
                RemoveEntityTag(entity);
                RemoveEntityGroup(entity);
            }

            entitiesToBeRemoved.Clear();

           
        }

        public void AddComponent<TComponent>(Entity entity, TComponent component) where TComponent : struct
        {
            var componentId = Component<TComponent>.GetId();
            var entityId = entity.GetId();


            //If the component id is greater than the current size of the componentPools, then resize the vector
            if (componentId >= componentPools.Count)
            {
                componentPools.AddRange(new IPool[componentId - componentPools.Count + 1]);
            }

            if (componentPools[componentId] == null)
            {
                componentPools[componentId] = new Pool<TComponent>();
            }

           

            //Get the pool of component values for that component type
            Pool<TComponent> componentPool = (Pool<TComponent>)componentPools[componentId];
            componentPool.Set(entityId, component);
            // if (componentPool == null)
            // {
            //     componentPool[componentId] = new Pool<TComponent>();
            // }

            // If the entity id is greater than the current size of the component pool, then resize the pool
            //if (entityId >= pool.GetSize())
            //{
            //    pool.Resize(numEntities);
            //}

            //add the new component to the component pool list, using the entity id as index
            componentPool[entityId] = component;

            // Finally, change the component signature of the entity and set the component id on the bitset to 1
            entityComponentSignatures[entityId]?.Set(componentId, true);

            Logger.Info($"Component id {componentId} was added to entity id {entityId}");

        }

        public void RemoveComponent<TComponent>(Entity entity) where TComponent : struct
        {
            var componentId = Component<TComponent>.GetId();
            var entityId = entity.GetId();
            

			var pool = (Pool<TComponent>)componentPools[componentId];
            pool.Remove(entityId);

			entityComponentSignatures[entityId]?.Set(componentId, false);

			Logger.Info($"Component id {componentId} was removed from entity id {entityId}");
        }

        public bool HasComponent<TComponent>(Entity entity) where TComponent : struct
        {
            var componentId = Component<TComponent>.GetId();
            var entityId = entity.GetId();

            return entityComponentSignatures[entityId]?[componentId] == true;
        }

        public ref TComponent GetComponent<TComponent>(Entity entity) where TComponent: struct
        {
            var componentId = Component<TComponent>.GetId();
            var pool = (Pool<TComponent>)componentPools[componentId];
            return ref pool.Get(entity.GetId());
        }

        public void AddSystem<TSystem>(TSystem system) where TSystem : System
        {
            systems[typeof(TSystem)] = system;
        }

        public void RemoveSystem<TSystem>()
        {
            var system = systems[typeof(TSystem)];
            systems.Remove(system.GetType());
        }

        public bool HasSystem<TSystem>() where TSystem : System
        {
            return systems.ContainsKey(typeof(TSystem));
        }

		public TSystem? GetSystem<TSystem>() where TSystem : System
		{
			if (systems.TryGetValue(typeof(TSystem), out var system))
			{
				return system as TSystem;
			}
			return null;  // System not found
		}

        private void AddEntityToSystems(Entity entity)
        {
            var entityId = entity.GetId();

            var entityComponentSignature = entityComponentSignatures[entityId];

            foreach (var system in systems.Values)
            {
                var systemComponentSignature = system.GetComponentSignature();

                if (entityComponentSignature == null) continue;
                var signatureResult = new Signature(entityComponentSignature).And(systemComponentSignature);

                var isInterested = signatureResult.AreValuesEqual(systemComponentSignature);

                if (isInterested)
                {
                    system.AddEntityToSystem(entity);
                }

            }
        }

        public void RemoveEntityFromSystems(Entity entity)
        {
            foreach (var system in systems)
            {
                system.Value.RemoveEntityFromSystem(entity);
            }
        }


	}


}
