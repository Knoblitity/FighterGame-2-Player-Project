using UnityEngine;
using System.Collections;

public class FireBallController : MonoBehaviour {

    public float speed;
    Rigidbody2D rigi;

    public GameObject player;
    Quaternion playerRotation;

	public string playerDirection;

    // Use this for initialization
    void Start () {
        rigi = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag(playerDirection);
        playerRotation = player.transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        fireTheBall();
	}

    void fireTheBall()
    {
        if (playerRotation.y == 0)
        {
            rigi.velocity = new Vector2(speed, rigi.velocity.y);
        }
        else
        {
            rigi.velocity = new Vector2(-speed, rigi.velocity.y);
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        Destroy(gameObject);
    }
}
