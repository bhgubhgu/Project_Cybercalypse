using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class CDataManager : SingleTonManager<CDataManager>
{
    /// <summary>
    /// 작성자 : 구용모
    /// 스크립트 : CyberCalypse의 모든 Data 들을 관리하는 매니저 스크립트
    /// 최초 작성일 : 2018.06.11
    /// 최종 수정일 : 2018.06.27
    /// </summary>

    ///<summary
    /// 모든 데이터 들을 관리
    /// 
    /// test - 1. Player 최대 체력, 현재 체력, 쉴드
    /// test - 2. Monster 최대 체력, 현재 체력
    /// 
    /// JSON 사용
    /// 
    /// 시작할때 JSON 데이터를 불러온다.
    /// 게임 종료 및 게임 세이브 포인트에 이르면 자동으로 저장된다.
    /// 싱글톤 패턴을 사용한다.
    /// 모든 데이터 들이 이곳에 있다.
    /// 이곳의 데이터들을 끌어다가 Start 에서 로드하여 사용한다.(Player, Monster등)
    /// string 형은 string을 float으로 변환한다.(CMonster, CExecutor에서 받아올때 --- float.Parse() 이것을 통해 변환)
    ///</summary>
    ///

    //각 데이터 클래스의 인스턴스
    private CPlayerData playerData;

    //추후 다른 것들도 List로 추가 예정
    private List<CMonsterData> monsterDataList;

    private new void Awake()
    {
        base.Awake(); //추상 싱글톤 클래스에서의 Awake 오버라이딩
        monsterDataList = new List<CMonsterData>();
        LoadJsonData();
    }

    private void LoadJsonData() //게임 시작시 로드할 데이터 메소드(처음 게임 시작시에는 미리 json에 저장된 데이터를 불러온다.)
    {
        string playerJsonString = File.ReadAllText(Application.dataPath + "/Cybercalypse/Data/playerData.json"); //Player의 데이터
        string monsterJsonString = File.ReadAllText(Application.dataPath + "/Cybercalypse/Data/monsterData.json"); //Monster들의 데이터

        JsonData playerJsonData = JsonMapper.ToObject(playerJsonString); //플레이어 데이터 불러옴
        JsonData monsterJsonData = JsonMapper.ToObject(monsterJsonString); //몬스터 데이터 불러옴

        //플레이어 데이터 로드
        playerData = new CPlayerData
            (
             playerJsonData["PlayerCurrentHealth"].ToString(),
             playerJsonData["PlayerMaxHealth"].ToString(),
             playerJsonData["PlayerCurrentShield"].ToString(),
             playerJsonData["PlayerMaxShield"].ToString(),
             playerJsonData["PlayerMoveForce"].ToString(),
             playerJsonData["PlayerJumpForce"].ToString(),
             playerJsonData["PlayerSavePosition"].ToString()
            );

        CGameManager.instance.playerObject.GetComponent<CExecutor>().MoveForce = float.Parse(playerData.PlayerMoveForce); //플레이어 moveForce 할당
        CGameManager.instance.playerObject.GetComponent<CExecutor>().JumpForce = float.Parse(playerData.PlayerJumpForce); //플레이어 jumpForce 할당

        //플레이어 저장된 position 로드
        string[] playerPositionData = playerData.PlayerSavePosition.Split('/');
        CGameManager.instance.playerObject.transform.position = new Vector3(float.Parse(playerPositionData[0]), float.Parse(playerPositionData[1]), float.Parse(playerPositionData[2]));
        

        //몬스터 데이터 로드
        for (int i = 0; i < monsterJsonData.Count; i++)
        {
            monsterDataList.Add
                (
                    new CMonsterData
                    (
                        int.Parse(monsterJsonData[i]["monsterInstanceID"].ToString()),
                        monsterJsonData[i]["monsterCurrentHealth"].ToString(),
                        monsterJsonData[i]["monsterMaxHealth"].ToString()
                    )
                );
        }
    }

    private void SaveJsonData() //현재 플레이어의 상태를 저장 (Player Object는 GameManager에서 가져온다.)  //게임 세이브시 동작할 데이터 메소드
    {
        playerData = new CPlayerData // ex -> CGameManager.instance.playerObject.GetComponent<CExecutor>().CurrentHealth
        (
            CGameManager.instance.playerObject.GetComponent<CExecutor>().CurrentHealth.ToString(), //Current HP
            CGameManager.instance.playerObject.GetComponent<CExecutor>().MaximumHealth.ToString(), //Max HP
            CGameManager.instance.playerObject.GetComponent<CExecutor>().CurrentShield.ToString(), //Current Shield
            CGameManager.instance.playerObject.GetComponent<CExecutor>().MaximumShield.ToString(), //Max Shield
            CGameManager.instance.playerObject.GetComponent<CExecutor>().MoveForce.ToString(), //MoveForce
            CGameManager.instance.playerObject.GetComponent<CExecutor>().JumpForce.ToString(), //Jump Force
            CGameManager.instance.playerObject.transform.position.x + "/"+ CGameManager.instance.playerObject.transform.position.y + "/" + CGameManager.instance.playerObject.transform.position.z //Position
        );
        //앞으로 플레이어에 관한 모든 정보를 저장

        JsonData savePlayerJsonData = JsonMapper.ToJson(playerData);

        File.WriteAllText(Application.dataPath + "/Cybercalypse/Data/playerData.json", savePlayerJsonData.ToString()); //데이터 저장
    }
}

