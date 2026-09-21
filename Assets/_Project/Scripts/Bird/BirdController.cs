using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private float FlipingRate = 200f;

    private bool died;
    private bool _isfyling;
    private Rigidbody2D _rb;
    private Animator _animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null)
            Debug.LogWarning("BirdController: Rigidbody2D bulunamadı!");

        _rb.simulated = false;

        _animator = GetComponentInChildren<Animator>();
        if (_animator == null)
            Debug.LogWarning("BirdController: Animator bulunamadı!");

        if (GameManager.Instance != null)
            GameManager.Instance.Registerplayer(this);
        else
            Debug.LogWarning("BirdController: GameManager.Instance null iken Registerplayer çağrıldı!");
    }

    public bool Isflying()
    {
        if (!Isdied() && Issim())
        {
            return !GameManager.Instance.startedGame;
        }
        return false;
    }

    private void Update()
    {
        if (!Isdied() && Input.GetMouseButtonDown(0))
            Fly();
    }

    private void FixedUpdate()
    {
        if (_rb != null)
        {
            _rb.velocity = _isfyling ? FlipingRate * Vector2.up : _rb.velocity;
            _isfyling = false;
        }
    }

    public bool Isdied() => died;
    public bool Setdied(bool state) => died = state;

    private void Fly()
    {
        if (UIController.Instance != null)
            UIController.Instance.CloseGuide();

        if (_rb != null)
        {
            _rb.simulated = true;
            _isfyling = true;
        }

        if (AudioManager.Instance != null && AudioManager.AudioClips != null && AudioManager.AudioClips.Length >= 1)
            AudioManager.Instance.PlaySFX(AudioManager.AudioClips[0]);
        else
            Debug.LogWarning("BirdController: Uçuş sesi çalınamadı!");

        if (_animator != null)
            _animator.SetTrigger("Flip");
    }

    public bool Issim() => _rb != null && _rb.simulated;

    private void OnCollisionEnter2D(Collision2D col) => Dead();

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (PointManager.Instance != null)
            PointManager.Instance.Addpoint();
        else
            Debug.LogWarning("BirdController: PointManager.Instance null!");
    }

    private void Dead()
    {
        if (!Isdied())
        {
            if (AudioManager.Instance != null && AudioManager.AudioClips != null && AudioManager.AudioClips.Length >= 3)
                AudioManager.Instance.PlaySFX(AudioManager.AudioClips[2]);
            else
                Debug.LogWarning("BirdController: Ölüm sesi çalınamadı!");

            if (_rb != null)
            {
                _rb.simulated = false;
                _rb.velocity = Vector2.zero;
            }

            if (GameManager.Instance != null)
                GameManager.Instance.OnDead();
            else
                Debug.LogWarning("BirdController: GameManager.Instance null iken OnDead çağrıldı!");
        }
    }
}