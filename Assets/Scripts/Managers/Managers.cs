using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatesManager))]
[RequireComponent(typeof(UIManager))]
[RequireComponent(typeof(SlotManager))]
[RequireComponent(typeof(BlockManager))]
[RequireComponent(typeof(PoolManager))]
[RequireComponent(typeof(StatsManager))]
public class Managers : MonoBehaviour {
    private static StatesManager states;
    public static StatesManager States { get { return states; } }

    private static UIManager ui;
    public static UIManager UI { get { return ui; } }

    private static SlotManager slot;
    public static SlotManager Slot { get { return slot; } }

    private static BlockManager block;
    public static BlockManager Block { get { return block; } }

    private static PoolManager pool;
    public static PoolManager Pool { get { return pool; } }

    private static StatsManager stats;
    public static StatsManager Stats { get { return stats; } }

    void Awake () {
        states = GetComponent<StatesManager>();
        ui = GetComponent<UIManager>();
        slot = GetComponent<SlotManager>();
        block = GetComponent<BlockManager>();
        pool = GetComponent<PoolManager>();
        stats = GetComponent<StatsManager>();
    }
	
	
	void Update () {
		
	}
}
