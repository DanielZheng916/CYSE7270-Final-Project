using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailMovement : MonoBehaviour
{

    [SerializeField] private Transform playerTransform;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            sr.flipX = true;
            transform.position = new Vector3(playerTransform.position.x + 0.6f, playerTransform.position.y, playerTransform.position.z);

        } else
        {
            sr.flipX = false;
            transform.position = new Vector3(playerTransform.position.x - 0.6f, playerTransform.position.y, playerTransform.position.z);
        }
    }
}
