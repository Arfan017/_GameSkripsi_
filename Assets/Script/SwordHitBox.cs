using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitBox : MonoBehaviour
{
    public float swordDemage = 1f;
    public float knockbackForce = 5000f;
    public Collider2D swordCollider;
    public Vector3 faceRight = new Vector3(0.11f, -0.017f, 0);
    public Vector3 faceLeft = new Vector3(-0.11f, -0.017f, 0);
    void Start(){
        if (swordCollider == null)
        {
            Debug.LogWarning("sword collider not set");
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        IDemageable demageableObject = col.GetComponent<IDemageable>();
        
        if (demageableObject != null)
        {
            Vector3 parentPosition = gameObject.GetComponent<Transform>().position;

            Vector2 direction = (Vector2) (col.gameObject.transform.position - parentPosition).normalized;

            Vector2 knockback = direction * knockbackForce;

            // col.collider.BroadcastMessage("OnHit", swordDemage, knockback);
            demageableObject.OnHit(swordDemage, knockback);
        }
        else
        {
            Debug.LogWarning("Collider does not implement IDemageable");
        }
    }

    void IsFacingRight(bool isFacingRight){
        if (isFacingRight)
        {
            gameObject.transform.localPosition = faceRight;
        }
        else
        {
            gameObject.transform.localPosition = faceLeft;
        }
    }
}
