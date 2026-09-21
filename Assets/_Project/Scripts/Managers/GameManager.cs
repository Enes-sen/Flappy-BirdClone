using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private BirdController player;
    public bool startedGame = false;

    public const string HsT = "HighScore";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (!PlayerPrefs.HasKey(HsT))
            PlayerPrefs.SetInt(HsT, 0);
    }

    public void SavePoints(int value)
    {
        int best = BestScore();
        if (value > best)
            PlayerPrefs.SetInt(HsT, value);
    }

    public void OnDead()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SavePoints(PointManager.Instance != null ? PointManager.Instance.Point : 0);

        if (AudioManager.Instance != null && AudioManager.AudioClips != null && AudioManager.AudioClips.Length >= 4)
            AudioManager.Instance.PlaySFX(AudioManager.AudioClips[3]);
        else
            Debug.LogWarning("GameManager: Ölüm sesi çalınamadı!");

        if (player != null)
            player.Setdied(true);

        if (UIController.Instance != null)
            UIController.Instance.OnGameOver();
    }

    private IEnumerator Wait(int lvl)
    {
        if (AudioManager.Instance != null && AudioManager.AudioClips != null && AudioManager.AudioClips.Length >= 5)
        {
            AudioManager.Instance.PlaySFX(AudioManager.AudioClips[4]);
            yield return new WaitForSeconds(0.45f);
        }
        else
            Debug.LogWarning("GameManager: Level geçiş sesi çalınamadı!");

        SceneManager.LoadScene(lvl);
    }

    public void LoadLevel(int lvl)
    {
        Debug.Log("LoadLevel called, level: " + lvl);
        StartCoroutine(Wait(lvl));
    }

    public void QuitGame() => StartCoroutine(THeQuitGame());

    private IEnumerator THeQuitGame()
    {
        if (AudioManager.Instance != null && AudioManager.AudioClips != null && AudioManager.AudioClips.Length > 0)
        {
            var cp = AudioManager.AudioClips[^1];
            AudioManager.Instance.PlaySFX(cp);
            yield return new WaitForSeconds(cp.length);
            Application.Quit();
        }
        else
        {
            Debug.LogWarning("GameManager: Çıkış sesi çalınamadı!");
            Application.Quit();
        }
    }

    public void Registerplayer(BirdController con) => player = con;

    public int BestScore() => PlayerPrefs.GetInt(HsT);
}