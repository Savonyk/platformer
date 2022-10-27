using Scripts.Model;
using Scripts.Model.Def;
using Scripts.Model.Def.Localization;
using Scripts.Model.Def.Player;
using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Widgets
{
    public class StatItemWidget : MonoBehaviour, IItemRenderer<StatDefinition>
    {
        [SerializeField]
        private Text _name;
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private Text _currentValue;
        [SerializeField]
        private Text _increaseValue;
        [SerializeField]
        private ProgressBarWidget _progressBar;
        [SerializeField]
        private GameObject _selected;

        private GameSession _session;
        private StatDefinition _data;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            UpdateView();
        }

        public void SetData(StatDefinition data, int index)
        {
            _data = data;

            if (_session == null) return;

            UpdateView();
        }

        private void UpdateView()
        {
            _icon.sprite = _data.Icon;
            _name.text = LocalizationManager.Instance.Localize(_data.Name);

            var statModel = _session.StatsModel;

            _currentValue.text = statModel.GetValue(_data.Id).ToString(CultureInfo.InvariantCulture);

            var currentLevel = statModel.GetLevel(_data.Id);
            var nextLevel = currentLevel + 1;
            var increaseValue = statModel.GetValue(_data.Id, nextLevel);

            _increaseValue.text = $"+{increaseValue}";
            _increaseValue.gameObject.SetActive(increaseValue > 0);

            var maxLevels = DefinitionFacade.Instance.Player.GetStat(_data.Id).Levels.Length - 1;

            _progressBar.SetProgress(currentLevel / (float)maxLevels);
            _selected.SetActive(_session.StatsModel.InterfaceSelectedStat.Value == _data.Id);
        }

        public void OnSelect()
        {
            _session.StatsModel.InterfaceSelectedStat.Value = _data.Id;
        }
    }
}
