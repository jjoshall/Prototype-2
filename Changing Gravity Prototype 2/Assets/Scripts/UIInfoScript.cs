using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInfoScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RectTransform objectRectTransform = gameObject.GetComponent<RectTransform> ();
		Debug.Log("width: " + objectRectTransform.rect.width + ", height: " + objectRectTransform.rect.height);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
