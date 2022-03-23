using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    private float Timer;
    private float PickupSpawnTime;

    public GameObject pickup;
    private BoxCollider2D zone;


    // Start is called before the first frame update
    void Start()
    {
        zone = GetComponent<BoxCollider2D>();
        PickupSpawnTime = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Pickup(Clone)") == null && !GameManager.IsAntiWall /*player isnt powered up*/)
        {
            Timer += Time.deltaTime;
        }

        if (Timer > PickupSpawnTime)
        {
            Timer = 0;
            PickupSpawnTime = Random.Range(1, 3);
            Spawn();
        }
    }

    void Spawn()
    {
        AudioManager.instance.Play("PickupSpawn");
        Vector2 rndPoint2D = RandomPointInBounds(zone.bounds, 1f);
        Vector2 rndPointInside = zone.ClosestPoint(new Vector2(rndPoint2D.x, rndPoint2D.y));
        if (rndPointInside.x == rndPoint2D.x && rndPointInside.y == rndPoint2D.y)
        {
            Instantiate(pickup, rndPointInside, Quaternion.identity);
        }
    }

    private Vector2 RandomPointInBounds(Bounds bounds, float scale)
    {
        return new Vector3(
            Random.Range(bounds.min.x * scale, bounds.max.x * scale),
            Random.Range(bounds.min.y * scale, bounds.max.y * scale)
        );
    }
}
