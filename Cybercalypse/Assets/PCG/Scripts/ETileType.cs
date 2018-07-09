

// 생성할 타입의 종류
public enum ETileType
{
    Ground, Platform, RightHill, LeftHill, Ceiling, LeftWall, RightWall, RightStair, LeftStair, Stuff, Empty,
    Portal, MonsterSponer, PlayerSponer, ItemSponer, Decorator, Water, COUNT
}
// 평지, 발판, 오르막길, 내리막길, 천장, 왼쪽 벽, 오른쪽 벽, 비었지가 갈 수 없는 공간, 플레이어가 이동할 수 있는 빈 공간,
// 포탈, 몬스터 스포너, 플레이어 스포너, 아이템 스포너, 장식품, 물, 총 개수