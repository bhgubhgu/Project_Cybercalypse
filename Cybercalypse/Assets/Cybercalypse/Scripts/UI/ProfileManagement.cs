using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManagement : MonoBehaviour
{
    /// <summary>
    /// 작성자 : 윤동준
    /// 스크립트 : Player의 HP, Shield, Energy 를 표시한 GUI 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.14
    /// </summary>

    private Image SP;
    private Image HP;
    private Image EP;

    public CExecutor executor;

	// Use this for initialization
	void Start () {
        SP = transform.Find("Fix_SP").GetChild(0).GetComponent<Image>();
        HP = transform.Find("Fix_HP").GetChild(0).GetComponent<Image>();
        EP = transform.Find("Fix_EP").GetChild(0).GetComponent<Image>();

        if (executor.Equals(null))
            GameObject.Find("Player").GetComponent<CExecutor>();
	}
	
	// Update is called once per frame
	void Update () {
        SP.fillAmount = executor.CurrentShield / executor.MaximumShield;
        HP.fillAmount = executor.CurrentHealth / executor.MaximumHealth;
        EP.fillAmount = executor.CurrentEnergy / executor.MaximumEnergy;
	}
}
