using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {

    [Header("Blocks")]
    public GameObject[] BlockOptions;
    private GameObject[] Blocks;

    private void Start()
    {
        
    }

    public void SpawnLeft()
    {
        int rand = UnityEngine.Random.Range(0, BlockOptions.Length - 1);

    }

    public void SpawnRight()
    {

    }
}
