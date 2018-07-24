using System;
using System.Collections.Generic;
using System.Text;

using Hydra;

using System.Xml;

using Microsoft.Xna.Framework;

using FarseerPhysics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;

namespace SquareTheAdventure.Map
{
    class TiledObject : SKNode
    {
        private int id;
        internal int width;
        internal int height;

        XmlNodeList ChildNodes;

        public TiledObject(XmlNode objectNode)
        {
            foreach (XmlAttribute objectAttribute in objectNode.Attributes)
            {
                switch (objectAttribute.Name)
                {
                    case "id":
                        id = int.Parse(objectAttribute.Value);
                        break;
                    case "x":
                        position = new Vector2(int.Parse(objectAttribute.Value), position.Y);
                        break;
                    case "y":
                        position = new Vector2(position.X, int.Parse(objectAttribute.Value));
                        break;
                    case "width":
                        width = int.Parse(objectAttribute.Value);
                        break;
                    case "height":
                        height = int.Parse(objectAttribute.Value);
                        break;
                    case "visible":
                        isHidden = int.Parse(objectAttribute.Value) == 0;
                        break;
                    case "name":
                        name = objectAttribute.Value;
                        break;
                    default:
#if DEBUG
                        throw new Exception();
#else
                        break;
#endif
                }
            }

            ChildNodes = objectNode.ChildNodes;
        }

        internal void loadPhysicsBody()
        {
            if (ChildNodes.Count > 0)
            {
                foreach (XmlNode objectChildNode in ChildNodes)
                {
                    switch (objectChildNode.Name)
                    {
                        case "ellipse":
                            physicsBody = new SKPhysicsBody(new Vector2(width, height), ShapeType.Circle);
                            break;
                        case "polygon":
                            physicsBody = new SKPhysicsBody(getVertices(objectChildNode));
                            break;
                        case "polyline":
                            physicsBody = new SKPhysicsBody(getVertices(objectChildNode));
                            break;
                        default:
#if DEBUG
                            throw new Exception();
#else
                        break;
#endif
                    }
                }
            }
            else
            {
                physicsBody = new SKPhysicsBody(new Vector2(width, height));
            }

            physicsBody.Position = ConvertUnits.ToSimUnits(positionInNode(SKScene.current.gameWorld));
            physicsBody.BodyType = FarseerPhysics.Dynamics.BodyType.Static;
        }

        private static Vertices getVertices(XmlNode objectChildNode)
        {
            Vertices vertices = new Vertices();
            foreach (XmlAttribute attribute in objectChildNode.Attributes)
            {
                switch (attribute.Name)
                {
                    case "points":
                        string[] points = attribute.Value.Split(' ');
                        foreach (string item in points)
                        {
                            string[] xy = item.Split(',');
                            Vector2 vector2 = new Vector2(
                                Convert.ToSingle(xy[0]),
                                Convert.ToSingle(xy[1]));
                            vertices.Add(vector2);

                        }
                        break;
                    default:
#if DEBUG
                        throw new Exception();
#else
                                        break;
#endif
                }
            }

            return vertices;
        }
    }
}
