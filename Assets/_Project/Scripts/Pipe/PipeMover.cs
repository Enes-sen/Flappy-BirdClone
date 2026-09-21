using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMover : MonoBehaviour
{
    private readonly float speed = 3.5f;
    private BirdController player;

    private void Start()
    {
        // Önce tag ile dene (Player tag'ı önerilir)
        var playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null)
            player = playerObj.GetComponent<BirdController>();

        // Fallback: Sahnedeki ilk BirdController'ı bul
        if (player == null)
            player = FindObjectOfType<BirdController>();

        if (player == null)
            Debug.LogWarning("PipeMover: Player bulunamadı! Pipe hareket etmeyecek.");
    }

    private void Update()
    {
        if (player == null)
            return; // Player yoksa hata vermeden çık
        
        // Player varsa normal davranış
        if (transform.position.x > -15f && !player.Isdied() && player.Issim())
        {
            transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }
        else if (transform.position.x <= -15f)
        {
            Destroy(gameObject, 0.05f);
        }
    }
}