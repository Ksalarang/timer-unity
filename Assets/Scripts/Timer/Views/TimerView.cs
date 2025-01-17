using UnityEngine;

namespace Timer.Views
{
    public class TimerView : MonoBehaviour
    {
        [field: SerializeField]
        public NumberFieldView HoursField { get; private set; }

        [field: SerializeField]
        public NumberFieldView MinutesField { get; private set; }

        [field: SerializeField]
        public NumberFieldView SecondsField { get; private set; }
    }
}