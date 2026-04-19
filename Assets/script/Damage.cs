using UnityEngine;

public class Damage : MonoBehaviour
{
    [Header("Pidgeon Input Values")]
    [SerializeField] private int health = 100;
    [SerializeField] private bool fake;

    [Header("Pidgeon Sounds")]
    [SerializeField] private AudioClip sound;
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die() 
    {
        GameObject tempAudio = new GameObject("TempAudio");
        tempAudio.transform.position = transform.position;

        AudioSource aSource = tempAudio.AddComponent<AudioSource>();
        aSource.clip = sound;

        aSource.pitch = 2.0f;

        aSource.Play();
        Destroy(tempAudio, sound.length / aSource.pitch);
        Destroy(gameObject);
    }
}
