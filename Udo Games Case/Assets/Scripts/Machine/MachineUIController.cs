using UnityEngine;
using UDOGames;
using TMPro;

public class MachineUIController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Transform _cameraTransform;
    [SerializeField] private TMP_Text _countText;


    private void Start()
    {
        _cameraTransform = Camera.main.transform;
    }


    public void SetUIValues(Sprite sprite, string count)
    {
        _spriteRenderer.sprite = sprite;
        _countText.text = "x " + count;
    }



    private void Update()
    {
        SetLookAtCamera();
    }


    private void SetLookAtCamera()
    {
        Utility.SetLookAtCamera(transform, _cameraTransform);
    }
}
