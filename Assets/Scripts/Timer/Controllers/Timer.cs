using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Timer.Controllers
{
    public class Timer : IDisposable
    {
        public event Action SecondElapsed;
        public event Action Elapsed;
        public event Action Stopped;

        public TimerState State => _state;
        public int TimeLeftMillis => _interval;

        private TimerState _state = TimerState.Idle;
        private int _interval;
        private CancellationTokenSource _tokenSource;

        public void Start(int intervalMillis)
        {
            if (_state != TimerState.Idle)
            {
                return;
            }

            _state = TimerState.Running;
            _interval = intervalMillis;

            if (_tokenSource != null)
            {
                _tokenSource.Cancel();
                _tokenSource.Dispose();
            }

            _tokenSource = new CancellationTokenSource();
            StartAsync(_tokenSource.Token).Forget();
        }

        public void Dispose()
        {
            if (_tokenSource != null)
            {
                _tokenSource.Cancel();
                _tokenSource.Dispose();
                _tokenSource = null;
            }
        }

        private async UniTask StartAsync(CancellationToken token)
        {
            while (_state == TimerState.Running && token.IsCancellationRequested == false)
            {
                await UniTask.Delay(TimeConstants.MillisInSecond, cancellationToken: token);

                if (_state != TimerState.Running || token.IsCancellationRequested)
                {
                    OnStopped();
                    break;
                }

                OnSecondElapsed();
            }
        }

        private void OnSecondElapsed()
        {
            _interval -= TimeConstants.MillisInSecond;
            SecondElapsed?.Invoke();

            if (_interval <= 0)
            {
                _state = TimerState.Idle;
                _interval = 0;
                Elapsed?.Invoke();
            }
        }

        private void OnStopped()
        {
            _state = TimerState.Idle;
            _interval = 0;
            Stopped?.Invoke();
        }
    }

    public enum TimerState
    {
        Idle,
        Running,
        Paused,
    }
}