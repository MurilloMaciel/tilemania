using System;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickupSfx;
    [SerializeField] private int pointForCoinPickup = 1;
    private bool _wasCollected = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_wasCollected || !other.CompareTag("Player")) return;
        _wasCollected = true;
        FindFirstObjectByType<GameSession>().AddToScore(pointForCoinPickup);
        AudioSource.PlayClipAtPoint(coinPickupSfx, transform.position);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
