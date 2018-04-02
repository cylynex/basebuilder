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
        Tile tileUnderMouse = GetTileAtSpot(currentFramePosition);
        if (tileUnderMouse != null) {
            circleCursor.SetActive(true);
            Vector3 cursorPosition = new Vector3(tileUnderMouse.x, tileUnderMouse.y, 0);
            circleCursor.transform.position = cursorPosition;
        } else {
            circleCursor.SetActive(false);
        }

        // Left Mouse click
        if (Input.GetMouseButtonUp(0)) {
            if (tileUnderMouse != null) {
                if (tileUnderMouse.type == Tile.TileType.Empty) {
                    tileUnderMouse.type = Tile.TileType.Floor;
                } else {
                    tileUnderMouse.type = Tile.TileType.Empty;
                }

                GameObject clickedGameObject = GetTileObject(tileUnderMouse.x, tileUnderMouse.y);
                WorldController.instance.OnTileTypeChanged(tileUnderMouse,clickedGameObject);
            }
        }

        // Screen Dragging
        if (Input.GetMouseButton(1)) { 
            Vector3 diff = lastFramePosition - currentFramePosition;
            Camera.main.transform.Translate(diff);
        }

        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

	}


    Tile GetTileAtSpot(Vector3 coord) {
        int x = Mathf.FloorToInt(coord.x);
        int y = Mathf.FloorToInt(coord.y);

        GameObject.FindObjectOfType<WorldController>();
        return WorldController.instance.world.GetTileAt(x, y);
    }


    GameObject GetTileObject(float x, float y) {
        string thisgo = "Tile_" + x + "_" + y;
        GameObject thisobj = GameObject.Find(thisgo);
        return thisobj;
    }

}
