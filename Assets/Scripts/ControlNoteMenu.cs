using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlNoteMenu : MonoBehaviour
{
    public GameObject controlNoteMenu;
    public bool isControlNoteMenu;
    // Start is called before the first frame update
    void Start()
    {
        controlNoteMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // maybe change that to a gesture input, else we can delete this and the two functions
        {
            if (isControlNoteMenu)
            {
                CloseControlNote();
            }
            else
            {
                ShowControlNote();
            }
        }
    }

    // Displays the ControlNote and stops the time
    public void ShowControlNote()
    {
        controlNoteMenu.SetActive(true);
        Time.timeScale = 0f;
        isControlNoteMenu = true;
    }

    // Closes the ControlNote and start counting time
    public void CloseControlNote()
    {
        controlNoteMenu.SetActive(false);
        Time.timeScale = 1f;
        isControlNoteMenu = false;
    }
}
