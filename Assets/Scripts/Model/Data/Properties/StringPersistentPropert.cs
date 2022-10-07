using UnityEngine;
using System;

namespace Scripts.Model.Data.Properties
{
    [Serializable]
    public class StringPersistentPropert : PrefsPersistentProperty<string>
    {
        public StringPersistentPropert(string defaultValue, string key) : base(defaultValue, key)
        {
            Init();
        }

        protected override string Read(string defaultValue)
        {
            return PlayerPrefs.GetString(Key, defaultValue);
        }

        protected override void Write(string value)
        {
            PlayerPrefs.SetString(Key, value);
            PlayerPrefs.Save();
        }
    }
}
