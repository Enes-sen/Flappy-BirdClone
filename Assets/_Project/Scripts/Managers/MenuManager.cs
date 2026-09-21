using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]private Text ScText;
    [SerializeField] private float delay;

    private void Awake() { Cursor.lockState = CursorLockMode.None; Cursor.visible = true; }
    private void Update() => UpdateText();

    public void Loadlevel(int lvl)
    {
        StartCoroutine(nameof(PlayCorutine), lvl);
    }
    public void ExitGame()
    {
        StartCoroutine(nameof(QuitCorutine));
    }
    private void UpdateText() => ScText.text = "Best Score:" + GameManager.Instance != null ? GameManager.Instance.BestScore().ToString() : 0.ToString();


    private IEnumerator PlayCorutine(int lvl)
    {
        yield return new WaitForSeconds(delay);
        if (GameManager.Instance != null)
            GameManager.Instance.LoadLevel(lvl);

    }
    private IEnumerator QuitCorutine()
    {
        yield return new WaitForSeconds(delay);
        if (GameManager.Instance != null)
            GameManager.Instance.QuitGame();
    }
}
