using UnityEngine;
using Scripts.UI.Widgets;
using UnityEngine.UI;
using Scripts.Model.Def;

namespace Scripts.UI.HUD.ActivePerks
{
    public class ActivePerksWidget : MonoBehaviour, IItemRenderer<string>
    {
        [SerializeField]
        private Image _icon;

        public void SetData(string id, int index)
        {
            var definition = DefinitionFacade.Instance.Perks.GetItem(id);
            _icon.sprite = definition.Icon;
        }
    }
}
