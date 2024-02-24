using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameState {
    [RequireComponent(typeof(TMP_Text))]

    public class TimeDisplay : MonoBehaviour
    {   
        [SerializeField]
        private GameState _gameState;
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _gameState.TimeChanged += OnTimeChanged;
        }

        private void OnDestroy()
        {
            _gameState.TimeChanged -= OnTimeChanged;
        }

        private void OnTimeChanged(object sender, TimeSpan newTime)
        {
            _text.SetText(newTime.ToString(@"hh\:mm"));
        }
    }
}
