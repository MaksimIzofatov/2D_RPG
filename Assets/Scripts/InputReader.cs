using UnityEngine;

    public class InputReader : MonoBehaviour
    {
        public float DirectionX{get; private set;}
        public float DirectionY{get; private set;}
        private bool _isAddForce;
        
        private const string HORIZONTAL_AXIS = "Horizontal";
        private const string VERTICAL_AXIS = "Vertical";
        
        private void Update()
        {
            DirectionX = Input.GetAxis(HORIZONTAL_AXIS);
            DirectionY = Input.GetAxis(VERTICAL_AXIS);
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isAddForce = true;
            }
        }

        public bool GetIsAddForce()
        {
            bool temp = _isAddForce;
            _isAddForce = false;
            return temp;
        }
    }
