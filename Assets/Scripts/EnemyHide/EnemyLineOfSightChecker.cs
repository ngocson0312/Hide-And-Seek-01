using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyLineOfSightChecker : MonoBehaviour
{

    public SphereCollider Collider;
    public float FieldOfView = 90f;
    public LayerMask LineOfSightLayers;

    public delegate void GainSightEvent(Transform Target);
    public GainSightEvent OnGainSight;
    public delegate void LoseSightEvent(Transform Target);
    public LoseSightEvent OnLoseSight;

    private Coroutine CheckForLineOfSightCoroutine;

   // public GameObject point;
    private void Awake()
    {
        Collider = GetComponent<SphereCollider>();

    }

    //lúc va chạm
    private void OnTriggerEnter(Collider other)
    {
        if (!CheckLineOfSight(other.transform))
        {
            CheckForLineOfSightCoroutine = StartCoroutine(CheckForLineOfSight(other.transform));
            Debug.Log("3");
        }
    }

    //lúc thoát va chạm
    private void OnTriggerExit(Collider other)
    {
        OnLoseSight?.Invoke(other.transform);
        if (CheckForLineOfSightCoroutine != null)
        {
            StopCoroutine(CheckForLineOfSightCoroutine);
            Debug.Log("2");
        }
    }

    //kiểm tra xem có ẩn nấp gì không
    private bool CheckLineOfSight(Transform Target)
    {
        Vector3 direction = (Target.transform.position - transform.position).normalized;
        float dotProduct = Vector3.Dot(transform.forward, direction);
        if (dotProduct >= Mathf.Cos(FieldOfView))
        {
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, Collider.radius, LineOfSightLayers))
            {
                OnGainSight?.Invoke(Target);
                Debug.Log("4");
                return true;
            }
        }

        return false;
    }


    //cho tý time 
    private IEnumerator CheckForLineOfSight(Transform Target)
    {
        WaitForSeconds Wait = new WaitForSeconds(0.000001f);

        while(!CheckLineOfSight(Target))
        {
            yield return Wait;
        }
    }
}
