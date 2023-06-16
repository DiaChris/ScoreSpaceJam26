using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDeletion : MonoBehaviour
{
   Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        transform.position = target.position + new Vector3(0,1.5f,0);
        transform.rotation = target.rotation * Quaternion.EulerAngles(new Vector3(-90, 0, 0));
        GetComponent<ParticleSystem>().Emit(50);
        StartCoroutine(ParticleBehevior());
    }
    private IEnumerator ParticleBehevior()
    {
        yield return new WaitForSeconds(10f);
        GameObject.Destroy(gameObject);

    }
}
