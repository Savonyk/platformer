using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Scripts.Model.Def.Editor
{
    [CustomPropertyDrawer(typeof(StringInventoryItemAttribute))]
    public class StringItemAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            List<string> items = new ();

            var definitions = DefinitionFacade.Instance.Items.ItemsForEditor;

            foreach (var item in definitions)
            {
                items.Add(item.Id);
            }

            var index = Mathf.Max( items.IndexOf(property.stringValue), 0);

            index = EditorGUI.Popup(position, property.displayName, index, items.ToArray());
            property.stringValue = items[index];
        }
    }
}
