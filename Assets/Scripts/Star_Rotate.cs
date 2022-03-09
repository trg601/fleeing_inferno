using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_Rotate : MonoBehaviour
{
    public int direction = 1;
    private float speed;
    private float time = 0f;
    private float timescale = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(0f, 0.3f) * direction;
        timescale = Random.Range(1f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        time += timescale * Time.deltaTime;
        if (time > 5000f) time -= 5000f;

        transform.Rotate(0, 0, speed, Space.Self);
        float alpha = (Mathf.Sin(time) / 2.5f) + 1f;
        transform.localScale = new Vector3(alpha, alpha, 1f);
    }
}
