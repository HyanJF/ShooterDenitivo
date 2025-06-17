using UnityEngine;
using Mirror;


public abstract class APickUp : NetworkBehaviour
{
	public PowerUpSpawner maiSpawner;
	[SyncVar(hook =(nameof(Activechanged)))]
	public bool isActive = true;

    private void OnTriggerEnter(Collider other)
    {
		if (!isServer) return;
		if (other.gameObject.TryGetComponent(out Jugador jugador))
		{
			TakeEffect(jugador);
		}
    }

	public void Initialize(PowerUpSpawner spawner)
    {
        maiSpawner = spawner;
    }

    [Server]
	public virtual void TakeEffect(Jugador playa) 
	{
		maiSpawner.StartCoroutine(nameof(PowerUpSpawner.CollectPowerUp));
	}

	private void Activechanged(bool oldActive, bool newActive)
	{
		gameObject.SetActive(newActive);
	}
}
