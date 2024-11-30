using Frith.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Frith.Managers;

namespace Frith.Systems
{
	public class CameraMovementSystem : System
	{
        private DisplayManager displayManager;
        private Game game;
        public CameraMovementSystem(Game game)
        {
            this.game = game;
            RequireComponent<TransformComponent>();
            displayManager = game.Services.GetService<DisplayManager>();    
        }

        public void Update(Rectangle camera)
        {
            foreach (var entity in GetSystemEntities())
            {
                var transform = entity.GetComponent<TransformComponent>();

             
                camera.X = (int)transform.Position.X;
                camera.Y = (int)transform.Position.Y;

              //  Logger.Info($"Camera changted its position to {camera.X}, {camera.Y}");
            }
        }
    }
}
