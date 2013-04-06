using System;
using System.Collections.Generic;
using Box2D.XNA;

namespace BlockWars
{
    class ContactListener : IContactListener
    {
        private List<Body[]> mBodies;

        public ContactListener()
        {
            mBodies = new List<Body[]>();
        }

        public void BeginContact(Contact contact)
        {
        }

        public void EndContact(Contact contact)
        {
        }

        public void PreSolve(Contact contact, ref Manifold oldManifold)
        {
        }

        public void PostSolve(Contact contact, ref ContactImpulse impulse)
        {
            Body[] bodies = new Body[2];
            bodies[0] = contact.GetFixtureA().GetBody();
            bodies[1] = contact.GetFixtureB().GetBody();
            mBodies.Add(bodies);
            if (mBodies.Count > 10000)
            {
                mBodies.Clear();
                Console.WriteLine("Clear contacts.");
            }
        }

        public List<Body[]> GetContacts()
        {
            List<Body[]> result = mBodies;
            mBodies = new List<Body[]>();
            return result;
        }
    }
}
