using UnityEngine;
using UnityEngine.Tilemaps;

public class AutoHarvest : MonoBehaviour
{
    public int counter = 0;
    public Tilemap vegetableTilemap;      // assign in Inspector
    public float checkRadius = 0.1f;      // small radius to detect center

    void Update()
    {
        AutoHarvestTile();
    }

    void AutoHarvestTile()
    {
        Vector3Int baseCell = vegetableTilemap.WorldToCell(transform.position);

        HarvestCell(baseCell);

        
        Vector3Int upperCell = new Vector3Int(baseCell.x, baseCell.y + 1, baseCell.z);
        HarvestCell(upperCell);
    }

    void HarvestCell(Vector3Int cellPos)
    {
        TileBase tile = vegetableTilemap.GetTile(cellPos);

        if (tile != null)
        {
            vegetableTilemap.SetTile(cellPos, null);
            counter++;
            if(counter % 2 == 0){
                Debug.Log("Crop Harvested: " + counter / 2);
            }
        }
    }
}