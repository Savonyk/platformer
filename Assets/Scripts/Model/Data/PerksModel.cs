using System;
using UnityEngine;
using Scripts.Model.Def;
using Scripts.Utils.Disposables;
using Scripts.Model.Def.Repository;
using Scripts.Model.Data.Properties;
using Scripts.Model.Def.Repository.Items;
using System.Collections.Generic;

namespace Scripts.Model.Data
{
    public class PerksModel : IDisposable
    {
        private readonly PlayerData _data;
        private readonly CompositeDisposable _trash = new ();

        public readonly StringObservableProperty InterfaceSelection = new StringObservableProperty();
        public event Action OnChanged;

        public List<string> Used => _data.Perks.UsingPerks;

        public bool IsSuperThrowSuported => _data.Perks.IsUnlocked("superThrow");

        public PerksModel(PlayerData data)
        {
            _data = data;
           InterfaceSelection.Value = DefinitionFacade.Instance.Perks.GetAll[0].Id;

            _trash.Retain(InterfaceSelection.Subscribe((x, y) => OnChanged?.Invoke()));
        }

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable( () => OnChanged -= call);
        }

        public void Unlock(string id)
        {
            var itemDefinition = DefinitionFacade.Instance.Perks.GetItem(id);

            var isEnough = _data.Inventory.IsEnoughResources(itemDefinition.Price);
            if (isEnough)
            {
                _data.Inventory.Remove(itemDefinition.Price.ItemId, itemDefinition.Price.Count);
                _data.Perks.UnlockPerk(id);
                OnChanged?.Invoke();
            }
        }

        public void UsePerk(string selected)
        {
            _data.Perks.UsePerk(selected);
            OnChanged?.Invoke();
        }

        internal void StopUsePerk(string selected)
        {
            _data.Perks.StopUsePerk(selected);
            OnChanged?.Invoke();
        }

        public bool IsUsed(string perkId)
        {
            return _data.Perks.IsUsing(perkId);
        }

        public bool IsUnlocked(string perkId)
        {
            return _data.Perks.IsUnlocked(perkId);
        }

        public bool CanBuy(string perkId)
        {
            var itemDefinition = DefinitionFacade.Instance.Perks.GetItem(perkId);
            return _data.Inventory.IsEnoughResources(itemDefinition.Price);
        }

        public bool CanUse()
        {
            return _data.Perks.CanUse();
        }

        public void Dispose()
        {
            _trash.Dispose();
        }
    }
}
