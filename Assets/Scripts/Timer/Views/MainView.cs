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
    }
}