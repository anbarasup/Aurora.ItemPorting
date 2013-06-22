using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aurora.ItemPorting.Client.Core
{
    /// <summary>
    /// Concreate implementation of FactoryElement
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FactoryElement<T> : IFactoryElement where T : new()
    {
        /// <summary>
        /// Function creates the concreate object
        /// </summary>
        /// <returns></returns>
        public object New()
        {
            return new T();
        }
    }
}
