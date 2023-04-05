using UnityEngine;
using UDOGames;

public class PlayerCollisionController : MonoBehaviour
{
    [SerializeField] private PlayerDataTransmitter _playerDataTransmitter;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tag.MATERIAL))
        {
            if (other.gameObject.GetComponent<MaterialController>().IsCollectable)
                _playerDataTransmitter.AddNewObjectToStack(other.gameObject);
            else
            {
                _playerDataTransmitter.AddToolToList(other.gameObject.GetComponent<MaterialController>().MaterialType);
                other.gameObject.GetComponent<MaterialController>().DeSpawnMaterial();
            }
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(Tag.MACHINE))
        {
            _playerDataTransmitter.RemovesObjectFromStack(other.gameObject, other.gameObject.GetComponentInParent<MachineController>().movePoint);
            Utility.SetSmoothScaleTranstations(other.gameObject.transform, other.gameObject.GetComponentInParent<MachineController>().endScale, 0.25f);
        }


        if (other.gameObject.CompareTag(Tag.BIN))
        {
            _playerDataTransmitter.RemovesObjectFromStack(other.gameObject, other.gameObject.GetComponentInParent<BinController>().movePoint);
            Utility.SetSmoothScaleTranstations(other.gameObject.transform, other.gameObject.GetComponentInParent<BinController>().endScale, 0.25f);
        }



        if (other.gameObject.CompareTag(Tag.BROKE_ROCK))
        {

        }
    }





    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Tag.MACHINE))
        {
            _playerDataTransmitter.ResetCurrentTime();
            Utility.SetSmoothScaleTranstations(other.gameObject.transform, other.gameObject.GetComponentInParent<MachineController>().currentScale, 0.25f);
        }


        if (other.gameObject.CompareTag(Tag.BIN))
        {
            _playerDataTransmitter.RemovesObjectFromStack(other.gameObject, other.gameObject.GetComponentInParent<BinController>().movePoint);
            Utility.SetSmoothScaleTranstations(other.gameObject.transform, other.gameObject.GetComponentInParent<BinController>().currentScale, 0.25f);
        }
    }
}
