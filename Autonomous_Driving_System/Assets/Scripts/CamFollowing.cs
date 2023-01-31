using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowing : MonoBehaviour
{
    // ī�޶� �ٶ� ���
    [SerializeField]
    public GameObject cameraView;

    // ī�޶��� ��ġ

    public GameObject cameraPos;

    // ī�޶� ������ �ӵ�
    [SerializeField]
    public float speed;

    // ī�޶��� ó���� LateUpdate���� ó���ϵ��� �Ѵ�.
    public void LateUpdate()
    {
        // Lerp�� ����ؼ� ī�޶� ������� ������ ���󰡵��� ������ش�.
        gameObject.transform.position = Vector3.Lerp(transform.position, cameraPos.transform.position, Time.deltaTime * speed);
        // ī�޶� �ٶ� ����� �����ش�.
        gameObject.transform.LookAt(cameraView.transform);

    }
}
