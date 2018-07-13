using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Myst.Tiled
{
    public class TiledData : TiledElement
    {
        /// <summary>
        /// Gets the type of encoding used to encode the tile layer data.
        /// </summary>
        public EncodingType Encoding { get; private set; } = EncodingType.Xml;

        /// <summary>
        /// Gets the type of compression used to compress the tile layer data.
        /// </summary>
        public CompressionType? Compression { get; private set; } = null;

        /// <summary>
        /// Gets the tiles stored in this data segment.
        /// </summary>
        public List<TiledDataTile> Tiles { get; private set; } = new List<TiledDataTile>();

        /// <summary>
        /// Gets the chunks stored in this data segment.
        /// </summary>
        public List<TiledChunk> Chunks { get; private set; } = new List<TiledChunk>();

        /// <summary>
        /// Determines if there are any tiles.
        /// </summary>
        public bool HasTiles => Tiles.Count != 0;

        /// <summary>
        /// Determines if there are any chunks.
        /// </summary>
        public bool HasChunks => Chunks.Count != 0;

        internal TiledData(XElement element)
            : base(element)
        {
        }

        protected override void ReadXml(XElement element)
        {
            var encoding = element.Attribute("encoding");
            if (encoding != null)
                Encoding = EncodingParser[encoding.Value];

            var compression = element.Attribute("compression");
            if (compression != null)
                Compression = CompressionParser[compression.Value];

            switch (Encoding)
            {
                case EncodingType.Xml:
                    foreach (var xChunk in element.Elements("chunk"))
                    {
                        var chunk = DecodeChunk(xChunk);
                        var tiles = new List<TiledDataTile>();
                        foreach (var xTile in xChunk.Elements("tile"))
                        {
                            Tiles.Add(new TiledDataTile(xTile));
                        }
                        chunk.Tiles = tiles;
                    }
                    foreach (var xTile in element.Elements("tile"))
                    {
                        Tiles.Add(new TiledDataTile(xTile));
                    }
                    break;
                case EncodingType.Csv:
                    foreach (var xChunk in element.Elements("chunk"))
                    {
                        var chunk = DecodeChunk(xChunk);
                        chunk.Tiles = DecodeCsv(xChunk.Value);
                        Chunks.Add(chunk);
                    }
                    if (element.Value != null)
                        Tiles = DecodeCsv(element.Value);
                    break;
                case EncodingType.Base64:
                    foreach (var xChunk in element.Elements("chunk"))
                    {
                        var chunk = DecodeChunk(xChunk);
                        if (xChunk.Value != null)
                            chunk.Tiles = DecodeBase64(xChunk.Value);
                        else
                            chunk.Tiles = new List<TiledDataTile>();
                        Chunks.Add(chunk);
                    }
                    break;
                default:
                    throw new InvalidOperationException("Invalid encoding type.");
            }
        }

        private TiledChunk DecodeChunk(XElement chunk)
        {
            var x = (int)chunk.Attribute("x");
            var y = (int)chunk.Attribute("y");
            var width = (int)chunk.Attribute("width");
            var height = (int)chunk.Attribute("height");
            return new TiledChunk(x, y, width, height);
        }

        private List<TiledDataTile> DecodeCsv(string data)
        {
            var result = new List<TiledDataTile>();
            string current = "";
            foreach(var chr in data)
            {
                if (char.IsDigit(chr))
                    current += chr;
                else if(current != "")
                {
                    result.Add(new TiledDataTile(uint.Parse(current)));
                    current = "";
                }
            }

            if (current != "")
                result.Add(new TiledDataTile(uint.Parse(current)));

            return result;

            // Dirty Linq 1-liner
            // O(n^2)
            //return new List<DataTile>(data.Replace("\n", "").Replace("\r", "").Split(',').Select(s => new DataTile(int.Parse(s))));
        }

        private List<TiledDataTile> DecodeBase64(string input)
        {
            var raw = Convert.FromBase64String(input);
            using(var ms = new MemoryStream(raw))
            {
                if (Compression.HasValue)
                {
                    switch(Compression)
                    {
                        case CompressionType.GZip:
                            using(var gzip = new GZipInputStream(ms))
                            {
                                return DecodeStream(gzip);
                            }
                        case CompressionType.ZLib:
                            using (var zlib = new InflaterInputStream(ms))
                            {
                                return DecodeStream(zlib);
                            }
                        default:
                            throw new InvalidOperationException("Invalid compression type.");
                    }
                }
                else
                {
                    return DecodeStream(ms);
                }
            }
        }

        private List<TiledDataTile> DecodeStream(Stream stream)
        {
            List<TiledDataTile> result = new List<TiledDataTile>();
            byte[] buffer = new byte[4];
            int count;

            while((count = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                // tmx DataTiles are stored using little endian byte ordering.
                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(buffer);

                //Use BitConverter to retrieve an int value from the buffer.
                result.Add(new TiledDataTile(BitConverter.ToUInt32(buffer, 0)));
            }

            return result;
        }
    }
}
