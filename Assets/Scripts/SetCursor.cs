using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
    public Texture2D cursorArrow;
    public GameObject background;

    public float speed = 1.0f;
    private float maxMove = 10.0f;
    private float mouseX;
    private float mouseY;
    private float backgroundMoveX;
    private float backgroundMoveY;
    private int randomMove;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

    void Update() {

    }
}
