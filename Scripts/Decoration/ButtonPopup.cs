using UnityEngine;
using Vuforia;
using System.Collections;
using UnityEngine.UI;


public class ButtonPopup : MonoBehaviour, ITrackableEventHandler
{
	CardView cView;
	public Canvas canvas;
	[SerializeField] int cNumber;

	private TrackableBehaviour mTrackableBehaviour;

	private bool mShowGUIButton = false;
	private Rect mButtonRect = new Rect(50, 50, 120, 60);

	void Start()
	{
		cView = FindObjectOfType<CardView>();
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}

	public void OnTrackableStateChanged(
									TrackableBehaviour.Status previousStatus,
									TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED)
		{
			mShowGUIButton = true;
		}
		else
		{
			mShowGUIButton = false;
			canvas.enabled = false;
		}
	}

	void OnGUI()
	{
		bool buttonPress = false;
		if (mShowGUIButton)
		{
			// draw the GUI button
			//if (GUI.Button(mButtonRect, "Meaning")) {
			// do something on button click
			if (Input.GetKeyDown(KeyCode.Space) && buttonPress == false)
			{
				cView.OpenCardsAR(cNumber);
				canvas.enabled = true;
				buttonPress = true;
				Debug.Log(buttonPress);
				if (Input.GetKeyDown(KeyCode.DownArrow) && buttonPress == true)
				{
					cView.OpenCardsAR(cNumber);
					canvas.enabled = true;
					buttonPress = false;
					Debug.Log(buttonPress);
				}

			}
		}
	}
}
