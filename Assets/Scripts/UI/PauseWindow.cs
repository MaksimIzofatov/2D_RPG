using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PauseWindow : PauseWindowBase
{
    [SerializeField] private Button _continueButton;
    
    protected void OnEnable()
    {
        base.OnEnable();
        
        _continueButton.onClick.AddListener(Continue);
    }

    private void OnDisable()
    {
        base.OnDisable();
        
        _continueButton.onClick.RemoveListener(Continue);
    }

    private void Continue()
    {
        gameObject.SetActive(false);
    }
}
