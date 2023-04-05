using UnityEngine;

public class PlayerDataTransmitter : MonoBehaviour
{
    [SerializeField] private PlayerAnimationController _playerAnimationController;
    [SerializeField] private PlayerStackController _playerStackController;
    [SerializeField] private PlayerAttackController _playerAttackController;




    #region Attack
    public void AddToolToList(string materialType)
    {
        _playerAttackController.AddToolToList(materialType);
    }



    public void SetFalseColliderActivity()
    {
        _playerAttackController.SetFalseColliderActivity();
    }
    #endregion



    #region Stack
    public void AddNewObjectToStack(GameObject otherGameObject)
    {
        _playerStackController.AddNewObjectToStack(otherGameObject);
    }



    public void ResetCurrentTime()
    {
        _playerStackController.ResetCurrentTime();
    }



    public void RemovesObjectFromStack(GameObject otherObject, Transform nextMovePoint)
    {
        _playerStackController.RemovesObjectFromStack(otherObject, nextMovePoint);
    }



    public string GetCurrentMaterialName()
    {
        return _playerStackController.CurrentMaterialName;
    }
    #endregion



    #region Animation
    public void SetRunAnimation(bool value)
    {
        _playerAnimationController.SetRunAnimation(value);
    }



    public void PlayDanceAnimation()
    {
        _playerAnimationController.PlayDanceAnimation();
    }



    public void PlayAttackAnimation()
    {
        _playerAnimationController.PlayAttackAnimation();
    }
    #endregion
}
