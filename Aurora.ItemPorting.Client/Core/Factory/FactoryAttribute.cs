using System;

namespace Aurora.ItemPorting.Client.Core
{
    /// <summary>
    /// Attribute used to mark the class that are 
    /// part of factory type. The type attribute is used to specify 
    /// which type of factory to be considered
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class FactoryTypeAttribute : Attribute
    {
        private readonly string _key;
        private readonly Type _factoryType;

        public string Key { get { return _key; } }
        public Type FactoryType { get { return _factoryType; } }

        public FactoryTypeAttribute(string key, Type factorytype)
        {
            this._key = key;
            this._factoryType = factorytype;
        }
    }
}
