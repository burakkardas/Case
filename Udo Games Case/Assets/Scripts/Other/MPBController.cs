using UnityEngine;



public class MPBController : MonoBehaviour
{
    public Color mainColor;
    [SerializeField] private Renderer _objectRenderer;
    private MaterialPropertyBlock _materialPropertyBlock;



    private void Awake()
    {
        _materialPropertyBlock = new MaterialPropertyBlock();
        _materialPropertyBlock.SetColor("_BaseColor", mainColor);
        _objectRenderer.SetPropertyBlock(_materialPropertyBlock);
    }
}
