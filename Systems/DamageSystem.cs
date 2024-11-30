using Frith.Components;
using Frith.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Systems
{
    public class DamageSystem : System
    {
        public DamageSystem()
        {
            RequireComponent<BoxColliderComponent>();
        }

        public void SubscribeToEvents(EventBus eventBus)
        {
            eventBus.SubscribeToEvent<CollisionEvent>(OnCollision);
        }

        public void OnCollision(CollisionEvent e)
        {
            Logger.Info($"The Damage System received an event collision between entities {e.EntityA.GetId()} && {e.EntityB.GetId()}");
            e.EntityA.RemoveSelf();
            e.EntityB.RemoveSelf();
        }
    }
}
