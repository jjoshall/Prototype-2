using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScript : MonoBehaviour
{
     public string sceneName;

     private void OnTriggerEnter(Collider other)
     {
          if (other.tag == "Player")
          {
               Debug.Log("Player entered the next scene trigger");

               // change to different scene
               SceneManager.LoadScene(sceneName);

          }
     }
}
