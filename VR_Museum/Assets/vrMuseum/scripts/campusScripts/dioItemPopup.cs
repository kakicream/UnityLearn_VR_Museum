using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using vrMuseumCourseware;

public class dioItemPopup : MonoBehaviour {

    [SerializeField] private TextMeshPro m_tmpLabel;

    private gazeableObject m_InteractiveItem;
    private gazeTimerVisual m_SelectionRadial;         // This controls when the selection is complete.
    private Renderer m_Renderer;

    mainPlayer thePlayer;

    private bool m_GazeOver;                                            // Whether the user is looking at the VRInteractiveItem currently.
    private bool m_Selected;                                            // Whether the user is looking at the VRInteractiveItem currently.

    private void Awake()
    {
        if (m_InteractiveItem == null)
            m_InteractiveItem = this.GetComponent<gazeableObject>();

        if (thePlayer == null)
        {
            thePlayer = GameObject.FindObjectOfType<mainPlayer>();
            m_SelectionRadial = thePlayer.GetComponent<gazeTimerVisual>();
        }
    }


    private void OnEnable()
    {
        m_InteractiveItem.OnOver += HandleOver;
        m_InteractiveItem.OnOut += HandleOut;
        m_SelectionRadial.OnSelectionComplete += HandleSelectionComplete;
        m_tmpLabel.enabled = false;
    }


    private void OnDisable()
    {
        m_InteractiveItem.OnOver -= HandleOver;
        m_InteractiveItem.OnOut -= HandleOut;
        m_SelectionRadial.OnSelectionComplete -= HandleSelectionComplete;
    }


    private void HandleSelectionComplete()
    {
        // If the user is looking at the rendering of the scene when the radial's selection finishes, activate the button.
        if (m_GazeOver)
            StartCoroutine(ActivateButton());
    }

    private IEnumerator ActivateButton()
    {
        // If anything is subscribed to the OnButtonSelected event, call it.
        //        if (OnButtonSelected != null)
        //            OnButtonSelected(this);

//        thePlayer.toggleAnim(setting);

        // Wait for the camera to fade out.
        yield return null;// StartCoroutine(m_CameraFade.BeginFadeOut(true));

        // Load the level.
        //        SceneManager.LoadScene(m_SceneToLoad, LoadSceneMode.Single);
    }

    //Handle the Over event
    private void HandleOver()
    {
        m_GazeOver = true;
        m_tmpLabel.enabled = true;
    }


    //Handle the Out event
    private void HandleOut()
    {
        // When the user looks away from the rendering of the scene, hide the radial.
        if (butPressedOnMe)
        {
            butPressedOnMe = false;
            m_SelectionRadial.Hide();
        }

        m_GazeOver = false;
        m_tmpLabel.enabled = false;
    }

    bool butPressedOnMe = false;
    void Update()
    {
        if (m_GazeOver)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                butPressedOnMe = true;
                m_SelectionRadial.Show();
            }
            if (Input.GetButtonUp("Fire1"))
            {
                butPressedOnMe = false;
                m_SelectionRadial.Hide();
            }
        }
    }
}
