using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class DriveAgent : Agent
{

    public Transform Goal;
    public float MoveSpeed = 1f;
    private Rigidbody _rb;

    [SerializeField] private Transform targetTransform;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //Debug.Log("Observing");

        // Cosa vede l'IA; nei fatti l'input della rete neurale.
        // Parametro che li regola è: Vector Observation>Space size
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // action buffer ha dentro ogni valore che predirrà (action)
        // Discreto va da 0 a N, mentre Continui va da -1 a 1
        // In discreto puoi decidere quanti elementi hai per ogni branch.

        // In pratica: il numero di branch è quanto è lungo il vettore,
        // e ogni cella del vettore è il branch specifico.
        // La dimensione del singolo branch dice che valori può assumere quel branch (range di valori)/

        //var discreteActions = actions.DiscreteActions;
        //Debug.Log($"Got {discreteActions.Length} actions");
        //for (int i = 0; i < discreteActions.Length; i++)
        //{
        //    Debug.Log(discreteActions[i]);
        //}

        //Debug.Log("Applying movement");

        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * MoveSpeed;

        var dst = (transform.localPosition - Goal.localPosition).sqrMagnitude;
        AddReward(-dst);

    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0, 1, 0);
        transform.rotation = Quaternion.Euler(Vector3.zero);
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // FOR TESTING!
        var continuousAction = actionsOut.ContinuousActions;
        continuousAction[0] = Input.GetAxisRaw("Horizontal");
        continuousAction[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        //var dst = (transform.localPosition - Goal.localPosition).sqrMagnitude;
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            Debug.Log("Goal reached!!!");
            SetReward(10000);
        }
        else
        {
            AddReward(-10000);
        }
        EndEpisode();  // serve a finire l'episodio e quindi resettare
    }

}

