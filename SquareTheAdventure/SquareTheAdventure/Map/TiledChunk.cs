using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;

namespace SquareTheAdventure.Map
{
    public class TiledChunk
    {
        int x;
        int y;
        int width;
        int height;
        internal string text;

        public TiledChunk(XmlNode chunkNode)
        {
            foreach (XmlAttribute chunkAttribute in chunkNode.Attributes)
            {
                switch (chunkAttribute.Name)
                {
                    case "x":
                        x = int.Parse(chunkAttribute.Value);
                        break;
                    case "y":
                        y = int.Parse(chunkAttribute.Value);
                        break;
                    case "width":
                        width = int.Parse(chunkAttribute.Value);
                        break;
                    case "height":
                        height = int.Parse(chunkAttribute.Value);
                        break;
                    default:
#if DEBUG
                        throw new Exception();
#else
                        break;
#endif
                }
            }

            foreach (XmlNode chunkChildNode in chunkNode.ChildNodes)
            {
                switch (chunkChildNode.Name)
                {
                    case "#text":
                        text = chunkChildNode.Value.Trim();
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
    }
}
