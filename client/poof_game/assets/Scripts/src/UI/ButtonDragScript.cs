using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * A script which allows an object to be dragged around the scene
 */
public class ButtonDragScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public bool dragOnSurfaces = true;
	private GameObject draggingIcon;
	private RectTransform draggingPlane;
    public Building building { get; set; }

    /**
     * Provides functionality for this object
     * when a user begins to drag it
     */
	public void OnBeginDrag (PointerEventData eventData){
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

		image.sprite = GetComponent<Image>().sprite;

		RectTransform rekt = GetComponent<RectTransform> ();
		image.rectTransform.sizeDelta = rekt.sizeDelta;
		
		if (dragOnSurfaces)
			draggingPlane = transform as RectTransform;
		else
			draggingPlane = canvas.transform as RectTransform;
		
		setDraggedPosition(eventData);

	}

    /**
     * Provides functionality for this object while
     * it is being dragged
     */
	public void OnDrag (PointerEventData eventData){
		if (draggingIcon != null) {
			setDraggedPosition(eventData);
		}
	}

    /**
     * Establishes the position of this object
     * based on the location of the mouse pointer
     */
	private void setDraggedPosition (PointerEventData eventData){
		if (dragOnSurfaces && eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null)
			draggingPlane = eventData.pointerEnter.transform as RectTransform;
		
		var rt = draggingIcon.GetComponent<RectTransform>();
		rt.position = Input.mousePosition;
	}

    /**
     * Establishes functionality for this object as
     * a drag completes
     */
	public void OnEndDrag (PointerEventData eventData){
		BuildingManager.buildingManager.makeNewBuilding(building);
		if (draggingIcon != null) {
			Destroy(draggingIcon);
		}
		BuildingPanel.buildingPanel.windowState = false;
	}

    /**
     * A generic method which will return a gameobject of
     * the type provided in the parent hierarchy if it exists
     *
     * Unity already supports this I believe
     * this.transform.GetComponentInParent<T>()
     */
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
}
