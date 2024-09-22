using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    private List<string> poolTags = new List<string>();
    private List<GameObject> poolPrefabs = new List<GameObject>();
    private List<Queue<GameObject>> poolQueues = new List<Queue<GameObject>>();

    public void AddPrefab(string tag, GameObject prefab)
    {
        int index = poolTags.IndexOf(tag);
        if (index == -1)
        {
            poolTags.Add(tag);
            poolPrefabs.Add(prefab);
            poolQueues.Add(new Queue<GameObject>());
        }
        else
        {
            poolPrefabs[index] = prefab;
        }
    }
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        int index = poolTags.IndexOf(tag);
        if (index == -1)
        {
            return null;
        }

        Queue<GameObject> queue = poolQueues[index];
        GameObject objectToSpawn;

        if (queue.Count == 0)
        {
            objectToSpawn = Instantiate(poolPrefabs[index]);
            objectToSpawn.name = tag;
        }
        else
        {
            objectToSpawn = queue.Dequeue();
        }

        objectToSpawn.SetActive(true);
        
        objectToSpawn.transform.SetParent(null);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.transform.localScale = poolPrefabs[index].transform.localScale;
    
        if (parent != null)
        {
            objectToSpawn.transform.SetParent(parent, true);
        }

        return objectToSpawn;
    }

    public void ReturnToPool(string tag, GameObject objectToReturn)
    {
        int index = poolTags.IndexOf(tag);
        if (index == -1)
        {
            Destroy(objectToReturn);
            return;
        }

        objectToReturn.SetActive(false);
        objectToReturn.transform.SetParent(transform);
        poolQueues[index].Enqueue(objectToReturn);
    }

    public bool ContainsTag(string tag)
    {
        return poolTags.Contains(tag);
    }
}