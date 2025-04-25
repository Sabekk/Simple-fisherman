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

        #endregion

        #region VARIABLES

        [SerializeField] private Rigidbody rb;
        [SerializeField] private float depthBeforeSubmerged = 1f;
        [SerializeField] private float displacementAmount = 3f;
        [SerializeField] private float rotationSpeed = 2f;

        private const string LAYER_WATER = "Water";

        #endregion

        #region PROPERTIES

        public bool IsInWater { get; set; }
        public Rigidbody Rigidbody => rb;

        #endregion

        #region UNITY_METHODS

        private void FixedUpdate()
        {
            if (CheckFloor(out Transform transformHit) && transformHit != null)
            {
                OnFloorHit?.Invoke();

                if (transformHit.gameObject.layer != LayerMask.NameToLayer(LAYER_WATER))
                    return;

                float displacementMultipler = Mathf.Clamp01(transformHit.position.y - transform.position.y / depthBeforeSubmerged) * displacementAmount;
                rb.AddForce(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultipler, 0f), ForceMode.Acceleration);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(Vector3.zero), rotationSpeed * Time.deltaTime);
            }
        }

        #endregion

        #region METHODS

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