
using UnityEngine;

public class CoilSpring : MonoBehaviour
{
    [SerializeField]
    private float m_fSpringConstant;
    [SerializeField]
    private float m_fDampingConstant;
    [SerializeField]
    private Vector3 m_vRestPos;
    [SerializeField]
    private float m_fMass;
    [SerializeField]
    private Rigidbody m_attachedBody = null;
    

    private Vector3 m_vForce;
    private Vector3 m_vPrevVel;
    private bool release = false;
    private void Start()
    {
        m_fMass = m_attachedBody.mass;
    }

    private void FixedUpdate()
    {
        UpdateSpringForce();
    }
    private void Update()
    {
        if (!release)
        {
            m_attachedBody.isKinematic = true;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            //plunger cant pass the ball when the space is pressed
            if (m_attachedBody.position.z > -4.7)
            {
                float yinput = Time.deltaTime;
                m_attachedBody.transform.position = new Vector3(m_attachedBody.transform.position.x,
                                                                m_attachedBody.transform.position.y,
                                                                m_attachedBody.transform.position.z - yinput);
            }

        }
        //plunger is idle so we can press the space
		if (m_attachedBody.velocity.magnitude == 0)
		{
			release = false;
		}

		if (Input.GetKeyUp(KeyCode.Space) && release == false)
        {
            Debug.Log("release");
            release = true;
            // UpdateSpringForce();
            m_attachedBody.isKinematic = false;


        }
    }
     

    private void UpdateSpringForce()
    {
        // F = -kx
        // F = -kx -bv

        

        m_vForce = -m_fSpringConstant * (m_vRestPos - m_attachedBody.transform.position) -
            m_fDampingConstant * (m_attachedBody.velocity - m_vPrevVel);

        m_attachedBody.AddForce(m_vForce, ForceMode.Acceleration);

        m_vPrevVel = m_attachedBody.velocity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(m_vRestPos, 1f);

        if (m_attachedBody)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(m_attachedBody.transform.position, 1f);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, m_attachedBody.transform.position);
        }
    }
}
