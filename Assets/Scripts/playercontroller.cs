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
    private Camera mainCamera;
    private Vector3 offset;

    public override void OnStartLocalPlayer()
    {
        body = GetComponent<Rigidbody2D>();
        position = gameObject.GetComponent<Transform>();
        //gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
        Cursor.visible = MouseVisible;
        mainCamera = Camera.main;
        Destroy(GetComponent<lightTransparency2D>());
        Destroy(transform.GetChild(0).GetChild(0).GetComponent<lightTransparency2D>());
    }

    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
        {
            return;
        }

        cameraFollow();
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdfireBullet();
        }
	}

    void FixedUpdate()
    {
        getMovement();
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
        var direction = (bulletSpawn.position - transform.position);
        var bullet = (GameObject)Instantiate(lightBullet, transform.position + (direction * 2.5f), bulletSpawn.rotation);

        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        NetworkServer.Spawn(bullet);

        Destroy(bullet, 2.0f);
    }

    void cameraFollow()
    {
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
