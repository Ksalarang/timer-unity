using TMPro;
using UnityEngine;

namespace Timer.Views
{
    public class NumberFieldView : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField _inputField;

        private void Start()
        {
            _inputField.onValueChanged.AddListener(OnTextChanged);
        }

        private void OnDestroy()
        {
            _inputField.onValueChanged.RemoveListener(OnTextChanged);
        }

        private void OnTextChanged(string newText)
        {

        }
    }
}