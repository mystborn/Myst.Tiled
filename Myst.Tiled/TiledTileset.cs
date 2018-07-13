using System.Collections.Generic;
using System.Xml.Linq;

namespace Myst.Tiled
{
    public class TiledTileset : TiledElement
    {
        /// <summary>
        /// Gets the first global tile ID of this set.
        /// </summary>
        public int FirstGid { get; private set; } = 0;

        /// <summary>
        /// Gets the path to the source TSX file.
        /// </summary>
        public string Source { get; private set; }

        /// <summary>
        /// Gets the name of this set.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the width of a tile in pixels.
        /// </summary>
        public int TileWidth { get; private set; }

        /// <summary>
        /// Gets the height of a tile in pixels.
        /// </summary>
        public int TileHeight { get; private set; }

        /// <summary>
        /// Gets the spacing in between tiles, in pixels.
        /// </summary>
        public int Spacing { get; private set; } = 0;

        /// <summary>
        /// Gets the margin around the tiles, in pixels.
        /// </summary>
        public int Margin { get; private set; } = 0;

        /// <summary>
        /// Gets the number of tiles in this set.
        /// </summary>
        public int TileCount { get; private set; }

        /// <summary>
        /// Gets the number of columns in this set.
        /// </summary>
        public int Columns { get; private set; }

        /// <summary>
        /// Specifies an offset in pixels to be applied when drawing tiles.
        /// </summary>
        public TiledOffset Offset { get; private set; } = TiledOffset.Zero;

        /// <summary>
        /// Determines how tile overlays for terrain and collision information are rendered.
        /// </summary>
        public TiledGrid Grid { get; private set; }

        /// <summary>
        /// Gets the custom properties assigned to the set.
        /// </summary>
        public List<TiledProperty> Properties { get; private set; }

        /// <summary>
        /// Gets the source image used by the set.
        /// </summary>
        public TiledImage Image { get; private set; }

        /// <summary>
        /// Gets the terrains defined by this set.
        /// </summary>
        public List<TiledTerrain> TerrainTypes { get; private set; }

        /// <summary>
        /// Gets the tiles defined for use in terrains.
        /// </summary>
        public List<TiledTile> Tiles { get; private set; }

        /// <summary>
        /// Gets the defined wang sets.
        /// </summary>
        public List<WangSet> WangSets { get; private set; }

        /// <summary>
        /// Loads a tileset from a tsx file.
        /// </summary>
        /// <param name="file">The location of the tsx file to be loaded.</param>
        public TiledTileset(string file)
            : base(XDocument.Load(file, LoadOptions.SetBaseUri).Element("tileset"))
        {
            Source = file;
        }

        internal TiledTileset(XElement element)
            : base(element)
        {
        }

        protected override void ReadXml(XElement xTileset)
        {
            var firstGid = xTileset.Attribute("firstgid");
            if (firstGid != null)
                FirstGid = (int)firstGid;

            Source = (string)xTileset.Attribute("source");
            if (Source != null)
            {
                Source = System.IO.Path.Combine(xTileset.BaseUri, "../", Source);
                var ts = new TiledTileset(Source);
                ReadFromSource(ts);
            }
            else
            {
                Name = (string)xTileset.Attribute("name");
                TileWidth = (int)xTileset.Attribute("tilewidth");
                TileHeight = (int)xTileset.Attribute("tileheight");

                var spacing = xTileset.Attribute("spacing");
                if (spacing != null)
                    Spacing = (int)spacing;

                var margin = xTileset.Attribute("margin");
                if (margin != null)
                    Margin = (int)margin;

                TileCount = (int)xTileset.Attribute("tilecount");
                Columns = (int)xTileset.Attribute("columns");

                var offset = xTileset.Element("tileoffset");
                if (offset != null)
                    Offset = new TiledOffset(offset);

                var grid = xTileset.Element("grid");
                if (grid != null)
                    Grid = new TiledGrid(grid);

                Properties = ReadProperties(xTileset.Element("properties"));

                var image = xTileset.Element("image");
                if (image != null)
                    Image = new TiledImage(image);

                TerrainTypes = new List<TiledTerrain>();
                var xTerrainTypes = xTileset.Element("terraintypes");
                if (xTerrainTypes != null)
                    foreach (var xTerrain in xTerrainTypes.Elements("terrain"))
                        TerrainTypes.Add(new TiledTerrain(xTerrain));

                Tiles = new List<TiledTile>();
                foreach (var xTile in xTileset.Elements("tile"))
                    Tiles.Add(new TiledTile(xTile));

                WangSets = new List<WangSet>();
                var xWangSets = xTileset.Element("wangsets");
                if (xWangSets != null)
                    foreach (var xWangSet in xWangSets.Elements("wangset"))
                        WangSets.Add(new WangSet(xWangSet));
            }
        }

        private void ReadFromSource(TiledTileset source)
        {
            if (source.FirstGid != 0)
                FirstGid = source.FirstGid;
            Name = source.Name;
            TileWidth = source.TileWidth;
            TileHeight = source.TileHeight;
            Spacing = source.Spacing;
            Margin = source.Margin;
            TileCount = source.TileCount;
            Columns = source.Columns;
            Offset = source.Offset;
            Grid = source.Grid;
            Properties = source.Properties;
            TerrainTypes = source.TerrainTypes;
            Tiles = source.Tiles;
            WangSets = source.WangSets;
        }
    }
}
