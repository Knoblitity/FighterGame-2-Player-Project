using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    // Update is called once per frame
    public float speed = 10f;
    Animator anim;
	public bool work;

    //change array number to fix number of attacks.
    private float attackRate;
    bool[] attack = new bool[5];
    float[] attackTimer = new float[5]; // These arrays
    int[] timesPressed = new int[5];

    public Transform firePoint;
    public GameObject fireBall;

	public float jumpHeight;
	public bool isJumping = false;

	public KeyCode projectileShoot;
	public KeyCode attackOne;
	public KeyCode attackTwo;
	public KeyCode attackThree;
	public KeyCode attackFour;
	public KeyCode leftMove;
	public KeyCode rightMove;
	public KeyCode jumpButton;
	public KeyCode dashButton;

    void Start()
    {
        anim = GetComponent<Animator>();
		if (projectileShoot == KeyCode.None) {
			projectileShoot = KeyCode.U;
		}
		if (attackOne == KeyCode.None) {
			attackOne = KeyCode.I;
		}
		if (attackTwo == KeyCode.None) {
			attackTwo = KeyCode.O;
		}
		if (attackThree == KeyCode.None) {
			attackThree = KeyCode.P;
		}
		if (leftMove == KeyCode.None) {
			leftMove = KeyCode.A;
		}
		if (rightMove == KeyCode.None) {
			rightMove = KeyCode.D;
		}
		if (jumpButton == KeyCode.None) {
			jumpButton = KeyCode.W;
		}
		if (dashButton == KeyCode.None) {
			dashButton = KeyCode.V;
		}
		if (attackFour == KeyCode.None) {
			attackFour = KeyCode.J;
		}
    }

    void Update()
    {
        Movement();
        AttackInput();
		Jump ();
    }

    /// <summary>
    /// attackRate is changed per attack. Set attack[] to whichever attack needed. Attack names are seperate, just remember which attack is which.
    /// Also remember to change HealthControl to affect how much dmg is done, its in seperate script.
    /// attackRate also needs to be precise or else hitbox will hit multipletimes. Change Times Pressed to make spamming not-working.
    /// </summary>
    void AttackInput()
    {
        //StandMed
		if (Input.GetKeyDown(attackOne))
        {
            if (anim.GetFloat("Movement") < 0.1f) //Only allows this attack when not moving.
            {
                attack[0] = true;
                attackTimer[0] = 0;
                timesPressed[0]++;
                anim.SetBool("Attack0", true);
            }
        }

        if (attack[0])
        {
            attackRate = 0.3f; //attack rate here works for StandMed.
            attackTimer[0] += Time.deltaTime;
            if (attackTimer[0] > attackRate || timesPressed[0] >= 3) //after 3 presses, attack animation cuts, but no one can hit that fast.
            {
                attackTimer[0] = 0;
                attack[0] = false;
                timesPressed[0] = 0;
                anim.SetBool("Attack0", false);
            }
        }
        //StandHeavy
		if (Input.GetKeyDown(attackTwo))
        {
            if (anim.GetFloat("Movement") < 0.1f)
            {
                attack[1] = true;
                attackTimer[1] = 0;
                timesPressed[1]++;
                anim.SetBool("Attack1", true);
            }
        }
        if (attack[1])
        {
            attackRate = 1.2f;
            attackTimer[1] += Time.deltaTime;
            if (attackTimer[1] > attackRate || timesPressed[1] >= 7)
            {
                attackTimer[1] = 0;
                attack[1] = false;
                timesPressed[1] = 0;
                anim.SetBool("Attack1", false);
            }
        }
        //LightKick
		if (Input.GetKeyDown(attackThree))
        {
            if (anim.GetFloat("Movement") < 0.1f)
            {
                attack[2] = true;
                attackTimer[2] = 0;
                timesPressed[2]++;
                anim.SetBool("Attack2", true);
            }
        }
        if (attack[2])
        {
            attackRate = 0.2f;
            attackTimer[2] += Time.deltaTime;
            if (attackTimer[2] > attackRate || timesPressed[2] >= 3)
            {
                attackTimer[2] = 0;
                attack[2] = false;
                timesPressed[2] = 0;
                anim.SetBool("Attack2", false);
            }
        }
		//FireballShoot
		if (Input.GetKeyDown(projectileShoot))
        {
			if (anim.GetFloat ("Movement") < 0.1f)
			{
				attack [3] = true;
				attackTimer [3] = 0;
				timesPressed [3]++;
				anim.SetBool ("Attack3", true);
			}
        }
        if (attack[3])
        {
            attackRate = 0.45f;
            attackTimer[3] += Time.deltaTime;
            if (attackTimer[3] > attackRate || timesPressed[3] >= 5)
            {
                Instantiate(fireBall, firePoint.position, firePoint.rotation);
                attackTimer[3] = 0;
                attack[3] = false;
                timesPressed[3] = 0;
				anim.SetBool("Attack3", false);
            }
        }
		//JumpingHeavyPunch (To Make it Work in the Air, Now modify JumpingAttacks to the number of the Attack you want,
		// e.g JumpingHeavyPunch is now Float 1.0f, while regular jumping is 0.0f.
		if (Input.GetKeyDown(attackFour))
		{
			if ((anim.GetFloat ("Movement") < 0.1f && anim.GetBool("OnGround") == false)
				|| anim.GetBool("OnGround") == false && anim.GetFloat("Movement") > 0.1f)
			{
				attack [4] = true;
				attackTimer [4] = 0;
				timesPressed [4]++;
				anim.SetFloat ("JumpingAttacks", 1.0f);
			}
		}
		if (attack[4])
		{
			attackRate = 0.3f;
			attackTimer[4] += Time.deltaTime;
			if (attackTimer[4] > attackRate || timesPressed[4] >= 3)
			{
				attackTimer[4] = 0;
				attack[4] = false;
				timesPressed[4] = 0;
				anim.SetFloat("JumpingAttacks", 0.0f);
			}
		}

    }

     void Movement()
    {
		if (isJumping == false) 
		{
			
			if (Input.GetKey (rightMove)) 
			{
				transform.Translate (Vector2.right * speed * Time.deltaTime);
				transform.eulerAngles = new Vector2 (0, 0);
				anim.SetFloat ("Movement", 1f);
			}
			else if (Input.GetKey (leftMove))
			{
				transform.Translate (Vector2.right * speed * Time.deltaTime);
				transform.eulerAngles = new Vector3 (0, 180);
				anim.SetFloat ("Movement", 1f);
			}
			else
			{
				anim.SetFloat ("Movement", 0f);
			}
		}
		if (Input.GetKeyDown (dashButton) && Input.GetKey (rightMove))
		{
			StartCoroutine (DashRight ());
		}
	
		if (Input.GetKeyDown (dashButton) && Input.GetKey (leftMove))
		{
			StartCoroutine (DashLeft ());
		}
		
    }
	void Jump()
	{
		if (Input.GetKeyDown (jumpButton) && Input.GetKeyDown (rightMove) && isJumping == false) 
		{
			GetComponent<Rigidbody2D> ().AddForce ((Vector2.up * jumpHeight));
			GetComponent<Rigidbody2D> ().velocity = new Vector2( Mathf.Cos(45)*4, Mathf.Sin(45));
			anim.SetBool ("OnGround", false);
			isJumping = true;
		}
		else if (Input.GetKeyDown (jumpButton) && Input.GetKeyDown (leftMove) && isJumping == false) 
		{
			GetComponent<Rigidbody2D> ().AddForce ((Vector2.up * jumpHeight));
			GetComponent<Rigidbody2D> ().velocity = new Vector2( Mathf.Cos(45)*-4, Mathf.Sin(45));
			anim.SetBool ("OnGround", false);
			isJumping = true;
		}

		else if (Input.GetKeyDown (jumpButton) && isJumping == false) 
		{
			GetComponent<Rigidbody2D> ().AddForce ((Vector2.up * jumpHeight));
			anim.SetBool ("OnGround", false);
			isJumping = true;
		}

	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Ground")
		{
			isJumping = false;
			anim.SetBool("OnGround", true);
		}
	}
		
	IEnumerator DashRight()
	{
		GetComponent<Rigidbody2D> ().AddForce ((Vector2.right * 500));
		yield return new WaitForSeconds (0.25f);
		GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
	}

	IEnumerator DashLeft()
	{
		GetComponent<Rigidbody2D> ().AddForce ((Vector2.left * 500));
		yield return new WaitForSeconds (0.25f);
		GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
	}
}
