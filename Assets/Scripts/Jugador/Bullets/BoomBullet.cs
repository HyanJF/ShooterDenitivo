using UnityEngine;
using Mirror;

public class BoomBullet : ABullet
{
    public float radius = 2.5f;
    public float forcePush = 15;

    private void OnCollisionEnter(Collision collision)
    {
        HasHit(collision);
    }

    public override void HasHit(Collision col)
    {
        
        Push(col.contacts[0].point);

        base.HasHit(col);
    }

    [Server]
    private void Push(Vector3 point)
    {
        var hits = Physics.OverlapSphere(point, radius);
        foreach (var deadGuy in hits)
        {
           if (deadGuy.TryGetComponent(out Jugador playah))
           {
                playah.GitPushed(point, forcePush, radius);
           }
        }
    }
}