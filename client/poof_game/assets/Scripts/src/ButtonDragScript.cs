﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class ButtonDragScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public bool dragOnSurfaces = true;
	public int buildingInstance;
	private GameObject draggingIcon;
	private RectTransform draggingPlane;
	GameObject modalPanel;
	bool hideModal;
	public void OnBeginDrag (PointerEventData eventData){
		//Canvas canvas = GetComponent<Canvas>();
		Canvas canvas = FindInParents<Canvas> (gameObject);
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

		RectTransform rekt = GetComponent<RectTransform> ();
		//image.SetNativeSize ();
		image.rectTransform.sizeDelta = rekt.sizeDelta;
		
		if (dragOnSurfaces)
			draggingPlane = transform as RectTransform;
		else
			draggingPlane = canvas.transform as RectTransform;
		
		setDraggedPosition(eventData);

		//find the modal panel
		modalPanel = FindInParents<BringToFront> (gameObject).gameObject;//searching for BringToFront because that's what modal panel has
		//hide the modal panel renderer
		hideModal = true;
		foreach (CanvasRenderer renderer in modalPanel.GetComponentsInChildren<CanvasRenderer>()) {
			//Debug.Log("i wish disabled renderer");
			//renderer. = false;
		}
	}

	public void OnDrag (PointerEventData eventData){
		if (draggingIcon != null) {
			setDraggedPosition(eventData);
		}
	}

	private void setDraggedPosition (PointerEventData eventData){
		if (dragOnSurfaces && eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null)
			draggingPlane = eventData.pointerEnter.transform as RectTransform;
		
		var rt = draggingIcon.GetComponent<RectTransform>();
		rt.position = Input.mousePosition;

//		Vector3 globalMousePos;
//		if (RectTransformUtility.PointInRectangleGlobalSpace(draggingPlane, out globalMousePos, eventData.position, eventData.pressEventCamera))
//		{
//			rt.position = globalMousePos;
//			rt.rotation = draggingPlane.rotation;
//		}
	}

	public void OnEndDrag (PointerEventData eventData){
		BuildingManager.Instance().makeNewBuilding(buildingInstance);
		if (draggingIcon != null) {
			Destroy(draggingIcon);
		}

	}

	static public T FindInParents<T>(GameObject go) where T : Component
	{
		if (go == null) return null;
		var comp = go.GetComponent<T>();
		
		if (comp != null)
			return comp;
		
		Transform t = go.transform.parent;
		while (t != null && comp == null)
		{
			comp = t.gameObject.GetComponent<T>();
			t = t.parent;
		}
		return comp;
	}
	// Update is called once per frame
	void Update () {
	}
}
