using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour {
    public Text Text;

    private Dictionary<int, string> CachedNumberStrings = new();
    private int[] _frameRateSamples;
    private int _cacheNumbersAmount = 600;
    private int _averageFromAmount = 30;
    private int _averageCounter = 0;
    private int _currentAveraged;

    private int[] _frameIntervals = {600, 500, 400, 300, 200, 100, 60, 0 };
    private int _currentInterval = 0;
    private int _isLowerCount = 0;
    private int _pauseThreshold = 100;

    void Awake() {
        // Cache strings and create array
        {
            for (int i = 0; i < _cacheNumbersAmount; i++) {
                CachedNumberStrings[i] = i.ToString();
            }
            _frameRateSamples = new int[_averageFromAmount];
        }

        Application.targetFrameRate = 600;
    }
    void Update() {
        TestIntervals();
        // Sample
        {
            var currentFrame = (int)Math.Round(1f / Time.smoothDeltaTime); // If your game modifies Time.timeScale, use unscaledDeltaTime and smooth manually (or not).
            _frameRateSamples[_averageCounter] = currentFrame;
        }

        // Average
        {
            var average = 0f;

            foreach (var frameRate in _frameRateSamples) {
                average += frameRate;
            }

            _currentAveraged = (int)Math.Round(average / _averageFromAmount);
            _averageCounter = (_averageCounter + 1) % _averageFromAmount;
        }

        // Assign to UI
        {
            Text.text = _currentAveraged switch {
                var x when x >= 0 && x < _cacheNumbersAmount => CachedNumberStrings[x],
                var x when x >= _cacheNumbersAmount => $"> {_cacheNumbersAmount}",
                var x when x < 0 => "< 0",
                _ => "?"
            };
        }
    }

    private void TestIntervals() {
        if (_currentAveraged < _frameIntervals[_currentInterval] && _currentAveraged > 0) {
            _isLowerCount++;
            if (_isLowerCount < _pauseThreshold) return;
            PauseManager.Instance.PauseGame();
            _currentInterval++;
            _isLowerCount = 0;
        } else { _isLowerCount = 0; }
    }
}