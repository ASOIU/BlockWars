using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlockWars
{
    abstract class AGameObject
    {
        public EObjectType ObjectType {protected get; set; }

        public abstract Vector2 GetPosition();

        public abstract void SetPosition(Vector2 position);
    }

}
