using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float enemySpeed;
    private bool movingLeft;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChangeDirections", 1, 3);
    }
    void ChangeDirections()
    {
        movingLeft = !movingLeft;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (movingLeft)
        {
            transform.Translate(Vector3.left * enemySpeed *.05f);
        }
        else
        {
            transform.Translate(Vector3.right * enemySpeed * .05f);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerScript>().TakeDamage();
            Destroy(gameObject);
        }
    }
}
