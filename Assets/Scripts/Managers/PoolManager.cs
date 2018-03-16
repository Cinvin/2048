using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {
    public GameObject BlockPrefab;
    public Transform BlockHolder;
    public Stack<GameObject> BlockPool;
	void Awake () {
        BlockPool = new Stack<GameObject>(16);
	}
	public Block SpawnBlock()
    {
        if (BlockPool.Count > 0)
        {
            GameObject block = BlockPool.Pop();
            block.SetActive(true);
            return block.GetComponent<Block>();
        }
        GameObject gameObject = GameObject.Instantiate(BlockPrefab);
        gameObject.transform.SetParent(BlockHolder);
        return gameObject.GetComponent<Block>();
    }
    public void DestroyBlock(GameObject block)
    {
        block.SetActive(false);
        BlockPool.Push(block);
    }
    public void CleanBlocks()
    {
        foreach(Transform block in BlockHolder)
        {
            DestroyBlock(block.gameObject);
        }
    }
}
