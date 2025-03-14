using UnityEngine;

    public class InputReader : MonoBehaviour
    {
        public float DirectionX{get; private set;}
        public float DirectionY{get; private set;}
        private bool _isAddForce;
        private bool _isInteract;
        private bool _isAttacking;
        
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

            if (Input.GetMouseButtonDown(0))
            {
                _isAttacking = true;
            }
        }
        
        public bool GetIsAddForce() => GetBoolAsTriggers(ref _isAddForce);
        
        public bool GetIsInteract() => GetBoolAsTriggers(ref _isInteract);
        public bool GetIsAttack() => GetBoolAsTriggers(ref _isAttacking);

        private bool GetBoolAsTriggers(ref bool value)
        {
            bool temp = value;
            value = false;
            return temp;
        }
    }

