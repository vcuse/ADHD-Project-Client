using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class TestMouse : MonoBehaviour
{

    [StructLayout(LayoutKind.Sequential)]
    struct POINT { public int x, y; }

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool GetCursorPos(out POINT lpPoint);

    /// <summary>
    /// Fetches desktop-space mouse coordinates.
    /// </summary>
    public static Vector2Int mousePosition
    {
        get
        {
            POINT pt;
            if (GetCursorPos(out pt))
            {
                Debug.Log("ptx" + pt.x + " pty" + pt.y);
                return new Vector2Int(pt.x, pt.y);
            }
            else return Vector2Int.zero;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(mousePosition);

    }
}
