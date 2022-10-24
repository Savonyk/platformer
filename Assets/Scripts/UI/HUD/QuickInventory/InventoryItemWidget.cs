using UnityEngine;
using UnityEngine.UI;
using Scripts.Utils.Disposables;
using Scripts.Model;
using Scripts.Model.Data;
using Scripts.Model.Def;
using Scripts.UI.Widgets;
using Scripts.Model.Def.Repository;

namespace Scripts.UI.HUD.QuickInventory
{
    public class InventoryItemWidget : MonoBehaviour, IItemRenderer<InventoryItemData>
    {
        [SerializeField] 
        private Image _icon;
        [SerializeField] 
        private GameObject _selection;
        [SerializeField] 
        private Text _value;

        private readonly CompositeDisposable _trash = new();
        private int _index;

        private void Start()
        {
            var session = FindObjectOfType<GameSession>();
            _trash.Retain(session.QuickInventory.SelectedIndex.SubscribeAndInvoke(OnIndexChanged));
        }

        private void OnIndexChanged(int newValue, int _)
        {
            _selection.SetActive(_index == newValue);
        }

        public void SetData(InventoryItemData item, int index)
        {
            _index = index;
            var definition = DefinitionFacade.Instance.Items.GetItem(item.Id);
            _icon.sprite = definition.Icon;
            _value.text = definition.HasTag(ItemTagDefinition.Stackable) ? item.Value.ToString() : string.Empty;
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}
