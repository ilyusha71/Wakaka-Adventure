using System.Collections.Generic;
using UnityEngine;

public static class HexagonMesh
{
    // 定義正六邊形（邊朝上）
    public static int side = 63; // 邊長（外接圓半徑）
    public static float height = side * Mathf.Sqrt(3); // 高（內接圓半徑）
    // 定義六個頂點與中心的相對座標（從左上開始）
    public static Vector2[] vertex = new Vector2[6] 
    {
        new Vector2(-side*0.5f,height*0.5f),
        new Vector2(side*0.5f, height*0.5f),
        new Vector2(side, 0),
        new Vector2(side*0.5f,-height*0.5f),
        new Vector2(-side*0.5f, -height*0.5f),
        new Vector2(-side, 0)
    };
    // 計算六角網格數
    public static int CalculateHexGrids(int layer)
    {
        int count = 1;
        for (int i = 1; i < layer; i++) { count += 6 * i; }
        return count;
    }
    // 定義六角網格座標系，x正向為NE，y正向為NW，Z正向為S，並滿足x + y + z = 0
    public static Vector3 N(Vector3 coordinate) { coordinate += new Vector3(0, 1, -1); return coordinate; }
    public static Vector3 NE(Vector3 coordinate) { coordinate += new Vector3(1, 0, -1); return coordinate; }
    public static Vector3 SE(Vector3 coordinate) { coordinate += new Vector3(1, -1, 0); return coordinate; }
    public static Vector3 S(Vector3 coordinate) { coordinate += new Vector3(0, -1, 1); return coordinate; }
    public static Vector3 SW(Vector3 coordinate) { coordinate += new Vector3(-1, 0, 1); return coordinate; }
    public static Vector3 NW(Vector3 coordinate) { coordinate += new Vector3(-1, 1, 0); return coordinate; }
    // 六角網格陣列
    public static HexGrid[] grids;
    public static List<int> gridsIndex;
    // 六角座標轉笛卡爾座標
    public static Vector3 HexToCartesian(Vector3 hexagonal)
    {
        return new Vector3(
            hexagonal.x * side * 1.5f, 
            hexagonal.x * height * 0.5f + hexagonal.y * height, 
            0);
    }
    // 臨近網格搜索
    public static List<int> FindNeighborIndex(int index)
    {
        Vector3 coordinate = grids[index].gridCoordinate;
        // 從六個方位尋找存在的臨近網格
        List<int> neigborPossible = new List<int>();
        for (int i = 0; i < grids.Length; i++)
        {
            if (grids[i].gridCoordinate == N(coordinate) ||
                grids[i].gridCoordinate == NE(coordinate) ||
                grids[i].gridCoordinate == SE(coordinate) ||
                grids[i].gridCoordinate == S(coordinate) ||
                grids[i].gridCoordinate == SW(coordinate) ||
                grids[i].gridCoordinate == NW(coordinate))
            {
                neigborPossible.Add(i);
            }
        }
        return neigborPossible;
    }
    public static HexGrid[] FindNeighbor(Vector3 coordinate)
    {
        // 從六個方位尋找存在的臨近網格
        HexGrid[] neigborPossible = new HexGrid[6];
        int numNeighbor = 0;
        for (int i = 0; i < grids.Length; i++)
        {
            if (grids[i].gridCoordinate == N(coordinate) ||
                grids[i].gridCoordinate == NE(coordinate) ||
                grids[i].gridCoordinate == SE(coordinate) ||
                grids[i].gridCoordinate == S(coordinate) ||
                grids[i].gridCoordinate == SW(coordinate) ||
                grids[i].gridCoordinate == NW(coordinate))
            {
                neigborPossible[numNeighbor] = grids[i];
                numNeighbor++;
            }
        }
        // 重整陣列（刪除多餘空陣列）
        HexGrid[] neigbor = new HexGrid[numNeighbor];
        numNeighbor = 0;
        for (int i = 0; i < neigborPossible.Length; i++)
        {
            if (neigborPossible[i])
            {
                neigbor[numNeighbor] = neigborPossible[i];
                numNeighbor++;
            }
        }
        return neigbor;
    }
    public static HexGrid[] GetNeighbor(int index)
    {
        Vector3 coordinate = grids[index].gridCoordinate;
        // 從六個方位尋找存在的臨近網格
        HexGrid[] neigbor = new HexGrid[6];
        for (int i = 0; i < grids.Length; i++)
        {
            if (grids[i].gridCoordinate == N(coordinate))
                neigbor[0] = grids[i];
            else if (grids[i].gridCoordinate == NE(coordinate))
                neigbor[1] = grids[i];
            else if (grids[i].gridCoordinate == SE(coordinate))
                neigbor[2] = grids[i];
            else if (grids[i].gridCoordinate == S(coordinate))
                neigbor[3] = grids[i];
            else if (grids[i].gridCoordinate == SW(coordinate))
                neigbor[4] = grids[i];
            else if (grids[i].gridCoordinate == NW(coordinate))
                neigbor[5] = grids[i];
        }
        return neigbor;
    }

}
