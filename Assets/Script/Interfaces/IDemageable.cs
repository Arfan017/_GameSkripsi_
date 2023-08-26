using UnityEngine;

public interface IDemageable
{
    public float Health{
        set;
        get;
    }

    public bool Targetable{
        set;
        get;
    }

    public bool Invicible{
        set;
        get;
    }

    public void OnHit(
        float demage, Vector2 knockback
    );

    public void OnHit(
        float demage
    );

    public void OnObjectDestroyed();
}