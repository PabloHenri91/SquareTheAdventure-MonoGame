using System;
using System.Collections.Generic;
using System.Text;

using Hydra;

using Microsoft.Xna.Framework;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;

namespace SquareTheAdventure
{
    class Player : SKSpriteNode
    {
        internal int canJump;

        float jumpMaxVelocity = 6;
        float moveForce = 9;
        float moveMaxVelocity = 3;
        float angularImpulse = 0.5f;
        float maxAngularVelocity = 5;
        float rotationToDestination = -(float)Math.Atan2(0, 1);

        public Player() : base("64")
        {
            color = new Color(0.5f, 0.5f, 1.0f);
            size = new Vector2(40, 40);
        }

        internal void update(bool left, bool right, bool up)
        {
            if (left)
            {
                if (physicsBody.LinearVelocity.X > -moveMaxVelocity)
                {
                    if (canJump > 0)
                    {
                        physicsBody.ApplyForce(new Vector2(-moveForce, 0));
                    }
                    else
                    {
                        physicsBody.ApplyForce(new Vector2(-moveForce * 0.125f, 0));
                    }
                }
            }

            if (right)
            {
                if (physicsBody.LinearVelocity.X < moveMaxVelocity)
                {
                    if (canJump > 0)
                    {
                        physicsBody.ApplyForce(new Vector2(moveForce, 0));
                    }
                    else
                    {
                        physicsBody.ApplyForce(new Vector2(moveForce * 0.125f, 0));
                    }
                }
            }

            if (up)
            {
                if (canJump > 0)
                {
                    if (Math.Abs(physicsBody.LinearVelocity.Y) < jumpMaxVelocity)
                    {
                        physicsBody.LinearVelocity = new Vector2(physicsBody.LinearVelocity.X, -jumpMaxVelocity);
                    }
                }
            }

            if (Math.Abs(physicsBody.AngularVelocity) < maxAngularVelocity)
            {
                float totalRotationToDestination = rotationToDestination - zRotation;

                while (totalRotationToDestination < -Math.PI) { totalRotationToDestination += (float)Math.PI * 2; }
                while (totalRotationToDestination > Math.PI) { totalRotationToDestination -= (float)Math.PI * 2; }

                physicsBody.ApplyTorque(totalRotationToDestination * angularImpulse);
            }
        }

        internal void draw()
        {

        }

        internal void loadPhysics()
        {
            physicsBody = new SKPhysicsBody(new Vector2(40, 40))
            {
                IsBullet = true
            };
            physicsBody.OnCollision += physicsBody_OnCollision;
            physicsBody.OnSeparation += physicsBody_OnSeparation;
        }

        bool physicsBody_OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            canJump += 1;
            return true;
        }

        void physicsBody_OnSeparation(Fixture fixtureA, Fixture fixtureB)
        {
            canJump -= 1;
        }
    }
}
