using ToolBox.Pools;
using UnityEngine;

public class PoolableGraphic : MonoBehaviour
{
    public void Release(){
        gameObject.Release();
    }
}
