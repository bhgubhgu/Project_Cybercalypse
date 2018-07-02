using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSwap : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    /// <summary>
    /// 작성자 : 윤동준, 구용모
    /// 스크립트 : 슬롯의 들어가는 객체들을 UI에서 변경할 수 있게 하는 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>

    TestChangeSkillSlot skillsInfo;

    private Vector3 mousePosition;

    private Transform prevParent;
    public CanvasScaler scaler;

    private Transform mySlot;
    private Transform slotGroup;

    private List<Sprite> imageList;
    private List<Vector2> vectorList;

    [SerializeField]
    private int slotIndex;

    private void Awake()
    {
        skillsInfo = this.gameObject.GetComponent<TestChangeSkillSlot>();
        slotIndex = transform.GetComponent<TestChangeSkillSlot>().slotIndex;
    }

    // Use this for initialization
    void Start ()
    {
        imageList = new List<Sprite>();
        vectorList = new List<Vector2>();

        mySlot = transform.parent;
        slotGroup = mySlot.parent;

        //!< 최초에 각 슬롯들이 배치되어 있는 상태(Sprite, Width, Height)를 저장한다.
        for (int i = 0; i < 6; i++)
        {
            imageList.Add(slotGroup.GetChild(i).GetComponent<Image>().sprite);
            vectorList.Add(slotGroup.GetChild(i).GetComponent<RectTransform>().sizeDelta);
        }
        scaler = slotGroup.parent.parent.GetComponent<CanvasScaler>();
    }

    private void setImagePosition(Transform _transform)
    {
        string name = _transform.parent.name;
        if (name.Equals("Skill_1"))
            _transform.localPosition = new Vector3(7.0f, 0.0f);
        else if (name.Equals("Skill_2") || name.Equals("Skill_3") || name.Equals("Skill_4") || name.Equals("Skill_5"))
            _transform.localPosition = new Vector3(2.0f, 0.0f);
        else if (name.Equals("Skill_6"))
            _transform.localPosition = new Vector3(-5.0f, 0.0f);
    }

    //!< 드래그 시작되면 해당 오브젝트의 부모를 바꿔준다(위에 렌더링 되기 위함)
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent.SetAsLastSibling();
        
        prevParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        //!< ViewPort 에서의 위치를 받아서 0은 0, x가 1인건 1920, y가 1인건 1080으로 본다.

        mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);

        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y);
    }

    //!< 첫번째 슬롯은 7, 다른 슬롯들은 2, 마지막 슬롯은 -5
    public void OnDrop(PointerEventData eventData)  //!< 드래그가 끝나면 드래그 하고 있었던 오브젝트에서 호출됨
    {
        //이곳에서 바뀐다.
        TestChangeSkillSlot dragingSlot = eventData.pointerDrag.transform.gameObject.GetComponent<TestChangeSkillSlot>();

        ASkill.Skill dragingSkill;
        ASkill.Skill currentSkill;

        dragingSkill = dragingSlot.skillOffset.CheckSlotSkill(dragingSlot.slotIndex);
        currentSkill = skillsInfo.skillOffset.CheckSlotSkill(skillsInfo.slotIndex);

        dragingSlot.skill = currentSkill;//currentSkill; //새로 들어온 스킬
        dragingSlot.skillOffset.ChangeSlot(dragingSlot.skill, dragingSlot.slotIndex);//왼쪽 : 바꾸려는 스킬, 오른쪽 : 현재 등록된 스킬 인덱스
        dragingSlot.currentSlotSkill = dragingSlot.skill;
        dragingSlot.skillIndex = dragingSlot.skillOffset.CheckSkillIndex(dragingSlot.currentSlotSkill);

        skillsInfo.skill = dragingSkill;
        skillsInfo.skillOffset.ChangeSlot(skillsInfo.skill, skillsInfo.slotIndex);
        skillsInfo.currentSlotSkill = skillsInfo.skill;
        skillsInfo.skillIndex = skillsInfo.skillOffset.CheckSkillIndex(skillsInfo.currentSlotSkill);

        //여기까지 바꾸는 공간

        //GameObject other = eventData.pointerDrag;

        Image targetSlotImage = transform.GetComponent<Image>();
        //Image targetSlotAlpha = transform.parent.Find("Alpha").GetComponent<Image>();
        Image originSlotImage = eventData.pointerDrag.GetComponent<Image>();
        //Image originSlotAlpha = eventData.pointerDrag.transform.parent.Find("Alpha").GetComponent<Image>();

        Sprite _sprite = targetSlotImage.sprite;
        /*CGameManager.skillSprites[slotIndex] = targetSlotImage.sprite = originSlotImage.sprite;
        CGameManager.skillSprites[eventData.pointerDrag.GetComponent<ItemSwap>().slotIndex] = 
            originSlotImage.sprite = _sprite;*/

        Transform prevParent = eventData.pointerDrag.transform;
        eventData.pointerDrag.transform.GetChild(0).SetParent(transform);
        transform.GetChild(0).SetParent(prevParent);
        transform.GetChild(0).localPosition = new Vector3(0, 0);

        //Color alpha = targetSlotAlpha.color;
        //targetSlotAlpha.color = originSlotAlpha.color;
        //transform.GetComponent<Image>().color = eventData.pointerDrag.transform.parent.Find("Alpha").GetComponent<Image>().color;
        //eventData.pointerDrag.transform.parent.Find("Alpha").GetComponent<Image>().color = alpha;

        /*CGameManager.skillIndex[slotIndex] = transform.GetComponent<TestChangeSkillSlot>().skillIndex;
        CGameManager.skillIndex[eventData.pointerDrag.GetComponent<TestChangeSkillSlot>().slotIndex]
            = eventData.pointerDrag.GetComponent<TestChangeSkillSlot>().skillIndex;*/

        /*prevParent = eventData.pointerDrag.transform.parent;
        eventData.pointerDrag.transform.SetParent(transform.parent);
        transform.SetParent(prevParent);........00..00
        setImagePosition(transform);*/
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        setImagePosition(transform);
        transform.GetChild(0).localPosition = new Vector3(0, 0);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    private void OrganizeSlot()
    {
        for (int i = 0; i < 6; i++)
        {
            slotGroup.GetChild(i).GetComponent<Image>().sprite = imageList[i];
            slotGroup.GetChild(i).GetComponent<RectTransform>().sizeDelta = vectorList[i];
        }

    }
}

//!< -6.06 -0.21