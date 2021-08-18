using UnityEngine;
using ToolBox.Pools;
public class Spawner : MonoBehaviour
{

    public void Spawn(GameObject prefab)
    {
        print($"Spawner: Spawning {prefab.name}");
        GameObject clone = prefab.Get();
        clone.transform.position = transform.position;
    }
}
