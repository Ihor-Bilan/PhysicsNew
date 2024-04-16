using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShell : MonoBehaviour {

    public float speed = 1.0f;


    void Update() {

        transform.Translate(0.0f, Time.deltaTime * speed*0.5f, Time.deltaTime * speed);
    }
}