public class CPlayerData
{
    //필드
    //플레이어의 현재 체력, 최대체력
    private string playerCurrentHealth;
    private string playerMaxHealth;

    //플레이어의 현재 쉴드, 최대 쉴드
    private string playerCurrentShield;
    private string playerMaxShield;

    //플레이어의 moveForce, jumpForce
    private string playerMoveForce;
    private string playerJumpForce;

    //플레이어의 Position
    private string playerSavePosition;

    //프로퍼티(앞글자 대문자)
    public string PlayerCurrentHealth
    {
        get
        {
            return playerCurrentHealth;
        }
        set
        {
            playerCurrentHealth = value;
        }
    }
    public string PlayerMaxHealth
    {
        get
        {
            return playerMaxHealth;
        }
        set
        {
            playerMaxHealth = value;
        }
    }
    public string PlayerCurrentShield
    {
        get
        {
            return playerCurrentShield;
        }
        set
        {
            playerCurrentShield = value;
        }
    }
    public string PlayerMaxShield
    {
        get
        {
            return playerMaxShield;
        }
        set
        {
            playerMaxShield = value;
        }
    }
    public string PlayerMoveForce
    {
        get
        {
            return playerMoveForce;
        }
        set
        {
            playerMoveForce = value;
        }
    }
    public string PlayerJumpForce
    {
        get
        {
            return playerJumpForce;
        }
        set
        {
            playerJumpForce = value;
        }
    }
    public string PlayerSavePosition
    {
        get
        {
            return playerSavePosition;
        }
        set
        {
            playerSavePosition = value;
        }
    }

    public CPlayerData(string currentHealth, string maxHealth, string currentShield, string maxShield, string moveForce, string jumpForce, string playerPositionData)
    {
        if (currentHealth == "0") //처음 게임 시작할때, 죽었을때 다시 부활할때
        {
            PlayerMaxHealth = maxHealth;
            PlayerCurrentHealth = PlayerMaxHealth;
            PlayerMaxShield = maxShield;
            PlayerCurrentShield = PlayerMaxShield;
            PlayerMoveForce = moveForce;
            PlayerJumpForce = jumpForce;
            PlayerSavePosition = playerPositionData;
        }
        else //게임 진행중
        {
            PlayerMaxHealth = maxHealth;
            PlayerCurrentHealth = currentHealth;
            PlayerMaxShield = maxShield;
            PlayerCurrentShield = currentShield;
            PlayerMoveForce = moveForce;
            PlayerJumpForce = jumpForce;
            PlayerSavePosition = playerPositionData;
        }
    }
}

public class CMonsterData
{
    //필드
    //플레이어의 현재 체력, 최대체력,몬스터 인스턴스 ID
    private int monsterInstanceID;
    private string monsterCurrentHealth;
    private string monsterMaxHealth;

    //프로퍼티(앞글자 대문자)
    public string MonsterCurrentHealth
    {
        get
        {
            return monsterCurrentHealth;
        }
        set
        {
            monsterCurrentHealth = value;
        }
    }
    public string MonsterMaxHealth
    {
        get
        {
            return monsterMaxHealth;
        }
        set
        {
            monsterMaxHealth = value;
        }
    }
    public int MonsterInstanceID
    {
        get
        {
            return monsterInstanceID;
        }
        set
        {
            monsterInstanceID = value;
        }
    }

    public CMonsterData(int instanceID, string currentHealth, string maxHealth)
    {
        MonsterInstanceID = instanceID;
        MonsterMaxHealth = maxHealth;
        MonsterCurrentHealth = MonsterMaxHealth;
    }
}

public class CSkillData
{

}

public class CAbilityData
{

}

public class CItemData
{

}