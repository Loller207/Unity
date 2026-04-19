using UnityEngine;

namespace Lesson2
{

    public class Bullet : MonoBehaviour
    {

        public int damage = 35;
        public GameObject explosionFX;

        private void OnCollisionEnter(Collision other)
        {
            GameObject effect2 = Instantiate(explosionFX, transform.position, Quaternion.identity);
            Destroy(effect2, 1);
            if (other.gameObject.CompareTag("fake"))
            {
                GameObject effect = Instantiate(explosionFX, transform.position, Quaternion.identity);
                Destroy(effect, 1);
                if (other != null)
                {
                    Damage enemy = other.gameObject.GetComponentInParent<Damage>();

                    if (enemy != null)
                    {
                        enemy.TakeDamage(damage); // Chiamo il metodo TakeDamage dell'Enemy passando il danno del proiettile
                    }
                    Debug.Log("Hit!");
                }

            }
            Destroy(gameObject);


        }

        // Distrugge il proiettile dopo 3 secondi se non collide con nulla per evitare problemi di memoria
        private void Start()
        {
            Destroy(gameObject, 3f);
        }

    }

}