using System.Xml.Linq;

namespace Myst.Tiled
{
    public class TiledProperty : TiledElement
    {
        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the type of the property.
        /// </summary>
        public PropertyType Type { get; private set; }

        /// <summary>
        /// Gets the value of the property.
        /// </summary>
        public object Value { get; private set; }

        internal TiledProperty(XElement element)
            : base(element)
        {
        }

        protected override void ReadXml(XElement xProp)
        {
            Name = (string)xProp.Attribute("name");
            var type = (string)xProp.Attribute("type");
            Type = type == null ? PropertyType.String : PropertyTypeParser[type];
            var value = (string)xProp.Attribute("value");
            switch (Type)
            {
                case PropertyType.Bool:
                    Value = value == "true";
                    break;
                case PropertyType.File:
                    Value = System.IO.Path.Combine(xProp.BaseUri, value);
                    break;
                case PropertyType.Float:
                    Value = float.Parse(value);
                    break;
                case PropertyType.Int:
                    Value = int.Parse(value);
                    break;
                case PropertyType.Color:
                    Value = new TiledColor(value);
                    break;
                default:
                    Value = value;
                    break;
            }
        }
    }
}
