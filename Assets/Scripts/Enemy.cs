using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [HideInInspector]
    public Transform player;

    public int health;
    public float speed;
    public float atkDelay;
    public int damage;
    public float atkSpeed;

    public Animator enemyAnimator;

    void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
