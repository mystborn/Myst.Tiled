using System.Collections.Generic;
using System.Xml.Linq;

namespace Myst.Tiled
{
    public class TiledObject : TiledElement
    {
        /// <summary>
        /// Gets the unique id of the object.
        /// </summary>
        public int? Id { get; private set; }

        /// <summary>
        /// Gets the name of the object.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Gets the x coordinate of the object.
        /// </summary>
        public int? X { get; private set; }

        /// <summary>
        /// Gets the y coordinate of the object.
        /// </summary>
        public int? Y { get; private set; }

        /// <summary>
        /// Gets the width of the object in pixels.
        /// </summary>
        public int? Width { get; private set; }

        /// <summary>
        /// Gets the height of the object in pixels.
        /// </summary>
        public int? Height { get; private set; }

        /// <summary>
        /// Gets the rotation of the object in degrees.
        /// </summary>
        public float Rotation { get; private set; }

        /// <summary>
        /// Gets a reference to a tile used to represent the object.
        /// </summary>
        public int? Gid { get; private set; }

        /// <summary>
        /// Determines whether the object is shown or hidden.
        /// </summary>
        public bool Visible { get; private set; }

        /// <summary>
        /// A referance to a template file the object inherits.
        /// </summary>
        public TiledTemplate Template { get; private set; } = null;

        /// <summary>
        /// Gets the custom properties defined by the object.
        /// </summary>
        public List<TiledProperty> Properties { get; private set; }

        /// <summary>
        /// Gets the type of the information stored in the object.
        /// </summary>
        public ObjectType ObjectType { get; private set; } = ObjectType.Rectangle;

        /// <summary>
        /// If needed, gets a list of points defined by the object.
        /// </summary>
        public List<TiledPoint> Points { get; private set; } = null;

        /// <summary>
        /// If needed gets the text defined by the object.
        /// </summary>
        public TiledText Text { get; private set; } = null;


        internal TiledObject(XElement xObject) : base(xObject) { }

        protected override void ReadXml(XElement xObject)
        {
            Id = (int?)xObject.Attribute("id");
            Name = (string)xObject.Attribute("name");
            Type = (string)xObject.Attribute("type");
            X = (int?)xObject.Attribute("x");
            Y = (int?)xObject.Attribute("y");
            Width = (int?)xObject.Attribute("width");
            Height = (int?)xObject.Attribute("height");
            Rotation = ((float?)xObject.Attribute("rotation")) ?? 0f;
            Gid = (int?)xObject.Attribute("gid");
            Visible = (((int?)xObject.Attribute("visible")) ?? 1) == 1;
            var template = (string)xObject.Attribute("template");
            if (template != null)
            {
                Template = new TiledTemplate(System.IO.Path.Combine(xObject.BaseUri, "../", template));
                var inherit = Template.TemplateObject;
                Name = Name ?? inherit.Name;
                Type = Type ?? inherit.Type;
                Width = Width ?? inherit.Width;
                Height = Height ?? inherit.Height;
                Points = Points ?? inherit.Points;
                Text = Text ?? inherit.Text;
            }
            Properties = ReadProperties(xObject.Element("properties"));
            
            var ellipse = xObject.Element("ellipse");
            var polygon = xObject.Element("polygon");
            var line = xObject.Element("polyline");
            var text = xObject.Element("text");

            if (ellipse != null)
                ObjectType = ObjectType.Ellipse;
            else if(polygon != null)
            {
                ObjectType = ObjectType.Polygon;
                Points = ParsePoints((string)polygon.Attribute("points"));
            }
            else if(line != null)
            {
                ObjectType = ObjectType.PolyLine;
                Points = ParsePoints((string)line.Attribute("points"));
            }
            else if(text != null)
            {
                ObjectType = ObjectType.Text;
                Text = new TiledText(text);
            }
        }

        private List<TiledPoint> ParsePoints(string pointString)
        {
            List<TiledPoint> result = new List<TiledPoint>();
            var pairs = pointString.Split(' ');
            foreach(var pair in pairs)
            {
                var point = pair.Split(',');
                var x = float.Parse(point[0]);
                var y = float.Parse(point[1]);
                result.Add(new TiledPoint(x, y));
            }
            return result;
        }
    }
}
