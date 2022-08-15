using UnityEditor;
using UnityEditor.UI;

namespace Scripts.UI.Widgets.Editor
{
    [CustomEditor(typeof(CustomButton), true)]
    [CanEditMultipleObjects]
    public class CustomButtonEditor : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            FindCustomButtonPropertyField("_normal");
            FindCustomButtonPropertyField("_pressed");
            serializedObject.ApplyModifiedProperties();

            base.OnInspectorGUI();
        }

        private void FindCustomButtonPropertyField(string propertyPath)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(propertyPath));
        }
    }
}
