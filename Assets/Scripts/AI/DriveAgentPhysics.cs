using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class DriveAgentPhysics : Agent
{
    public Transform Goal;
    public float MoveSpeed = 1f;
    public float MaxDepth = -20f;
    public Transform StartPosition;
    private Rigidbody _rb;
    public GameObject Tile;

    [SerializeField] private Transform targetTransform;
    private Renderer _tileRenderer;

    private float _episodeStartTime = 0;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _tileRenderer = Tile.GetComponent<Renderer>();
    }
    private void Update()
    {
        var y = transform.position.y;
        var elapsedTime = Time.time - _episodeStartTime;
        if (y < MaxDepth || y > -MaxDepth || elapsedTime > 6)
        {
            _tileRenderer.material.color = Color.red;
            AddReward(-1);
            EndEpisode();
        }
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        //Debug.Log("Observing");

        // Cosa vede l'IA; nei fatti l'input della rete neurale.
        // Parametro che li regola è: Vector Observation>Space size
        //sensor.AddObservation(transform.localPosition);
        //sensor.AddObservation(targetTransform.localPosition);

        sensor.AddObservation(_rb.velocity);
        sensor.AddObservation(_rb.angularVelocity);
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

        float acceleration = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];
        //float moveZ = actions.ContinuousActions[2];

        //transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * MoveSpeed;

        _rb.AddRelativeForce(Vector3.forward * (MoveSpeed * acceleration), ForceMode.Acceleration);
        _rb.AddRelativeTorque(150 * moveY * Vector3.up);

        AddReward(GetDistanceReward());
    }

    private float GetDistanceReward()
    {
        var distance = Mathf.Abs((Goal.localPosition - transform.localPosition).sqrMagnitude);
        if (distance < 0.01) return 100;
        return 1 / distance;
    }

    public override void OnEpisodeBegin()
    {
        if (_rb == null) return;

        _episodeStartTime = Time.time;

        transform.localPosition = StartPosition.localPosition;
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
            SetReward(10 * GetDistanceReward());

            _tileRenderer.material.color = Color.green;
        }
        else
        {
            AddReward(-10 * GetDistanceReward());

            _tileRenderer.material.color = Color.red;
        }
        EndEpisode();  // serve a finire l'episodio e quindi resettare
    }

    private float CalulcateReward(float reward)
    {
        var elapsedTime = Time.time - _episodeStartTime;
        return reward * (reward > 0 ? (1 / elapsedTime) : 1);
    }

    private new void AddReward(float reward)
    {
        base.AddReward(CalulcateReward(reward));
    }

    private new void SetReward(float reward)
    {
        base.SetReward(CalulcateReward(reward));
    }

}

