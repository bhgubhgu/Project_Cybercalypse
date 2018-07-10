using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSelect : MonoBehaviour
{
    private int selectSlotIndex = 0;

    private Vector3 initSelectPosition;

    private GameObject[] itemSlotList;
    private GameObject[] shopSlotList;
    private GameObject[] inventorySlotCategoryList;
    private GameObject[] inventoryShopSlotList;

    private GameObject playerInventory;
    private GameObject select;
    private GameObject weaponButton;
    private GameObject armorButton;
    private GameObject skillButton;
    private GameObject abilityButton;
    private GameObject weaponCategory;
    private GameObject armorCategory;
    private GameObject skillCategory;
    private GameObject abilityCategory;

    private GameObject shopInventory;

    private Button weaponBtn;
    private Button armorBtn;
    private Button skillBtn;
    private Button abilityBtn;

    private void Awake()
    {
        select = this.gameObject;
        initSelectPosition = this.GetComponent<RectTransform>().localPosition;

        playerInventory = GameObject.Find("Player Inventory").gameObject;
        shopInventory = GameObject.Find("Shop Inventory").gameObject;

        weaponButton = GameObject.Find("Weapon Button").gameObject;
        armorButton = GameObject.Find("Armor Button").gameObject;
        skillButton = GameObject.Find("Skill Button").gameObject;
        abilityButton = GameObject.Find("Ability Button").gameObject;

        weaponCategory = GameObject.Find("Weapon Category Inventory").gameObject;
        armorCategory = GameObject.Find("Armor Category Inventory").gameObject;
        skillCategory = GameObject.Find("Skill Category Inventory").gameObject;
        abilityCategory = GameObject.Find("Ability Category Inventory").gameObject;

        weaponBtn = weaponButton.GetComponent<Button>();
        armorBtn = armorButton.GetComponent<Button>();
        skillBtn = skillButton.GetComponent<Button>();
        abilityBtn = abilityButton.GetComponent<Button>();

        itemSlotList = new GameObject[6]; //인벤토리 안의 아이템 슬롯
        shopSlotList = new GameObject[9 + itemSlotList.Length]; //상점 안의 아이템 슬롯( + 인벤토리까지)
        inventorySlotCategoryList = new GameObject[3]; //인벤토리 - 카테고리 버튼 - 장착슬롯
        inventoryShopSlotList = new GameObject[2]; // 인벤토리 - 상점
    }

    private void OnEnable()
    {
        selectSlotIndex = 0;
        select.transform.localPosition = initSelectPosition;
    }

    private void Start()
    {
        for (int i = 0; i < itemSlotList.Length; i++)
        {
            itemSlotList[i] = playerInventory.transform.GetChild(i).gameObject;
        }

        for (int i = 0, j = 3, k = 6, x = 9, y = 12; i < 3; i++, j++, k++, x++, y++)
        {
            shopSlotList[i] = playerInventory.transform.GetChild(i).gameObject;
            shopSlotList[j] = shopInventory.transform.GetChild(i).gameObject;
            shopSlotList[k] = playerInventory.transform.GetChild(j).gameObject;
            shopSlotList[x] = shopInventory.transform.GetChild(j).gameObject;
            shopSlotList[y] = shopInventory.transform.GetChild(k).gameObject;
        }

        CInputManager.instance.HMenuMove += HInventorySlotMove;
        CInputManager.instance.VMenuMove += VInventorySlotMove;
        CInputManager.instance.HShopMove += HShopMove;
        CInputManager.instance.VShopMove += VShopMove;

        select.SetActive(false);
    }

    private void Update()
    {
        if(this.transform.position == weaponButton.transform.position && Input.GetKeyDown(KeyCode.Z))
        {
            weaponBtn.onClick.Invoke();
        }
        else if (this.transform.position == armorButton.transform.position && Input.GetKeyDown(KeyCode.Z))
        {
            armorBtn.onClick.Invoke();
        }
        else if (this.transform.position == skillButton.transform.position && Input.GetKeyDown(KeyCode.Z))
        {
            skillBtn.onClick.Invoke();
        }
        else if (this.transform.position == abilityButton.transform.position && Input.GetKeyDown(KeyCode.Z))
        {
            abilityBtn.onClick.Invoke();
        }
    }

    private void HInventorySlotMove(float inputHValue)
    {
        if (inputHValue > 0)
        {
            if (selectSlotIndex >= itemSlotList.Length - 1)
            {
                selectSlotIndex = -1;
            }

            select.transform.position = itemSlotList[++selectSlotIndex].transform.position;
        }
        else if (inputHValue < 0)
        {
            if (selectSlotIndex == 0)
            {
                selectSlotIndex = itemSlotList.Length;
            }

            select.transform.position = itemSlotList[--selectSlotIndex].transform.position;
        }
    }

    private void VInventorySlotMove(float inputVValue)
    {
        if (inputVValue > 0)
        {
            selectSlotIndex -= 3;

            if (selectSlotIndex < 0)
            {
                selectSlotIndex += 3;
                return;
            }

            select.transform.position = itemSlotList[selectSlotIndex].transform.position;
        }
        else if (inputVValue < 0)
        {
            selectSlotIndex += 3;

            if (selectSlotIndex > itemSlotList.Length - 1)
            {
                selectSlotIndex -= 3;
                return;
            }

            select.transform.position = itemSlotList[selectSlotIndex].transform.position;
        }
    }

    private void HShopMove(float inputHValue)
    {
        if (inputHValue > 0)
        {
            if (selectSlotIndex >= shopSlotList.Length - 1)
            {
                return;
            }

            select.transform.position = shopSlotList[++selectSlotIndex].transform.position;
        }
        else if (inputHValue < 0)
        {
            if (selectSlotIndex == 0)
            {
                return;
            }

            select.transform.position = shopSlotList[--selectSlotIndex].transform.position;
        }
    }

    private void VShopMove(float inputVValue)
    {
        if (inputVValue > 0)
        {
            if (selectSlotIndex >= 12 && selectSlotIndex < 15)
            {
                selectSlotIndex -= 3;
            }
            else if (selectSlotIndex < 0 || selectSlotIndex >= 0 && selectSlotIndex < 6)
            {
                return;
            }
            else
            {
                selectSlotIndex -= 6;
            }

            select.transform.position = shopSlotList[selectSlotIndex].transform.position;
        }
        else if (inputVValue < 0)
        {
            if(selectSlotIndex >= 9 && selectSlotIndex < 12)
            {
                selectSlotIndex += 3;
            }
            else if(selectSlotIndex >=6 && selectSlotIndex < 9)
            {
                return;
            }
            else
            {
                selectSlotIndex += 6;
            }

            if (selectSlotIndex > shopSlotList.Length - 1)
            {
                return;
            }

            select.transform.position = shopSlotList[selectSlotIndex].transform.position;
        }
    }
}
