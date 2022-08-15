using Scripts.Utils.Disposables;
using System;
using UnityEngine;

namespace Scripts.Model.Data.Properties
{
    [Serializable]
    public class ObservableProperty<TPropertyType>
    {
        [SerializeField] protected TPropertyType _value;

        public delegate void OnPropertyChanged(TPropertyType newValue, TPropertyType oldValue);
        public event OnPropertyChanged OnChanged;

        public IDisposable Subscribe(OnPropertyChanged call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        public virtual TPropertyType Value
        {
            get => _value;

            set
            {
                var isEqual = _value.Equals(value);
                if (isEqual) return;

                var oldValue = _value;
                _value = value;
                InvokeChangeedEvent(_value, oldValue);
            }
        }

        protected void InvokeChangeedEvent(TPropertyType newValue, TPropertyType oldValue)
        {
            OnChanged?.Invoke(newValue, oldValue);
        }
    }
}
