using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerV : MonoBehaviour
{
    public int speed;
    public int startPoint;
    public int endPoint;

    void Update()
    {
        transform.position = new Vector2(transform.position.x, Mathf.PingPong(Time.time * speed, endPoint) + startPoint);
        transform.Rotate(new Vector3(0, 0, -60) * Time.deltaTime);
    }
}
