using UnityEngine;



public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private PlayerDataTransmitter _playerDataTransmitter;
    [SerializeField] private JoystickController _joystickController;
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private float _movementSpeed;



    private float _horizontalValue;
    private float _verticalValue;


    private void Update()
    {
        GetMovementInputValues();
    }



    private void FixedUpdate()
    {
        SetPlayerMovement();
        SetRotatePlayerAndRunAnimation();
    }




    private void GetMovementInputValues()
    {
        _horizontalValue = _joystickController.Horizontal();
        _verticalValue = _joystickController.Vertical();
    }




    private void SetPlayerMovement()
    {
        if (CanMovement())
        {
            _playerRigidbody.velocity = GetNewVelocity();
        }
    }



    private Vector3 GetNewVelocity()
    {
        return new Vector3(_horizontalValue * _movementSpeed * Time.fixedDeltaTime, _playerRigidbody.velocity.y, _verticalValue * _movementSpeed * Time.fixedDeltaTime);
    }



    private bool CanMovement()
    {
        return GameManager.Instance.IsStart && !GameManager.Instance.IsGameEnd;
    }



    private void SetRotatePlayerAndRunAnimation()
    {
        if (Mathf.Abs(_horizontalValue) > 0 || Mathf.Abs(_verticalValue) > 0)
        {
            transform.rotation = Quaternion.LookRotation(_playerRigidbody.velocity);
            _playerDataTransmitter.SetRunAnimation(true);
        }
        else
            _playerDataTransmitter.SetRunAnimation(false);

    }
}
