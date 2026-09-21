using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PointManager : MonoBehaviour
{
    public static PointManager Instance { get; private set; }
    public int Point { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);
    }
    private void Start() => Point = 0;
    public void Addpoint() { 
            Point++;  UIController.Instance.UpdateScore(Point); if (AudioManager.Instance != null 
            && AudioManager.AudioClips != null && AudioManager.AudioClips.Length >=5) {
            AudioManager.Instance.PlaySFX(AudioManager.AudioClips[1]);
        } }
}
