using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class playercontroller : NetworkBehaviour
{

    public float speed = 10;
    public float rotationSpeed = 10;
    public GameObject lightBullet;
    public float bulletSpeed;
    public bool MouseVisible;
    public Transform barrel;
    public Transform bulletSpawn;

    private Rigidbody2D body;
    private Transform position;

    public override void OnStartLocalPlayer()
    {
        body = GetComponent<Rigidbody2D>();
        position = gameObject.GetComponent<Transform>();
        transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
        Cursor.visible = MouseVisible;
    }

    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
        {
            return;
        }

        getMovement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdfireBullet();
        }
	}

    void getMovement()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(0, moveVertical);

        body.AddRelativeForce(movement * speed);
        body.MoveRotation(body.rotation + -(moveHorizontal * rotationSpeed));

        Vector2 mousePoint = new Vector2(Input.mousePosition.x - (Screen.width/ 2), Input.mousePosition.y - (Screen.height/2));
        float mouseAngle = (Mathf.Atan(mousePoint.y / mousePoint.x) * Mathf.Rad2Deg) - (90 + position.eulerAngles.z);
        //Debug.Log(mousePoint);
        //Debug.Log(mouseAngle);

        if (mousePoint.x < 0 && mousePoint.y >= 0) mouseAngle += 180;
        else if (mousePoint.y < 0 && mousePoint.x <= 0) mouseAngle += 180;
        else if (mousePoint.x > 0 && mousePoint.y < 0) mouseAngle += 360;

        barrel.localEulerAngles = new Vector3(0, 0, mouseAngle);
    }

    [Command]
    void CmdfireBullet()
    {
        Debug.Log("Fired"); // Fix this next please
        var direction = (bulletSpawn.position - transform.position);
        var bullet = (GameObject)Instantiate(lightBullet, transform.position + (direction * 2.3f), bulletSpawn.rotation);

        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        NetworkServer.Spawn(bullet);

        Destroy(bullet, 2.0f);
    }
}
