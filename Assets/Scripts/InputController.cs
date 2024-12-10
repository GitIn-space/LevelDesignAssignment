using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    private Vector3 moveInput;
    private Vector3 moveForce;
    [SerializeField] private float drag = 10f;
    private float speedMod = 1f;
    [SerializeField] private float dashForce = 20f;
    private Camera cam;
    private Vector2 rotInput;
    [SerializeField] private float rotSpeedX = 1f;
    [SerializeField] private float rotSpeedY = 1f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravityForce = 1f;
    [SerializeField] private float terminalVelocity = 50f;
    [SerializeField] private float verticalMass = 1f;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private float checkLength = 1f;
    [SerializeField] private Transform armPivot;
    [SerializeField] private Transform bulletSpawn;
    private Coroutine shootRoutine;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed = 2f;
    private bool canShoot = true;
    [SerializeField] private float rateOfFire = 0.1f;
    [SerializeField] private float dashCD = 1f;
    private bool canDash = true;

    private Rigidbody rb;

    private void FixedUpdate()
    {
        Vector3 environmentalForce;
        Vector3 verticalForce = new Vector3(0f, rb.linearVelocity.y);
        rb.linearVelocity -= verticalForce;

        if (IsGrounded())
            moveForce = moveSpeed * speedMod * (transform.right * moveInput.x + transform.forward * moveInput.z);
        else
        {
            environmentalForce = moveForce;
            moveForce = Vector3.zero;
        }

        if ((rb.linearVelocity + verticalForce).magnitude < moveSpeed * speedMod && IsGrounded())
            environmentalForce = Vector3.zero;
        else
            environmentalForce = rb.linearVelocity - moveForce;

        if ((environmentalForce + verticalForce).magnitude < drag && IsGrounded())
            environmentalForce = Vector3.zero;
        else if (IsGrounded())
            environmentalForce = (environmentalForce.magnitude - drag * Time.fixedDeltaTime) * environmentalForce.normalized;

        Vector3 deltaForce = (environmentalForce + verticalForce + moveForce) - rb.linearVelocity;

        rb.AddForce(deltaForce, ForceMode.Impulse);

        rb.rotation *= Quaternion.Euler(0f, rotInput.x * rotSpeedX, 0f);
        cam.transform.localRotation *= Quaternion.Euler(rotInput.y * rotSpeedY, 0f, 0f);

        if (cam.transform.localRotation.eulerAngles.x < 360f - 45f && cam.transform.localRotation.eulerAngles.x > 180f)
            cam.transform.localRotation = Quaternion.Euler(360 - 45, cam.transform.localRotation.y, cam.transform.localRotation.z);

        if (cam.transform.localRotation.eulerAngles.x > 45f && cam.transform.localRotation.eulerAngles.x < 180f)
            cam.transform.localRotation = Quaternion.Euler(45f, cam.transform.localRotation.y, cam.transform.localRotation.z);

        armPivot.localRotation = cam.transform.localRotation;

        Gravity();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        cam.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
        (moveInput[1], moveInput[2]) = (moveInput[2], moveInput[1]);
    }

    private void OnDash(InputValue input)
    {
        if (canDash)
        {
            float y = rb.linearVelocity.y;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            float dir = Vector3.Dot(transform.TransformVector(moveInput).normalized, rb.linearVelocity.normalized);
            if (input.isPressed)
                if (dir < -0.1f)
                    rb.AddForce((transform.right * moveInput.x + transform.forward * moveInput.z) * dashForce * 2f, ForceMode.Impulse);
                else
                    rb.AddForce((transform.right * moveInput.x + transform.forward * moveInput.z) * dashForce, ForceMode.Impulse);

            rb.linearVelocity += new Vector3(0f, y);
            StartCoroutine(DashCD());
        }
    }

    private void OnRotation(InputValue input)
    {
        rotInput = input.Get<Vector2>();
        rotInput[1] *= -1f;
    }

    private void OnJump(InputValue input)
    {
        if (input.isPressed && IsGrounded())
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void Gravity()
    {
        rb.linearVelocity -= new Vector3(0, gravityForce) * verticalMass * Time.fixedDeltaTime;
        if (rb.linearVelocity.y < -terminalVelocity)
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, -terminalVelocity, rb.linearVelocity.z);
    }

    private bool IsGrounded()
    {
        if (Physics.Raycast(groundCheck.transform.position, Vector3.down, checkLength, LayerMask.NameToLayer("Player")))
            return true;

        return false;
    }

    private void OnShoot(InputValue input)
    {
        if (input.isPressed)
            Shoot();
    }

    private void Shoot()
    {
        if (canShoot)
        {
            Ray dir = cam.ScreenPointToRay(new Vector2((cam.pixelWidth - 1f) / 2f, (cam.pixelHeight - 1f) / 2f));

            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, dir.direction, out hit, Mathf.Infinity, LayerMask.NameToLayer("Player")))
                bulletSpawn.LookAt(hit.point);
            else
                bulletSpawn.LookAt(dir.GetPoint(500f));

            GameObject newBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);

            StartCoroutine(ShootCD());
        }
    }

    private IEnumerator DashCD()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCD);
        canDash = true;
    }

    private IEnumerator ShootCD()
    {
        canShoot = false;
        yield return new WaitForSeconds(rateOfFire);
        canShoot = true;
    }
}