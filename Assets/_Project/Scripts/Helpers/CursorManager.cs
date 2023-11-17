using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public enum CursorBehavior { Menu, Playing}

    public CursorBehavior behavior;

    private void Start()
    {
        if(behavior == CursorBehavior.Menu)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        if(behavior == CursorBehavior.Playing)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
