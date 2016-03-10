using UnityEngine;
using System.Collections;

public class TileMap{

    int size_x;
    int size_y;
    int[,] map_data;
    

    int checkDir(int i1, int i2)
    {
        if (i1 > i2) {
            return -1;
        }
        else {
            return 1;
        }
    }

    void createLine(int[,] map_data, int[] w1, int[] w2, int tile_type)
    {
        int i = 0;
        int dir;
        if (w1[0] == w2[0])
        {
            i = w1[1];
            dir = checkDir(w1[1], w2[1]);
            while(i != w2[1])
            {
                map_data[w1[0], i] = tile_type;
                i += dir;
                //Debug.Log(i);
            }
        }
        else if (w1[1] == w2[1])
        {
            i = w1[0];
            dir = checkDir(w1[0], w2[0]);
            while (i != w2[0])
            {
                map_data[i, w1[1]] = tile_type;
                i += dir;
                //Debug.Log(i);
            }
        }
        else
        {
            //do what?
        }
    }

    void createConstructionSite(int left, int bot, int width, int height)
    {

    }

    void coverMapWithDefaultTexture()
    {

    }

    public TileMap(int width, int height)
    {
        size_x = width;
        size_y = height;
        map_data = new int[size_x, size_y];
        int x1, x2, y1, y2;
        x1 = Mathf.FloorToInt(size_x / 4);
        x2 = 3 * x1;
        y1 = Mathf.FloorToInt(size_y / 4);
        y2 = 3 * y1;
        createLine(map_data, new int[2] { x1, y1 }, new int[2] { x2, y1 }, 3);
        createLine(map_data, new int[2] { x1, y1 }, new int[2] { x1, y2 }, 3);
        createLine(map_data, new int[2] { x2, y1 }, new int[2] { x2, y2 }, 3);
    }

    public int GetTile(int x, int y)
    {
        if (x < 0 || x >= size_x || y < 0 || y >= size_y)
        {
            return -1;
        }
        return map_data[x, y];
    }
}
