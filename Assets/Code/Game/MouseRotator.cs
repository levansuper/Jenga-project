using System;
using UnityEngine;

namespace JengaGame
{
    [RequireComponent(typeof(Jenga))]
    public class MouseRotator : MonoBehaviour
    {
        private float _clickXPosition;
        private bool _isSelected;
        private Jenga _jenga;
        private float _originalYRotation;

        [SerializeField] private Transform _rotateObject;

        private void Start()
        {
            _jenga = GetComponent<Jenga>();
        }

        void Update()
    {

        if (_jenga.IsSelected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _originalYRotation = _rotateObject.transform.rotation.eulerAngles.y;
         
                Vector3 mousePos = Input.mousePosition;
                _isSelected = true;
                _clickXPosition = mousePos.x;
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            _originalYRotation = _rotateObject.transform.rotation.eulerAngles.y;
            _isSelected = false;
        }
        
        if (_isSelected)
        {
            float mouseXMovement = (Input.mousePosition.x - _clickXPosition);
            float newYRotation = _originalYRotation + mouseXMovement;
            
        
            Quaternion newRotation = Quaternion.Euler(
                _rotateObject.transform.rotation.eulerAngles.x,
                newYRotation,
                _rotateObject.transform.rotation.eulerAngles.z
            );
            _rotateObject.transform.rotation = newRotation;

        }
    }
    
    }
}