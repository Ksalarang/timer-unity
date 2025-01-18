using UnityEngine;
using UnityEngine.UI;

namespace Timer.Views
{
    public class MainView : MonoBehaviour
    {
        [field: SerializeField]
        public TimerView TimerView { get; private set; }

        [field: SerializeField]
        public Button PlayButton { get; private set; }

        [field: SerializeField]
        public Button StopButton { get; private set; }

        [SerializeField]
        private Sprite PlayButtonSprite;

        [SerializeField]
        private Sprite PauseButtonSprite;

        public void SetPlayButtonState(bool playing)
        {
            PlayButton.image.sprite = playing ? PauseButtonSprite : PlayButtonSprite;
        }
    }
}