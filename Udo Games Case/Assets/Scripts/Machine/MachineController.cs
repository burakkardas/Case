using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

public class MachineController : MonoBehaviour
{
    public MachineData machineData = new MachineData();
    public PoolType poolType;
    public Dictionary<string, int> itemData = new Dictionary<string, int>();
    public List<string> itemNameList = new List<string>();
    public Transform movePoint;
    public Transform machineInsidePoint;
    public Vector3 currentScale;
    public Vector3 endScale;


    [SerializeField] private GameObject _uiPrefab;
    [SerializeField] private Vector3 _productSpawnPoint;
    [SerializeField] private Transform _machineUITransform;
    [SerializeField] private float _machineUIOffset;
    [SerializeField] private float _offset;


    private Transform _lastProductTransform;
    private int _index;


    private void Start()
    {
        GenerateItemData();
        GenerateMachineUI();
    }



    private void GenerateItemData()
    {
        for (int i = 0; i < machineData.materialDatas.Count; i++)
        {
            itemData.Add(machineData.materialDatas[i].materialName, 0);
            itemNameList.Add(itemData.ElementAt(i).Key);
        }
    }



    public void AddItemToData(string objectName)
    {
        itemData[objectName]++;
    }


    private void ProdusceNewProduct()
    {
        if (_index == itemData.Count)
        {
            for (int i = 0; i < itemData.Count; i++)
            {
                itemData[itemNameList[i]] -= machineData.materialDatas[i].materialCount;
            }
        }
    }




    public async Task ProduceNewProduct()
    {
        if (_index == itemData.Count)
        {
            for (int i = 0; i < itemData.Count; i++)
            {
                itemData[itemNameList[i]] -= machineData.materialDatas[i].materialCount;
            }
            await Task.Delay(3000);
            SpawnNewProduct();
        }
    }



    public void CheckProductCount()
    {
        _index = 0;
        for (int i = 0; i < itemData.Count; i++)
        {
            if (itemData[itemNameList[i]] >= machineData.materialDatas[i].materialCount)
            {
                _index++;
                var produceNewProduct = ProduceNewProduct();
            }
        }
    }




    public void GenerateMachineUI()
    {
        for (int i = 0; i < machineData.materialDatas.Count; i++)
        {
            GameObject newUI = Instantiate(_uiPrefab);
            newUI.transform.SetParent(_machineUITransform);
            newUI.transform.localPosition = new Vector3(_machineUITransform.localPosition.x, _machineUIOffset, _machineUITransform.localPosition.z);
            newUI.GetComponent<MachineUIController>().SetUIValues(machineData.materialDatas[i].materialSprite, machineData.materialDatas[i].materialCount.ToString());
            _machineUIOffset += 1;
        }
    }




    private void SpawnNewProduct()
    {
        ObjectPooler.Instance.SpawnObject(poolType, _productSpawnPoint, transform.rotation);
        _productSpawnPoint.x += _offset;
    }
}



[System.Serializable]
public class MachineData
{
    public List<MaterialData> materialDatas = new List<MaterialData>();
}



[System.Serializable]
public class MaterialData
{
    public Sprite materialSprite;
    public string materialName;
    public int materialCount;
}
