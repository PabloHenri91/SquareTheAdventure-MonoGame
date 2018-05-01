using System;
using System.Collections.Generic;
using System.Text;

using Hydra;

using Microsoft.Xna.Framework;

using System.Xml;
using System.IO;
using System.IO.Compression;

namespace SquareTheAdventure.Map
{
    class TiledMap : SKNode
    {
        internal static Vector2 size;

        string version;
        string tiledversion;
        string orientation;
        string renderorder;
        int width;
        int height;
        internal static int tilewidth;
        internal static int tileheight;
        int nextobjectid;
        bool infinite;

        public TiledMap(float x, float y)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("Content\\Map\\A\\A.tmx");

            List<TiledTileset> tilesetList = new List<TiledTileset>();
            List<TiledLayer> layerList = new List<TiledLayer>();
            List<TiledObjectGroup> objectgroupList = new List<TiledObjectGroup>();

            foreach (XmlNode xmlDocumentNode in xmlDocument.ChildNodes)
            {
                switch (xmlDocumentNode.Name)
                {
                    case "xml":
                        break;
                    case "map":
                        foreach (XmlAttribute mapAttribute in xmlDocumentNode.Attributes)
                        {
                            switch (mapAttribute.Name)
                            {
                                case "version":
                                    version = mapAttribute.Value;
                                    break;
                                case "tiledversion":
                                    tiledversion = mapAttribute.Value;
                                    break;
                                case "orientation":
                                    orientation = mapAttribute.Value;
                                    break;
                                case "renderorder":
                                    renderorder = mapAttribute.Value;
                                    break;
                                case "width":
                                    width = int.Parse(mapAttribute.Value);
                                    break;
                                case "height":
                                    height = int.Parse(mapAttribute.Value);
                                    break;
                                case "tilewidth":
                                    tilewidth = int.Parse(mapAttribute.Value);
                                    break;
                                case "tileheight":
                                    tileheight = int.Parse(mapAttribute.Value);
                                    break;
                                case "infinite":
                                    infinite = int.Parse(mapAttribute.Value) == 1;
                                    break;
                                case "nextobjectid":
                                    nextobjectid = int.Parse(mapAttribute.Value);
                                    break;
                                default:
#if DEBUG
                                    throw new Exception();
#else
                                    break;
#endif
                            }
                        }

                        foreach (XmlNode mapChildNode in xmlDocumentNode.ChildNodes)
                        {
                            switch (mapChildNode.Name)
                            {
                                case "tileset":
                                    TiledTileset tileset = new TiledTileset(mapChildNode);
                                    tilesetList.Add(tileset);
                                    break;
                                case "layer":
                                    TiledLayer layer = new TiledLayer(mapChildNode);
                                    layerList.Add(layer);
                                    break;
                                case "objectgroup":
                                    TiledObjectGroup objectgroup = new TiledObjectGroup(mapChildNode);
                                    objectgroupList.Add(objectgroup);
                                    break;
                                default:
#if DEBUG
                                    throw new Exception();
#else
                                    break;
#endif
                            }
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

            size.X = width * tilewidth;
            size.Y = height * tileheight;

            position = new Vector2(size.X * x, size.Y * y);

            foreach (TiledLayer layer in layerList)
            {
                foreach (TiledData data in layer.dataList)
                {
                    switch (data.encoding)
                    {
                        case null: // XML
                            break;
                        case "csv": // CSV
                            if (infinite)
                            {
                                foreach (TiledChunk chunk in data.chunkList)
                                {
                                    foreach (string item in chunk.text.Split(','))
                                    {
                                        int gid = int.Parse(item);
                                        data.tileList.Add(new TiledTile(gid));
                                    }
                                }
                            }
                            else
                            {
                                foreach (string item in data.text.Split(','))
                                {
                                    int gid = int.Parse(item);
                                    data.tileList.Add(new TiledTile(gid));
                                }
                            }
                            break;
                        case "base64":

                            byte[] buffer = Convert.FromBase64String(data.text);
                            Stream memoryStream = new MemoryStream(buffer, false);

                            switch (data.compression)
                            {
                                case null: // Base64(descomprimido)
                                    break;
                                case "gzip": // Base64(comprimido com gzip)
                                    memoryStream = new GZipStream(memoryStream, CompressionMode.Decompress);
                                    break;
                                case "zlib": // Base64(comprimido com zlib)
                                    int bufferLength = buffer.Length - 6;
                                    byte[] bufferAux = new byte[bufferLength];
                                    Array.Copy(buffer, 2, bufferAux, 0, bufferLength);

                                    MemoryStream stream = new MemoryStream(bufferAux, false);
                                    memoryStream = new DeflateStream(stream, CompressionMode.Decompress);
                                    break;
                            }

                            using (BinaryReader binaryReader = new BinaryReader(memoryStream))
                            {
                                int count = width * height;
                                for (int j = 0; j < count; j++)
                                {
                                    int gid = (int)binaryReader.ReadUInt32();
                                    data.tileList.Add(new TiledTile(gid));
                                }
                            }
                            break;
                    }

                    int i = 0;

                    foreach (TiledTile tile in data.tileList)
                    {
                        if (tile.gid > 0)
                        {
                            tile.size = new Vector2(tilewidth, tileheight);

                            tile.position = new Vector2(
                                (i % width) * tilewidth - size.X / 2,
                                (i / height) * tileheight - size.Y / 2);
                            tile.setTexture(tilesetList);
                            addChild(tile);
                        }
                        i++;
                    }

                    foreach (TiledObjectGroup tiledObjectGroup in objectgroupList)
                    {
                        foreach (TiledObject tiledObject in tiledObjectGroup.objectList)
                        {
                            tiledObject.position.X = tiledObject.position.X - size.X / 2 + tiledObject.width / 2 - tilewidth / 2;
                            tiledObject.position.Y = tiledObject.position.Y - size.Y / 2 + tiledObject.height / 2 - tileheight / 2;
                            addChild(tiledObject);
                        }
                    }
                }
            }
        }

        internal new void removeFromParent()
        {
            removeAllChildren();
            base.removeFromParent();
        }

        internal void update()
        {

        }
    }
}
