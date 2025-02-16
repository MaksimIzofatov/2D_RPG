using TMPro;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textDied;
    [SerializeField] private TextMeshProUGUI _textWin;
    [SerializeField] private KeyForFinish _keyForFinish;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_keyForFinish.IsActivate)
            _textWin.gameObject.SetActive(true);
        else
            _textDied.gameObject.SetActive(true);
        
        other.gameObject.SetActive(false);
    }
}
