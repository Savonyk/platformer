using Scripts.Model.Def;
using Scripts.Model.Def.Repository.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Widgets
{
    public class PriceItemWidget : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private Text _priceValue;

        public GameObject Icon => _icon.gameObject;
        public GameObject PriceValue => _priceValue.gameObject;

        public void SetData(ItemWithCount item)
        {
            var definition = DefinitionFacade.Instance.Items.GetItem(item.ItemId);
            _icon.sprite = definition.Icon;
            _priceValue.text = item.Count.ToString();
            
        }
    }
}
