using System;
using UnityEngine;
using UnityEngine.UI;
using Scripts.Model;
using Scripts.Model.Data;
using Scripts.UI.Widgets;
using Scripts.Utils.Disposables;
using Scripts.Model.Def.Player;
using Scripts.Model.Def;

namespace Scripts.UI.Windows
{
    public class PlayerStatWindow : AnimatedWindow
    {
        [SerializeField]
        private PriceItemWidget _price;
        [SerializeField]
        private Button _updateButton;
        [SerializeField]
        private Button _closeButton;
        [SerializeField]
        private Transform _container;
        [SerializeField]
        private StatItemWidget _prefab;

        private readonly CompositeDisposable _trash = new();
        private GameSession _session;
        private DataGroup<StatDefinition, StatItemWidget> _dataGroup;


        protected override void Start()
        {
            base.Start();

            _dataGroup = new DataGroup<StatDefinition, StatItemWidget>(_prefab, _container);

            _session = FindObjectOfType<GameSession>();
            _session.StatsModel.InterfaceSelectedStat.Value = DefinitionFacade.Instance.Player.Stats[0].Id;

            _trash.Retain(_session.StatsModel.Subscribe(OnStatsChanged));
            _trash.Retain(_updateButton.onClick.Subscribe(UpdateStat));
            _trash.Retain(_closeButton.onClick.Subscribe(base.Close));

            OnStatsChanged();
        }

        private void OnStatsChanged()
        {
            var stats = DefinitionFacade.Instance.Player.Stats;
            _dataGroup.SetData(stats);

            var statModel = _session.StatsModel;
            var selected = statModel.InterfaceSelectedStat.Value;
            var nextLevel = statModel.GetLevel(selected) + 1;
            var definition = statModel.GetLevelDefinition(selected, nextLevel);

            _price.SetData(definition.Price);
            var isPriceVisible = definition.Price.Count 
                <= _session.Data.Inventory.ItemCount(definition.Price.ItemId);

            if(nextLevel > DefinitionFacade.Instance.Player.Stats.Length || isPriceVisible)
            {
                _price.Icon.SetActive(false);
                _price.PriceValue.SetActive(false);
            } 

            _updateButton.gameObject.SetActive(!statModel.IsMaximalLevel(selected));
            _updateButton.interactable = statModel.CanLevelUp(selected);
        }

        private void UpdateStat()
        {
            var selected = _session.StatsModel.InterfaceSelectedStat.Value;
            _session.StatsModel.LevelUp(selected);
        }


        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}
