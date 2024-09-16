using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueNChips : MonoBehaviour
{
    public GameObject _Player;
    public float _PosX;
    public float _PosY;

    private void Update()
    {
        if (_Player.GetComponent<SpriteRenderer>().flipX == false)
        {
            transform.localPosition = new Vector2(_PosX, _PosY);
        }
        else if (_Player.transform.GetComponent<SpriteRenderer>().flipX == true)
        {
            transform.localPosition = new Vector2(-_PosX, _PosY);
        }
    }
}
