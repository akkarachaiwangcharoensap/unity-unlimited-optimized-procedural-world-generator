using UnityEngine;

public class ProceduralMapGenerator : MonoBehaviour
{
    /**
     * <summary>
     * Seed
     * </summary>
     */
    [SerializeField]
    public float seed = 0;

    /**
     * <summary>
     * Size    
     * </summary>
     */
    [SerializeField]
    private Vector2 size = new Vector2(0, 0);

    /**
     * <summary>
     * Frequency
     * </summary>
     */
    public float frequency = 1;

    /**
     * <summary>
     * Mesh filter
     * </summary>
     */
    private MeshFilter meshFilter;

    /**
     * <summary>
     * UVs
     * </summary>
     */
    [SerializeField]
    private Vector2[] UVs;

    /**
     * <summary>
     * Vertex triangles
     * </summary>
     */
    private int[] triangles;

    /**
     * <summary>
     * Vertices
     * </summary>
     */
    private Vector3[] vertices;

    /**
     * <summary>
     * Normals
     * </summary>
     */
    private Vector3[] normals;

    /**
     * <summary>
     * Position
     * </summary>
     */
    private Vector2 position;

    /**
     * <summary>
     * Run before start() is called.
     * </summary>
     * 
     * <returns>
     * void
     * </returns>
     */
    private void Awake() {}

    /**
     * <summary>
     * Use this for initialization
     * </summary>
     * 
     * <returns>
     * void
     * </returns>
     */
    private void Start()
    {
        this.meshFilter = this.GetComponent<MeshFilter>();

        Vector2 position = new Vector2(this.transform.position.x, this.transform.position.z);
        this.Generate(position);
    }

    /**
     * <summary>
     * Update is called once per frame    
     * </summary>
     * 
     * <returns>
     * void
     * </returns>
     */
    private void Update() {}

    /**
     * <summary>
     * Generate a new world based on the properties    
     * </summary>
     *
     * <param name="initPosition">Vector2 initPosition</param>
     * 
     * <returns>
     * GameObject
     * </returns>
     */
    public void Generate(Vector2 initPosition)
    {
        this.meshFilter = this.GetComponent<MeshFilter>();

        int numberOfTiles = (int)(this.size.x * this.size.y); // x * y
        int numberOfTriangles = numberOfTiles * 6; //
        int numberOfVertices = numberOfTiles * 4; // Vertices or the node

        this.UVs = new Vector2[numberOfVertices];

        // Generate vertices
        this.vertices = this.GenerateVertices();

        // Generate UVs
        this.UVs = this.GenerateAndMapUV(initPosition);

        // Generate Triangles
        this.triangles = this.GenerateTriangles();

        // Generate normals
        this.normals = this.GenerateNormals();

        Mesh mesh = new Mesh();

        // Set the maximum rendering meshes
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        mesh.name = "Optimized Procedural Mesh";
        mesh.vertices = this.vertices;
        mesh.triangles = this.triangles;
        mesh.normals = this.normals;
        mesh.uv = this.UVs;

        this.meshFilter.mesh = mesh;
        this.meshFilter.mesh.RecalculateTangents();
        this.meshFilter.mesh.RecalculateNormals();

        this.gameObject.AddComponent<BoxCollider>();
    }

    /**
     * <summary>
     * Generate vertices
     * </summary>
     *
     * <returns>
     * void
     * </returns>
     */
    private Vector3[] GenerateVertices()
    {
        int numberOfTiles = (int)(this.size.x * this.size.y); // x * y
        int numberOfVertices = numberOfTiles * 4;

        int vertice = 0;

        this.vertices = new Vector3[numberOfVertices];

        for (int x = 0; x < (int)this.size.x; x++)
        {
            for (int y = 0; y < (int)this.size.y; y++)
            {
                this.vertices[vertice + 0] = new Vector3(x, 0, y);
                this.vertices[vertice + 1] = new Vector3(x + 1, 0, y);
                this.vertices[vertice + 2] = new Vector3(x + 1, 0, y + 1);
                this.vertices[vertice + 3] = new Vector3(x, 0, y + 1);

                vertice = vertice + 4;
            }
        }

        return this.vertices;
    }

