using System.Collections;
using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    public float life, range,damagePerClick, ClickInterval,grenadeHealthDamageRatio,grenadeShieldDamageRatio;
    private void Start()
    {
        StartCoroutine(Damage());
        Destroy(gameObject, life);
    }
    IEnumerator Damage() {
        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, range);
            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Player") || col.CompareTag("Enemy") || col.CompareTag("Destroyable"))
                {
                    HealthAndShield VictimHealth = col.gameObject.GetComponent<HealthAndShield>();
                    float distance = Vector3.Distance(col.gameObject.transform.position, transform.position);
                    if (distance < range)
                        VictimHealth.damage(damagePerClick, grenadeHealthDamageRatio, grenadeShieldDamageRatio);

                }
            }
            yield return new WaitForSeconds(ClickInterval);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
