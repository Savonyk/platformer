using Scripts.Model.Data;
using System;
using Scripts.Model.Def.Player;
using Scripts.Model.Data.Properties;
using Scripts.Utils.Disposables;
using Scripts.Model.Def;
using Scripts.Model.Def.Repository.Items;

namespace Scripts.Model
{
    public class StatsModel : IDisposable
    {
        private PlayerData _data;
        private readonly CompositeDisposable _trash = new();
        public ObservableProperty<StatId> InterfaceSelectedStat = new();
        public event Action OnChanged;
        public event Action<StatId> OnUpgraded;

        public StatsModel(PlayerData data)
        {
            _data = data;

            _trash.Retain(InterfaceSelectedStat.Subscribe((x, y) => OnChanged?.Invoke()));
        }

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        public void LevelUp(StatId id)
        {
            if (!CanLevelUp(id)) return;

            var price = GetCurrentPrice(id);

            _data.Inventory.Remove(price.ItemId, price.Count);
            _data.Levels.LevelUp(id);

            OnChanged?.Invoke();
            OnUpgraded?.Invoke(id);
        }

        public bool CanLevelUp(StatId id)
        {
            if(IsMaximalLevel(id)) return false;

            if (!_data.Inventory.IsEnoughResources(GetCurrentPrice(id))) return false;

            return true;
        }

        public bool IsMaximalLevel(StatId id)
        {
            var statDef = DefinitionFacade.Instance.Player.GetStat(id);
            var nextLevel = GetLevel(id) + 1;

            if (statDef.Levels.Length > nextLevel) return false;

            return true;
        }

        private ItemWithCount GetCurrentPrice(StatId id)
        {
            var statDef = DefinitionFacade.Instance.Player.GetStat(id);
            var nextLevel = GetLevel(id) + 1;

            return statDef.Levels[nextLevel].Price;
        }

        public StatLevelDef GetLevelDefinition(StatId id, int level = -1)
        {
            if(level == -1)
            {
                level = GetLevel(id);
            }
            var statDef = DefinitionFacade.Instance.Player.GetStat(id);

            if(statDef.Levels.Length > level) return statDef.Levels[level];

            return default;
        }

        public float GetValue(StatId id, int level = -1)
        {

            return GetLevelDefinition(id, level).Value;
        }

        public int GetLevel(StatId id)
        {
            return _data.Levels.GetLevel(id);
        }

        public void Dispose()
        {
            _trash.Dispose();
        }
        }
    }
