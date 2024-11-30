using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Events
{
	public class KeyPressedEvent : Event
	{
		private Keys key;


        public Keys Key
        {
            get { return key; }
            set { key = value; }
        }

        public KeyPressedEvent(Keys key)
        {
            this.key = key;
           
            
        }
    }
}
