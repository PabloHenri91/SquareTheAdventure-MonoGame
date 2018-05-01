using System;
using System.Collections.Generic;
using System.Text;

using Hydra;

using Microsoft.Xna.Framework;

using System.Xml;

using FarseerPhysics;

namespace SquareTheAdventure.Map
{
    class TiledTile : SKSpriteNode
    {
        internal int gid;

        public TiledTile(int gid) : base("null")
        {
            this.gid = gid;
        }

        public TiledTile(XmlNode tileNode) : base("null")
        {
            foreach (XmlAttribute tileAttribute in tileNode.Attributes)
            {
                switch (tileAttribute.Name)
                {
                    case "gid":
                        gid = int.Parse(tileAttribute.Value);
                        break;
                    default:
                        break;
                }
            }
        }

        internal void setTexture(List<TiledTileset> tilesetList)
        {
            int tileCount = 0;
            int lastTileCount;

            foreach (TiledTileset tileset in tilesetList)
            {
                lastTileCount = tileCount;
                tileCount = tileCount + tileset.tilecount;

                if (gid > lastTileCount && gid <= tileCount)
                {
                    texture2D = tileset.texture2D;

                    int x = (gid - lastTileCount - 1) % (tileset.texture2D.Width / tileset.tilewidth) * tileset.tilewidth;
                    int y = (gid - lastTileCount - 1) / (tileset.texture2D.Height / tileset.tileheight) * tileset.tileheight;

                    sourceRectangle = new Rectangle(x, y, tileset.tileheight, tileset.tileheight);
                    scale = Vector2.One;
                    origin = new Vector2(tileset.tileheight / 2, tileset.tilewidth / 2);
                    break;
                }
            }
        }

        internal void loadPhysicsBody()
        {
            physicsBody = new SKPhysicsBody(new Vector2(TiledMap.tilewidth, TiledMap.tileheight))
            {
                BodyType = FarseerPhysics.Dynamics.BodyType.Static
            };
            physicsBody.Position = ConvertUnits.ToSimUnits(positionInNode(SKScene.current.gameWorld));
        }
    }
}
