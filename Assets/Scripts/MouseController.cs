using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

    Vector3 lastFramePosition;
    public GameObject circleCursor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 currentFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Update cursor position
        circleCursor.transform.position = currentFramePosition;

        // Screen Dragging
        if (Input.GetMouseButton(1)) { 
            Vector3 diff = lastFramePosition - currentFramePosition;
            Debug.Log("difference is " + diff);
            Camera.main.transform.Translate(diff);
        }

        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

	}
}
