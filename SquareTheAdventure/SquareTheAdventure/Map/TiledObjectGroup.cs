using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;

namespace SquareTheAdventure.Map
{
    class TiledObjectGroup
    {
        private string name;

        internal List<TiledObject> objectList;

        public TiledObjectGroup(XmlNode objectgroupNode)
        {
            foreach (XmlAttribute objectgroupAttribute in objectgroupNode.Attributes)
            {
                switch (objectgroupAttribute.Name)
                {
                    case "name":
                        name = objectgroupAttribute.Value;
                        break;
                    default:
#if DEBUG
                        throw new Exception();
#else
                        break;
#endif
                }
            }

            objectList = new List<TiledObject>();

            foreach (XmlNode objectgroupChildNode in objectgroupNode.ChildNodes)
            {
                switch (objectgroupChildNode.Name)
                {
                    case "object":
                        TiledObject tiledObject = new TiledObject(objectgroupChildNode);
                        objectList.Add(tiledObject);
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
