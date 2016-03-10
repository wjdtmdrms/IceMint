using UnityEditor;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TGMap))]
public class TGMapMouse : MonoBehaviour {

    public Camera mainCamera;
    public Transform tileSelectorTransform;
    TGMap _tgmap;
    Renderer rend;
    Vector3 destTileCoord;

    void Start()
    {
        _tgmap = GetComponent<TGMap>();
        rend = gameObject.GetComponent<Renderer>();
    }

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast (ray, out hitInfo, Mathf.Infinity))
        {
            //Debug.Log(hitInfo.point - transform.position);
            int x = Mathf.FloorToInt(hitInfo.point.x / _tgmap.tileSize);
            int z = Mathf.FloorToInt(hitInfo.point.z / _tgmap.tileSize);

            destTileCoord.x = x + (_tgmap.tileSize / 2);
            destTileCoord.y = tileSelectorTransform.position.y;
            destTileCoord.z = z + (_tgmap.tileSize / 2);

            //Debug.Log(destTileCoord);
            tileSelectorTransform.position = destTileCoord;
        }
        else
        {
            
        }
    }
    /*
    void OnGUI()
    {
        Event evt = Event.current;
        Rect contextRect = new Rect(10, 10, 10, 10);
        if (evt.type == EventType.mouseDown)
        {
            Debug.Log("Clicked");
            Vector2 mousePos = new Vector2(tileSelectorTransform.position.x, tileSelectorTransform.position.z);
            EditorUtility.DisplayPopupMenu(new Rect(mousePos.x, mousePos.y, 0, 0), "Assets/", null);
            
            if (contextRect.Contains(mousePos))
            {
                EditorUtility.DisplayPopupMenu(new Rect(mousePos.x, mousePos.y, 0, 0), "Assets/", null);
            }
            
        }
    }
    */
}
