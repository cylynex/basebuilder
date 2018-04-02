using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

    World world;

    public Sprite floorSprite;

	// Use this for initialization
	void Start () {
        world = new World();
        world.RandomizeTiles();

        // create gameobject for each tile
        for (int x = 0; x < world.width; x++) {
            for (int y = 0; y < world.height; y++) {
                // create tile now
                GameObject tileObject = new GameObject();
                tileObject.name = "Tile_" + x + "_" + y;
                SpriteRenderer tileSprite = tileObject.AddComponent<SpriteRenderer>();
                Tile tileData = world.GetTileAt(x, y);
                if (tileData.type == Tile.TileType.Floor) {
                    Debug.Log("floor");
                    tileSprite.sprite = floorSprite;
                } else if (tileData.type == Tile.TileType.Empty) {
                    Debug.Log("empty");
                }

                tileObject.transform.position = new Vector2(tileData.x, tileData.y);
                tileObject.transform.SetParent(this.transform, true);

            }
        }
	}



	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTileTypeChanged(Tile tileData, GameObject tileObject) {
        if (tileData.type == Tile.TileType.Floor) {
            tileObject.GetComponent<SpriteRenderer>().sprite = floorSprite;
        } else if (tileData.type == Tile.TileType.Empty) {
            tileObject.GetComponent<SpriteRenderer>().sprite = null;
        } else {
            Debug.LogError("Changing File Type - Unrecognized tile");
        }
    }

}
