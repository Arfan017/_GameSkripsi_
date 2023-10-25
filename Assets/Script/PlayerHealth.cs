using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDemageable
{
    
    float IDemageable.Health { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    bool IDemageable.Targetable { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    bool IDemageable.Invicible { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public float _health = 3f;
    public bool _targetable = true;

    public void OnHit(float demage, Vector2 knockback)
    {
        throw new System.NotImplementedException();
    }

    public void OnHit(float demage)
    {
        throw new System.NotImplementedException();
    }

    public void OnObjectDestroyed()
    {
        throw new System.NotImplementedException();
    }

    void IDemageable.OnHit(float demage, Vector2 knockback)
    {
        throw new System.NotImplementedException();
    }

    void IDemageable.OnHit(float demage)
    {
        throw new System.NotImplementedException();
    }

    void IDemageable.OnObjectDestroyed()
    {
        throw new System.NotImplementedException();
    }
}