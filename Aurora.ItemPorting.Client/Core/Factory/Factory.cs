using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Aurora.ItemPorting.Client.Core
{
    public class Factory<K, T> : IFactory<K, T> where K : IComparable
    {
        /// Elements that can be created
        readonly Dictionary<K, IFactoryElement> _elements = new Dictionary<K, IFactoryElement>();

        /// <summary>
        ///  Add a new creatable kind of object to the factory. 
        /// Here is the key with the beauty of the constrains in generics. 
        /// Look that we are saying that V should be derived of T and it must be creatable
        /// </summary>
        public void Add<V>(K key) where V : T, new()
        {
            _elements.Add(key, new FactoryElement<V>());
        }

        public T Create(K key)
        {
            if (_elements.ContainsKey(key))
            {
                return (T)_elements[key].New();
            }
            throw new ArgumentException();
        }
    }
}
