using System;
using System.Collections.Generic;
using System.Text;

using Hydra;

using Microsoft.Xna.Framework.Graphics;

using System.Xml;

namespace SquareTheAdventure.Map
{
    class TiledTileset
    {
        private int firstgid;
        private string name;
        internal int tilewidth;
        internal int tileheight;
        internal int tilecount;
        private int columns;

        internal Texture2D texture2D;

        public TiledTileset(int firstgid, string name, int tilewidth, int tileheight, int tilecount, int columns)
        {
            this.firstgid = firstgid;
            this.name = name;
            this.tilewidth = tilewidth;
            this.tileheight = tileheight;
            this.tilecount = tilecount;
            this.columns = columns;

            load();
        }

        public TiledTileset(XmlNode tilesetNode)
        {
            foreach (XmlAttribute tilesetAttribute in tilesetNode.Attributes)
            {
                switch (tilesetAttribute.Name)
                {
                    case "firstgid":
                        firstgid = int.Parse(tilesetAttribute.Value);
                        break;
                    case "name":
                        name = tilesetAttribute.Value;
                        break;
                    case "tilewidth":
                        tilewidth = int.Parse(tilesetAttribute.Value);
                        break;
                    case "tileheight":
                        tileheight = int.Parse(tilesetAttribute.Value);
                        break;
                    case "tilecount":
                        tilecount = int.Parse(tilesetAttribute.Value);
                        break;
                    case "columns":
                        columns = int.Parse(tilesetAttribute.Value);
                        break;
                    default:
#if DEBUG
                        throw new Exception();
#else
                        break;
#endif
                }
            }

            foreach (XmlNode tilesetChildNode in tilesetNode.ChildNodes)
            {
                switch (tilesetChildNode.Name)
                {
                    case "image":
                        break;
                    default:
#if DEBUG
                        throw new Exception();
#else
                        break;
#endif
                }
            }

            load();
        }

        void load()
        {
            texture2D = SKScene.current.contentManager.Load<Texture2D>("Texture2D/" + name);
        }
    }
}