    /**
     * <summary>
     * Generate UV
     * </summary>
     *
     * <returns>
     * void
     * </returns>
     */
    private Vector2[] GenerateAndMapUV(Vector2 initPosition)
    {
        int vertice = 0;

        for (int x = (int)initPosition.x; x < (int)initPosition.x + (int)this.size.x; x++)
        {
            for (int z = (int)initPosition.y; z < (int)initPosition.y + (int)this.size.y; z++)
            {
                double nx = this.seed + (double)x / this.size.x - 0.5f;
                double nz = this.seed + (double)z / this.size.y - 0.5f;

                float elevationNoise = Mathf.PerlinNoise(frequency * (float)nx, frequency * (float)nz);

                this.MapUVTile(
                    elevationNoise,
                    vertice
                );

                vertice = vertice + 4;
            }
        }

        return this.UVs;
    }

    /**
     * <summary>
     * Map Tile UV
     * </summary>
     *
     * <param name="e">float elevation</param>
     * <param name="vertice">int vertice</param>
     *
     * <returns>
     * void
     * </returns>
     */
    private void MapUVTile(float e, int vertice)
    {
        // Water
        if (e < 0.2f)
        {
            Vector2[] waterUVs = TileUVMap.Water();

            this.UVs[vertice + 0] = waterUVs[0];
            this.UVs[vertice + 1] = waterUVs[1];
            this.UVs[vertice + 2] = waterUVs[2];
            this.UVs[vertice + 3] = waterUVs[3];
        }

        // Sand
        else if (e < 0.3f)
        {
            Vector2[] sandUVs = TileUVMap.Sand();

            this.UVs[vertice + 0] = sandUVs[0];
            this.UVs[vertice + 1] = sandUVs[1];
            this.UVs[vertice + 2] = sandUVs[2];
            this.UVs[vertice + 3] = sandUVs[3];
        }

        // Dirt
        else if (e > 0.6f)
        {
            Vector2[] dirtUVs = TileUVMap.Dirt();

            this.UVs[vertice + 0] = dirtUVs[0];
            this.UVs[vertice + 1] = dirtUVs[1];
            this.UVs[vertice + 2] = dirtUVs[2];
            this.UVs[vertice + 3] = dirtUVs[3];
        }

        // Grass
        else
        {
            Vector2[] grassUVs = TileUVMap.Grass();

            this.UVs[vertice + 0] = grassUVs[0];
            this.UVs[vertice + 1] = grassUVs[1];
            this.UVs[vertice + 2] = grassUVs[2];
            this.UVs[vertice + 3] = grassUVs[3];
        }
    }

    /**
     * <summary>
     * Generate triangles
     * </summary>
     *
     * <returns>
     * void
     * </returns>
     */
    private int[] GenerateTriangles()
    {
        int numberOfTiles = (int)(size.x * size.y); // x * y
        int numberOfTriangles = numberOfTiles * 6; //

        int[] triangles = new int[numberOfTriangles];

        int index = 0;
        int vertice = 0;

        for (int i = 0; i < numberOfTiles; i++)
        {
            triangles[index + 0] += (vertice + 2);
            triangles[index + 1] += (vertice + 1);
            triangles[index + 2] += (vertice + 0);
            triangles[index + 3] += (vertice + 3);
            triangles[index + 4] += (vertice + 2);
            triangles[index + 5] += (vertice + 0);

            vertice = vertice + 4;
            index = index + 6;
        }

        return triangles;
    }

    /**
     * <summary>
     * Generate normals
     * </summary>
     *
     * <returns>
     * void
     * </returns>
     */
    private Vector3[] GenerateNormals()
    {
        int numberOfTiles = (int)(this.size.x * this.size.y); // x * y
        int numberOfVertices = numberOfTiles * 4;

        this.normals = new Vector3[numberOfVertices];

        // Setting normals
        for (int i = 0; i < numberOfVertices; i++)
        {
            this.normals[i] = new Vector3(0, 1, 0);
        }

        return this.normals;
    }

