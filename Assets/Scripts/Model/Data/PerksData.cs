using System;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Model.Data.Properties;
using Scripts.Model.Def;
using Scripts.Utils.Disposables;

namespace Scripts.Model.Data
{
    [Serializable]
    public class PerksData
    {
        [SerializeField]
        private List<string> _usingPerks;
        [SerializeField]
        private List<string> _unlocked;

        private int currentIndex = 0;
        public List<string> UsingPerks => _usingPerks;

        public PerksData()
        {
            _unlocked = new List<string>();
        }

        public void UnlockPerk(string id)
        {
            if (!_unlocked.Contains(id))
                _unlocked.Add(id);
        }

        public bool IsUnlocked(string id) 
        {
            return _unlocked.Contains(id); 
        }

        public void UsePerk(string id)
        {

            if (IsUsing(id) || !CanUse()) return;

            _usingPerks.Add(id);
            currentIndex++;
        }

        public bool IsUsing(string id)
        {
            return _usingPerks.Contains(id);
        }

        public void StopUsePerk(string id)
        {
            if (!IsUsing(id)) return;

            _usingPerks.Remove(id);
            currentIndex--;
        }

        public bool CanUse()
        {
            return currentIndex < DefinitionFacade.Instance.Player.MaxPerksCount;
        }
    }
}
