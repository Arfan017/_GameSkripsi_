using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
// , IDataPersistence
{
    // bool isAlive = false;
    public float damage = 1;
    public float knockbackForce = 100f;
    public float moveSpeed = 500f;
    public DetectionZone detectionZone;
    Rigidbody2D rb;
    DamageableCharacter damageableCharacter;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damageableCharacter = GetComponent<DamageableCharacter>();
    }

    private void FixedUpdate()
    {
        if (damageableCharacter.Targetable && detectionZone.detectedObjs.Count > 0)
        {
            // Debug.Log("Player deteksi");
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;

            rb.AddForce(direction * moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Collider2D collider = col.collider;
        IDemageable damageable = collider.GetComponent<IDemageable>();

        if (damageable != null)
        {
            Vector2 direction = (collider.transform.position - transform.position).normalized;

            Vector2 knockback = direction * knockbackForce;

            damageable.OnHit(damage, knockback);
        }
    }

    // public void LoadData(GameData data)
    // {
    //     data.EnemyDefeat.TryGetValue(id, out isAlive);
    //     if (isAlive)
    //     {
    //         gameObject.SetActive(false);
    //     }
    // }

    // public void SaveData(GameData data)
    // {
    //     if (data.EnemyDefeat.ContainsKey(id))
    //     {
    //         data.EnemyDefeat.Remove(id);
    //     }
    //     data.EnemyDefeat.Add(id, collected);
    // }
}
