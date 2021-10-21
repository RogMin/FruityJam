using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NoMouseCursor : MonoBehaviour 
{
   private bool isLocked;

	void Start () 
	{	
		SetCursorLock (true);
	}
	public void SetCursorLock(bool isLocked)
	{
		this.isLocked = isLocked;
		if (this.isLocked)
        {
			Cursor.lockState = CursorLockMode.Locked;
		}
        else
        {
			Cursor.lockState = CursorLockMode.None;
		}
		Cursor.visible = !isLocked;
	}
}