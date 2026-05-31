using UnityEngine;

public class RoadTextureScroll : MonoBehaviour
{
    [Header("Íàñòðîéêè")]
    public float scrollSpeed = 1f;      // Ñêîðîñòü ïðîêðóòêè òåêñòóðû
    public int terrainLayerIndex = 0;   // Èíäåêñ ñëîÿ TerrainLayer (îáû÷íî 0)

    private Terrain terrain;
    private TerrainLayer[] terrainLayers;
    private float offset;

    void Start()
    {
        // Ïîëó÷àåì êîìïîíåíò Terrain
        terrain = GetComponent<Terrain>();
        if (terrain != null)
        {
            terrainLayers = terrain.terrainData.terrainLayers;
        }
        else
        {
            Debug.LogError("Terrain êîìïîíåíò íå íàéäåí íà îáúåêòå " + gameObject.name);
        }
    }

    void Update()
    {
        if (terrain != null && terrainLayers != null && terrainLayers.Length > terrainLayerIndex)
        {
            // Ñìåùàåì òåêñòóðó ïî îñè Y (âïåðåä/íàçàä)
            offset += scrollSpeed * Time.deltaTime;

            // Ñîçäàåì íîâûé âåêòîð ñìåùåíèÿ äëÿ âûáðàííîãî ñëîÿ
            Vector2 newOffset = new Vector2(0, offset);

            // Ïðèìåíÿåì ñìåùåíèå ê òåêñòóðå òåððåéíà
            terrainLayers[terrainLayerIndex].tileOffset = newOffset;

            // Îáíîâëÿåì òåððåéí
            //terrain.terrainData.SetTerrainLayers(terrainLayers, true);
        }
    }

    // Ìåòîä äëÿ èçìåíåíèÿ ñêîðîñòè ïðîêðóòêè èç äðóãèõ ñêðèïòîâ
    public void SetScrollSpeed(float newSpeed)
    {
        scrollSpeed = newSpeed;
    }

    // Ìåòîä äëÿ ïîëó÷åíèÿ òåêóùåé ñêîðîñòè
    public float GetScrollSpeed()
    {
        return scrollSpeed;
    }

    // Ìåòîä äëÿ ñáðîñà ïðîêðóòêè
    public void ResetScroll()
    {
        offset = 0;
        if (terrain != null && terrainLayers != null && terrainLayers.Length > terrainLayerIndex)
        {
            terrainLayers[terrainLayerIndex].tileOffset = Vector2.zero;
            //terrain.terrainData.SetTerrainLayers(terrainLayers, true);
        }
    }
}