using UnityEngine;

public class PillarSpawner : MonoBehaviour
{
    [Header("Настройки столбов")]
    public int count = 24;
    public float spacing = 10f;
    public float startZ = 10f;
    public float sideX = 5f;

    [Header("Размер столба")]
    public Vector3 pillarScale = new Vector3(0.5f, 4f, 0.5f);

    [Header("Материал")]
    public Material pillarMaterial;

    [ContextMenu("1. Spawn Pillars")]
    void SpawnAll()
    {
        SpawnRow(-sideX, "Pillar_L_");
        SpawnRow(sideX, "Pillar_R_");
        Debug.Log($"Создано {count * 2} столбов в редакторе");
    }

    [ContextMenu("2. Clear Pillars")]
    void ClearAll()
    {
        
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        Debug.Log("Столбы удалены");
    }

    void SpawnRow(float xPos, string namePrefix)
    {
        for (int i = 0; i < count; i++)
        {
            float z = startZ + i * spacing;
            Vector3 pos = new Vector3(xPos, pillarScale.y / 2f, z);

            GameObject pillar = GameObject.CreatePrimitive(PrimitiveType.Cube);
            pillar.name = namePrefix + (i + 1);
            pillar.transform.position = pos;
            pillar.transform.localScale = pillarScale;

            // удаляем коллайдер 
            DestroyImmediate(pillar.GetComponent<Collider>());

            if (pillarMaterial != null)
                pillar.GetComponent<Renderer>().sharedMaterial = pillarMaterial;

            pillar.transform.SetParent(transform);
        }
    }
}