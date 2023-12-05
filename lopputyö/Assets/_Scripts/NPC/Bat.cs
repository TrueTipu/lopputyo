using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] Vector2 Dir;
    float timer = 2f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0){
            _rb.AddForce(Dir, ForceMode2D.Impulse);
            Dir.x = Random.Range(-1.5f, 1.5f);
            timer = Random.Range(1f, 4f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _rb.AddForce(Dir, ForceMode2D.Impulse);
        }
    }
}