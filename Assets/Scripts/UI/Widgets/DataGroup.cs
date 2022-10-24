using System.Collections.Generic;
using UnityEngine;

namespace Scripts.UI.Widgets
{
    public class DataGroup<TDataType, TItemType> where TItemType : MonoBehaviour, IItemRenderer<TDataType>
    {
        protected readonly List<TItemType> CreatedItems = new();
        private readonly TItemType _prefab;
        private readonly Transform _container;

        public DataGroup(TItemType prefab, Transform container)
        {
            _prefab = prefab;
            _container = container;
        }

        public virtual void SetData(IList<TDataType> data)
        {
            CreateRequiredItems(data);

            UpdateAndActivateData(data);

            HideUnusedItems(data);
        }

        internal void SetData(object compositeData)
        {
            throw new System.NotImplementedException();
        }

        private void CreateRequiredItems(IList<TDataType> data)
        {
            for (int i = CreatedItems.Count; i < data.Count; i++)
            {
                var item = Object.Instantiate(_prefab, _container);
                CreatedItems.Add(item);
            }
        }

        private void UpdateAndActivateData(IList<TDataType> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                CreatedItems[i].SetData(data[i], i);
                CreatedItems[i].gameObject.SetActive(true);
            }
        }

        private void HideUnusedItems(IList<TDataType> data)
        {
            for (int i = data.Count; i < CreatedItems.Count; i++)
            {
                CreatedItems[i].gameObject.SetActive(false);
            }
        }
    }

    public interface IItemRenderer<TDataType>
    {
        void SetData(TDataType type, int index);
    } 
}
