using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareView : MonoBehaviour
{
    public int PosX;
    public int PosY;

    public Vector3 Rotation;
    public Vector3 Scale;

    public string Color;
    public string Objname;

    [SerializeField]
    SpriteRenderer spriteRenderer;
    Color newCol;

    public void Init()
    {
        this.transform.localEulerAngles = Rotation;
        this.transform.localScale = Scale;
        this.transform.name = Objname;
        if (ColorUtility.TryParseHtmlString(Color, out newCol))
        {
            spriteRenderer.color = newCol;
        }
    }
}
