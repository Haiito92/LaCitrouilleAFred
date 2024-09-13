using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class widow : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        BorderlessWindow.SetFramelessWindow();
        BorderlessWindow.MoveWindowPos(Vector2Int.zero, 1920*2, 1080);
    }
}
