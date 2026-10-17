using UnityEngine;

public class HouseSpawner : MonoBehaviour
{
    [Header("Ќастройки домов")]
    public int count = 6;              // сколько домов (всего)
    public float startZ = 40f;         // от какого Z начинать
    public float spacingZ = 35f;       // рассто€ние между домами по Z
    public float sideX = 10f;          // на сколько от центра (по бокам)
    public bool alternateSides = true; // чередовать лево/право

    [Header("–азмер дома")]
    public Vector3 houseScale = new Vector3(4f, 6f, 4f);

    [Header("ћатериал")]
    public Material houseMaterial;

    [ContextMenu("1. Spawn Houses")]
    void SpawnAll()
    {
        for (int i = 0; i < count; i++)
        {
            // вычисл€ем сторону 
            float x;
            if (alternateSides)
                x = (i % 2 == 0) ? -sideX : sideX;
            else
                x = -sideX;

            float z = startZ + i * spacingZ;
            Vector3 pos = new Vector3(x, houseScale.y / 2f, z);

            GameObject house = GameObject.CreatePrimitive(PrimitiveType.Cube);
            house.name = "House_" + (i + 1);
            house.transform.position = pos;
            house.transform.localScale = houseScale;

            // убираем коллайдер
            DestroyImmediate(house.GetComponent<Collider>());

            // красим
            if (houseMaterial != null)
                house.GetComponent<Renderer>().sharedMaterial = houseMaterial;

            house.transform.SetParent(transform);
        }
        Debug.Log($"—оздано {count} домов");
    }

    [ContextMenu("2. Clear Houses")]
    void ClearAll()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        Debug.Log("ƒома удалены");
    }
}