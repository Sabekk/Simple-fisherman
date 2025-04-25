using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Fishing
{
    public class Floater : MonoBehaviour
    {
        #region ACTION

        public event Action OnFloorHit;
        public event Action<bool> OnWaterHit;

        #endregion

        #region VARIABLES

        [SerializeField] private Rigidbody rb;
        [SerializeField] private float depthBeforeSubmerged = 1f;
        [SerializeField] private float displacementAmount = 3f;
        [SerializeField] private float rotationSpeed = 2f;

        private const string LAYER_WATER = "Water";
        private bool isOnWater;

        #endregion

        #region PROPERTIES

        public Rigidbody Rigidbody => rb;
        public bool IsOnWater
        {
            get
            {
                return isOnWater;
            }
            set
            {
                bool oldValue = isOnWater;
                isOnWater = value;
                if (oldValue != isOnWater)
                    OnWaterHit?.Invoke(isOnWater);
            }
        }

        #endregion

        #region UNITY_METHODS

        private void FixedUpdate()
        {
            if (CheckFloor(out Transform transformHit) && transformHit != null)
            {
                OnFloorHit?.Invoke();
                if (transformHit.gameObject.layer != LayerMask.NameToLayer(LAYER_WATER))
                {
                    if (IsOnWater == true)
                        IsOnWater = false;
                    return;
                }

                if (IsOnWater == false)
                    IsOnWater = true;

                float displacementMultipler = Mathf.Clamp01(transformHit.position.y - transform.position.y / depthBeforeSubmerged) * displacementAmount;
                rb.AddForce(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultipler, 0f), ForceMode.Acceleration);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(Vector3.zero), rotationSpeed * Time.deltaTime);
            }
            else
            {
                if (IsOnWater == true)
                    IsOnWater = false;
            }
        }

        #endregion

        #region METHODS

        //TODO Change to coroutine with drowing
        public void MakeBite()
        {
            rb.AddForce(Vector3.up, ForceMode.Impulse);
        }

        //Delete and use objectpooling
        public void DestroyFloater()
        {
            Destroy(gameObject);
        }

        private bool CheckFloor(out Transform transformHit)
        {
            transformHit = null;

            Ray ray = new Ray(transform.position + Vector3.up * 5f, Vector3.down);
            RaycastHit[] hits = Physics.RaycastAll(ray, 5.3f);

            foreach (RaycastHit hit in hits)
            {
                if (hit.transform == transform)
                    continue;

                transformHit = hit.transform;
                return true;
            }

            return false;
        }


        #endregion
    }
}