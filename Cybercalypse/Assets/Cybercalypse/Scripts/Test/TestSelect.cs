using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSelect : MonoBehaviour
{
#region select index
    private int selectSlotIndex = 0;
    private int selelctButtonIndex = 0;
    private int selectCategoryIndex = 0;
    #endregion

#region slot open check
    private bool isOpenWeaponSlot;
    private bool isOpenArmorSlot;
    private bool isOpenSkillSlot;
    private bool isOpenAbilitySlot;
    #endregion

#region select position
    private Vector3 initSelectPosition;
    #endregion

#region gameobject array
    private GameObject[] itemSlotList;
    private GameObject[] itemButtonList;
    private GameObject[] shopSlotList;
    private GameObject[] inventorySlotCategoryList;
    private GameObject[] inventoryShopSlotList;
    #endregion

#region gameobject
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
    #endregion

#region button
    private Button weaponBtn;
    private Button armorBtn;
    private Button skillBtn;
    private Button abilityBtn;
#endregion

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
        itemButtonList = new GameObject[4]; //인벤토리 안의 슬롯 버튼
        shopSlotList = new GameObject[9 + itemSlotList.Length]; //상점 안의 아이템 슬롯( + 인벤토리까지)
        inventorySlotCategoryList = new GameObject[4]; //인벤토리 - 카테고리 버튼 - 장착슬롯
        inventoryShopSlotList = new GameObject[2]; // 인벤토리 - 상점
    }

    private void OnEnable()
    {
        selectSlotIndex = 0;
        selelctButtonIndex = 0;
        select.transform.localPosition = initSelectPosition;
    }

    private void Start()
    {
        for (int i = 0; i < itemSlotList.Length; i++)
        {
            itemSlotList[i] = playerInventory.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < itemButtonList.Length; i++)
        {
            itemButtonList[i] = playerInventory.transform.GetChild(i + 7).gameObject;
        }

        for (int i = 0, j = 3, k = 6, x = 9, y = 12; i < 3; i++, j++, k++, x++, y++)
        {
            shopSlotList[i] = playerInventory.transform.GetChild(i).gameObject;
            shopSlotList[j] = shopInventory.transform.GetChild(i).gameObject;
            shopSlotList[k] = playerInventory.transform.GetChild(j).gameObject;
            shopSlotList[x] = shopInventory.transform.GetChild(j).gameObject;
            shopSlotList[y] = shopInventory.transform.GetChild(k).gameObject;
        }

        inventorySlotCategoryList[0] = weaponCategory;
        inventorySlotCategoryList[1] = armorCategory;
        inventorySlotCategoryList[2] = skillCategory;
        inventorySlotCategoryList[3] = abilityCategory;

        CInputManager.instance.HMenuMove += HInventorySlotMove;
        CInputManager.instance.VMenuMove += VInventorySlotMove;
        CInputManager.instance.HShopMove += HShopMove;
        CInputManager.instance.VShopMove += VShopMove;

        select.SetActive(false);
    }

    private void Update()
    {
        #region 키보드로 버튼 누르기
        if (this.transform.position == weaponButton.transform.position && Input.GetKeyDown(KeyCode.Z))
        {
            weaponBtn.onClick.Invoke();
            weaponBtn.GetComponent<Image>().sprite = weaponBtn.GetComponent<TestClickListner>().clickImage;
            isOpenWeaponSlot = true;
            isOpenSkillSlot = false;
            isOpenArmorSlot = false;
            isOpenAbilitySlot = false;
            selectCategoryIndex = 0;
        }
        else if (this.transform.position == armorButton.transform.position && Input.GetKeyDown(KeyCode.Z))
        {
            armorBtn.onClick.Invoke();
            armorButton.GetComponent<Image>().sprite = armorButton.GetComponent<TestClickListner>().clickImage;
            isOpenWeaponSlot = false;
            isOpenSkillSlot = false;
            isOpenArmorSlot = true;
            isOpenAbilitySlot = false;
            selectCategoryIndex = 0;
        }
        else if (this.transform.position == skillButton.transform.position && Input.GetKeyDown(KeyCode.Z))
        {
            skillBtn.onClick.Invoke();
            skillButton.GetComponent<Image>().sprite = skillButton.GetComponent<TestClickListner>().clickImage;
            isOpenWeaponSlot = false;
            isOpenSkillSlot = true;
            isOpenArmorSlot = false;
            isOpenAbilitySlot = false;
            selectCategoryIndex = 0;
        }
        else if (this.transform.position == abilityButton.transform.position && Input.GetKeyDown(KeyCode.Z))
        {
            abilityBtn.onClick.Invoke();
            abilityButton.GetComponent<Image>().sprite = abilityButton.GetComponent<TestClickListner>().clickImage;
            isOpenWeaponSlot = false;
            isOpenSkillSlot = false;
            isOpenArmorSlot = false;
            isOpenAbilitySlot = true;
            selectCategoryIndex = 0;
        }
        #endregion

#region 키보드로 버튼 누를때 색 바뀌는거 원래대로
        if (this.transform.position == weaponButton.transform.position && Input.GetKeyUp(KeyCode.Z))
        {
            weaponBtn.GetComponent<Image>().sprite = weaponBtn.GetComponent<TestClickListner>().initImage;
        }
        else if (this.transform.position == armorButton.transform.position && Input.GetKeyUp(KeyCode.Z))
        {
            armorButton.GetComponent<Image>().sprite = armorButton.GetComponent<TestClickListner>().initImage;
        }
        else if (this.transform.position == skillButton.transform.position && Input.GetKeyUp(KeyCode.Z))
        {
            skillButton.GetComponent<Image>().sprite = skillButton.GetComponent<TestClickListner>().initImage;
        }
        else if (this.transform.position == abilityButton.transform.position && Input.GetKeyUp(KeyCode.Z))
        {
            abilityButton.GetComponent<Image>().sprite = abilityButton.GetComponent<TestClickListner>().initImage;
        }
        #endregion

#region 상점에서 물건 사고 팔때
        if (((2 < selectSlotIndex && selectSlotIndex < 6) || (8 < selectSlotIndex && selectSlotIndex < 16)) && Input.GetKeyDown(KeyCode.Z) && TestShop.isShopOpen)
        {
            shopSlotList[selectSlotIndex].transform.GetChild(0).GetComponent<TestShopInventorySlot>().BuyItemUseKeyBoard();
        }
        else if((select.transform.position == itemSlotList[0].transform.position 
            || select.transform.position == itemSlotList[1].transform.position 
            || select.transform.position == itemSlotList[2].transform.position 
            || select.transform.position == itemSlotList[3].transform.position 
            || select.transform.position == itemSlotList[4].transform.position 
            || select.transform.position == itemSlotList[5].transform.position) &&  Input.GetKeyDown(KeyCode.Z) && TestShop.isShopOpen)
        {
            if(selectSlotIndex > 5 && selectSlotIndex < 9)
            {
                itemSlotList[selectSlotIndex - 3].transform.GetChild(0).GetComponent<TestPlayerInventorySlot>().SellItemUseKeyBoard();
            }
            else
            {
                itemSlotList[selectSlotIndex].transform.GetChild(0).GetComponent<TestPlayerInventorySlot>().SellItemUseKeyBoard();
            }
        }
        #endregion

#region 아이템 장착
        if ((select.transform.position == itemSlotList[0].transform.position || select.transform.position == itemSlotList[1].transform.position || select.transform.position == itemSlotList[2].transform.position || select.transform.position == itemSlotList[3].transform.position || select.transform.position == itemSlotList[4].transform.position || select.transform.position == itemSlotList[5].transform.position) && Input.GetKeyDown(KeyCode.Z) && TestPlayerInventoryOnOff.isOnInventory)
        {
            if (isOpenWeaponSlot)
            {
                inventorySlotCategoryList[0].transform.GetChild(0).transform.GetChild(0).GetComponent<TestPlayerEquipmentInventory>().SetItemUseKeyBoard(itemSlotList[selectSlotIndex]);
            }
            else if (isOpenArmorSlot)
            {
                inventorySlotCategoryList[1].transform.GetChild(0).transform.GetChild(0).GetComponent<TestPlayerEquipmentInventory>().SetItemUseKeyBoard(itemSlotList[selectSlotIndex]);
            }
            else if (isOpenSkillSlot)
            {
                inventorySlotCategoryList[2].transform.GetChild(0).transform.GetChild(0).GetComponent<TestPlayerSkillInventory>().SetItemUseKeyBoard(itemSlotList[selectSlotIndex]);
            }
            else if (isOpenAbilitySlot)
            {
                inventorySlotCategoryList[3].transform.GetChild(0).transform.GetChild(0).GetComponent<TestPlayerAbilityInventory>().SetItemUseKeyBoard(itemSlotList[selectSlotIndex]);
            }
        }
        #endregion

#region 아이템 해제
        if ((select.transform.position == inventorySlotCategoryList[0].transform.GetChild(0).transform.position 
            || select.transform.position == inventorySlotCategoryList[1].transform.GetChild(0).transform.position
            || select.transform.position == inventorySlotCategoryList[2].transform.GetChild(0).transform.position
            || select.transform.position == inventorySlotCategoryList[2].transform.GetChild(1).transform.position
            || select.transform.position == inventorySlotCategoryList[3].transform.GetChild(0).transform.position
            || select.transform.position == inventorySlotCategoryList[3].transform.GetChild(1).transform.position
            || select.transform.position == inventorySlotCategoryList[3].transform.GetChild(2).transform.position)
            && Input.GetKeyDown(KeyCode.Z) && TestPlayerInventoryOnOff.isOnInventory)
        {
            if(IsFullInventory())
            {
                return;
            }

            if (isOpenWeaponSlot)
            {
                inventorySlotCategoryList[0].transform.GetChild(0).transform.GetChild(0).GetComponent<TestPlayerEquipmentInventory>().ResetItemUseKeyBoard(CheckEmptyInventorySlot());
            }
            else if (isOpenArmorSlot)
            {
                inventorySlotCategoryList[1].transform.GetChild(0).transform.GetChild(0).GetComponent<TestPlayerEquipmentInventory>().ResetItemUseKeyBoard(CheckEmptyInventorySlot());
            }
            else if (isOpenSkillSlot)
            {
                inventorySlotCategoryList[2].transform.GetChild(0).transform.GetChild(0).GetComponent<TestPlayerSkillInventory>().ResetItemUseKeyBoard(CheckEmptyInventorySlot());
            }
            else if (isOpenAbilitySlot)
            {
                inventorySlotCategoryList[3].transform.GetChild(0).transform.GetChild(0).GetComponent<TestPlayerAbilityInventory>().ResetItemUseKeyBoard(CheckEmptyInventorySlot());
            }
        }
        #endregion
    }

    private void HInventorySlotMove(float inputHValue)
    {
        if (inputHValue > 0)
        {
            //버튼의 움직임들
            if (select.transform.position == itemButtonList[0].transform.position || select.transform.position == itemButtonList[1].transform.position || select.transform.position == itemButtonList[2].transform.position || select.transform.position == itemButtonList[3].transform.position || select.transform.position == inventorySlotCategoryList[2].transform.GetChild(0).transform.position || select.transform.position == inventorySlotCategoryList[2].transform.GetChild(1).transform.position || select.transform.position == inventorySlotCategoryList[3].transform.GetChild(0).transform.position || select.transform.position == inventorySlotCategoryList[3].transform.GetChild(1).transform.position || select.transform.position == inventorySlotCategoryList[3].transform.GetChild(2).transform.position)
            {
                if (isOpenWeaponSlot) //weapon slot 켜졌을때
                {
                    selectCategoryIndex = 0;

                    select.transform.position = inventorySlotCategoryList[0].transform.GetChild(selectCategoryIndex).transform.position;
                    select.GetComponent<RectTransform>().sizeDelta = inventorySlotCategoryList[0].transform.GetChild(0).transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;

                    return;
                }
                else if (isOpenArmorSlot) //armor slot 켜졌을때
                {
                    selectCategoryIndex = 0;

                    select.transform.position = inventorySlotCategoryList[1].transform.GetChild(selectCategoryIndex).transform.position;
                    select.GetComponent<RectTransform>().sizeDelta = inventorySlotCategoryList[1].transform.GetChild(0).transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;

                    return;
                }
                else if (isOpenSkillSlot) //skilll slot 켜졌을때
                {
                    select.transform.position = inventorySlotCategoryList[2].transform.GetChild(selectCategoryIndex++).transform.position;
                    select.GetComponent<RectTransform>().sizeDelta = inventorySlotCategoryList[2].transform.GetChild(0).transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;

                    if (selectCategoryIndex > 1)
                    {
                        selectCategoryIndex = 1;
                        return;
                    }

                    return;
                }
                else if (isOpenAbilitySlot) //ability slot 켜졌을때
                {
                    select.transform.position = inventorySlotCategoryList[3].transform.GetChild(selectCategoryIndex++).transform.position;
                    select.GetComponent<RectTransform>().sizeDelta = inventorySlotCategoryList[3].transform.GetChild(0).transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;

                    if (selectCategoryIndex > 2)
                    {
                        selectCategoryIndex = 2;
                        return;
                    }

                    return;
                }
                else
                {
                    return;
                }
            }

            if (selectSlotIndex == 2 || selectSlotIndex == 5)
            {
                select.transform.position = itemButtonList[0].transform.position;
                select.GetComponent<RectTransform>().sizeDelta = itemButtonList[0].GetComponent<RectTransform>().sizeDelta;
                return;
            }

            //인벤토리 내에서만의 움직임

            if (selectSlotIndex >= itemSlotList.Length - 1)
            {
                selectSlotIndex = -1;
            }

            select.transform.position = itemSlotList[++selectSlotIndex].transform.position;
            select.GetComponent<RectTransform>().sizeDelta = itemSlotList[selectSlotIndex].GetComponent<RectTransform>().sizeDelta;
        }
        else if (inputHValue < 0)
        {
            if (select.transform.position == itemButtonList[0].transform.position || select.transform.position == itemButtonList[1].transform.position || select.transform.position == itemButtonList[2].transform.position || select.transform.position == itemButtonList[3].transform.position)
            {
                selelctButtonIndex = 0;
                selectSlotIndex = 2;
                select.transform.position = itemSlotList[selectSlotIndex].transform.position;
                select.GetComponent<RectTransform>().sizeDelta = itemSlotList[selectSlotIndex].GetComponent<RectTransform>().sizeDelta;
                return;
            }

            if (select.transform.position == inventorySlotCategoryList[0].transform.GetChild(0).transform.position
                || select.transform.position == inventorySlotCategoryList[1].transform.GetChild(0).transform.position
                || select.transform.position == inventorySlotCategoryList[2].transform.GetChild(0).transform.position
                || select.transform.position == inventorySlotCategoryList[2].transform.GetChild(1).transform.position
                || select.transform.position == inventorySlotCategoryList[3].transform.GetChild(0).transform.position
                || select.transform.position == inventorySlotCategoryList[3].transform.GetChild(1).transform.position
                || select.transform.position == inventorySlotCategoryList[3].transform.GetChild(2).transform.position)
            {
                if (isOpenWeaponSlot) //weapon slot 켜졌을때
                {
                    select.transform.position = weaponButton.transform.position;
                    select.GetComponent<RectTransform>().sizeDelta = itemButtonList[selectSlotIndex].GetComponent<RectTransform>().sizeDelta;

                    isOpenWeaponSlot = false;
                    inventorySlotCategoryList[0].SetActive(false);
                    return;
                }
                else if (isOpenArmorSlot) //armor slot 켜졌을때
                {
                    select.transform.position = armorButton.transform.position;
                    select.GetComponent<RectTransform>().sizeDelta = itemButtonList[selectSlotIndex].GetComponent<RectTransform>().sizeDelta;

                    isOpenArmorSlot = false;
                    inventorySlotCategoryList[1].SetActive(false);
                    return;
                }
                else if (isOpenSkillSlot) //skilll slot 켜졌을때
                {
                    if (selectCategoryIndex < 1)
                    {
                        select.transform.position = skillButton.transform.position;
                        select.GetComponent<RectTransform>().sizeDelta = itemButtonList[selectSlotIndex].GetComponent<RectTransform>().sizeDelta;

                        isOpenSkillSlot = false;
                        inventorySlotCategoryList[2].SetActive(false);
                        return;
                    }
                    else
                    {
                        select.transform.position = inventorySlotCategoryList[2].transform.GetChild(--selectCategoryIndex).transform.position;
                        return;
                    }              
                }
                else if (isOpenAbilitySlot) //ability slot 켜졌을때
                {
                    if (selectCategoryIndex < 1)
                    {
                        select.transform.position = abilityButton.transform.position;
                        select.GetComponent<RectTransform>().sizeDelta = itemButtonList[selectSlotIndex].GetComponent<RectTransform>().sizeDelta;

                        isOpenAbilitySlot = false;
                        inventorySlotCategoryList[3].SetActive(false);
                        return;
                    }
                    else
                    {
                        select.transform.position = inventorySlotCategoryList[3].transform.GetChild(--selectCategoryIndex).transform.position;
                        return;
                    }
                }          
            }
            else
            {
                //인벤토리 내에서만의 움직임

                if (selectSlotIndex == 0)
                {
                    selectSlotIndex = itemSlotList.Length;
                }

                select.transform.position = itemSlotList[--selectSlotIndex].transform.position;
                select.GetComponent<RectTransform>().sizeDelta = itemSlotList[selectSlotIndex].GetComponent<RectTransform>().sizeDelta;
                return;
            }
        }
    }

    private void VInventorySlotMove(float inputVValue)
    {
        if (inputVValue > 0)
        {
            if (select.transform.position == itemButtonList[0].transform.position || select.transform.position == itemButtonList[1].transform.position || select.transform.position == itemButtonList[2].transform.position || select.transform.position == itemButtonList[3].transform.position)
            {
                if(selelctButtonIndex == 0)
                {
                    selelctButtonIndex = itemButtonList.Length;
                }

                select.transform.position = itemButtonList[--selelctButtonIndex].transform.position;
                select.GetComponent<RectTransform>().sizeDelta = itemButtonList[selelctButtonIndex].GetComponent<RectTransform>().sizeDelta;
                return;
            }
            else if (select.transform.position == inventorySlotCategoryList[0].transform.GetChild(0).transform.position
                     || select.transform.position == inventorySlotCategoryList[1].transform.GetChild(0).transform.position
                     || select.transform.position == inventorySlotCategoryList[2].transform.GetChild(0).transform.position
                     || select.transform.position == inventorySlotCategoryList[2].transform.GetChild(1).transform.position
                     || select.transform.position == inventorySlotCategoryList[3].transform.GetChild(0).transform.position
                     || select.transform.position == inventorySlotCategoryList[3].transform.GetChild(1).transform.position
                     || select.transform.position == inventorySlotCategoryList[3].transform.GetChild(2).transform.position)
            {
                return;
            }


                //인벤토리 내에서만의 움직임

                selectSlotIndex -= 3;

            if (selectSlotIndex < 0)
            {
                selectSlotIndex += 3;
                return;
            }

            select.transform.position = itemSlotList[selectSlotIndex].transform.position;
            select.GetComponent<RectTransform>().sizeDelta = itemSlotList[selectSlotIndex].GetComponent<RectTransform>().sizeDelta;
        }
        else if (inputVValue < 0)
        {
            if (select.transform.position == itemButtonList[0].transform.position || select.transform.position == itemButtonList[1].transform.position || select.transform.position == itemButtonList[2].transform.position || select.transform.position == itemButtonList[3].transform.position)
            {
                if (selelctButtonIndex == itemButtonList.Length - 1)
                {
                    selelctButtonIndex = -1;
                }


                //인벤토리 내에서만의 움직임

                select.transform.position = itemButtonList[++selelctButtonIndex].transform.position;
                select.GetComponent<RectTransform>().sizeDelta = itemButtonList[selelctButtonIndex].GetComponent<RectTransform>().sizeDelta;
                return;
            }
            else if (select.transform.position == inventorySlotCategoryList[0].transform.GetChild(0).transform.position
         || select.transform.position == inventorySlotCategoryList[1].transform.GetChild(0).transform.position
         || select.transform.position == inventorySlotCategoryList[2].transform.GetChild(0).transform.position
         || select.transform.position == inventorySlotCategoryList[2].transform.GetChild(1).transform.position
         || select.transform.position == inventorySlotCategoryList[3].transform.GetChild(0).transform.position
         || select.transform.position == inventorySlotCategoryList[3].transform.GetChild(1).transform.position
         || select.transform.position == inventorySlotCategoryList[3].transform.GetChild(2).transform.position)
            {
                return;
            }

            selectSlotIndex += 3;

            if (selectSlotIndex > itemSlotList.Length - 1)
            {
                selectSlotIndex -= 3;
                return;
            }

            select.transform.position = itemSlotList[selectSlotIndex].transform.position;
            select.GetComponent<RectTransform>().sizeDelta = itemSlotList[selectSlotIndex].GetComponent<RectTransform>().sizeDelta;
        }
        else
        {
            return;
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
            select.GetComponent<RectTransform>().sizeDelta = shopSlotList[selectSlotIndex].GetComponent<RectTransform>().sizeDelta;
        }
        else if (inputHValue < 0)
        {
            if (selectSlotIndex == 0)
            {
                return;
            }

            select.transform.position = shopSlotList[--selectSlotIndex].transform.position;
            select.GetComponent<RectTransform>().sizeDelta = shopSlotList[selectSlotIndex].GetComponent<RectTransform>().sizeDelta;
        }
        else if(inputHValue == 0 && ((2 < selectSlotIndex && selectSlotIndex < 6) || (9 < selectSlotIndex && selectSlotIndex < 16) ) && Input.GetKeyDown(KeyCode.Z))
        {
            shopSlotList[selectSlotIndex].GetComponent<TestShopInventorySlot>().BuyItemUseKeyBoard();
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
            select.GetComponent<RectTransform>().sizeDelta = shopSlotList[selectSlotIndex].GetComponent<RectTransform>().sizeDelta;
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
            select.GetComponent<RectTransform>().sizeDelta = shopSlotList[selectSlotIndex].GetComponent<RectTransform>().sizeDelta;
        }
    }

    private bool IsFullInventory()
    {
        for(int i = 0; i < itemSlotList.Length; i++)
        {
            if(itemSlotList[i].transform.GetChild(0).GetComponent<Image>().sprite.name == "NullWeapon" || itemSlotList[i].transform.GetChild(0).GetComponent<Image>().sprite.name == "NullArmor" || itemSlotList[i].transform.GetChild(0).GetComponent<Image>().sprite.name == "NullSkill" || itemSlotList[i].transform.GetChild(0).GetComponent<Image>().sprite.name == "NullAbility")
            {
                return false;
            }
        }

        return true;
    }

    private GameObject CheckEmptyInventorySlot()
    {
        for (int i = 0; i < itemSlotList.Length; i++)
        {
            if (itemSlotList[i].transform.GetChild(0).GetComponent<Image>().sprite.name == "NullWeapon" || itemSlotList[i].transform.GetChild(0).GetComponent<Image>().sprite.name == "NullArmor" || itemSlotList[i].transform.GetChild(0).GetComponent<Image>().sprite.name == "NullSkill" || itemSlotList[i].transform.GetChild(0).GetComponent<Image>().sprite.name == "NullAbility")
            {
                return itemSlotList[i];
            }
        }

        return null;
    }
}
