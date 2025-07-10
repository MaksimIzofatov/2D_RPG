using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private const int DEFAULT_GAME_INDEX = 1;
    
    [SerializeField] private Button _startGameButton;
    

    private void OnEnable()
    {
        _startGameButton.onClick.AddListener(LoadScene);
    }

    private void OnDisable()
    {
        _startGameButton.onClick.RemoveListener(LoadScene);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(DEFAULT_GAME_INDEX);
    }
}
