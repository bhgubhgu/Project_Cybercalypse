using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSlotControl : MonoBehaviour {

    public List<GameObject> Slots;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < transform.childCount; i++)
        {
            Slots.Add(transform.GetChild(i).gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public List<GameObject> GetListRef()
    {
        return Slots;
    }

    public static GameObject FindEmptySlot(List<GameObject> Slots)
    {
        foreach (GameObject item in Slots)
        {
            //!< item이 비어있는지 확인한다 -> Image의 Sprite가 null이라면 비어있는 것
            if (item.GetComponent<UnityEngine.UI.Image>().sprite.Equals(null))
            {
                return item;    //!< 빈 슬롯이 존재한다
            }
        }
        return null;    //!< 빈 슬롯이 존재하지 않는다
    }
}
