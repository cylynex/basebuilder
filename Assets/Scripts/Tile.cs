using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile {

    public enum TileType { Empty, Floor };
    public TileType type = TileType.Empty;

    Action cbTileTypeChanged;

    LooseObject looseObject;
    InstalledObject installedObject;

    World world;
    public int x;
    public int y;

    public Tile(World world, int x, int y) {
        this.world = world;
        this.x = x;
        this.y = y;
    }

}
