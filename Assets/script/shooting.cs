using UnityEngine;
using StarterAssets;

namespace Lesson2
{
    public class Shooting : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public Transform shootingPoint;
        public float bulletForce = 20f;
        public float bulletLifeTime = 1f; // Aggiunta variabile per il tempo di vita

        private StarterAssetsInputs _input;

        void Start()
        {
            _input = GetComponent<StarterAssetsInputs>();
        }

        void Update()
        {
            if (_input.shoot)
            {
                ExecuteShoot();
                _input.shoot = false;
            }
        }

        private void ExecuteShoot()
        {
            Quaternion extraRotation = Quaternion.Euler(90, 0, 0);
            GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation * extraRotation);

            Destroy(bullet, bulletLifeTime);

            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            if (bulletRb != null)
            {
                // --- DISATTIVA LA GRAVITÀ ---
                bulletRb.useGravity = false;
                // ----------------------------

                bulletRb.AddForce(shootingPoint.forward * bulletForce, ForceMode.VelocityChange);
            }
        }
    }
}