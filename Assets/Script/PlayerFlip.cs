using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer Player;
    [SerializeField] private SpriteRenderer Crossbow;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TopRight"))
        {
            Player.flipX = false;
            Debug.Log("tl");
        }
        if (collision.CompareTag("TopLeft"))
        {
            Player.flipX = true;
            Debug.Log("tr");
        }
        if (collision.CompareTag("BottomRight"))
        {
            Player.flipX = false;
            Debug.Log("br");
        }
        if (collision.CompareTag("BottomLeft"))
        {
            Player.flipX = true;
            Debug.Log("bl");
        }
    }
}
