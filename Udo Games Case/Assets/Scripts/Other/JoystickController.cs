using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class JoystickController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _imgJoystickBg;
    [SerializeField] private Image _imgJoystick;


    private Vector2 _positionInput;



    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_imgJoystickBg.rectTransform, eventData.position, eventData.pressEventCamera, out _positionInput))
        {
            _positionInput.x = _positionInput.x / (_imgJoystickBg.rectTransform.sizeDelta.x);
            _positionInput.y = _positionInput.y / (_imgJoystickBg.rectTransform.sizeDelta.y);


            if (_positionInput.magnitude > 1.0f)
            {
                _positionInput = _positionInput.normalized;
            }


            _imgJoystick.rectTransform.anchoredPosition = new Vector2(_positionInput.x * (_imgJoystickBg.rectTransform.sizeDelta.x / 2), _positionInput.y * (_imgJoystickBg.rectTransform.sizeDelta.y / 2));
        }
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }



    public void OnPointerUp(PointerEventData eventData)
    {
        _positionInput = Vector2.zero;
        _imgJoystick.rectTransform.anchoredPosition = Vector2.zero;
    }



    public float Horizontal()
    {
        if (_positionInput.x != 0)
            return _positionInput.x;
        else
            return Input.GetAxis("Horizontal");
    }



    public float Vertical()
    {
        if (_positionInput.y != 0)
            return _positionInput.y;
        else
            return Input.GetAxis("Vertical");
    }
}
