using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform cam;
    [SerializeField] float range = 50f;
    [SerializeField] float damage = 10f;

    public ParticleSystem muzzleFlash;

    [SerializeField] LayerMask HitLayerMask;

    public int playerIndex;

void Awake() 
{
    cam = GetComponentInParent<Camera>().transform;
    muzzleFlash = GetComponentInChildren<ParticleSystem>();
}

   

    public void Shoot()
{
       

        if (TurnManager.GetInstance().IsItPlayerTurn(playerIndex))
        {
            Debug.Log(muzzleFlash.isPlaying);
            var emission = muzzleFlash.emission;
            emission.enabled = true;
            muzzleFlash.Play();
            RaycastHit hit;

            Debug.DrawRay(cam.position, cam.forward, Color.red, 10f);

            if (Physics.Raycast(cam.position, cam.forward, out hit, range, HitLayerMask))//true=hit something
            {
                PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
                

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }
                else
                {
                    Debug.Log("not hitting a player");
                }
            }
        }
        
}

}
