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
            _inputField.onDeselect.AddListener(OnTextDeselected);
        }

        private void OnDestroy()
        {
            _inputField.onValueChanged.RemoveListener(OnTextChanged);
        }

        private void OnTextChanged(string newText)
        {

        }

        private void OnTextDeselected(string text)
        {
            FormatText();
        }

        private void FormatText()
        {
            var text = _inputField.text;

            if (string.IsNullOrEmpty(text))
            {
                _inputField.text = "00";
            }
            else if (text.Length == 1)
            {
                _inputField.text = $"0{text}";
            }
        }
    }
}