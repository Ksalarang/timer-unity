using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utils;

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

        [SerializeField]
        private Vector3 _stopButtonStartPosition;

        [SerializeField]
        private Vector3 _stopButtonEndPosition;

        [SerializeField]
        private float _stopButtonDuration;

        public void SetPlayButtonState(bool playing)
        {
            PlayButton.image.sprite = playing ? PauseButtonSprite : PlayButtonSprite;
        }

        public async UniTask ShowStopButtonAsync(CancellationToken token)
        {
            SetButtonsInteractable(false);
            StopButton.gameObject.SetActive(true);
            StopButton.image.SetAlpha(0f);
            StopButton.image.DOFade(1f, _stopButtonDuration).WithCancellation(token).Forget();
            await StopButton.transform.DOLocalMove(_stopButtonEndPosition, _stopButtonDuration).WithCancellation(token);

            SetButtonsInteractable(true);
        }

        public async UniTask HideStopButtonAsync(CancellationToken token)
        {
            SetButtonsInteractable(false);
            StopButton.image.SetAlpha(1f);
            StopButton.image.DOFade(0f, _stopButtonDuration).WithCancellation(token).Forget();
            await StopButton.transform.DOLocalMove(_stopButtonStartPosition, _stopButtonDuration)
                .WithCancellation(token);

            StopButton.gameObject.SetActive(false);
            SetButtonsInteractable(true);
        }

        private void SetButtonsInteractable(bool interactable)
        {
            PlayButton.interactable = interactable;
            StopButton.interactable = interactable;
        }
    }
}