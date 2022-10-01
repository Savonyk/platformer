using System;
using UnityEngine;

namespace Scripts.Model.Data
{
    [Serializable]
    public class DialogData
    {
        [SerializeField] 
        private string[] _phrases;

        public string[] Phrases => _phrases;
    }
}
