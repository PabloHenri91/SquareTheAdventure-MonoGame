using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;

namespace SquareTheAdventure.Map
{
    class TiledData
    {
        internal string encoding;
        internal string compression;

        internal string text;

        internal List<TiledTile> tileList;

        internal List<TiledChunk> chunkList;

        public TiledData(XmlNode dataNode)
        {
            tileList = new List<TiledTile>();

            chunkList = new List<TiledChunk>();

            foreach (XmlAttribute dataAttribute in dataNode.Attributes)
            {
                switch (dataAttribute.Name)
                {
                    case "encoding":
                        encoding = dataAttribute.Value;
                        break;
                    case "compression":
                        compression = dataAttribute.Value;
                        break;
                    default:
#if DEBUG
                        throw new Exception();
#else
                        break;
#endif
                }
            }

            foreach (XmlNode dataChildNode in dataNode.ChildNodes)
            {
                switch (dataChildNode.Name)
                {
                    case "#text":
                        text = dataChildNode.Value.Trim();
                        break;
                    case "tile":
                        TiledTile tile = new TiledTile(dataChildNode);
                        tileList.Add(tile);
                        break;
                    case "chunk":
                        TiledChunk chunk = new TiledChunk(dataChildNode);
                        chunkList.Add(chunk);
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
