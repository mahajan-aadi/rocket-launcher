using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving_objects : MonoBehaviour
{
    [SerializeField] Vector3 move;
    Vector3 main_pos;
    [SerializeField] float cycles = 2;
    void Start()
    {
        main_pos = transform.position;
    }

    void Update()
    {
        moving();
    }
    void moving()
    {
        const  float TAU= Mathf.PI * 2f;
        float offset = Mathf.Sin(Time.timeSinceLevelLoad * TAU / cycles);
        offset /= 2f;
        offset += 0.5f;
        transform.position = main_pos + offset * move;
    }
}
