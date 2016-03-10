using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TGMap : MonoBehaviour {

    public int size_x = 32;
    public int size_z = 18;
    public float tileSize = 1.0f;
    public int tileResolution = 16;
    public Texture2D tile_texture;
    public Transform rightTop;
    TileMap map;
    //public int numTiles = 4;

    void Start () {
        BuildMesh();
    }

    void setRightTop()
    {
        rightTop.position = new Vector3((float)size_x - (float)0.5, rightTop.position.y, (float)size_z - (float)0.5);
        Debug.Log("Done Texture!");
    }

    Color[][] chopUpTiles()
    {
        int numTilesPerRow = tile_texture.width / tileResolution;
        int numRows = tile_texture.height / tileResolution;
        Color[][] tiles = new Color[numTilesPerRow * numRows][];

        for(int y=0; y<numRows; y++)
        {
            for(int x=0; x<numTilesPerRow; x++)
            {
                tiles[y*numTilesPerRow + x] = tile_texture.GetPixels(x * tileResolution, y * tileResolution, tileResolution, tileResolution);
            }
        }
        return tiles;
        //Color[] p = tile_texture.GetPixels(tileResolution, 0, tileResolution, tileResolution);
    }

    void BuildTexture()
    {
        //int tileResolution = tile_texture.height;
        
        int texWidth = size_x * tileResolution;
        int texHeight = size_z * tileResolution;
        Debug.Log(((float)size_x - (float)0.5) + ", " + rightTop.position.y + ", " + ((float)size_z - (float)0.5));
        Texture2D texture = new Texture2D(texWidth, texHeight);
        Color[][] tiles = chopUpTiles();

        for (int y = 0; y < size_z; y++)
        {
            for (int x = 0; x < size_x; x++)
            {
                Color[] p = tiles[map.GetTile(x, y)];
                texture.SetPixels(x * tileResolution, y * tileResolution, tileResolution, tileResolution, p);
            }
        }
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.Apply();
        MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
        mesh_renderer.sharedMaterials[0].mainTexture = texture;
    }

    public void BuildMesh() {

        map = new TileMap(size_x, size_z);

        int vsize_x = size_x + 1;
        int vsize_z = size_z + 1;
        int numOfvertices = vsize_x * vsize_z;
        int numOftiles = size_x * size_z;
        int numOftriangles = numOftiles * 2;

        // Create Mesh data
        Vector3[] vertices = new Vector3[numOfvertices];
        Vector3[] normals = new Vector3[numOfvertices];
        Vector2[] uv = new Vector2[numOfvertices];
        int[] triangles = new int[numOftriangles * 3];
        
        int x, z;
        for (z = 0; z < vsize_z; z++)
        {
            for (x = 0; x < vsize_x; x++)
            {
                vertices[z * vsize_x + x] = new Vector3(x * tileSize, 0, z * tileSize);
                normals[z * vsize_x + x] = Vector3.up;
                uv[z * vsize_x + x] = new Vector2((float)x / size_x, (float)z / size_z);
            }
        }

        for (z = 0; z < size_z; z++)
        {
            for (x = 0; x < size_x; x++)
            {
                int squareIndex = z * size_x + x;
                int triOffset = squareIndex * 6;
                triangles[triOffset] = z * vsize_x + x;
                triangles[triOffset + 1] = (z + 1) * vsize_x + x;
                triangles[triOffset + 2] = z * vsize_x + x + 1;

                triangles[triOffset + 3] = (z + 1) * vsize_x + x;
                triangles[triOffset + 4] = (z + 1) * vsize_x + x + 1;
                triangles[triOffset + 5] = z * vsize_x + x + 1;
            }
        }

        // Create new Mesh
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;

        // Assign our mesh to mesh components.
        MeshFilter mesh_filter = GetComponent<MeshFilter>();
        MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
        MeshCollider mesh_collider = GetComponent<MeshCollider>();

        mesh_filter.mesh = mesh;
        mesh_collider.sharedMesh = mesh;
        setRightTop();
        BuildTexture();
	}
}
