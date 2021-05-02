using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Enemy Set")]
public class EnemySet : ScriptableObject
{
    [Serializable]
    public struct Entry
    {
        public GameObject prefab;
        public float weight;
    }

    public List<Entry> entries;

    public GameObject RandomChoice()
    {
        float sumOfWeights = entries.Select(entry => entry.weight).Sum();
        float t = Random.Range(0, sumOfWeights);

        foreach (Entry entry in entries)
        {
            if (t < entry.weight)
            {
                return entry.prefab;
            }
            t -= entry.weight;
        }
        return entries[0].prefab;
    }
}
