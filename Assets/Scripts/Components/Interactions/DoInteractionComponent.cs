using UnityEngine;

namespace Scripts.Components.Interactions
{
    public class DoInteractionComponent : MonoBehaviour
    {
        public void DoInteraction(GameObject interactableObject)
        {
            var interactableComponent = interactableObject.GetComponent<InteractableComponent>();
            if(interactableComponent != null)
            {
                interactableComponent.Interact();
            }
        }
    }
}
