using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollect : MonoBehaviour
{

    private int cherries = 0;

    [SerializeField] private Text cherriesUI;
    [SerializeField] private AudioSource collectSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            cherries++;
            Debug.Log("Cherries: " + cherries);
            cherriesUI.text = "Cherries: " + cherries;
        }
    }
}
