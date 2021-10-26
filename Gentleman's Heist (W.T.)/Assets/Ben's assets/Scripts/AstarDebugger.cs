using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AstarDebugger : MonoBehaviour
{
    private static AstarDebugger instance;

    public static AstarDebugger MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AstarDebugger>();
            }

            return instance;
        }
    }

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private Tilemap tilemap;

    [SerializeField]
    private Tile tile;

    [SerializeField]
    private Color openColor, closedColor, pathColor, currentColor, startColor, goalColor;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private GameObject debugTextPrefab;

    private List<GameObject> debugObjects = new List<GameObject>();

    public void CreateTiles(HashSet<Node> openList,HashSet<Node> closedList,Dictionary<Vector3Int,Node> allNodes, Vector3Int start, Vector3Int goal, Stack<Vector3Int> path = null)
    {
        foreach (Node node in openList)
        {
            ColorTile(node.Position, openColor);
        }

        foreach (Node node in closedList)
        {
            ColorTile(node.Position, closedColor);
        }

        if (path != null)
        {
            foreach (Vector3Int pos in path)
            {
                if (pos != start && pos != goal)
                {
                    ColorTile(pos, pathColor);
                }
            }
        }

        ColorTile(start, startColor);
        ColorTile(goal, goalColor);

        foreach (KeyValuePair<Vector3Int,Node> node in allNodes)
        {
            if (node.Value.Parent != null)
            {
                //GameObject go = Instantiate(debugTextPrefab, canvas.transform);
                //go.transform.position = grid.CellToWorld(node.Key);
                //debugObjects.Add(go);
            }
        }
    }

    private void GenerateDebugText(Node node, DebugText debugtext)
    {

    }

    public void ColorTile(Vector3Int position, Color color)
    {
        tilemap.SetTile(position, tile);
        tilemap.SetTileFlags(position, TileFlags.None);
        tilemap.SetColor(position, color);
    }

    public void ShowHide()
    {
        canvas.gameObject.SetActive(!canvas.isActiveAndEnabled);
        Color c = tilemap.color;
        c.a = c.a != 0 ? 0 : 1;
        tilemap.color = c;
    }

    public void Reset(Dictionary<Vector3Int ,Node> allNodes)
    {
        foreach (GameObject go in debugObjects)
        {
            Destroy(go);
        }

        debugObjects.Clear();

        foreach (Vector3Int position in allNodes.Keys)
        {
            tilemap.SetTile(position, null);
        }

    }
}