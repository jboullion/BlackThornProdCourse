using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : Enemy
{

    [SerializeField] float stopDistance;
    [SerializeField] float atkTime;

    // FixedUpdate is called every 1/50 of a sec. Usually less than Update.
    void Update()
    {
        if(player != null)
        {
            if (Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                enemyAnimator.SetBool("isRunning", true);
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                // Make sure we don't attack immidiately after we stop running;
                atkTime = Time.time + atkDelay;
            }
            else
            {
                enemyAnimator.SetBool("isRunning", false);
                if (Time.time >= atkTime)
                {
                    //Attack
                    atkTime = Time.time + atkDelay;
                    enemyAnimator.Play("RedditAttack");
                    StartCoroutine(Attack());
                }
            }
        }
    }

    IEnumerator Attack()
    {
        player.GetComponent<BrainPlayer>().TakeDamage(damage);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;
        
        float percent = 0f;
        while(percent <= 1)
        {
            percent += Time.deltaTime * atkSpeed;
            float atkRate = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, atkRate);
            yield return null;
        }
    }
}
