using UnityEngine;

public class spawnManager : MonoBehaviour
{
    [SerializeField] Transform[] spawns;

    public Transform getSpawn(int id)
    {
        return spawns[id-1];
    }
}
