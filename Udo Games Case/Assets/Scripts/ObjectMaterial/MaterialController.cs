using UnityEngine;

public class MaterialController : MonoBehaviour
{
    public PoolType poolType;

    [SerializeField] private string _materialType;
    public string MaterialType => _materialType;

    [SerializeField] private bool _isCollectable;
    public bool IsCollectable => _isCollectable;


    public void DeSpawnMaterial()
    {
        ObjectPooler.Instance.DeSpawnObject(poolType, gameObject);
    }
}
