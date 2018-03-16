using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour {

    public Transform Grid;
    private Vector3[,] slots;

    public void HideSlots()
    {                             
        Grid.gameObject.SetActive(false);
    }
    public void ShowSlots()
    {
        Grid.gameObject.SetActive(true);
    }

    public Vector3 GetSlot(int i,int j)
    {
        return slots[i, j];
    }
    
    void Awake () {
        slots=new Vector3[4,4];
        if (Grid != null)
        {
            int i=0, j = 0;
            foreach (Transform slot in Grid)
            {
                //Debug.Log(slot.name);
                if (j <= 3)
                {
                    slots[i, j] = slot.position;
                    j++;
                }
                else
                {
                    if (i <= 3)
                    {
                        i++;j = 0;
                        slots[i, j] = slot.position;
                        j++;
                    }
                }
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
