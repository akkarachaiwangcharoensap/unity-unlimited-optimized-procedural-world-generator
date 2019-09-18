using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MapWatcher : MonoBehaviour
{
    /**
     * <summary>
     * Active tiles
     * </summary>
     */
    private GameObject[] activeTiles;

    // Start is called before the first frame update
    private void Start()
    {
        this.activeTiles = new GameObject[9];
    }

    /**
     * <summary>
     * Update is called once per frame
     * </summary>
     *    
     * <returns>
     * void
     * <returns>
     */
    private void Update()
    {
        // Watch neighbours
        this.WatchNeighbours();
    }

    /**
     * <summary>
     * Watch neighbours based on
     * top left, top right, bottom left,
     * bottom right, top, left, bottom and right.
     * </summary>
     *
     * <returns>
     * void
     * </returns>
     */
    public void WatchNeighbours()
    {
        if (this.GetCenterChunk())
        {
            ProceduralMapGenerator mapGeneratorBehaviour = this.GetCenterChunk()
                .GetComponent<ProceduralMapGenerator>();

            int x = (int)mapGeneratorBehaviour.GetPosition().x;
            int y = (int)mapGeneratorBehaviour.GetPosition().y;

            // Vertical
            if (!TilesManager.Chunks.ContainsKey(x))
            {
                TilesManager.Chunks[x] = new Dictionary<int, GameObject>();
            }

            if (!TilesManager.Chunks.ContainsKey(x + 1))
            {
                TilesManager.Chunks[x + 1] = new Dictionary<int, GameObject>();
            }

            if (!TilesManager.Chunks.ContainsKey(x - 1))
            {
                TilesManager.Chunks[x - 1] = new Dictionary<int, GameObject>();
            }

            // Center
            this.activeTiles[4] = this.GetCenterChunk();

            // Top Left
            if (!TilesManager.Chunks[x].ContainsKey(y + 1))
            {
                Vector2 position = new Vector2(x, y + 1);

                GameObject topLeft = mapGeneratorBehaviour.GenerateTopLeftChunk();
                topLeft.GetComponent<ProceduralMapGenerator>().SetPosition(position);

                TilesManager.Chunks[x][y + 1] = topLeft;
            }
            // Show top left tiles if exists
            else
            {
                GameObject topLeft = TilesManager.Chunks[x][y + 1];

                // Top left
                this.activeTiles[0] = topLeft;
            }

            // Bottom Right
            if (!TilesManager.Chunks[x].ContainsKey(y - 1))
            {
                Vector2 position = new Vector2(x, y - 1);

                GameObject bottomRight = mapGeneratorBehaviour.GenerateBottomRightChunk();
                bottomRight.GetComponent<ProceduralMapGenerator>().SetPosition(position);
                TilesManager.Chunks[x][y - 1] = bottomRight;
            }
            // Show bottom right tiles if exists
            else
            {
                GameObject bottomRight = TilesManager.Chunks[x][y - 1];

                // Bottom right
                this.activeTiles[8] = bottomRight;
            }

            // Top Right
            if (!TilesManager.Chunks[x + 1].ContainsKey(y))
            {

                Vector2 position = new Vector2(x + 1, y);

                GameObject topRight = mapGeneratorBehaviour.GenerateTopRightChunk();
                topRight.GetComponent<ProceduralMapGenerator>().SetPosition(position);
                TilesManager.Chunks[x + 1][y] = topRight;
            }
            // Show top right tiles if exists
            else
            {
                GameObject topRight = TilesManager.Chunks[x + 1][y];

                // Top right
                this.activeTiles[2] = topRight;
            }

            // Bottom Left
            if (!TilesManager.Chunks[x - 1].ContainsKey(y))
            {
                Vector2 position = new Vector2(x - 1, y);

                GameObject bottomLeft = mapGeneratorBehaviour.GenerateBottomLeftChunk();
                bottomLeft.GetComponent<ProceduralMapGenerator>().SetPosition(position);
                TilesManager.Chunks[x - 1][y] = bottomLeft;
            }
            // Show bottom left tiles if exists
            else
            {
                GameObject bottomLeft = TilesManager.Chunks[x - 1][y];

                // Bottom left
                this.activeTiles[6] = bottomLeft;
            }

            // Left
            if (!TilesManager.Chunks[x - 1].ContainsKey(y + 1))
            {

                Vector2 position = new Vector2(x - 1, y + 1);

                GameObject left = mapGeneratorBehaviour.GenerateLeftChunk();
                left.GetComponent<ProceduralMapGenerator>().SetPosition(position);
                TilesManager.Chunks[x - 1][y + 1] = left;
            }
            // Show left tiles if exists
            else
            {
                GameObject left = TilesManager.Chunks[x - 1][y + 1];

                // Left
                this.activeTiles[3] = left;
            }

            // Right
            if (!TilesManager.Chunks[x + 1].ContainsKey(y - 1))
            {
                Vector2 position = new Vector2(x + 1, y - 1);

                GameObject right = mapGeneratorBehaviour.GenerateRightChunk();
                right.GetComponent<ProceduralMapGenerator>().SetPosition(position);
                TilesManager.Chunks[x + 1][y - 1] = right;
            }
            // Show right tiles if exists
            else
            {
                GameObject right = TilesManager.Chunks[x + 1][y - 1];

                // Right
                this.activeTiles[5] = right;
            }

            // Bottom
            if (!TilesManager.Chunks[x - 1].ContainsKey(y - 1))
            {
                Vector2 position = new Vector2(x - 1, y - 1);

                GameObject bottom = mapGeneratorBehaviour.GenerateBottomChunk();
                bottom.GetComponent<ProceduralMapGenerator>().SetPosition(position);
                TilesManager.Chunks[x - 1][y - 1] = bottom;
            }
            // Show bottom tiles if exists
            else
            {
                GameObject bottom = TilesManager.Chunks[x - 1][y - 1];

                // Bottom
                this.activeTiles[7] = bottom;
            }

            // Top
            if (!TilesManager.Chunks[x + 1].ContainsKey(y + 1))
            {
                Vector2 position = new Vector2(x + 1, y + 1);

                GameObject top = mapGeneratorBehaviour.GenerateTopChunk();
                top.GetComponent<ProceduralMapGenerator>().SetPosition(position);
                TilesManager.Chunks[x + 1][y + 1] = top;
            }
            // Show top tiles if exists
            else
            {
                GameObject top = TilesManager.Chunks[x + 1][y + 1];

                // Top
                this.activeTiles[1] = top;
            }

        }
    }

    /**
     * <summary>
     * Get top left chunk game object
     * </summary>
     *
     * <returns>
     * GameObject
     * </returns>
     */
    public GameObject GetTopLeftChunk()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0, 1));
        RaycastHit[] hits = Physics.RaycastAll(ray);

        GameObject chunk = null;
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.CompareTag("Chunk"))
            {
                chunk = hit.transform.gameObject;
                break;
            }
        }

        return chunk;
    }

    /**
     * <summary>
     * Get top right chunk game object
     * </summary>
     *
     * <returns>
     * GameObject
     * </returns>
     */
    public GameObject GetTopRightChunk()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(1, 1));
        RaycastHit[] hits = Physics.RaycastAll(ray);

        GameObject chunk = null;
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.CompareTag("Chunk"))
            {
                chunk = hit.transform.gameObject;
                break;
            }
        }

        return chunk;
    }

    /**
     * <summary>
     * Get bottom left chunk game object
     * </summary>
     *
     * <returns>
     * GameObject
     * </returns>
     */
    public GameObject GetBottomLeftChunk()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0, 0));
        RaycastHit[] hits = Physics.RaycastAll(ray);

        GameObject chunk = null;
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.CompareTag("Chunk"))
            {
                chunk = hit.transform.gameObject;
                break;
            }
        }

        return chunk;
    }

    /**
     * <summary>
     * Get bottom right chunk game object
     * </summary>
     *
     * <returns>
     * GameObject
     * </returns>
     */
    public GameObject GetBottomRightChunk()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(1, 0));
        RaycastHit[] hits = Physics.RaycastAll(ray);

        GameObject chunk = null;
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.CompareTag("Chunk"))
            {
                chunk = hit.transform.gameObject;
                break;
            }
        }

        return chunk;
    }

    /**
     * <summary>
     * Get top chunk game object
     * </summary>
     *
     * <returns>
     * GameObject
     * </returns>
     */
    public GameObject GetTopChunk()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 1f));
        RaycastHit[] hits = Physics.RaycastAll(ray);

        GameObject chunk = null;
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.CompareTag("Chunk"))
            {
                chunk = hit.transform.gameObject;
                break;
            }
        }

        return chunk;
    }

    /**
     * <summary>
     * Get bottom chunk game object
     * </summary>
     *
     * <returns>
     * GameObject
     * </returns>
     */
    public GameObject GetBottomChunk()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0f));
        RaycastHit[] hits = Physics.RaycastAll(ray);

        GameObject chunk = null;
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.CompareTag("Chunk"))
            {
                chunk = hit.transform.gameObject;
                break;
            }
        }

        return chunk;
    }

    /**
     * <summary>
     * Get left chunk game object
     * </summary>
     *
     * <returns>
     * GameObject
     * </returns>
     */
    public GameObject GetLeftChunk()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0f, 0.5f));
        RaycastHit[] hits = Physics.RaycastAll(ray);

        GameObject chunk = null;
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.CompareTag("Chunk"))
            {
                chunk = hit.transform.gameObject;
                break;
            }
        }

        return chunk;
    }

    /**
     * <summary>
     * Get right chunk game object
     * </summary>
     *
     * <returns>
     * GameObject
     * </returns>
     */
    public GameObject GetRightChunk()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(1f, 0.5f));
        RaycastHit[] hits = Physics.RaycastAll(ray);

        GameObject chunk = null;
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.CompareTag("Chunk"))
            {
                chunk = hit.transform.gameObject;
                break;
            }
        }

        return chunk;
    }

    /**
     * <summary>
     * Get center chunk game object
     * </summary>
     *
     * <returns>
     * GameObject
     * </returns>
     */
    public GameObject GetCenterChunk()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        RaycastHit[] hits = Physics.RaycastAll(ray);

        GameObject chunk = null;
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.CompareTag("Chunk"))
            {
                chunk = hit.transform.gameObject;
                break;
            }
        }

        return chunk;
    }
}
