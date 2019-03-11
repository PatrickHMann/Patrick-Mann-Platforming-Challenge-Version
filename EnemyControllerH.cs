using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerH : MonoBehaviour
{
    public int speed;
    public int startPoint;
    public int endPoint;

    void Update()
    {
        transform.position = new Vector2(Mathf.PingPong(Time.time * speed, endPoint)+startPoint, transform.position.y);
        transform.Rotate(new Vector3(0, 0, -60) * Time.deltaTime);
    }
}
