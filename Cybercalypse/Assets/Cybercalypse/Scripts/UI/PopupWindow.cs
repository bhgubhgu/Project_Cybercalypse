using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupWindow : MonoBehaviour
{
    /// <summary>
    /// 작성자 : 윤동준
    /// 스크립트 : 게임 시작 및 종료를 알리는 팝업 UI 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>


    /// <summary>
    /// 하나의 오브젝트이고, 알림이 활성화 되면, 매개변수로 받은 Message를 표시해준다.
    /// </summary>

    public string Message { get; set; }
    //public bool IsExecute { get; set; }
    public bool IsShow { get; set; }
    
    private Text content;

    // Use this for initialization
    void Start () {
        content = transform.Find("Content").GetComponent<Text>();
        //gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void NotifyMessage(string message)
    {
        content.text = message;
        TurnOnWindow();
    }

    public void TurnOnWindow()
    {
        transform.position = Vector3.zero;
    }

    public void TurnOffWindow()
    {
        transform.position = new Vector3(0, 5000.0f);
    }
}
