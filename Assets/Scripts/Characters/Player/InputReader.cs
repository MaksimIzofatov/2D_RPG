using UnityEngine;

    public class InputReader : MonoBehaviour
    {
        public float DirectionX{get; private set;}
        public float DirectionY{get; private set;}
        private bool _isAddForce;
        private bool _isInteract;
        
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

            if (Input.GetKeyDown(KeyCode.E))
            {
                _isInteract = true;
            }
        }
        
        public bool GetIsAddForce() => GetBoolAsTriggers(ref _isAddForce);
        
        public bool GetIsInteract() => GetBoolAsTriggers(ref _isInteract);

        private bool GetBoolAsTriggers(ref bool value)
        {
            bool temp = value;
            value = false;
            return temp;
        }
    }

