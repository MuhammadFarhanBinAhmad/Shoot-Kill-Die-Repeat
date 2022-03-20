using System;
using UnityEngine;

namespace Tarodev {
    
    public class Missile : MonoBehaviour {
        [Header("REFERENCES")] 
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private PlayerManager _target;

        [Header("MOVEMENT")] 
        [SerializeField] private float _speed = 15;
        [SerializeField] private float _rotateSpeed = 95;

        [Header("PREDICTION")] 
        [SerializeField] private float _maxDistancePredict = 100;
        [SerializeField] private float _minDistancePredict = 5;
        [SerializeField] private float _maxTimePrediction = 5;
        private Vector3 _standardPrediction, _deviatedPrediction;

        [Header("DEVIATION")] 
        [SerializeField] private float _deviationAmount = 50;
        [SerializeField] private float _deviationSpeed = 2;

        private void Start()
        {
            _target = FindObjectOfType<PlayerManager>();
        }

        private void FixedUpdate() {
            _rb.velocity = transform.forward * _speed;

            var leadTimePercentage = Mathf.InverseLerp(_minDistancePredict, _maxDistancePredict, Vector3.Distance(transform.position, _target.transform.position));

            PredictMovement(leadTimePercentage);

            AddDeviation(leadTimePercentage);

            RotateRocket();
        }

        private void PredictMovement(float leadTimePercentage) {
            var predictionTime = Mathf.Lerp(0, _maxTimePrediction, leadTimePercentage);

            _standardPrediction = _target.GetComponent<Rigidbody>().position + _target.GetComponent<Rigidbody>().velocity * predictionTime;
        }

        private void AddDeviation(float leadTimePercentage) {
            var deviation = new Vector3(Mathf.Cos(Time.time * _deviationSpeed), 0, 0);
            
            var predictionOffset = transform.TransformDirection(deviation) * _deviationAmount * leadTimePercentage;

            _deviatedPrediction = _standardPrediction + predictionOffset;
        }

        private void RotateRocket() {
            var heading = _deviatedPrediction - transform.position;

            var rotation = Quaternion.LookRotation(heading);
            _rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, _rotateSpeed * Time.deltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<BulletStats_ForPlayer>() !=null || other.GetComponent<PlayerManager>() != null || other.GetComponent<Explosion_Universal>() != null || other.tag == "Ground")
            {
                AmmoPool AP = FindObjectOfType<AmmoPool>();
                for (int i = 0; i < AP.Explosion_Universal_Pool.Count; i++)
                {
                    if (!AP.Explosion_Universal_Pool[i].activeInHierarchy)
                    {
                        AP.Explosion_Universal_Pool[i].transform.position = this.transform.position;
                        AP.Explosion_Universal_Pool[i].transform.rotation = this.transform.rotation;
                        AP.Explosion_Universal_Pool[i].SetActive(true);
                        break;
                    }
                }
                Destroy(gameObject);
            }
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _standardPrediction);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(_standardPrediction, _deviatedPrediction);
        }
    }
}