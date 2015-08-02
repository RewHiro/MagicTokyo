using UnityEngine;
using System.Collections;

public class Ike3KudamonMoveLimit : MonoBehaviour
{

    private const float LEFT_WALL = -4.0f;
    private const float RIGHT_WALL = 4.0f;

    private const float UP_WALL = 6.0f;
    private const float DOWN_WALL = -1.0f;

    private const float TOP_WALL = 7.0f;

    private const float BOUND_VALUE = -0.5f;

    private Rigidbody rigidbody;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 velocity = rigidbody.velocity;
        if (pos.x < LEFT_WALL) {
            pos.x = LEFT_WALL;
            velocity.x *= BOUND_VALUE;
        }
        if (pos.x > RIGHT_WALL) {
            pos.x = RIGHT_WALL;
            velocity.x *= BOUND_VALUE;
        }
        if (pos.z < DOWN_WALL) {
            pos.z = DOWN_WALL;
            velocity.z *= BOUND_VALUE;
        }
        if (pos.z > UP_WALL) {
            pos.z = UP_WALL;
            velocity.z *= BOUND_VALUE;
        }
        if (pos.y > TOP_WALL) {
            pos.y = TOP_WALL;
            velocity.y *= BOUND_VALUE;
        }
        transform.position = pos;
        rigidbody.velocity = velocity;
    }
}
