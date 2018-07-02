using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverGround : MonoBehaviour {

    private float velocity;
    private Direction hoverDirection;
    private float hoverDistance;

    public float Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }
    public Direction HoverDirection
    {
        get { return hoverDirection; }
        set { hoverDirection = value; }
    }
    public float HoverDistance
    {
        get
        {
            return hoverDistance;
        }

        set
        {
            hoverDistance = value;
        }
    }

    private Vector3 departurePosition;
    private Vector3 arrivalPosition;

	// Use this for initialization
	void Start () {
        velocity = 0.01f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 이 함수가 호출되면 지형이 호버링 상태로 전환됨.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    IEnumerator Hovering()
    {
        if (hoverDirection.Equals(Direction.left))
            transform.Translate(Vector3.left * velocity);
        else if (hoverDirection.Equals(Direction.right))
            transform.Translate(Vector3.right * velocity);

        yield return null;
    }

    public enum Direction
    {
        left = -1,
        right = 1
    }
}
