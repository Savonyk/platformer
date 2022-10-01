using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Widgets
{
    public class ProgressBarWidget : MonoBehaviour
    {
        [SerializeField] 
        private Image _progressBar;

        public void SetProgress(float progress)
        {
            _progressBar.fillAmount = progress;
        }
    }
}
