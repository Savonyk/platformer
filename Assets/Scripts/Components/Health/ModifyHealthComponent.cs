using UnityEngine;


namespace Scripts.Components.Health
{
    public class ModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] private int _healthDelta;


        public void ChangeHealth(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();

            if (healthComponent != null)
            {

                healthComponent.ModifyHealth(_healthDelta);
            }
        }

    }

}