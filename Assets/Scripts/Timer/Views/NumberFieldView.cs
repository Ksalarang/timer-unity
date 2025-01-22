using System;
using System.Text.RegularExpressions;
using Timer.Controllers;
using TMPro;
using UnityEngine;

namespace Timer.Views
{
    public class NumberFieldView : MonoBehaviour
    {
        public int Value
        {
            get => int.Parse(_inputField.text);
            set
            {
                var text = value < 10 ? $"0{value}" : value.ToString();
                _inputField.text = text;
            }
        }

        [SerializeField]
        private TimeFieldType _type;

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

        public void SetInteractable(bool interactable)
        {
            _inputField.interactable = interactable;
        }

        private void OnTextChanged(string text)
        {
            if (text.Length == 0 || Regex.IsMatch(text, @"^0\d$"))
            {
                return;
            }

            var textValue = text.Replace("-", "");
            var value = int.Parse(textValue);

            switch (_type)
            {
                case TimeFieldType.Hour:
                    break;
                case TimeFieldType.Minute:
                    value = Mathf.Min(value, TimeConstants.MinutesInHour - 1);
                    break;
                case TimeFieldType.Second:
                    value = Mathf.Min(value, TimeConstants.SecondsInMinute - 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_type), $"Case {_type} is not defined");
            }

            _inputField.SetTextWithoutNotify(value.ToString());
        }

        private void OnTextDeselected(string text)
        {
            AddLeadingZero(text);
        }

        private void AddLeadingZero(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                _inputField.SetTextWithoutNotify("00");
            }
            else if (text.Length == 1)
            {
                _inputField.SetTextWithoutNotify($"0{text}");
            }
        }
    }

    public enum TimeFieldType
    {
        Hour,
        Minute,
        Second,
    }
}