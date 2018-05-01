using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;

namespace SquareTheAdventure.Map
{
    class TiledLayer
    {
        string name;
        int width;
        int height;

        internal List<TiledData> dataList;

        public TiledLayer(XmlNode layerNode)
        {
            dataList = new List<TiledData>();

            foreach (XmlAttribute layerAttribute in layerNode.Attributes)
            {
                switch (layerAttribute.Name)
                {
                    case "name":
                        name = layerAttribute.Value;
                        break;
                    case "width":
                        width = int.Parse(layerAttribute.Value);
                        break;
                    case "height":
                        height = int.Parse(layerAttribute.Value);
                        break;
                    default:
#if DEBUG
                        throw new Exception();
#else
                        break;
#endif
                }
            }

            foreach (XmlNode layerChildNode in layerNode.ChildNodes)
            {
                switch (layerChildNode.Name)
                {
                    case "data":
                        TiledData data = new TiledData(layerChildNode);
                        dataList.Add(data);
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
