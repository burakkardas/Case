using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using System.Collections.Generic;


namespace UDOGames
{
    public class Utility
    {
        public static bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }


        public static void SetShakeCamera(Transform cameraVector, float duration, float strength, int vibrato, float randomness)
        {
            cameraVector.DOShakeRotation(duration, strength, vibrato, randomness);
        }



        public static void SetSmoothMoveToPoint(Transform objectTransform, Vector3 point, float duration, Ease ease)
        {
            objectTransform.DOLocalMove(point, duration).SetEase(ease);
        }


        public static void SetSmoothScaleTranstations(Transform objectTransform, Vector3 endScale, float duration)
        {
            objectTransform.DOScale(endScale, duration);
        }



        public static void SetLookAtCamera(Transform objectTransform, Transform cameraTransform)
        {
            objectTransform.rotation = Quaternion.LookRotation(objectTransform.position - cameraTransform.position);
        }
    }
}