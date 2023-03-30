using UnityEngine;
using Random = UnityEngine.Random;

public class LivingAsteroid : MonoBehaviour
{

    [SerializeField] private float speed = 5f; // The speed at which the enemy moves towards the player
    [SerializeField] private float startFollowingDistance = 2f;
    [SerializeField] private float followDistance = 10f; // The distance at which the enemy starts following the player

    [SerializeField] private float hoverAmount = 0.1f;
    [SerializeField] private float idleHeight = 5f; // The idleHeight at which the enemy floats
    [SerializeField] private float followHeight = 1f; // The idleHeight at which the enemy floats toward player

    [SerializeField] private float avoidDistance = 2f; // distance at which enemy will avoid obstacles

    [SerializeField] private Mesh disguisedMesh; // The mesh for when the player is outside the follow distance
    private Mesh normalMesh; // The mesh for when the player is inside the follow distance
    [SerializeField] private MeshFilter meshFilter;

    private Transform player; // The player's transform component
    private Vector3 startingPosition; // The enemy's starting position

    private bool isFollowingPlayer = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player object using its tag
    }

    void Start()
    {
        startingPosition = transform.position; // Store the enemy's starting position
        normalMesh = meshFilter.mesh; // set the normal mesh to the current mesh
    }

    void Update()
    {
        // Calculate the distance between the enemy and the player
        float distance = Vector3.Distance(transform.position, player.position);

        Vector3 targetPosition;

        if (isFollowingPlayer)
            isFollowingPlayer = distance <= followDistance ? true : false;
        else
            isFollowingPlayer = distance <= startFollowingDistance ? true : false;

        // If the player is close enough, follow them
        if (isFollowingPlayer)
        {
            targetPosition = new Vector3(player.position.x, player.position.y + followHeight + (followHeight * Mathf.Cos(Time.time) * 0.5f), player.position.z); // Calculate the target position to move towards

            meshFilter.mesh = normalMesh; // set the mesh to the inside distance mesh

            Vector3 direction = (player.position - transform.position).normalized;

            RaycastHit hit;
            Ray ray = new Ray(transform.position, direction);
            Debug.DrawRay(transform.position, direction, Color.yellow);

            if (Physics.Raycast(ray, out hit, avoidDistance))
            {
                if (!hit.transform.gameObject.CompareTag("Player"))
                {
                    targetPosition += Vector3.up * 10f;
                }
            }

            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Make the enemy face the player
            transform.LookAt(targetPosition, Vector3.up);
        }
        else
        {
            targetPosition = new Vector3(startingPosition.x, startingPosition.y + idleHeight + (idleHeight * Mathf.Cos(Time.time) * hoverAmount), startingPosition.z);

            meshFilter.mesh = disguisedMesh; // set the mesh to the outside distance mesh

            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Make the enemy face the starting position
            if (transform.position != targetPosition)
            {
                transform.LookAt(startingPosition, Vector3.up);
            }
        }
    }
}
