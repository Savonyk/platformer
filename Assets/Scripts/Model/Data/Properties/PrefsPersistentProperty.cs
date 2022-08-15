﻿namespace Scripts.Model.Data.Properties
{
    public abstract class PrefsPersistentProperty<TPropertyType> : PersistentProperty<TPropertyType>
    {
        protected string Key;

        public string GetKey => Key;

        protected PrefsPersistentProperty(TPropertyType defaultValue, string key) : base(defaultValue)
        {
            Key = key;
        }
    }
}