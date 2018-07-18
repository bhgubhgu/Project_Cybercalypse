using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CInventory : MonoBehaviour {

    public static GameObject weaponPanel;
    public static GameObject armorPanel;
    public static GameObject consumablePanel;

    public static GameObject skillPanel;
    public static GameObject abilityPanel;

    public static GameObject currentInventoryTab;

    public static int money = 0;

    //!<
    //!< ---

    //private CInventoryAbility[] inventoryAbilities;
    //private CInventorySkill[] inventorySkills;
    //private FixInventorySkill[] inventorySkills;
    //private static CSkill[] skills;

    public static GameObject[] skillInventory;  //!< 인벤토리(슬롯들)에 담겨있는 CSkill에 대한 참조(참조라는 것이 중요함.)
    public static GameObject[] abilityInventory;  //!< 인벤토리(슬롯들)에 담겨있는 CAbility에 대한 참조(참조라는 것이 중요함.)

    public static int SkillInventoryIndex { get; set; } //!< 
    public static int AbilityInventoryIndex { get; set; }

    public const int maxSlotCount = 32;

    private void Awake()
    {
        weaponPanel = GameObject.Find("Panel_Inventory_Weapon");
        armorPanel = GameObject.Find("Panel_Inventory_Armor");
        consumablePanel = GameObject.Find("Panel_Inventory_Consumable");

        skillPanel = GameObject.Find("Panel_Inventory_Skill");
        abilityPanel = GameObject.Find("Panel_Inventory_Ability");
    }

    // Use this for initialization
    void Start ()
    {
        currentInventoryTab = weaponPanel;
        Debug.Log(currentInventoryTab);

        armorPanel.SetActive(false);
        consumablePanel.SetActive(false);
        skillPanel.SetActive(false);
        abilityPanel.SetActive(false);

        //inventoryAbilities = new CInventoryAbility[maxSlotCount];
        //inventorySkills = new CInventorySkill[maxSlotCount];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivateTab(GameObject _object)
    {
        currentInventoryTab.SetActive(false);
        currentInventoryTab = _object;
        currentInventoryTab.SetActive(true);
        //DeActivateExcludkddeTab(_object);
    }

    public void DeActivateExcludeTab(GameObject _object)
    {

    }

    public static void AddItem(GameObject Item)
    {
        string tag = Item.tag;

        switch(tag)
        {
            case "Equipment":
                break;
            case "Consumable":
                break;
            case "Skill":
                //skillInventory[SkillInventoryIndex]
                var boo = GetEmptySlot(skillPanel);
                Debug.Log(boo.name);
                var emptySlot = GetEmptySlot(skillPanel);
                PasteComponentValues<CSkill>(Item.GetComponent<CSkill>(), GetEmptySlot(skillPanel));
                
                break;
            case "Ability":
                break;
        }
    }

    public static void AddItem(AItem.EItemCategory itemCategory)
    {
        switch(itemCategory)
        {
            case AItem.EItemCategory.Equipment:
                break;
            case AItem.EItemCategory.Consumable:
                break;
            case AItem.EItemCategory.Talent:
                break;
        }
    }

    public static GameObject GetEmptySlot(GameObject panel)
    {
        for (int i = 0; i < panel.transform.childCount; i++)
        {
            if (panel.transform.GetChild(i).GetComponent<CSlot>().IsEmpty)
                return panel.transform.GetChild(i).gameObject;
        }
        return null;
    }

    //private static T PasteComponentValues<T>(T original, GameObject destination) where T : Component
    //{
    //    System.Type type = original.GetType();
    //    Component copy = destination.GetComponent(type);
    //    System.Reflection.FieldInfo[] fields = type.GetFields();
    //    foreach (System.Reflection.FieldInfo field in fields)
    //    {
    //        field.SetValue(copy, field.GetValue(original));
    //    }

    //    return copy as T;
    //}

    private static void PasteComponentValues<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        var copy = destination.transform.GetChild(0).GetComponent(type);
        PropertyInfo[] properties = type.GetProperties();

        foreach (var property in properties)
        {
            if(property.DeclaringType.Equals(typeof(CSkill)))
            {
                Debug.Log(property);
                Debug.Log(property.GetValue(original));
                property.SetValue(copy, property.GetValue(original));
            }

            //Debug.Log(property.DeclaringType.Equals(typeof(AItem)));
            //if (property.IsDefined(property.GetType(), true))
            //{
            //    property.SetValue(copy, property.GetValue(original));
            //    Debug.Log(property);
            //    Debug.Log(property.GetValue(original));
            //}
        }
    }

    /* Legarcy Code
    /// <summary>
    /// 아이템을 인벤토리에 집어넣어주는 함수
    /// </summary>
    /// <typeparam name="T">AItem을 상속받는 모든 오브젝트는 인벤토리에 Get가능</typeparam>
    /// <param name="_item">인벤토리에 집어넣을 아이템 인스턴스</param>
    public void AddItem<T>(T _item) where T : AItem
    {
        switch(_item.ItemCategory)
        {
            case AItem.EItemCategory.Equipment:
                break;
            case AItem.EItemCategory.Consumable:
                break;
            case AItem.EItemCategory.Talent:
                if (_item.GetComponent<ATalent>().TalentCategory.Equals(ATalent.ETalentCategory.Ability))
                    Debug.Log("Ability");
                else
                    Debug.Log("Skill");
                break;
            default:
                Debug.Log("Not exist Item");
                break;
        }
    }

    public void GetMoney()
    {

    }

    static public T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy as T;
    }

    //!<인벤토리가 아니라 탭에 대한 클래스가 될 듯
    //!< 자기 자식에 있는 슬롯들에 대한 정보(갯수, 지금 빈 공간 등등) -> -s, -ies;
    //!< 
    //!< CShopSkill이나 CShopAbility등이 눌리면 Inventory의 AddItem을 호출할 것임.
    //!< Add할 Item을 매개변수로 받아서, 어떤 형식의 아이템인지 알아야 함.
    //!< Item의 ItemCategory를 따져서 장비/소비/특성을 가려낸다.
    //!< 만약 특성이라면, 스킬일 수도 어빌리티일 수도 있기 때문에 한번 더 가려야 한다.(그러나 Item으로는 불가능)
    //!< item의 GetType()에 따라서 
    public T FindObject<T>() where T : Component
    {
        for(int i=0; ;i++)
        {
            //if (!inventorySkills[i].IsContain)
            //{
            //    break;
            //}
        }

        while(true)
        {
            //foreach(var item in inventorySkills)
            //{
            //    if (item.IsContain.Equals(false))
            //        break;
            //}
        }
        Component component = new Component();
        return component as T;
    }
    */
}