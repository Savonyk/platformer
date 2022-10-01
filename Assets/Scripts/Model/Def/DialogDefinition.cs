using UnityEngine;
using Scripts.Model.Data;

namespace Scripts.Model.Def
{
    [CreateAssetMenu(fileName = "DialogDefinition", menuName = "Definition/DialogDefinition")]
    public class DialogDefinition : ScriptableObject
    {
        [SerializeField]
        private DialogData _phrases;

        public DialogData Data => _phrases;
    }
}
