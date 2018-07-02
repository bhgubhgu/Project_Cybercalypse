﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CInputManager : SingleTonManager<CInputManager>
{
    /// <summary>
    /// 작성자 : 구용모, 윤동준
    /// 스크립트 : CyberCalypse의 Player의 Input을 관리하는 매니저 스크립트
    /// 최초 작성일 : . . .
    /// 최종 수정일 : 2018.06.27
    /// </summary>

    //리팩토링시 팩토리 패턴 내부의 모든 메소드들 private 처리  
    /* 필드 */
    #region private
    bool isDownMenuUniqueKey;
    bool isDownCharacterJumpKey;
    bool isDownCharacterDashKey;

    bool isDownSkill1;
    bool isDownSkill2;
    bool isDownSkill3;
    bool isDownSkill4;
    bool isDownSkillMouseLeft;
    bool isDownSkillMouseRight;

    int runCount;
    float inputHMoveValue;
    float inputVMoveValue;

    //!< 커서
    float inputCursorHMoveValue;
    float inputCursorVMoveValue;

    bool isDownInteractKey;

    bool isMenuActive; // Command 메소드 OR 클래스를 통해 구별
    bool isGameRetry;
    bool isPressAnykey;

    // Switch 를 위한 변수 필요
    // AI와 Player를 구별하기 위한 메소드 필요 ( 보류 )
    // 반환 값을 통해 Switch 로 구분할 값 할당
    // 

    private PlayerCommand playerCommand;
    #endregion

    #region delegate
    /* 델리게이트 */
    //입력 받는 객체에 따라 달라지는 메소드
    //고유 기능이 있는 입력 델리게이트(선택,공격,점프,대쉬 등등)
    //이동 기능이 있는 입력 델리게이트(메뉴 이동, 캐릭터 이동 등등)
    public delegate void UniqueInput(bool isDownUniqueKey);
    public delegate void MoveInput(float inputHMoveValue);
    public delegate bool Command(bool isCheckInput);
    public delegate float MoveCommand(float inputHMoveValue);
    #endregion

    #region public
    public event UniqueInput GamePause;
    public event UniqueInput GameRetry;
    public event UniqueInput MenuClose;
    public event UniqueInput Jump;
    public event UniqueInput Dash;

    public event UniqueInput Skill1;
    public event UniqueInput Skill2;
    public event UniqueInput Skill3;
    public event UniqueInput Skill4;
    public event UniqueInput SkillMouseLeft;
    public event UniqueInput SkillMouseRight;

    public event UniqueInput Interact;

    public event MoveInput Menumove;
    public event MoveInput PlayerHMove;
    public event MoveInput HRun;
    public event MoveInput PlayerVMove;

    //!< 커서
    public event MoveInput CursorHMove;
    public MoveInput CursorVMove;

    //move event Command
    public event MoveCommand HMoveCommand;
    public event MoveCommand VMoveCommand;
    public event MoveCommand HCursorCommand;
    public event MoveCommand VCursorCommand;

    //certain event Command
    public event Command JumpCommand;
    public event Command DashCommand;
    public event Command InteractCommand;

    //skill event Command
    public event Command Skill1Command;
    public event Command Skill2Command;
    public event Command Skill3Command;
    public event Command Skill4Command;
    public event Command SkillMouseLeftCommand;
    public event Command SkillMouseRightCommand;

    //game system Command
    public event Command SaveCommand;
    public event Command RetryCommand;
    public event Command MenuCommand;
    #endregion
    /* 메소드 */
    #region private Method
    private new void Awake()
    {
        base.Awake();
        playerCommand = new PlayerCommand(); //InputManager가 먼저 싱글톤 인스턴스를 생성한 다음 PlayerCommand로 넘어가기 때문에 Awake에 있어도 된다.
    }

    //캐릭터 이동과 메뉴 이동을 위한 업데이트
    void Update()
    {
        /* 플레이어 */
        inputHMoveValue = HMoveCommand(inputHMoveValue);
        inputVMoveValue = VMoveCommand(inputVMoveValue);
        isDownCharacterJumpKey = JumpCommand(isDownCharacterJumpKey);
        isDownCharacterDashKey = DashCommand(isDownCharacterDashKey);

        isDownInteractKey = InteractCommand(isDownInteractKey);

        //!< 커서
        inputCursorHMoveValue = HCursorCommand(inputCursorHMoveValue);
        inputCursorVMoveValue = VCursorCommand(inputCursorVMoveValue);

        //스킬
        isDownSkill1 = Skill1Command(isDownSkill1);
        isDownSkill2 = Skill2Command(isDownSkill2);
        isDownSkill3 = Skill3Command(isDownSkill3);
        isDownSkill4 = Skill4Command(isDownSkill4);
        isDownSkillMouseLeft = SkillMouseLeftCommand(isDownSkillMouseLeft);
        isDownSkillMouseRight = SkillMouseRightCommand(isDownSkillMouseRight);

        /* 메뉴 */
        isMenuActive = MenuCommand(isMenuActive);

        /* 다시 시작*/
        isGameRetry = RetryCommand(isGameRetry);
        isPressAnykey = isGameRetry;

        //!< 커서
        /*CursorHMove(inputCursorHMoveValue);
        CursorVMove(inputCursorVMoveValue);*/

        try
        {
            HandOverInputEvent();
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }

    private void HandOverInputEvent()
    {
        switch (isMenuActive || CGameManager.instance.isGameOver)
        {
            case true:
                //Menu Acting
                GamePause(isMenuActive);
                if (isGameRetry)
                {
                    GameRetry(isGameRetry);
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                break;

            case false:
                //Menu unActing
                if (isPressAnykey) //메뉴를 닫을려면 아무키나 눌러도 된다.
                {
                    MenuClose(isPressAnykey);
                }
                if (runCount < 2) //구현 주석처리 해 놓은 run을 기준으로 aa 또는 dd가 아닐때 걷기를 실행한다.
                {
                    PlayerHMove(inputHMoveValue);
                }
                /*else if (runCount >= 2) //달리기 지금은 주석처리(애니메이션 나올때까지 보류)
                {
                    HRun(inputHMoveValue);
                }*/
                PlayerVMove(inputVMoveValue); //수직이동

                if (isDownCharacterJumpKey) //점프
                {
                    Jump(isDownCharacterJumpKey);
                }
                else if (isDownCharacterDashKey) //대쉬
                {
                    Dash(isDownCharacterDashKey);
                }
                else if (isDownSkill1) //스킬 발사 1번
                {
                    Skill1(isDownSkill1);
                }
                else if (isDownSkill2) //스킬 발사 2번
                {
                    Skill2(isDownSkill2);
                }
                else if (isDownSkill3) //스킬 발사 3번
                {
                    Skill3(isDownSkill3);
                }
                else if (isDownSkill4) //스킬 발사 4번
                {
                    Skill4(isDownSkill4);
                }
                else if (isDownSkillMouseLeft) //스킬 발사 마우스 왼쪽
                {
                    SkillMouseLeft(isDownSkillMouseLeft);
                }
                else if (isDownSkillMouseRight) //스킬 발사 마우스 오른쪽
                {
                    SkillMouseRight(isDownSkillMouseRight);
                }
                else if (isDownInteractKey) //상호작용(npc와의 대화 등등)
                {
                    Interact(isDownInteractKey);
                }
                break;
        }
    }
}
#endregion

#region Abstract
abstract class AbsCommand
{
    //값을 반환시켜 InputManager에 보낸다.
    public abstract float HMove(float hInputValue);
    public abstract float VMove(float vInputValue);

    public abstract int HRun(); // 몇초안에 2번 누르면 달리기 실행

    public abstract bool isJumpInput(bool isDownJumpKey);
    public abstract bool isDashInput(bool isDownDashKey);

    public abstract bool isSkillInput1(bool isDownSkillKey1);
    public abstract bool isSkillInput2(bool isDownSkillKey2);
    public abstract bool isSkillInput3(bool isDownSkillKey3);
    public abstract bool isSkillInput4(bool isDownSkillKey4);
    public abstract bool isSkillInputMouseLeft(bool isDownSkillKeyMouseLeft);
    public abstract bool isSkillInputMouseRight(bool isDownSkillKeyMouseRight);

    public abstract bool isInteractInput(bool isDownInteractKey);

    public abstract bool isRetryInput(bool isRetryKey);
    public abstract bool IsMenuKeyInput(bool isKeyDown);

    //!< 커서
    public abstract float CursorHMove(float hCursorInput);
    public abstract float CursorVMove(float vCursorInput);
}
#endregion

class PlayerCommand : AbsCommand
{
    #region private
    private bool isMenuActing;
    private bool isGameRetring;
    private bool isJumpActing;
    private bool isDashActing;

    //!< 커서
    private float cursorHMoveValue;
    private float cursorVMoveValue;

    //스킬 사용 키
    private bool isSkill1Acting;
    private bool isSkill2Acting;
    private bool isSkill3Acting;
    private bool isSkill4Acting;
    private bool isSkillMouseLeftActing;
    private bool isSkillMouseRightActing;

    //상호작용
    private bool isInteracting;

    //run을 위한 변수 (수정 예정)
    private int rightRunCount;
    private int leftRunCount;
    private IEnumerator stopLeftCount;
    private IEnumerator stopRightCount;
    #endregion


    public PlayerCommand()
    {
        CInputManager.instance.JumpCommand += isJumpInput;
        CInputManager.instance.DashCommand += isDashInput;

        CInputManager.instance.Skill1Command += isSkillInput1;
        CInputManager.instance.Skill2Command += isSkillInput2;
        CInputManager.instance.Skill3Command += isSkillInput3;
        CInputManager.instance.Skill4Command += isSkillInput4;
        CInputManager.instance.SkillMouseLeftCommand += isSkillInputMouseLeft;
        CInputManager.instance.SkillMouseRightCommand += isSkillInputMouseRight;

        CInputManager.instance.InteractCommand += isInteractInput;

        CInputManager.instance.HMoveCommand += HMove;
        CInputManager.instance.VMoveCommand += VMove;

        CInputManager.instance.RetryCommand += isRetryInput;
        CInputManager.instance.MenuCommand += IsMenuKeyInput;

        CInputManager.instance.HCursorCommand += CursorHMove;
        CInputManager.instance.VCursorCommand += CursorVMove;

        stopLeftCount = LeftRunCount();
        stopRightCount = RightRunCount();
    }

    #region override Method
    public override bool isRetryInput(bool isRetryKey)
    {
        isRetryKey = Input.anyKeyDown;
        isGameRetring = isRetryKey;
        return isGameRetring;
    }

    public override bool IsMenuKeyInput(bool isMenukeyInput)
    {
        if ((Input.GetKeyDown(KeyCode.JoystickButton7) || Input.GetKeyDown(KeyCode.P)) && !isMenuActing)
        {
            isMenukeyInput = true;
            isMenuActing = isMenukeyInput;
            return isMenuActing;
        }
        else if ((Input.GetKeyDown(KeyCode.JoystickButton7) || Input.GetKeyDown(KeyCode.P)) && isMenuActing)
        {
            isMenukeyInput = false;
            isMenuActing = isMenukeyInput;
            return isMenuActing;
        }
        else
        {
            isMenuActing = isMenukeyInput;
            return isMenuActing;
        }
    }

    public override float HMove(float hInputValue)
    {
        hInputValue = Input.GetAxisRaw("Move Horizontally");

        if (hInputValue == 0)
            hInputValue = Input.GetAxisRaw("XBoxHorizontal");

        return hInputValue;
    }

    public override int HRun() //매니저에서 코루틴을 쓰지 않는 방법
    {
        //키를 다르게 눌렸을때 가속을 못하게 한다.(한쪽 키만 빠르게 따닥 눌렸을때만 뛰기)
        bool isDownLeftKey = Input.GetKeyDown(KeyCode.A);
        bool isDownRightKey = Input.GetKeyDown(KeyCode.D);
        bool isDownRightKeyUp = Input.GetKeyUp(KeyCode.D);
        bool isDownLeftKeyUp = Input.GetKeyUp(KeyCode.A);
        bool isRightKey = Input.GetKey(KeyCode.D);
        bool isLeftKey = Input.GetKey(KeyCode.A);

        if (isDownRightKey)
        {
            rightRunCount++;
            return rightRunCount;
        }
        else if (isDownLeftKey)
        {
            leftRunCount++;
            return leftRunCount;
        }

        if (isDownRightKeyUp)
        {
            CCoroutineHandler.instance.StartCoroutine(stopRightCount);
            return rightRunCount;
        }
        else if (isDownLeftKeyUp)
        {
            CCoroutineHandler.instance.StartCoroutine(stopLeftCount);
            return leftRunCount;
        }

        if (isLeftKey)
        {
            CCoroutineHandler.instance.StopCoroutine(stopLeftCount);
            stopLeftCount = LeftRunCount();
            return leftRunCount;
        }
        else if (isRightKey)
        {
            CCoroutineHandler.instance.StopCoroutine(stopRightCount);
            stopRightCount = RightRunCount();
            return rightRunCount;
        }
        else
        {
            return 0;
        }
    }

    IEnumerator LeftRunCount()
    {
        yield return new WaitForSeconds(0.2f);
        leftRunCount = 0;
    }

    IEnumerator RightRunCount()
    {
        yield return new WaitForSeconds(0.2f);
        rightRunCount = 0;
    }


    public override float VMove(float vInputValue)
    {
        vInputValue = Input.GetAxisRaw("Move Vertically");

        if (vInputValue == 0)
            vInputValue = Input.GetAxisRaw("XBoxVertical");

        return vInputValue;
    }

    public override bool isJumpInput(bool isDownJumpKey)
    {
        isDownJumpKey = Input.GetButtonDown("Accelerate Upward");

        if (!isDownJumpKey)
            isDownJumpKey = Input.GetKeyDown(KeyCode.JoystickButton0);

        isJumpActing = isDownJumpKey;

        return isJumpActing;
    }

    public override bool isDashInput(bool isDownDashKey)
    {
        isDownDashKey = Input.GetButtonDown("Accelerate Horizontally");
        if (!isDownDashKey)
            isDownDashKey = Input.GetKeyDown(KeyCode.JoystickButton1);

        isDashActing = isDownDashKey;
        return isDashActing;
    }

    public override bool isInteractInput(bool isDownInteractKey)
    {
        isDownInteractKey = Input.GetKeyDown(KeyCode.F);

        isInteracting = isDownInteractKey;
        return isInteracting;
    }

    public override bool isSkillInput1(bool isDownSkillKey1)
    {
        isDownSkillKey1 = Input.GetButtonDown("Cast Skill 1");
        if (!isDownSkillKey1)
            isDownSkillKey1 = Input.GetKeyDown(KeyCode.JoystickButton4);

        isSkill1Acting = isDownSkillKey1;
        return isSkill1Acting;
    }

    public override bool isSkillInput2(bool isDownSkillKey2)
    {
        isDownSkillKey2 = Input.GetButtonDown("Cast Skill 2");
        if (!isDownSkillKey2)
            isDownSkillKey2 = Input.GetKeyDown(KeyCode.JoystickButton5);

        isSkill2Acting = isDownSkillKey2;
        return isSkill2Acting;
    }

    public override bool isSkillInput3(bool isDownSkillKey3)
    {
        float axisTrigger = Input.GetAxisRaw("XBoxTrigger");
        isDownSkillKey3 = Input.GetButtonDown("Cast Skill 3");
        if (!isDownSkillKey3)
        {
            if (axisTrigger > 0.0f)
                isDownSkillKey3 = true;
        }

        isSkill3Acting = isDownSkillKey3;
        return isSkill3Acting;
    }

    public override bool isSkillInput4(bool isDownSkillKey4)
    {
        float axisTrigger = Input.GetAxisRaw("XBoxTrigger");
        isDownSkillKey4 = Input.GetButtonDown("Cast Skill 4");
        if (!isDownSkillKey4)
        {
            if (axisTrigger < 0.0f)
                isDownSkillKey4 = true;
        }

        isSkill4Acting = isDownSkillKey4;
        return isSkill4Acting;
    }

    public override bool isSkillInputMouseLeft(bool isDownSkillKeyMouseLeft)
    {
        isDownSkillKeyMouseLeft = Input.GetButtonDown("Cast Skill Mouse Left");
        if (!isDownSkillKeyMouseLeft)
            isDownSkillKeyMouseLeft = Input.GetKeyDown(KeyCode.JoystickButton2);

        isSkillMouseLeftActing = isDownSkillKeyMouseLeft;
        return isSkillMouseLeftActing;
    }

    public override bool isSkillInputMouseRight(bool isDownSkillKeyMouseRight)
    {
        isDownSkillKeyMouseRight = Input.GetButtonDown("Cast Skill Mouse Right");
        if (!isDownSkillKeyMouseRight)
            isDownSkillKeyMouseRight = Input.GetKeyDown(KeyCode.JoystickButton3);

        isSkillMouseRightActing = isDownSkillKeyMouseRight;
        return isSkillMouseRightActing;
    }

    //!< 커서
    public override float CursorHMove(float hCursorInput)
    {
        hCursorInput = Input.GetAxisRaw("XBoxAimHorizontal");
        cursorHMoveValue = hCursorInput;
        return cursorHMoveValue;
    }

    public override float CursorVMove(float vCursorInput)
    {
        vCursorInput = Input.GetAxisRaw("XBoxAimVertical");
        cursorVMoveValue = vCursorInput;
        return cursorVMoveValue;
    }

    #endregion
}

#region ccoroutine Handler
/* 코루틴 이용을 위한 Handler Class */
public class CCoroutineHandler : MonoBehaviour //나중에 다른 오브젝트 및 스크립트로 빼놓기, 캐싱 고려
{
    private static CCoroutineHandler _instance;
    public static CCoroutineHandler instance
    {
        get
        {
            if (CCoroutineHandler.Equals(_instance, null))//_instance == null)
            {
                _instance = new GameObject("cCoroutineHandler").AddComponent<CCoroutineHandler>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
#endregion
