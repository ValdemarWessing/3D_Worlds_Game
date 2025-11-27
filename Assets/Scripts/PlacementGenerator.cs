using UnityEngine;

public class PlacementGenerator : MonoBehaviour
{
    [System.Serializable]
    public class PrefabGroup
    {
        public string name = "Group";          // optional label in the Inspector
        public GameObject[] prefabs;           // prefabs in this group
        public int density = 10;               // how many to place
        public float minHeight = 0f;           // min allowed height
        public float maxHeight = 100f;         // max allowed height
        public Vector2 xRange = new Vector2(0, 200);
        public Vector2 zRange = new Vector2(0, 200);
    }

    [SerializeField] PrefabGroup[] groups;

    public void Generate()
    {
        Clear();

        foreach (var group in groups)
        {
            if (group.prefabs == null || group.prefabs.Length == 0)
                continue;

            for (int i = 0; i < group.density; i++)
            {
                float sampleX = Random.Range(group.xRange.x, group.xRange.y);
                float sampleZ = Random.Range(group.zRange.x, group.zRange.y);
                Vector3 rayStart = new Vector3(sampleX, group.maxHeight, sampleZ);

                if (!Physics.Raycast(rayStart, Vector3.down, out RaycastHit hitInfo, Mathf.Infinity))
                    continue;
                if (hitInfo.point.y < group.minHeight)
                    continue;

                GameObject prefab = group.prefabs[Random.Range(0, group.prefabs.Length)];
                GameObject instance = Instantiate(prefab, hitInfo.point, Quaternion.identity, transform);
            }
        }
    }

    public void Clear()
    {
        while (transform.childCount != 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    void Start()
    {
        Generate(); // Optional: auto-generate when scene starts
    }
}