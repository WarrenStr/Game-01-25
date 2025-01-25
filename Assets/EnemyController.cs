using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    private Rigidbody rb;
    public float speed = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
    }

    void ChasePlayer()
    {
        rb.AddForce((player.transform.position - transform.position).normalized * speed);
        // // Move towards the player
        // transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
    }
}
