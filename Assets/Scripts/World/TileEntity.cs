using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileEntity : MonoBehaviour
{
    public float virtualZ;

    public Tilemap tileMap = null;

    public List<Vector3> availablePlaces;

    void Start()
    {
        tileMap = transform.GetComponentInParent<Tilemap>();
        availablePlaces = new List<Vector3>();

        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tileMap.transform.position.y));
                Vector3 place = tileMap.CellToWorld(localPlace);
                if (tileMap.HasTile(localPlace))
                {
                    //Tile at "place"
                    availablePlaces.Add(place);

                }
                else
                {
                    //No tile at "place"
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
