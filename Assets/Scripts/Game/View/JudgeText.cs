using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeText : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;
    private float lifeTime = 0.2f;
    private float speed = 0.02f;
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    void Update()
    {
        transform.position = transform.position + Vector3.up * speed;
        sprite.color -= new Color(0, 0, 0, Time.deltaTime / lifeTime);
    }
}