    /**
 * <summary>
 * Generate center chunk
 * </summary>
 *
 * <param name="position">Vector2 position</param>
 * 
 * <returns>
 * GameObject
 * </returns>
 */
    public GameObject GenerateCenterChunk()
    {
        Vector3 referencePosition = new Vector3(
            this.transform.position.x,
            this.transform.position.y,
            this.transform.position.z
        );

        GameObject chunk = MonoBehaviour.Instantiate(
            Resources.Load("Prefabs/Map/Map Generator") as GameObject,
            referencePosition,
            Quaternion.identity
        );

        Vector2 initPosition = new Vector2(
            referencePosition.x,
            referencePosition.z
        );

        ProceduralMapGenerator chunkGenerator = chunk.GetComponent<ProceduralMapGenerator>();
        chunkGenerator.Generate(initPosition);

        return chunk;
    }

    /**
     * <summary>
     * Generate top left chunk
     * </summary>
     *
     * <param name="position">Vector2 position</param>
     * 
     * <returns>
     * GameObject
     * </returns>
     */
    public GameObject GenerateTopLeftChunk()
    {
        Vector3 referencePosition = new Vector3(
            this.transform.position.x,
            this.transform.position.y,
            this.transform.position.z + 100f
        );

        GameObject chunk = MonoBehaviour.Instantiate(
            Resources.Load("Prefabs/Map/Map Generator") as GameObject,
            referencePosition,
            Quaternion.identity
        );

        Vector2 initPosition = new Vector2(
            referencePosition.x,
            referencePosition.z
        );

        ProceduralMapGenerator chunkGenerator = chunk.GetComponent<ProceduralMapGenerator>();
        chunkGenerator.Generate(initPosition);

        return chunk;
    }

    /**
     * <summary>
     * Generate top right chunk
     * </summary>
     *
     * <param name="position">position</param>
     * 
     * <returns>
     * GameObject
     * </returns>
     */
    public GameObject GenerateTopRightChunk()
    {
        Vector3 referencePosition = new Vector3(
            this.transform.position.x + 100,
            this.transform.position.y,
            this.transform.position.z
        );

        GameObject chunk = MonoBehaviour.Instantiate(
            Resources.Load("Prefabs/Map/Map Generator") as GameObject,
            referencePosition,
            Quaternion.identity
        );

        Vector2 initPosition = new Vector2(
            referencePosition.x,
            referencePosition.z
        );

        ProceduralMapGenerator chunkGenerator = chunk.GetComponent<ProceduralMapGenerator>();
        chunkGenerator.Generate(initPosition);

        return chunk;
    }

    /**
     * <summary>
     * Generate bottom left chunk
     * </summary>
     *
     * <returns>
     * GameObject
     * </returns>
     */
    public GameObject GenerateBottomLeftChunk()
    {
        Vector3 referencePosition = new Vector3(
            this.transform.position.x - 100,
            this.transform.position.y,
            this.transform.position.z
        );

        GameObject chunk = MonoBehaviour.Instantiate(
            Resources.Load("Prefabs/Map/Map Generator") as GameObject,
            referencePosition,
            Quaternion.identity
        );

        Vector2 initPosition = new Vector2(
            referencePosition.x,
            referencePosition.z
        );

        ProceduralMapGenerator chunkGenerator = chunk.GetComponent<ProceduralMapGenerator>();
        chunkGenerator.Generate(initPosition);

        return chunk;
    }

    /**
     * <summary>
     * Generate bottom right chunk
     * </summary>
     *
     * <param name="position">Vector2 position</param>
     * 
     * <returns>
     * GameObject
     * </returns>
     */
    public GameObject GenerateBottomRightChunk()
    {
        Vector3 referencePosition = new Vector3(
            this.transform.position.x,
            this.transform.position.y,
            this.transform.position.z - 100
        );

        GameObject chunk = MonoBehaviour.Instantiate(
            Resources.Load("Prefabs/Map/Map Generator") as GameObject,
            referencePosition,
            Quaternion.identity
        );

        Vector2 initPosition = new Vector2(
            referencePosition.x,
            referencePosition.z
        );

        ProceduralMapGenerator chunkGenerator = chunk.GetComponent<ProceduralMapGenerator>();
        chunkGenerator.Generate(initPosition);

        return chunk;
    }

