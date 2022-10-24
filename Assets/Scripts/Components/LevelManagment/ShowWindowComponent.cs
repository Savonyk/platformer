using UnityEngine;
using Scripts.Utils;

namespace Scripts.Components.LevelManagment
{
    class ShowWindowComponent : MonoBehaviour
    {
        [SerializeField]
        private string _path = "UI/ManagePerksWindow";

        public void Show()
        {
            OpenWindowUtils.CreateWindow(_path);
        }
    }
}
