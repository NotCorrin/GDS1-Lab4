using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    private float Timer;
    private float PickupSpawnTime;

    public GameObject pickup;
    private BoxCollider2D polygonCollider;


    // Start is called before the first frame update
    void Start()
    {
        polygonCollider = GetComponent<BoxCollider2D>();
        PickupSpawnTime = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;

        if (Timer > PickupSpawnTime /*&& !PickupExists*/)
        {
            Debug.Log("Spawned");
            Timer = 0;
            PickupSpawnTime = Random.Range(1, 3);
            Spawn();
        }
    }

    void Spawn()
    {
        Vector2 rndPoint2D = RandomPointInBounds(polygonCollider.bounds, 1f);
        Vector2 rndPointInside = polygonCollider.ClosestPoint(new Vector2(rndPoint2D.x, rndPoint2D.y));
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
