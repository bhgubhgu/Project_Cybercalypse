using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CMenuButton : MonoBehaviour
{
    private List<GameObject> allResolutionList;

    [SerializeField]
    private int currentResolutionNum;
    [SerializeField]
    private bool isFullSize = true;

    private SpriteRenderer sprite;
    private CResolutionMenu cResolutionMenu;

    public CanvasScaler scaler;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        cResolutionMenu = transform.parent.gameObject.transform.parent.gameObject.GetComponent<CResolutionMenu>();
    }

    private void Start()
    {
        cResolutionMenu.GetCurrentWindowSize(isFullSize);
        scaler.referenceResolution = new Vector2(Screen.width, Screen.height);
        StartCoroutine(CheckResolution()); //현재 해상도 스크립트를 가진 오브젝트들이 현재 해상도넘버와 window Size인지 체크한다.
    }

    private void OnMouseEnter()
    {
        //버튼 올리기
        if(Equals(this.gameObject,allResolutionList[0]))
        {
            this.sprite.color = new Vector4(0, 1, 1, 1);
        }
        else if (Equals(this.gameObject, allResolutionList[1]))
        {
            this.sprite.color = new Vector4(0, 1, 1, 1);
        }
        else if (Equals(this.gameObject, allResolutionList[2]))
        {
            this.sprite.color = new Vector4(0, 1, 1, 1);
        }
        else if (Equals(this.gameObject, allResolutionList[3]))
        {
            this.sprite.color = new Vector4(0, 1, 1, 1);
        }
    }

    private void OnMouseDown()
    {
        //버튼 누르기
        if (Equals(this.gameObject, allResolutionList[0]))
        {
            
            currentResolutionNum = 0;
            cResolutionMenu.GetCurrentResolutionNum(currentResolutionNum); //부모로 보내어 현재의 해상도 넘버를 저장한다.
            Screen.SetResolution(640, 480, isFullSize); //Windows(640 * 480)
            scaler.referenceResolution = new Vector2(640, 480);
        }
        else if (Equals(this.gameObject, allResolutionList[1])) //부모로 보내어 현재의 해상도 넘버를 저장한다.
        {
            currentResolutionNum = 1;
            cResolutionMenu.GetCurrentResolutionNum(currentResolutionNum);
            Screen.SetResolution(1280, 720, isFullSize); //4:3(1280 * 720)HD
            scaler.referenceResolution = new Vector2(1280, 720);
        }
        else if (Equals(this.gameObject, allResolutionList[2])) //부모로 보내어 현재의 해상도 넘버를 저장한다.
        {
            currentResolutionNum = 2;
            cResolutionMenu.GetCurrentResolutionNum(currentResolutionNum);
            Screen.SetResolution(1920, 1080, isFullSize); // 16:9(1920 * 1080)FHD
            scaler.referenceResolution = new Vector2(1920, 1080);
        }
        else if (Equals(this.gameObject, allResolutionList[3])) //부모로 보내어 현재의 해상도 넘버를 저장한다.
        {
            currentResolutionNum = 3;
            cResolutionMenu.GetCurrentResolutionNum(currentResolutionNum);
            Screen.SetResolution(2560, 1080, isFullSize); //(2560 * 1080)WFHD
            scaler.referenceResolution = new Vector2(2560, 1080);
        }
        else if (Equals(this.gameObject, allResolutionList[4])) //window Mode 윈도우 사이즈로 되는지 체크
        {
            if (isFullSize)
            {
                isFullSize = false;
                cResolutionMenu.GetCurrentWindowSize(isFullSize); //윈도우 사이즈가 되면 부모로 윈도우 사이즈가 된  현재의 bool 값을 넘겨준다.
                this.sprite.color = new Vector4(0, 1, 1, 1);
            }
            else
            {
                isFullSize = true;
                cResolutionMenu.GetCurrentWindowSize(isFullSize); //다시 원래 풀 화면으로 되면 부모로 풀 사이즈가 된  현재의 bool 값을 넘겨준다.
                this.sprite.color = new Vector4(1, 1, 1, 1);
            }

            currentResolutionNum = cResolutionMenu.ReturnResolutionNum();
            ChangeSetWindow();
        }
    }

    private void OnMouseExit()
    {
        // 버튼 나가기
        if (Equals(this.gameObject, allResolutionList[0]))
        {
            this.sprite.color = new Vector4(1, 1, 1, 1);
        }
        else if (Equals(this.gameObject, allResolutionList[1]))
        {
            this.sprite.color = new Vector4(1, 1, 1, 1);
        }
        else if (Equals(this.gameObject, allResolutionList[2]))
        {
            this.sprite.color = new Vector4(1, 1, 1, 1);
        }
        else if (Equals(this.gameObject, allResolutionList[3]))
        {
            this.sprite.color = new Vector4(1, 1, 1, 1);
        }
    }

    public void GetResolutionData(List<GameObject> resolutionList) //부모가 갖고있는 해상도들을 가져옴
    {
        allResolutionList = new List<GameObject>();

        for (int i = 0; i < resolutionList.Count; i++)
        {
            allResolutionList.Add(resolutionList[i]);
        }
    }

    private void ChangeSetWindow() //창모드 체크 버튼을 누를때 사이즈를 바꿔야한다 (풀화면 -> 창모드, 창모드 -> 풀화면)
    {
        if (int.Equals(currentResolutionNum, 0))
        {
            Screen.SetResolution(640, 480, isFullSize); //Windows(640 * 480)
        }
        else if (int.Equals(currentResolutionNum, 1))
        {
            Screen.SetResolution(1280, 720, isFullSize); //4:3(1280 * 720)HD
        }
        else if (int.Equals(currentResolutionNum, 2))
        {
            Screen.SetResolution(1920, 1080, isFullSize); // 16:9(1920 * 1080)FHD
        }
        else if (int.Equals(currentResolutionNum, 3))
        {
            Screen.SetResolution(2560, 1080, isFullSize); //(2560 * 1080)WFHD
        }
    }

    private IEnumerator CheckResolution() //update 대신
    {
        while(true)
        {
            currentResolutionNum = cResolutionMenu.ReturnResolutionNum();
            isFullSize = cResolutionMenu.IsReturnWindowSize();
            yield return null;
        }
    }
}
