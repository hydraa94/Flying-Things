using UnityEngine;
using UnityEngine.InputSystem;


public class Target : MonoBehaviour
{
    private Rigidbody rb;
    private GameManager gameManager;
    private InputAction clickAction;
    private Camera mainCamera;

    public int pointValue;
    public ParticleSystem explosionParticle;


    private float minSpeed = 10f;
    private float maxSpeed = 20f;

    private float maxTorque = 10f;

    private float xRange = 4f;
    private float ySpawnPos = -6f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        clickAction = InputSystem.actions.FindAction("Attack");
        mainCamera = Camera.main;

        rb.AddForce(Vector2.up * RandomSpeed(), ForceMode.Impulse);
        rb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGameActive) return;
        if (!clickAction.triggered || !mainCamera) return;
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hitInfo) && hitInfo.collider.gameObject == gameObject)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
            gameManager.UpdateScore(pointValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!gameManager.isGameActive) return;
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    float RandomSpeed()
    {
        return Random.Range(minSpeed, maxSpeed);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

}
