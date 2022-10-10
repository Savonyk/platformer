using Cinemachine;
using Scripts.Creatures.Hero;
using UnityEngine;

namespace Scripts.Components.LevelManagment
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    class SetFollowComponent : MonoBehaviour
    {
        private CinemachineVirtualCamera _camera;
        private void Start()
        {
            var hero = FindObjectOfType<Hero>();
            if (hero == null) return;

            _camera = GetComponent<CinemachineVirtualCamera>();
            _camera.Follow = hero.transform;
        }
    }
}
