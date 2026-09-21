using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text ScoreTXT, HighScore, Score;
    [SerializeField] private GameObject GameOver, GameGuide;

    public static UIController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private IEnumerator Wait(int lvl)
    {
        if (AudioManager.Instance != null && AudioManager.AudioClips != null && AudioManager.AudioClips.Length > 0)
        {
            var cp = AudioManager.AudioClips[^1];
            AudioManager.Instance.PlaySFX(cp);
            yield return new WaitForSeconds(cp.length);
        }
        else
            Debug.LogWarning("UIController: Level yükleme sesi çalınamadı!");

        SceneManager.LoadScene(lvl);
    }

    public void LoadLevel(int lvl) => StartCoroutine(nameof(Wait),lvl);

    public void UpdateScore(int point)
    {
        if (ScoreTXT != null)
            ScoreTXT.text = point.ToString();
        else
            Debug.LogWarning("UIController: ScoreTXT atanmadı!");
    }

    public void OnGameOver()
    {
        if (HighScore != null)
            HighScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        else
            Debug.LogWarning("UIController: HighScore Text referansı yok!");

        if (Score != null && PointManager.Instance != null)
            Score.text = PointManager.Instance.Point.ToString();
        else if (Score == null)
            Debug.LogWarning("UIController: Score Text referansı yok!");

        if (GameOver != null)
            GameOver.SetActive(true);
        else
            Debug.LogWarning("UIController: GameOver UI atanmadı!");

        if (ScoreTXT != null)
            ScoreTXT.gameObject.SetActive(false);
    }

    public void CloseGuide()
    {
        if (GameGuide != null)
            GameGuide.SetActive(false);
    }
}