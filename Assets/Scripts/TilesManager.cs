using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public class TilesManager : MonoBehaviour
{
    /**
     * <summary>
     * Chunks of tiles
     * </summary>
     */
    public static Dictionary<int, Dictionary<int, GameObject>> Chunks;

    /**
     * <summary>
     * Tiles
     * </summary>
     */
    public static Dictionary<int, Dictionary<int, Tile>> Tiles;

    /**
     * <summary>
     * A global reference to the current instance    
     * </summary>
     */
    public static TilesManager Self;

    private void Awake()
    {
        TilesManager.Chunks = new Dictionary<int, Dictionary<int, GameObject>>();
    }

    // Use this for initialization
    private void Start() {}

    // Update is called once per frame
    private void Update() { }
}
