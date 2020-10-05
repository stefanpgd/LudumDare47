using UnityEngine;
#pragma warning disable 649

public class Arrow : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private AudioSource audio;

    private bool hitWall = false;
    private float timer;

    private void Update()
    {
        if(hitWall)
        {
            timer += 1f * Time.deltaTime;

            if(timer >= 0.15f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            audio.Play();
            renderer.enabled = false;
            hitWall = true;
        }
    }
}
