using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class AItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /// <summary>
    /// 작성자 : 윤동준, 김현우
    /// 스크립트 : 아이템,스킬,어빌리티 들의 속성을 갖고 있는 최상위 클래스(추상클래스)
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>

    /// <summary>
    /// !-- 아이템의 범주를 설명한다
    /// </summary>
    public enum EItemCategory { None, Consumable, Resource, Equipment, Talent };

    public abstract string ItemName { get; set; }
    public abstract string ItemDesc { get; set; }
    public abstract SpriteRenderer ItemIcon { get; set; }
    public abstract SpriteRenderer ItemSubs { get; set; }
    public abstract EItemCategory ItemCategory { get; set; }
    
    private Vector3 mousePosition;
    private Vector3 targetPosition;

    private Vector3 objPosition;

    private GameObject _object;

    private Canvas canvas;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _object = eventData.hovered[0];
    }

    public void OnDrag(PointerEventData eventData)
    {
        //!< ViewPort 에서의 위치를 받아서 0은 0, x가 1인건 1920, y가 1인건 1080으로 본다.
        //!< 
        //Debug.Log("Boo");

        mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        //targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        targetPosition = Camera.main.ScreenToViewportPoint(mousePosition);
        _object.transform.position
        /*eventData.pointerDrag.transform.position */= new Vector3(targetPosition.x * 1920, targetPosition.y * 1080);

        //Debug.Log(transform.position);
        //Debug.Log(eventData.hovered[0].transform.position);
        //Debug.Log(targetPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        for (int index = 0; index < eventData.hovered.Count; index++)
            if (eventData.hovered[index].tag.Equals("Swapoble"))
            {

            }
    }
}
