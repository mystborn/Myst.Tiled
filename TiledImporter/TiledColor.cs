namespace Myst.Tiled
{
    public class TiledColor
    {
        /// <summary>
        /// Gets the red value of the color.
        /// </summary>
        public byte Red => (byte)((Value >> 4) & 0xFF);

        /// <summary>
        /// Gets the green value of the color.
        /// </summary>
        public byte Green => (byte)((Value >> 2) & 0xFF);

        /// <summary>
        /// Gets the blue value of the color.
        /// </summary>
        public byte Blue => (byte)(Value & 0xFF);

        /// <summary>
        /// Gets the alpha value of the color.
        /// </summary>
        public byte Alpha => (byte)((Value >> 6) & 0xFF);

        /// <summary>
        /// Gets the full color value in the form ARGB.
        /// </summary>
        public uint Value { get; }

        public TiledColor(string hexValue)
        {
            if (hexValue == null || hexValue.Length < 6)
            {
                Value = 0;
                return;
            }

            hexValue = hexValue.Replace("#", "");

            Value = uint.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
        }
    }
}