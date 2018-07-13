using System.Collections;
using System.Collections.Generic;

namespace Myst.Tiled
{
    /// <summary>
    /// Simple 2 way dictionary used to map enum names with their values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class Parser<T> : IEnumerable<T> where T : struct
    {
        private Dictionary<string, T> _to = new Dictionary<string, T>();
        private Dictionary<T, string> _from = new Dictionary<T, string>();

        /// <summary>
        /// Gets an enum value given its name.
        /// </summary>
        /// <param name="name">The name of the enum value.</param>
        /// <returns></returns>
        public T this[string name] => _to[name];

        /// <summary>
        /// Gets an enum name given its value.
        /// </summary>
        /// <param name="value">The value of the enum to get the name of.</param>
        /// <returns></returns>
        public string this[T value] => _from[value];

       /// <summary>
       /// Maps an enum value to its name.
       /// </summary>
       /// <param name="name">The name of the enum value.</param>
       /// <param name="value">The enum value.</param>
        public void Add(string name, T value)
        {
            _to.Add(name, value);
            _from.Add(value, name);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _from.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
