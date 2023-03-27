using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField]
    private float speed, time;
    private float direction = 1;

    private float currentTime = 0;

    private void Update()
    {
        currentTime += Time.deltaTime;

        transform.Translate(new Vector3(speed * Time.deltaTime * direction, 0, 0));

        if (currentTime >= time)
        {
            direction *= -1;
            currentTime = 0;
        }
    }

    //IEnumerator Stop(float gp)
}
