using System.Xml.Linq;

namespace Myst.Tiled
{
    public class TiledTemplate : TiledElement
    {
        /// <summary>
        /// Gets the path to the file represented by the template.
        /// </summary>
        public string File { get; private set; }

        /// <summary>
        /// Gets the tileset referenced by the template.
        /// </summary>
        public TiledTileset ExternalTileset { get; private set; } = null;

        /// <summary>
        /// Gets the object defined by the template.
        /// </summary>
        public TiledObject TemplateObject { get; private set; }

        /// <summary>
        /// Loads a template from a template file.
        /// </summary>
        /// <param name="file">The path to the template file to load.</param>
        public TiledTemplate(string file) 
            : base(XDocument.Load(file, LoadOptions.SetBaseUri).Element("template"))
        {
            File = file;
        }

        internal TiledTemplate(XElement element)
            : base(element)
        {
        }

        protected override void ReadXml(XElement xTemplate)
        {
            var tileset = xTemplate.Element("tileset");
            if (tileset != null)
                ExternalTileset = new TiledTileset(tileset);

            TemplateObject = new TiledObject(xTemplate.Element("object"));
        }
    }
}
