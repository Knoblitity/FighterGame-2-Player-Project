using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class HealthControl : MonoBehaviour {

	public Image healthBar;
	public float health; //between 0-100
	public GameObject restartDialog;
    public bool canDamage = true;
    //public float invTime; //Later for better control I can change this to Private, and edit the invTime depending on moves, allowing some moves
                          //to combo and others to not.

    Rigidbody2D rigi;
    float yRotation;

    Animator anim;

    public string playerTag;
    // Use this for initialization
    void Start() {
        ShowRestartDialog(false);
        rigi = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void Update () {
		CheckHealth ();
        yRotation = GameObject.FindGameObjectWithTag(playerTag).gameObject.transform.rotation.y;
	}

	void CheckHealth() {
		healthBar.rectTransform.localScale = new Vector3 (health / 100, healthBar.rectTransform.localScale.y,
			healthBar.rectTransform.localScale.z);
		if (health <= 0.0f) {
			ShowRestartDialog (true);
		}
	}

    IEnumerator ApplyDamage(float dmg, float invTime)
    {
        SubtractHealth(dmg);
        canDamage = false;
        anim.SetBool("Damaged", true);
        yield return new WaitForSeconds(invTime);
        anim.SetBool("Damaged", false);
        canDamage = true;
    }

    public void knockBack(float x, float y)
    {
        rigi.velocity = Vector3.zero;
        if (yRotation == 0)
        {
            rigi.AddForce(new Vector2(x, y), ForceMode2D.Impulse);
        }
        else
        {
            rigi.AddForce(new Vector2(-x, y), ForceMode2D.Impulse);
        }
    }

    // How to do damage, change the ApplyDamage to be depending on the move.
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("StandMed"))
        {
            if (canDamage)
            {
                StartCoroutine(ApplyDamage(12, 0.75f));
                knockBack(3, 5);
            }
        }
        if (coll.gameObject.CompareTag("StandHeavy"))
        {
            if (canDamage)
            {
                StartCoroutine(ApplyDamage(25, 1.2f));
                knockBack(5, 10);
            }
        }
        if (coll.gameObject.CompareTag("LightKick"))
        {
            if (canDamage)
            {
                StartCoroutine(ApplyDamage(9, 0.3f));
                knockBack(2, 2);
            }
        }
		if (coll.gameObject.CompareTag("Fireball"))
		{
			if (canDamage)
			{
				StartCoroutine(ApplyDamage(8, 0.3f));
				knockBack(1, 0);
			}
		}
		if (coll.gameObject.CompareTag("JumpHeavy"))
		{
			if (canDamage)
			{
				StartCoroutine(ApplyDamage(10, 0.3f));
				knockBack(3, 0);
			}
		}
    }

    public void AddHealth(float amount) {
		if (health + amount > 100.0f) {
			health = 100.0f;
		} 
		else {
			health += amount;
		}
	}

	public void SubtractHealth(float amount) {
		if (health - amount < 0.0f) {
			health = 0.0f;
		} 
		else {
			health -= amount;
		}
	}

	public void ShowRestartDialog(bool c) {
		if (c) {
			Time.timeScale = 0.0f;
		} else {
			Time.timeScale = 1.0f;
		}
		restartDialog.SetActive (c);
	}

	public void Restart() {
        SceneManager.LoadScene("FighterScene");
	}

    /*
    public IEnumerator KnockBack(float knockDur, float knockPwr, Vector2 knockDir)
    {
        float timer = 0;
        while (knockDur > timer)
        {
            timer += Time.deltaTime;
            rigi.AddForce(new Vector2(knockDir.x * -100, knockDir.y * knockPwr));
        }
        yield return 0;
    }
    */
}
