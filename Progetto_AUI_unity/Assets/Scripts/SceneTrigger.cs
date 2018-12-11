using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class SceneTrigger : MonoBehaviour
{

    [Space(10)]
    [Header("Toggle for the gui on off")]
    public bool GuiOn;


    [Space(10)]
    [Header("Window Layout Options")]
    public string WindowTitle = "Go inside?";

    [Tooltip("This is the window Box's size. It will be mid screen. Add or reduce the X and Y to move the box in Pixels. ")]
    public Rect BoxSize = new Rect(0, 0, 200, 100);

    [System.Serializable]
    public struct buttons
    {

        public string Name;

        [System.Serializable]
        public enum onPressFunction
        {
            CloseWindow,
            LoadAScene
        }
        public onPressFunction OnPressFunction;

        public Rect ButtonSize;

        [Tooltip("This LoadSceneName must be a scene in File > Build Setting > Scenes in build. It will fail if LoadSceneName is spelt wrong or its not Added to the build")]
        public string LoadSceneName;

        public buttons(string name, onPressFunction function, Rect buttonSize, string SceneName)
        {
            Name = name;
            OnPressFunction = function;
            ButtonSize = buttonSize;
            LoadSceneName = SceneName;
        }

    }
    public buttons[] ButtonsOptions = new buttons[2]
    {
        new buttons("Enter", buttons.onPressFunction.LoadAScene, new Rect (10,60,80,30), "Inside"),
        new buttons("Not Yet", buttons.onPressFunction.CloseWindow, new Rect (110, 60,80,30), "")
    };

    [Space(10)]
    public GUISkin customSkin;



    // if this script is on an object with a collider display the Gui
    void OnTriggerEnter()
    {
        GuiOn = true;
    }


    void OnTriggerExit()
    {
        GuiOn = false;
    }

    void OnGUI()
    {

        if (customSkin != null)
        {
            GUI.skin = customSkin;
        }

        if (GuiOn == true)
        {
            // Make a group on the center of the screen
            GUI.BeginGroup(new Rect((Screen.width - BoxSize.width) / 2, (Screen.height - BoxSize.height) / 2, BoxSize.width, BoxSize.height));
            // All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

            // We'll make a box so you can see where the group is on-screen.
            GUI.Box(BoxSize, WindowTitle);

            for (int i = 0; i < ButtonsOptions.Length; ++i)
            {
                if (GUI.Button(ButtonsOptions[i].ButtonSize, ButtonsOptions[i].Name))
                {

                    if (ButtonsOptions[i].OnPressFunction == buttons.onPressFunction.CloseWindow)
                    {
                        GuiOn = false;
                    }
                    else if (ButtonsOptions[i].OnPressFunction == buttons.onPressFunction.LoadAScene)
                    {
                        SceneManager.LoadScene(ButtonsOptions[i].LoadSceneName, LoadSceneMode.Single);
                    }

                }
            }

            // End the group we started above. This is very important to remember!
            GUI.EndGroup();

        }


    }

}