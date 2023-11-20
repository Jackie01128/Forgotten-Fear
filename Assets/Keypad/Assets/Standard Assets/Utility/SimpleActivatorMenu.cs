using UnityEngine;
using UnityEngine.UI; // Make sure to include this for Text

namespace UnityStandardAssets.Utility
{
    public class SimpleActivatorMenu : MonoBehaviour
    {
        // An incredibly simple menu which, when given references
        // to gameobjects in the scene
        public Text camSwitchButtonText; // Use UnityEngine.UI.Text instead of GUI.Text
        public GameObject[] objects;

        private int m_CurrentActiveObject;

        private void OnEnable()
        {
            // active object starts from the first in the array
            m_CurrentActiveObject = 0;
            UpdateCamSwitchButtonText();
        }

        private void UpdateCamSwitchButtonText()
        {
            if (camSwitchButtonText != null && objects.Length > 0)
            {
                camSwitchButtonText.text = objects[m_CurrentActiveObject].name;
            }
        }

        public void NextCamera()
        {
            int nextActiveObject = (m_CurrentActiveObject + 1) % objects.Length;

            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].SetActive(i == nextActiveObject);
            }

            m_CurrentActiveObject = nextActiveObject;
            UpdateCamSwitchButtonText();
        }
    }
}