    /**
     * <summary>
     * Generate left chunk
     * </summary>
     *
     * <param name="position">Vector2 position</param>
     * 
     * <returns>
     * GameObject
     * </returns>
     */
    public GameObject GenerateLeftChunk()
    {
        Vector3 referencePosition = new Vector3(
            this.transform.position.x - 100,
            this.transform.position.y,
            this.transform.position.z + 100
        );

        GameObject chunk = MonoBehaviour.Instantiate(
            Resources.Load("Prefabs/Map/Map Generator") as GameObject,
            referencePosition,
            Quaternion.identity
        );

        Vector2 initPosition = new Vector2(
            referencePosition.x,
            referencePosition.z
        );

        ProceduralMapGenerator chunkGenerator = chunk.GetComponent<ProceduralMapGenerator>();
        chunkGenerator.Generate(initPosition);

        return chunk;
    }

    /**
     * <summary>
     * Generate right chunk
     * </summary>
     *
     * <returns>
     * GameObject
     * </returns>
     */
    public GameObject GenerateRightChunk()
    {
        Vector3 referencePosition = new Vector3(
            this.transform.position.x + 100,
            this.transform.position.y,
            this.transform.position.z - 100
        );

        GameObject chunk = MonoBehaviour.Instantiate(
            Resources.Load("Prefabs/Map/Map Generator") as GameObject,
            referencePosition,
            Quaternion.identity
        );

        Vector2 initPosition = new Vector2(
            referencePosition.x,
            referencePosition.z
        );

        ProceduralMapGenerator chunkGenerator = chunk.GetComponent<ProceduralMapGenerator>();
        chunkGenerator.Generate(initPosition);

        return chunk;
    }

    /**
     * <summary>
     * Generate top chunk
     * </summary>
     *
     * <returns>
     * GameObject
     * </returns>
     */
    public GameObject GenerateTopChunk()
    {
        Vector3 referencePosition = new Vector3(
            this.transform.position.x + 100,
            this.transform.position.y,
            this.transform.position.z + 100
        );

        GameObject chunk = MonoBehaviour.Instantiate(
            Resources.Load("Prefabs/Map/Map Generator") as GameObject,
            referencePosition,
            Quaternion.identity
        );

        Vector2 initPosition = new Vector2(
            referencePosition.x,
            referencePosition.z
        );

        ProceduralMapGenerator chunkGenerator = chunk.GetComponent<ProceduralMapGenerator>();
        chunkGenerator.Generate(initPosition);

        return chunk;
    }

    /**
     * <summary>
     * Generate bottom chunk
     * </summary>
     *
     * <returns>
     * GameObject
     * </returns>
     */
    public GameObject GenerateBottomChunk()
    {
        Vector3 referencePosition = new Vector3(
            this.transform.position.x - 100,
            this.transform.position.y,
            this.transform.position.z - 100
        );

        GameObject chunk = MonoBehaviour.Instantiate(
            Resources.Load("Prefabs/Map/Map Generator") as GameObject,
            referencePosition,
            Quaternion.identity
        );

        Vector2 initPosition = new Vector2(
            referencePosition.x,
            referencePosition.z
        );

        ProceduralMapGenerator chunkGenerator = chunk.GetComponent<ProceduralMapGenerator>();
        chunkGenerator.Generate(initPosition);

        return chunk;
    }

    /**
      * <summary>
      * Set position
      * </summary>
      *
      * <param name="position">Vector2 position</param>
      * 
      * <returns>
      * void
      * </returns>
      */
    public void SetPosition(Vector2 position)
    {
        this.position = position;
    }

    /**
     * <summary>
     * Get position
     * </summary>
     *
     * <returns>
     * Vector2 this.position
     * </returns>
     */
    public Vector2 GetPosition()
    {
        return this.position;
    }
}
