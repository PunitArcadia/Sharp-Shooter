using UnityEngine;

public class Weapon : MonoBehaviour
{
    RaycastHit hit;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
            {
                Debug.Log(hit.transform.gameObject.name);
                EnemyHealth eh = hit.transform.gameObject.GetComponent<EnemyHealth>();
                if (eh)
                {
                    eh.TakeDamage(1);
                }
            }
        }
    }
}
