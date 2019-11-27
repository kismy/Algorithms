using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct PlayerInfo
{
    public int EXP;
    public int HP;
    public Vector2 position;
}
public class Player : MonoBehaviour {

    public static Player instance;
    public PlayerInfo playerInfo;
    void Awake () {
        instance = this;
    }

    private void Judge()
    {
        if (playerInfo.EXP < 0)
        {
            Debug.LogError("游戏失败！");
        }
        else if(playerInfo.EXP>=100)
        {
            playerInfo.EXP -= 100;
            Debug.Log("角色升级！");
        }
    }

    public void OnClickPoint(Point point)
    {
        transform.position = point.pointInfo.position;

        switch (point.pointInfo.pointType)
        {
            case PointType.PointType_Home:
                playerInfo.HP = 100;
                break;
            case PointType.PointType_Hole:
                playerInfo.HP -= 20;
                playerInfo.EXP += 10;
                Judge();
                Destroy(point.gameObject);
                break;
            case PointType.PointType_Farm:
                playerInfo.HP -= 10;
                playerInfo.EXP += 5;
                Judge();
                Destroy(point.gameObject);
                break;
            default:
                break;
        }
    }


}
