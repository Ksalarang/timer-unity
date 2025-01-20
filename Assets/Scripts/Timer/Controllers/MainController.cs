using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Timer.Views;
using UnityEngine;

namespace Timer.Controllers
{
    public class MainController : MonoBehaviour
    {
        [SerializeField]
        private MainView _view;

        private readonly Timer _timer = new();
        private readonly CancellationTokenSource _tokenSource = new();

        private TimeSpan _currentTimeSpan;

        private void Start()
        {
            _view.PlayButton.onClick.AddListener(OnPlayClick);
            _view.StopButton.onClick.AddListener(OnStopClick);

            _timer.SecondElapsed += OnSecondElapsed;
            _timer.Elapsed += OnTimerElapsed;
        }

        private void OnDestroy()
        {
            _view.PlayButton.onClick.RemoveListener(OnPlayClick);
            _view.StopButton.onClick.RemoveListener(OnStopClick);

            _timer.SecondElapsed -= OnSecondElapsed;
            _timer.Elapsed -= OnTimerElapsed;
            _timer.Dispose();

            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }

        private void OnPlayClick()
        {
            if (_timer.State == TimerState.Idle)
            {
                var timerView = _view.TimerView;
                _currentTimeSpan = new TimeSpan(timerView.HoursField.Value, timerView.MinutesField.Value,
                    timerView.SecondsField.Value);

                _timer.Start((int) _currentTimeSpan.TotalMilliseconds);
                _view.SetPlayButtonState(true);
                _view.ShowStopButtonAsync(_tokenSource.Token).Forget();
            }
        }

        private void OnSecondElapsed()
        {
            var timeSpan = TimeSpan.FromMilliseconds(_timer.TimeLeftMillis);

            _view.TimerView.HoursField.Value = timeSpan.Hours;
            _view.TimerView.MinutesField.Value = timeSpan.Minutes;
            _view.TimerView.SecondsField.Value = timeSpan.Seconds;
        }

        private void OnTimerElapsed()
        {
            _view.SetPlayButtonState(false);
            _view.TimerView.HoursField.Value = _currentTimeSpan.Hours;
            _view.TimerView.MinutesField.Value = _currentTimeSpan.Minutes;
            _view.TimerView.SecondsField.Value = _currentTimeSpan.Seconds;
        }

        private void OnStopClick()
        {
            _view.HideStopButtonAsync(_tokenSource.Token).Forget();
        }
    }
}