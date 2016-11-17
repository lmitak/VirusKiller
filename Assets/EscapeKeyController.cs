using UnityEngine;
using System.Collections;

public class EscapeKeyController : MonoBehaviour {

    public LevelStatsDisplay popUpPanel;
    public TransitionBall transitionBall;

    private Camera mainCamera;
    private Vector3 popUpPanelPosition;
    private float popUpPanelHeight;

	// Use this for initialization
	void Start () {
        mainCamera = Camera.main;
        popUpPanelPosition = mainCamera.WorldToScreenPoint(popUpPanel.transform.position);
        popUpPanelHeight = popUpPanel.GetComponent<RectTransform>().rect.height;

     }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (popUpPanel.isLastSibling())
            {
                transitionBall.KickAntibacterialBall();
            }else
            {
                Application.Quit();
            }
        }

        foreach(Touch touch in Input.touches)
        {

            if ( ( (popUpPanelPosition.y + popUpPanelHeight) < (touch.rawPosition.y - (touch.radius + touch.radiusVariance )) )
                && transitionBall.GetComponent<Rigidbody2D>().isKinematic
                && popUpPanel.isLastSibling())
            {
                transitionBall.KickAntibacterialBall();
            }
        }


	}
}
