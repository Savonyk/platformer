using System;
using UnityEngine;
using UnityEngine.UI;
using Scripts.UI.Widgets;
using Scripts.Model;
using Scripts.Model.Def.Repository.Items;
using Scripts.Utils.Disposables;
using Scripts.Model.Def;
using Scripts.Model.Def.Localization;

namespace Scripts.UI.Windows
{
    public class ManagePerksWindow : AnimatedWindow
    {
        [SerializeField]
        private Button _useButton;
        [SerializeField]
        private Button _unuseButton;
        [SerializeField]
        private Button _buyButton;
        [SerializeField]
        private Button _closeButton;
        [SerializeField]
        private PriceItemWidget _priceInfo;
        [SerializeField]
        private Text _textInfo;
        [SerializeField]
        private Transform _perksContainer;

        private PredefinedDataGroup<PerkDefinition, PerksWidget> _dataGroup;
        private GameSession _session;
        private readonly CompositeDisposable _trash = new();

        protected override void Start()
        {
            base.Start();

            _dataGroup = new PredefinedDataGroup<PerkDefinition, PerksWidget>(_perksContainer);
            _session = FindObjectOfType<GameSession>();

            _trash.Retain(_session.PerksModel.Subscribe(OnPerksChanged));
            _trash.Retain(_buyButton.onClick.Subscribe(OnBuy));
            _trash.Retain(_closeButton.onClick.Subscribe(base.Close));
            _trash.Retain(_useButton.onClick.Subscribe(OnUse));
            _trash.Retain(_unuseButton.onClick.Subscribe(OnNonUse));

            OnPerksChanged();
        }

        private void OnPerksChanged()
        {
            _dataGroup.SetData(DefinitionFacade.Instance.Perks.GetAll);

            var selected = _session.PerksModel.InterfaceSelection.Value;

            _useButton.gameObject.SetActive(_session.PerksModel.IsUnlocked(selected));
            _useButton.interactable = !_session.PerksModel.IsUsed(selected) && _session.PerksModel.CanUse();

            _unuseButton.gameObject.SetActive(_session.PerksModel.IsUnlocked(selected));
            _unuseButton.interactable = _session.PerksModel.IsUsed(selected);

            _buyButton.gameObject.SetActive(!_session.PerksModel.IsUnlocked(selected));
            _buyButton.interactable = _session.PerksModel.CanBuy(selected);

            var definition = DefinitionFacade.Instance.Perks.GetItem(selected);
            _priceInfo.SetData(definition.Price);

            _textInfo.text = LocalizationManager.Instance.Localize(definition.Info);

        }

        private void OnBuy()
        {
            var selected = _session.PerksModel.InterfaceSelection.Value;
            _session.PerksModel.Unlock(selected);
        }

        private void OnUse()
        {
            var selected = _session.PerksModel.InterfaceSelection.Value;
            _session.PerksModel.UsePerk(selected);
        }

        private void OnNonUse()
        {
            var selected = _session.PerksModel.InterfaceSelection.Value;
            _session.PerksModel.StopUsePerk(selected);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}
