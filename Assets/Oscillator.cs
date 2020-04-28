using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;

    //todo remove from inspector later
    [Range(0, 1)]
    [SerializeField]
    float movementFactor; // 0 for not moved, 1 for fully moved.

    Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        //print("x : " + startingPos.x);
        //print("y : " + startingPos.y);
        //print("z : " + startingPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        if( period <= Mathf.Epsilon ) { return; }

        //print("time " + Time.time);

        float cycles = Time.time / period; // grows continually from 0

        //print("cycles " + cycles);

        const float tau = Mathf.PI * 2; // about 6.28

        float rawSineWave = Mathf.Sin(cycles * tau);

        print("rawsinewave " + rawSineWave);

        movementFactor = rawSineWave / 2f + 0.5f;

        //print("mov vect : " + movementVector);
        Vector3 offset = movementVector * movementFactor; // if movementFactor will be 0.5 then offset will be (20, 0, 0) * 0.5 = (10, 0, 0)
        //print("x : " + startingPos.x);
        //print("y : " + startingPos.y);
        //print("z : " + startingPos.z);
        transform.position = startingPos + offset;
    }
}
