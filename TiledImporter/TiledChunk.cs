using System.Collections.Generic;
using System.Linq;

namespace Myst.Tiled
{
    public class TiledChunk
    {
        /// <summary>
        /// Gets the x coordinate of the chunk in tiles.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Gets the y coordinate of the chunk in tiles.
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// Gets the width of the chunk in tiles.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the height of the chunk in tiles.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Gets the tiles stored in the chunk.
        /// </summary>
        public List<TiledDataTile> Tiles { get; internal set; } = new List<TiledDataTile>();

        internal TiledChunk(int x, int y, int width, int height)
        {
            InitPositionData(x, y, width, height);
            Tiles = null;
        }

        internal TiledChunk(int x, int y, int width, int height, IEnumerable<int> tileIds)
            : this(x, y, width, height, tileIds.Select(i => new TiledDataTile((uint)i)))
        {
        }

        internal TiledChunk(int x, int y, int width, int height, IEnumerable<TiledDataTile> tiles)
        {
            InitPositionData(x, y, width, height);
            Tiles = new List<TiledDataTile>(tiles);
        }
        
        private void InitPositionData(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
