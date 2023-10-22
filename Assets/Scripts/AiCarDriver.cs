using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CarController), typeof(NavMeshAgent))]

public class AiCarDriver : MonoBehaviour
{
    [SerializeField] private CarSensor sensor;

    private CarController carController;
    private NavMeshAgent navMeshAgent;

    [SerializeField] private Transform[] wayPoints;

    private int currentWaypointIndex;

    private float moveValue;

    public bool fireWeapon;



    private void OnEnable()
    {
        carController = GetComponent<CarController>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        moveValue = 1f;

        Shuffle(wayPoints);
        navMeshAgent.SetDestination(wayPoints[0].position);
    }

    private void Shuffle(Transform[] array)
    {
        for (int t = 0; t < array.Length; t++)
        {
            Transform trans = array[t];
            int r = Random.Range(t, array.Length);
            array[t] = array[r];
            array[r] = trans;
        }
    }

    private void FixedUpdate()
    {
        carController.verticalInput = moveValue;

        carController.isBraking = (Vector3.Distance(navMeshAgent.transform.position,
            navMeshAgent.steeringTarget) < 10) ? true : false;

        if (sensor.hasDetectedThePlayer)
        {
            carController.isBraking = true;

            if (sensor.playerTransform.gameObject.tag == "Player")
            {
                navMeshAgent.isStopped = true;
            }

            fireWeapon = true;
        }

        else
        {
            fireWeapon = false;

            carController.isBraking = false;
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(wayPoints[currentWaypointIndex].position);

            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                //Debug.Log(navMeshAgent.steeringTarget);
                currentWaypointIndex = (currentWaypointIndex + 1) % wayPoints.Length;
                navMeshAgent.SetDestination(wayPoints[currentWaypointIndex].position);
            }
        }

        

    }
}
