using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSoundEffect;
    [SerializeField] private Rigidbody2D playerRb;

    // Start is called before the first frame update
    void Start()
    {
        finishSoundEffect = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        finishSoundEffect.Play();
        playerRb.bodyType = RigidbodyType2D.Static;
        Invoke("toNextLevel", 2f);
        //toNextLevel();
    }

    private void toNextLevel()
    {
        Debug.Log("Next Level!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
