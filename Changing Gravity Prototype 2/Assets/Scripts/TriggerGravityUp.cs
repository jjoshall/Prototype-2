using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGravityUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
     {
          if (other.tag == "Player")
          {
               Debug.Log("Player entered the gravity up trigger");

               // set gravity to go up
               Physics.gravity = new Vector3(0, 9.81f, 0);
          }
     }
}
