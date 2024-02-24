using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameState {
    public class GameState : MonoBehaviour
    {
        public event EventHandler<TimeSpan> TimeChanged;

        [SerializeField] 
        private float _daylength;
        [SerializeField] 
        private float _nightlength;
        private TimeSpan _currentGameTime;
        private float _minutelength => 60 * (_daylength+_nightlength) / TimeConstants.MinutesInDay;
        private TimeSpan _daylengthSpan;
        private int _day = 1;
        private bool _isDayTime = true;
        [SerializeField] private PlayerSpawner _playerSpawner;

        private void Start()
        {
            _daylengthSpan = TimeSpan.FromMinutes(60*_daylength / _minutelength);
            StartCoroutine(AddMinute());
        }

        private IEnumerator AddMinute()
        {
            _currentGameTime += TimeSpan.FromMinutes(1);
            TimeChanged?.Invoke(this, _currentGameTime);
            yield return new WaitForSeconds(_minutelength);
            StartCoroutine(AddMinute());

            if (_isDayTime == true & _currentGameTime.CompareTo(_daylengthSpan) > 0)
            {
                Debug.Log("Night");

                // Spawn Enemies
                _playerSpawner.SpawnPlayers();

                _isDayTime = !_isDayTime;
                _daylengthSpan = _daylengthSpan.Add(TimeSpan.FromDays(1));
            }
            else if (_isDayTime == false & _currentGameTime.CompareTo(TimeSpan.FromDays(_day)) > 0)
            {
                Debug.Log("Day");

                // Switch them Enemies
                foreach (var gnome in _playerSpawner.gnomes)
                {   
                    _playerSpawner.players[0].GetComponent<Gnome>().moveSpeed *= -1f;
                }

                _isDayTime = !_isDayTime;
                _day++;
            }
        }

    }
}
