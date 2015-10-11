using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class ButtonDragScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public bool dragOnSurfaces = true;
	private GameObject draggingIcon;
	private RectTransform draggingPlane;

	public void OnBeginDrag (PointerEventData eventData){
		Canvas canvas = GetComponent<Canvas>(gameObject);
		if (canvas == null) {
			Debug.Log ("ButtonDragScript: could not find canvas");
			return;
		}
		
		// We have clicked something that can be dragged.
		// What we want to do is create an icon for this.
		draggingIcon = new GameObject("icon");
		
		draggingIcon.transform.SetParent (canvas.transform, false);
		draggingIcon.transform.SetAsLastSibling();
		
		var image = draggingIcon.AddComponent<Image>();
		// The icon will be under the cursor.
		// We want it to be ignored by the event system.
		//draggingIcon.AddComponent<IgnoreRaycast>();
		
		image.sprite = GetComponent<Image>().sprite;
		image.SetNativeSize();
		
		if (dragOnSurfaces)
			draggingPlane = transform as RectTransform;
		else
			draggingPlane = canvas.transform as RectTransform;
		
		setDraggedPosition(eventData);
	}

	public void onDrag (PointerEventData eventData){
		if (draggingIcon != null) {
			setDraggedPosition(eventData);
		}
	}

	private void setDraggedPosition (PointerEventData eventData){
		if (dragOnSurfaces && eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null)
			draggingPlane = eventData.pointerEnter.transform as RectTransform;
		
		var rt = draggingIcon.GetComponent<RectTransform>();
		Vector3 globalMousePos;
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingPlane, out globalMousePos, eventData.position, eventData.pressEventCamera))
		{
			rt.position = globalMousePos;
			rt.rotation = draggingPlane.rotation;
		}
	}

	public void endDrag (PointerEventData eventData){
		if (draggingIcon != null) {
			Destroy(draggingIcon);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
