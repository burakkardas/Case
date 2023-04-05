using System.Collections.Generic;
using UnityEngine;
using UDOGames;
using System.Threading.Tasks;

public class RockController : MonoBehaviour
{

    [SerializeField] private List<GameObject> pieceList = new List<GameObject>();
    [SerializeField] private GameObject _rockPrefab;
    [SerializeField] private PoolType poolType;
    [SerializeField] private int _objectSpawnCount;
    [SerializeField] private int _minPieceCount;
    [SerializeField] private float _radius;



    private Transform _cameraTransform;
    private List<GameObject> currentPieceList = new List<GameObject>();
    private float _angle;
    private float _newPositionX;
    private float _newPositionZ;
    private bool _isBrockable;


    private void Start()
    {
        _cameraTransform = GameObject.FindGameObjectWithTag(Tag.CAMERA).transform;
    }



    public void SetActivitiyRockPiece(int index)
    {
        if (_objectSpawnCount > pieceList.Count) _objectSpawnCount = pieceList.Count;

        for (int i = 0; i < _objectSpawnCount; i++)
        {

        }
        Debug.Log("Çalıştı");
        pieceList[index].SetActive(false);
        currentPieceList.Add(pieceList[index]);
        pieceList.RemoveAt(index);
    }



    private Vector3 GetRandomSpawnPosition()
    {
        _angle = Random.Range(0, 360);
        _newPositionX = _radius * Mathf.Cos(_angle);
        _newPositionZ = _radius * Mathf.Sin(_angle);

        return new Vector3(transform.position.x + _newPositionX, 1f, transform.position.z + _newPositionZ);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tag.PICK_AXE) && !_isBrockable)
        {
            other.gameObject.GetComponentInParent<PlayerDataTransmitter>().SetFalseColliderActivity();
            _objectSpawnCount = 2;

            for (int i = 0; i < _objectSpawnCount; i++)
            {
                SetActivitiyRockPiece(i);
                ObjectPooler.Instance.SpawnObject(poolType, GetRandomSpawnPosition(), _rockPrefab.transform.rotation);
            }

            if (IsRockBroken())
            {
                var setAllActivePieces = SetAllActivePieces();
            }


            Utility.SetShakeCamera(_cameraTransform, 0.2f, 1, 2, 90);
        }
    }


    private async Task SetAllActivePieces()
    {
        await Task.Delay(2000);
        currentPieceList.Clear();
        pieceList.Clear();
        foreach (Transform child in this.gameObject.transform)
        {
            child.gameObject.SetActive(true);
            pieceList.Add(child.gameObject);
        }
        _isBrockable = false;
    }


    private bool IsRockBroken()
    {
        bool isRockBroken = true;
        int brokenIndex = 0;

        foreach (Transform child in this.gameObject.transform)
        {
            if (child.gameObject.activeSelf)
            {
                brokenIndex++;
            }
        }

        if (brokenIndex > 2) isRockBroken = false;
        else _isBrockable = true;
        return isRockBroken;
    }
}
