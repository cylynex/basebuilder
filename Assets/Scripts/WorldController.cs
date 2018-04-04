using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;

public class WorldController : MonoBehaviour {

	public static WorldController Instance { get; protected set; }
    public Sprite floorSprite;

    Dictionary<Tile, GameObject> tileGameObjectMap;

	// World and tile data
	public World World { get; protected set; }

	void Start () {
		if(Instance != null) {
			Debug.LogError("There should never be two world controllers.");
		}
		Instance = this;

		// Create a world with Empty tiles
		World = new World();

        // Create dictionary
        tileGameObjectMap = new Dictionary<Tile, GameObject>();

		// Create a GameObject for each tile
		for (int x = 0; x < World.Width; x++) {
			for (int y = 0; y < World.Height; y++) {
				// Get the tile data
				Tile tile_data = World.GetTileAt(x, y);

				// This creates a new GameObject and adds it to our scene.
				GameObject tile_go = new GameObject();
                tileGameObjectMap.Add(tile_data, tile_go);

                tile_go.name = "Tile_" + x + "_" + y;
				tile_go.transform.position = new Vector3( tile_data.X, tile_data.Y, 0);
				tile_go.transform.SetParent(this.transform, true);

				// Add a sprite renderer, but don't bother setting a sprite
				// because all the tiles are empty right now.
				tile_go.AddComponent<SpriteRenderer>();

                // Use a lambda to create an anonymous function to "wrap" our callback function
                // FUCK LAMBDAS
                // tile_data.RegisterTileTypeChangedCallback( (tile) => { OnTileTypeChanged(tile, tile_go); } );
                tile_data.RegisterTileTypeChangedCallback(OnTileTypeChanged);
                //OnTileTypeChanged(tile_data);

			}
		}

		World.RandomizeTiles();
	}

	// Update is called once per frame
	void Update () {

	}

	// This function should be called automatically whenever a tile's type gets changed.
	void OnTileTypeChanged(Tile tile_data) {

        // Use dictionary to get the gameobject.  which is way easier than just passing the fucking GO in from the get go
        // but whatever

        if (tileGameObjectMap.ContainsKey(tile_data) == false) {
            Debug.LogError("no tile_data");
            return;
        }


        GameObject tile_go = tileGameObjectMap[tile_data];


        if (tile_go == null) {
            Debug.LogError("no tile_go");
            return;
        }

        Debug.Log("tileGO - " + tile_go);
        Debug.Log("tiledata - " + tile_data);

		if(tile_data.Type == Tile.TileType.Floor) {
			tile_go.GetComponent<SpriteRenderer>().sprite = floorSprite;
		}
		else if( tile_data.Type == Tile.TileType.Empty ) {
			tile_go.GetComponent<SpriteRenderer>().sprite = null;
		}
		else {
			Debug.LogError("OnTileTypeChanged - Unrecognized tile type.");
		}


	}

	/// <summary>
	/// Gets the tile at the unity-space coordinates
	/// </summary>
	/// <returns>The tile at world coordinate.</returns>
	/// <param name="coord">Unity World-Space coordinates.</param>
	public Tile GetTileAtWorldCoord(Vector3 coord) {
		int x = Mathf.FloorToInt(coord.x);
		int y = Mathf.FloorToInt(coord.y);
		
		return World.GetTileAt(x, y);
	}


}
