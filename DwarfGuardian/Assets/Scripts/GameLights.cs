using UnityEngine;
using UnityEngine.Rendering.Universal;
using System;

namespace GameState {
    [RequireComponent(typeof(UnityEngine.Rendering.Universal.Light2D))]
    public class GameLights : MonoBehaviour
    {
        [SerializeField]
        private GameState _gameState;
        [SerializeField]
        private Gradient _gradient;
        private UnityEngine.Rendering.Universal.Light2D _light;

        private void Awake()
        {
            _light = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
            _gameState.TimeChanged += OnTimeChanged;
        }

        private void OnDestroy()
        {
            _gameState.TimeChanged -= OnTimeChanged;
        }

        private void OnTimeChanged(object sender, System.TimeSpan newTime)
        {
            _light.color = _gradient.Evaluate(PercentageOfDay(newTime));
        }

        private float PercentageOfDay(TimeSpan timeSpan)
        {
            return (float) timeSpan.TotalMinutes % TimeConstants.MinutesInDay / TimeConstants.MinutesInDay;
        }

    }
}
