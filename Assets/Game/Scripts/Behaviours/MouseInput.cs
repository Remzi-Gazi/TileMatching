using UnityEngine;

public class MouseInput : MonoBehaviour
{
    [SerializeField]
    private BoardController _boardController;

    private int _primaryMouseButton = 0;
    private Camera _mainCamera;

    private bool _allowInput;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _allowInput = true;
    }

    private void Update()
    {
        if (_allowInput && Input.GetMouseButtonDown(_primaryMouseButton))
        {
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, _mainCamera.farClipPlane * 2))
            {
                //bi event kaldırıp
                TileController interactedTileController = hit.transform.GetComponent<TileController>();
                _boardController.InteractWithTile(interactedTileController);
            }

        }
    }
    public void AllowInput()
    {
        _allowInput = true;
    }
}
