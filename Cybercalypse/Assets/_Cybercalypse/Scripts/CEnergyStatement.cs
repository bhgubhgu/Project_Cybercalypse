using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnergyStatement : MonoBehaviour // 임시용
{
    public GameObject energyBar;
    public SpriteRenderer sprite;
    public static float energy;

    SpriteRenderer spritePivot;

    public GameObject parentTarget;


    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        CInputManager.instance.PlayerVMove += VMoveEnergyState;
    }

    public void VMoveEnergyState(float inputValue)
    {
        energy = this.sprite.size.x;

        if (this.sprite.size.x > 3f)
        {
            this.sprite.size = new Vector2(3, this.sprite.size.y);
            return;
        }
        else if(this.sprite.size.x < 0)
        {
            this.sprite.size = new Vector2(0, this.sprite.size.y);
            return;
        }
    }
}
