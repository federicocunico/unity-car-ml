using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMaze : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;
    public GameObject TileObject;
    public int MaximumTiles = 10;

    private Vector3 _currPos;
    private Vector3 _tileSize;
    private List<GameObject> _tiles = new List<GameObject>();

    private void spawnTile()
    {
        //var go = Instantiate(TileObject, _currPos, Quaternion.identity);
        var go = Instantiate(TileObject);
        go.transform.position = _currPos;
        _tiles.Add(go);
        _tileSize = go.GetComponentInChildren<Collider>().bounds.size;
    }

    void initMaze()
    {
        _currPos = StartPoint.position;
        spawnTile();
    }

    void generateMaze()
    {
        while (_tiles.Count < MaximumTiles)
        {
            // New random direction
            var rInt = Random.Range(0, 2);
            if (rInt == 0)
            {
                // Forward: +Z
                var newPos = new Vector3(0, 0, 0);
                newPos.z = _tileSize.z;
                _currPos = _currPos + newPos;

            }
            else if (rInt == 1)
            {
                // Left: -X
                var newPos = new Vector3(0, 0, 0);
                newPos.x = -_tileSize.x;
                _currPos = _currPos + newPos;

            }
            else if (rInt == 2)
            {
                // Right: +X
                var newPos = new Vector3(0, 0, 0);
                newPos.x = _tileSize.x;
                _currPos = _currPos + newPos;
            }
            else
            {
                throw new System.Exception();
            }

            spawnTile();
        }
    }

    void setArrival()
    {
        var arrival = _tiles[_tiles.Count - 1].transform.position;
        //var go = GameObject.Find("TrackArrival");
        EndPoint.transform.position = arrival;
    }

    // Start is called before the first frame update
    void Start()
    {
        initMaze();
        generateMaze();
        setArrival();
    }

    // Update is called once per frame
    void Update()
    {
        //generateMaze();

    }
}
