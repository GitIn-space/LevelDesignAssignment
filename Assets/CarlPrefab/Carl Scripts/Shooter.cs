using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{

    private InputActionMap inputActionMap;
    private InputAction shootAction;
    private bool canShoot = true;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private float bulletSpeed = 2f;
    [SerializeField] private float rateOfFire = 0.1f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputActionMap = InputSystem.actions.FindActionMap("Player");
        shootAction = inputActionMap.FindAction("Shoot");
    }

    // Update is called once per frame
    void Update()
    {
        if (shootAction.WasPressedThisFrame() && canShoot)
        {
            
            GameObject newBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);

            StartCoroutine(ShootCD());
        }
    }

    private IEnumerator ShootCD()
    {
        canShoot = false;
        yield return new WaitForSeconds(rateOfFire);
        canShoot = true;
    }
}
