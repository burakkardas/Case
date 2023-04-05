using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UDOGames;
using DG.Tweening;


public class PlayerAttackController : MonoBehaviour
{
    public List<GameObject> toolList = new List<GameObject>();


    [SerializeField] private PlayerDataTransmitter _playerDataTransmitter;
    [SerializeField] private List<GameObject> currentToolList = new List<GameObject>();
    [SerializeField] private Image _reloadImage;
    [SerializeField] private float _attackTime;


    private float _currentTime;
    private float _currentLerp;
    private int _toolIndex = 0;
    private bool _canAttack = true;
    private bool _isActiveCollider = false;



    private void Start()
    {
        ResetAttackTime();
    }


    private void Update()
    {
        SetTimeForAttack();
    }


    public void AttackButton()
    {
        if (_canAttack)
        {
            _canAttack = false;
            _currentLerp = 1;
            _playerDataTransmitter.PlayAttackAnimation();
            SetSmoothReloadImage();
        }
    }



    private void SetTimeForAttack()
    {
        if (!_canAttack)
        {
            if (_currentTime <= 0)
            {
                ResetAttackTime();
                _canAttack = true;
            }
            else
            {
                _currentTime -= Time.deltaTime;
            }
        }
    }



    public void AddToolToList(string materialType)
    {
        for (int i = 0; i < currentToolList.Count; i++)
        {
            if (CanTakeTool(i, materialType))
            {
                toolList.Add(currentToolList[i]);
            }
        }
    }



    public void SetTrueColliderActivity()
    {
        _isActiveCollider = true;
        SetActivityColliderActiveTool();
    }



    public void SetFalseColliderActivity()
    {
        _isActiveCollider = false;
        SetActivityColliderActiveTool();
    }


    public void SetActivityColliderActiveTool()
    {
        toolList[_toolIndex].GetComponent<BoxCollider>().enabled = _isActiveCollider;
    }



    private bool CanTakeTool(int index, string materialType)
    {
        return currentToolList[index].GetComponent<MaterialController>().MaterialType == materialType && !toolList.Contains(currentToolList[index]);
    }



    public void ChangeTool()
    {
        if (toolList.Count > 1)
        {
            _toolIndex++;

            for (int i = 0; i < toolList.Count; i++)
            {
                toolList[i].SetActive(false);
            }

            if (_toolIndex >= toolList.Count)
            {
                _toolIndex = 0;
            }

            toolList[_toolIndex].SetActive(true);
        }
    }




    private void SetSmoothReloadImage()
    {
        DOTween.To(() => _currentLerp, x => _currentLerp = x, 0, _attackTime).
            OnUpdate(() =>
            {
                _reloadImage.fillAmount = _currentLerp;
            });
    }



    private void ResetAttackTime()
    {
        _currentTime = _attackTime;
    }
}
