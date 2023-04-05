using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UDOGames;

public class PlayerStackController : MonoBehaviour
{
    public Stack<GameObject> objectStack = new Stack<GameObject>();


    [SerializeField] private Transform _stackTransform;
    [SerializeField] private Ease _ease;
    [SerializeField] private int _maxStackCount;
    [SerializeField] private float _moveDuration;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _removeTime;


    private GameObject _lastRemovedObject;
    private Transform _lastObjectTransform;
    private float _curentTime;
    private string _currentMaterialName;
    public string CurrentMaterialName => _currentMaterialName;
    private bool _isMoved = true;


    private void Start()
    {
        ResetCurrentTime();
    }



    public void AddNewObjectToStack(GameObject otherGameObject)
    {
        if (objectStack.Count < _maxStackCount)
        {
            otherGameObject.gameObject.GetComponent<BoxCollider>().enabled = false;


            if (objectStack.Count >= 1)
            {
                if (_currentMaterialName == otherGameObject.gameObject.GetComponent<MaterialController>().MaterialType)
                {
                    _lastObjectTransform = objectStack.Peek().transform;
                    objectStack.Push(otherGameObject);
                    objectStack.Peek().transform.SetParent(transform);
                    objectStack.Peek().transform.DOLocalMove(GetNewLocalPosition(), _moveDuration).SetEase(_ease);
                }
            }
            else
            {
                objectStack.Push(otherGameObject);
                objectStack.Peek().transform.SetParent(transform);
                objectStack.Peek().transform.DOLocalMove(_stackTransform.localPosition, _moveDuration).SetEase(_ease);
                _currentMaterialName = otherGameObject.gameObject.GetComponent<MaterialController>().MaterialType;
            }



        }
    }

    private Vector3 GetNewLocalPosition()
    {
        return new Vector3(_stackTransform.localPosition.x, _lastObjectTransform.localPosition.y + _offsetY, _stackTransform.localPosition.z);
    }



    public void RemovesObjectFromStack(GameObject otherObject, Transform nextMovePoint)
    {
        if (_curentTime <= 0)
        {
            if (objectStack.Count > 0)
            {
                if (otherObject.tag == Tag.MACHINE)
                {
                    for (int i = 0; i < otherObject.GetComponentInParent<MachineController>().itemData.Count; i++)
                    {
                        if (otherObject.GetComponentInParent<MachineController>().itemData.Keys.ElementAt(i) == _currentMaterialName)
                        {
                            if (_isMoved)
                            {
                                _isMoved = false;
                                ResetCurrentTime();
                                _lastRemovedObject = objectStack.Peek();
                                objectStack.Peek().transform.parent = null;
                                objectStack.Peek().transform.DOLocalMove(nextMovePoint.position, _moveDuration).SetEase(_ease).SetDelay(0.3f).
                                    OnComplete(() =>
                                        {
                                            _lastRemovedObject.transform.DOMove(otherObject.GetComponentInParent<MachineController>().machineInsidePoint.position, 2);
                                            _isMoved = true;
                                        });

                                objectStack.Pop();
                                otherObject.gameObject.GetComponentInParent<MachineController>().AddItemToData(_currentMaterialName);
                                ResetCurrentMaterialName();
                                otherObject.GetComponentInParent<MachineController>().CheckProductCount();
                            }
                        }
                    }
                }
                else if (otherObject.tag == Tag.BIN)
                {
                    ResetCurrentTime();


                    objectStack.Peek().transform.parent = null;
                    objectStack.Peek().transform.DOLocalMove(nextMovePoint.position, _moveDuration).SetEase(_ease);
                    objectStack.Pop();

                    ResetCurrentMaterialName();
                }
            }
        }
        else
        {
            _curentTime -= Time.deltaTime;
        }
    }



    private void ResetCurrentMaterialName()
    {
        if (objectStack.Count == 0)
        {
            _currentMaterialName = "";
        }
    }



    public void ResetCurrentTime()
    {
        _curentTime = _removeTime;
    }
}
