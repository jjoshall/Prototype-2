using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendToRespawn : MonoBehaviour
{
     // making location for where to respawn the player
     public Transform targetLocation;
     public Transform targetRotation;
     public Transform targetPosition;

     private void OnTriggerEnter(Collider other)
     {
          if (other.tag == "Player")
          {
               Debug.Log("Player entered the death trigger");
               other.transform.position = targetPosition.position;
               other.transform.rotation = targetRotation.rotation;

               // set gravity to default
               Physics.gravity = new Vector3(0, -9.81f, 0);
          }
     }
}
