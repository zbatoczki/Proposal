using System.Collections;
using UnityEngine;

public class ArrowLauncher : MonoBehaviour
{
    private float timeDelayInSeconds = 1.5f;
    private float timer = 0f;
    public GameObject arrow;
    public GameObject player;
    public bool canFire = false;
    public bool dialogueOpen = true;
    public Damageable enemyHealth;

    // Update is called once per frame
    void Update()
    {
        CheckEnemyHealth();
        if (canFire)
        {
            if (timer >= timeDelayInSeconds)
            {
                SetNewPosition();
                ShootArrow();
                timer = 0;
            }
            else
                timer += Time.deltaTime;
        }
        
    }

    private void CheckEnemyHealth()
    {
        if(enemyHealth.CurrentHealth <= 75 && !canFire && !dialogueOpen)
            canFire = true;
        if (enemyHealth.CurrentHealth <= 50 && enemyHealth.CurrentHealth > 25)
            timeDelayInSeconds = 1f;
        else if (enemyHealth.CurrentHealth <= 25)
            timeDelayInSeconds = 0.5f;
    }

    private void ShootArrow()
    {
        Instantiate(arrow, transform.position, transform.rotation);
    }


    private void SetNewPosition()
    {
        Vector2[] spawnAreas =
        {
            new Vector2(Random.Range(-13,9),8),
            new Vector2(9, Random.Range(-5, 8)),
            new Vector2(Random.Range(-13, 9), -6),
            new Vector2(-13, Random.Range(-6, 7))
        };

        transform.position = spawnAreas[Random.Range(0, spawnAreas.Length)];

        Vector3 directionToPlayer = (player.transform.position - transform.position);
        //Debug.DrawLine(transform.position, player.transform.position, Color.red, 1f);      
        Debug.DrawRay(transform.position, directionToPlayer * 2, Color.green, 1f);
        transform.up = directionToPlayer;
        transform.Rotate(0, 0, 45);

    }

    public void DestroyAllArrows()
    {
        IEnumerable arrows = GameObject.FindGameObjectsWithTag("Arrow");
        foreach (GameObject ar in arrows)
        {
            Destroy(ar);
        }
    }
}
