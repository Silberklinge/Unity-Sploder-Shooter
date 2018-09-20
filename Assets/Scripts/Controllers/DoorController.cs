using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorController : MonoBehaviour {

    private static readonly float MAX_DIST = 1.5f;
    private static readonly float PROPAGATION_DELAY = 1 / 3.0f;
    private static readonly float OPEN_DURATION = 2.5f;
    private static DoorController[] allDoors;
    
    public Collider2D collisionBox;
    public Collider2D triggerBox;
    public bool open;
    private List<DoorController> adjacentDoors;
    private float lastObstructed;
    private int numberOfObstructions;

    void Start() {
        if(allDoors == null)
            allDoors = FindObjectsOfType<DoorController>();
        adjacentDoors = new List<DoorController>();
        for(int i = 0; i < allDoors.Length; i++) {
            if(allDoors[i] != this) {
                var thisPosition = transform.position;
                var otherPosition = allDoors[i].transform.position;
                if((otherPosition - thisPosition).sqrMagnitude <= MAX_DIST * MAX_DIST)
                    adjacentDoors.Add(allDoors[i]);
            }
        }
    }

    void Update() {
        if(Time.time - lastObstructed > OPEN_DURATION && numberOfObstructions == 0)
            collisionBox.enabled = true;
    }

    IEnumerator OnCollisionEnter2D(Collision2D collision) {
        yield return new WaitForSeconds(PROPAGATION_DELAY);
        collisionBox.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        numberOfObstructions++;
    }

    void OnTriggerStay2D(Collider2D collider) {
        lastObstructed = Time.time;
    }

    void OnTriggerExit2D(Collider2D collider) {
        numberOfObstructions--;
    }
}