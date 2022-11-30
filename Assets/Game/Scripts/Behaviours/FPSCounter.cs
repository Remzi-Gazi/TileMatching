using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private int _fpsAccumulator = 0;
    private float _fpsNextMeasure = 0f;
    private float _fpsMeasurePeriod = 0.5f;

    private string[] _fpsCounts;
    private int _currentFps;
    
    private TMP_Text _fpsText;

    private const int _maxFps = 60;
    
    void Awake()
    {
        Application.targetFrameRate = _maxFps;
        _fpsText = GetComponent<TMP_Text>();

        _fpsCounts = new string[_maxFps+1];
        
        for(int i = 0; i < _fpsCounts.Length; i++)
        {
            _fpsCounts[i] = (i) + " FPS";
        }

        _fpsNextMeasure = Time.realtimeSinceStartup + _fpsNextMeasure;

    }

    void Update()
    {
        // measure average frames per second
        _fpsAccumulator++;
        if (Time.realtimeSinceStartup > _fpsNextMeasure)
        {
            _currentFps = (int)(_fpsAccumulator / _fpsMeasurePeriod);
            _fpsAccumulator = 0;
            _fpsNextMeasure += _fpsMeasurePeriod;

            if(_currentFps > _maxFps)
            {
                _fpsText.text = _fpsCounts[_maxFps];
            }
            else
            {
                _fpsText.text = _fpsCounts[_currentFps];
            }

        }
    }
}
