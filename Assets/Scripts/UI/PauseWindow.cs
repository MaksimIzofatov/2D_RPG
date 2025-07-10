using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PauseWindow : MonoBehaviour
{
    private const int MAIN_MENU_SCENE_INDEX = 0;
    
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    
    private void OnEnable()
    {
        TimeManager.Pause();
        
        _continueButton.onClick.AddListener(Continue);
        _restartButton.onClick.AddListener(Restart);
        _exitButton.onClick.AddListener(Exit);
    }

    private void OnDisable()
    {
        TimeManager.Run();
        
        _continueButton.onClick.RemoveListener(Continue);
        _restartButton.onClick.RemoveListener(Restart);
        _exitButton.onClick.RemoveListener(Exit);
    }

    private void Continue()
    {
        gameObject.SetActive(false);
    }
    
    private void Restart()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private void Exit()
    {
        LoadScene(MAIN_MENU_SCENE_INDEX);
    }
    
    private void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
