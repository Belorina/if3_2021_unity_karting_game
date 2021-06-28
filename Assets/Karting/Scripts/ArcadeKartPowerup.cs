using KartGame.KartSystems;
using UnityEngine;
using UnityEngine.Events;

public class ArcadeKartPowerup : MonoBehaviour {

    public ArcadeKart.StatPowerup boostStats = new ArcadeKart.StatPowerup
    {
        MaxTime = 5
    };

    public bool isCoolingDown { get; private set; }     // get = public ; set = private ; ==> lecture seule 
    public float lastActivatedTimestamp { get; private set; }

    public float cooldown = 5f;

    public bool disableGameObjectWhenActivated;
    public UnityEvent onPowerupActivated;
    public UnityEvent onPowerupFinishCooldown;

    private void Awake()        // metre en place avant update / start 
    {
        lastActivatedTimestamp = -9999f;
    }


    private void Update()
    {
        if (isCoolingDown) {        // espece de timer

            if (Time.time - lastActivatedTimestamp > cooldown) {        // Time.time = le temps au debut de cette frame // time - la derniere fois ou elle a ete activé 
                //finished cooldown!
                isCoolingDown = false;
                onPowerupFinishCooldown.Invoke();
            }

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isCoolingDown) return;

        var rb = other.attachedRigidbody;
        if (rb) {

            var kart = rb.GetComponent<ArcadeKart>();

            if (kart)
            { 
                lastActivatedTimestamp = Time.time;
                kart.AddPowerup(this.boostStats);
                onPowerupActivated.Invoke();
                isCoolingDown = true;

                if (disableGameObjectWhenActivated) this.gameObject.SetActive(false);
            }
        }
    }

}
