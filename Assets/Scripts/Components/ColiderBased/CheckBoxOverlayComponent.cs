using UnityEngine;
using UnityEditor;
using Scripts.Utils;

namespace Scripts.Components.ColiderBased
{
    public class CheckBoxOverlayComponent : CheckFigureOverlayComponent
    {
        [SerializeField] private Vector2 _size;
        [SerializeField] private Vector2 _offset;

       private Vector2 _newCenter;

        protected override void OnDrawGizmosSelected()
        {
            GetNewCenter();

            var faceColor = HandlesColors.TransparentRed;
            var outlineColor = faceColor;
            var rectangular = new Rect(_newCenter, _size);
            Handles.DrawSolidRectangleWithOutline(rectangular, faceColor, outlineColor);
        }

        protected override int GetSize()
        {
            GetNewCenter();

            return Physics2D.OverlapBoxNonAlloc(_newCenter, _size, 0f, _results);
        }

        private void GetNewCenter()
        {
            _newCenter = new Vector2(transform.position.x + _offset.x, //
                         transform.position.y + _offset.y);
        }
    }
}
