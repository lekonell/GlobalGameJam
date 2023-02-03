using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float destroyTime = 3f;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    void FixedUpdate()
    {
        transform.position += Vector3.up * Time.fixedDeltaTime * moveSpeed;
    }
}
