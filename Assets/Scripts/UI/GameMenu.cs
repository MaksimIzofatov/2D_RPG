using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Button _pauseGameButton;
    [SerializeField] private PauseWindow _pauseWindow;
    
    private void OnEnable()
    {
        _pauseGameButton.onClick.AddListener(OpenPauseWindow);
    }

    private void OnDisable()
    {
        _pauseGameButton.onClick.RemoveListener(OpenPauseWindow);
    }

    private void OpenPauseWindow()
    {
        _pauseWindow.gameObject.SetActive(true);
    }
}
