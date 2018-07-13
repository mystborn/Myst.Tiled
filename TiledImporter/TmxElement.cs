using System.Collections.Generic;
using System.Xml.Linq;

namespace Myst.Tiled
{
    public abstract class TiledElement
    {
        internal static Parser<PropertyType> PropertyTypeParser { get; } = new Parser<PropertyType>()
        {
            { "bool", PropertyType.Bool },
            { "color", PropertyType.Color },
            { "file", PropertyType.File },
            { "float", PropertyType.Float },
            { "int", PropertyType.Int },
            { "string", PropertyType.String }
        };

        internal static Parser<OrientationType> OrientationParser { get; } = new Parser<OrientationType>()
        {
            { "orthogonal", OrientationType.Orthogonal },
            { "isometric", OrientationType.Isometric },
            { "staggered", OrientationType.Staggered },
            { "hexagonal", OrientationType.Hexagonal }
        };

        internal static Parser<RenderOrderType> RenderOrderParser { get; } = new Parser<RenderOrderType>()
        {
            { "right-down", RenderOrderType.RightDown },
            { "right-up", RenderOrderType.RightUp },
            { "left-down", RenderOrderType.LeftDown },
            { "left-up", RenderOrderType.LeftUp },
        };

        internal static Parser<StaggerAxisType> AxisParser { get; } = new Parser<StaggerAxisType>()
        {
            { "x", StaggerAxisType.X },
            { "y", StaggerAxisType.Y }
        };

        internal static Parser<StaggerIndexType> StaggerIndexParser { get; } = new Parser<StaggerIndexType>()
        {
            { "even", StaggerIndexType.Even },
            { "odd", StaggerIndexType.Odd }
        };

        internal static Parser<EncodingType> EncodingParser { get; } = new Parser<EncodingType>()
        {
            { "xml", EncodingType.Xml },
            { "csv", EncodingType.Csv },
            { "base64", EncodingType.Base64 }
        };

        internal static Parser<CompressionType> CompressionParser { get; } = new Parser<CompressionType>()
        {
            { "gzip", CompressionType.GZip },
            { "zlib", CompressionType.ZLib }
        };

        internal static Parser<DrawOrderType> DrawOrderParser { get; } = new Parser<DrawOrderType>()
        {
            { "index", DrawOrderType.Index },
            { "topdown", DrawOrderType.TopDown }
        };

        internal static Parser<ObjectType> ObjectTypeParser { get; } = new Parser<ObjectType>()
        {
            { "ellipse", ObjectType.Ellipse },
            { "point", ObjectType.Point },
            { "polygon", ObjectType.Polygon },
            { "polyline", ObjectType.PolyLine },
            { "text", ObjectType.Text }
        };

        internal static Parser<HorizontalAlignment> HorizontalAlignmentParser { get; } = new Parser<HorizontalAlignment>()
        {
            { "left", HorizontalAlignment.Left },
            { "center", HorizontalAlignment.Center },
            { "right", HorizontalAlignment.Right }
        };

        internal static Parser<VerticalAlignment> VerticalAlignmentParser { get; } = new Parser<VerticalAlignment>()
        {
            { "top", VerticalAlignment.Top },
            { "center", VerticalAlignment.Center },
            { "bottom", VerticalAlignment.Bottom }
        };

        internal TiledElement() { }

        internal TiledElement(XElement element)
        {
            ReadXml(element);
        }

        protected abstract void ReadXml(XElement element);

        protected List<TiledProperty> ReadProperties(XElement xProp)
        {
            var result = new List<TiledProperty>();

            if (xProp != null)
            {
                foreach (var property in xProp.Elements("property"))
                    result.Add(new TiledProperty(property));
            }

            return result;
        }
    }
}
