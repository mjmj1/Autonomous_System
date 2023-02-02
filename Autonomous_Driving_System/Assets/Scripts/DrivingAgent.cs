using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System;

public class DrivingAgent : Agent
{
    [SerializeField]
    private TextMeshProUGUI[] text;

    [SerializeField]
    private RawImage handle;

    //�� �ݶ��̴� 4��
    [SerializeField]
    private WheelCollider[] wheels = new WheelCollider[4];

    // ���� ���� ���� �κ� 4��
    [SerializeField]
    private GameObject[] wheelMesh = new GameObject[4];

    [SerializeField]
    private float power; // ������ ȸ����ų ��

    [SerializeField]
    private float downForceValue;

    [SerializeField]
    private float radius = 6;

    enum DriveType
    {
        FRONTDRIVE,
        REARDRIVE,
        ALLDRIVE
    }

    [SerializeField] DriveType drive;

    private new Transform transform;
    private new Rigidbody rigidbody;

    float reward = 0;

    TcpClient client;
    string serverIP = "10.101.34.109";
    int port = 60001;
    byte[] receivedBuffer;
    StreamReader reader;
    bool socketReady = false;
    NetworkStream stream;

    public string serverMsg;

    void Start()
    {
        CheckReceive();
    }

    void Update()
    {
        if (socketReady)
        {
            if (stream.DataAvailable)
            {
                receivedBuffer = new byte[100];
                stream.Read(receivedBuffer, 0, receivedBuffer.Length);  // stream�� �ִ� ����Ʈ�迭 ������ ���� ������ ����Ʈ�迭�� �ֱ�
                string msg = Encoding.UTF8.GetString(receivedBuffer, 0, receivedBuffer.Length);
                if (msg.Equals("quit"))
                    UnityEditor.EditorApplication.isPlaying = false;
                //Application.Quit();
                
                if(msg != null)
                {
                    //Debug.Log(msg);
                    serverMsg = msg;
                }
            }
        }
    }

    public void CheckReceive()
    {
        if (socketReady) return;
        try
        {
            client = new TcpClient(serverIP, port);
            if (client.Connected)
            {
                stream = client.GetStream();
                Debug.Log("Connect Success");
                socketReady = true;
            }
        }
        catch (Exception ex)
        {
            Debug.Log("On Client connect exception " + ex);
        }
    }

    private void OnApplicationQuit()
    {
        CloseSocket();
    }

    void CloseSocket()
    {
        if (!socketReady) return;

        reader.Close();
        client.Close();
        socketReady = false;
    }

    public override void Initialize()
    {
        MaxStep = 10000;

        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();

        // ���� �߽��� y�� �Ʒ��������� �����.
        rigidbody.centerOfMass = new Vector3(0, -1f, 0);

        for (int i = 0; i < wheelMesh.Length; i++)
        {
            // ���ݶ��̴��� ��ġ�� �����޽��� ��ġ�� ���� �̵���Ų��.
            wheels[i].steerAngle = 0;
            wheels[i].transform.position = wheelMesh[i].transform.position;
        }
    }

    public override void OnEpisodeBegin()
    {
        Resources.UnloadUnusedAssets();

        rigidbody.velocity = rigidbody.angularVelocity = Vector3.zero;
        transform.localPosition = new Vector3(1.8f, -4.7f, 0f);
        transform.localRotation = Quaternion.identity;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var action = actions.ContinuousActions;

        if(serverMsg.Contains("drive") || serverMsg.Contains("green_right"))
        {
            text[0].text = "Action[0] : " + action[0].ToString();
            text[1].text = "Action[1] : " + action[1].ToString();
            handle.transform.localRotation = Quaternion.Euler(0, 0, action[1] * -45f);

            Drive(action[0]);
            SteerVehicle(action[1]);

            UpdateMeshesPostion();
            AddDownForce();

            if (action[0] >= 0.3f)
            {
                reward = 1f;
            }
            else if (action[0] < 0.3f)
            {
                reward = -1f;
            }

            if (transform.position.y < 0)
            {
                SetReward(-1f);
                EndEpisode();
            }

            AddReward(reward / MaxStep);
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var action = actionsOut.ContinuousActions;

        if (serverMsg.Contains("drive") || serverMsg.Contains("green_right"))
        {
            action[0] = Input.GetAxis("Vertical");
            action[1] = Input.GetAxis("Horizontal");
        }
        else
        {
            rigidbody.velocity= Vector3.zero;
            action[0] = 0;
            action[1] = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathLine"))
        {
            SetReward(-1f);
            EndEpisode();
        }
    }

    void Drive(float vertical)
    {
        // ���� ������ ��
        if (drive == DriveType.ALLDRIVE)
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = vertical * (power / 4);
            }

            if (vertical == 0)	// ���� ���� �ƴ� ��
            {
                for (int i = 0; i < wheels.Length; i++)
                {
                    wheels[i].brakeTorque = power / 4;
                }
            }
            else	// Ű�� ������ ��
            {
                for (int i = 0; i < wheels.Length; i++)
                {
                    wheels[i].brakeTorque = 0; // �극��ũ ����
                }
            }
        }

        else if (drive == DriveType.REARDRIVE)	// �ķ������� ��
        {
            // �޹�������.
            for (int i = 2; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = vertical * (power / 2);
            }
            if (vertical == 0)
            {
                for (int i = 0; i < wheels.Length; i++)
                {
                    wheels[i].brakeTorque = power / 2;
                }
            }
            else
            {
                for (int i = 0; i < wheels.Length; i++)
                {
                    wheels[i].brakeTorque = 0;
                }
            }
        }
        else	// ���� ������ ��
        {	// �չ�������
            for (int i = 0; i < 2; i++)
            {
                wheels[i].motorTorque = vertical * (power / 2);
            }
            if (vertical == 0)
            {
                for (int i = 0; i < wheels.Length; i++)
                {
                    wheels[i].brakeTorque = power / 2;
                }
            }
            else
            {
                for (int i = 0; i < wheels.Length; i++)
                {
                    wheels[i].brakeTorque = 0;
                }
            }
        }
    }

    void AddDownForce()
    {
        rigidbody.AddForce(-transform.up * downForceValue * rigidbody.velocity.magnitude);
    }

    void UpdateMeshesPostion()
    {
        for (int i = 0; i < 4; i++)
        {
            wheels[i].GetWorldPose(out Vector3 pos, out Quaternion quat);
            wheelMesh[i].transform.position = pos;
            wheelMesh[i].transform.rotation = quat;
        }
    }

    void SteerVehicle(float horizontal)
    {
        //steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * horizontalInput;
        if (horizontal > 0)
        {   // rear tracks size is set to 1.5f          wheel base has been set to 2.55f
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * horizontal;
        }
        else if (horizontal < 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * horizontal;
            // transform.Rotate(Vector3.up * steerHelping)
        }
        else
        {
            wheels[0].steerAngle = 0;
            wheels[1].steerAngle = 0;
        }
    }
}
