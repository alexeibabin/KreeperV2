using UnityEngine;
using System.Collections;

public class LeverController : MonoBehaviour {

	public ControllableObject[] controlledObjects;

	public float actionCoolDown = 2f;
    public float timeToActivation = 2f;

	private bool isActivated=false;
	private bool isDeactivated=false;
	private float lookTime = 0;
    private float cooldownTime = 0;

	private Animator _animator; 

	private Transform localTransform;
    private Color defaultColor;
    private Material _mat;

	void Start(){
		_animator = GetComponent<Animator> ();
		localTransform = transform;
        _mat = GetComponentInChildren<Renderer>().material;
        defaultColor = _mat.color;
	}

	void Activate(){
		Debug.Log ("Activating");
		isActivated = true;
		isDeactivated = false;
        cooldownTime = actionCoolDown;
		_animator.SetTrigger ("Activate");

        foreach (var contObj in controlledObjects)
        {
            contObj.Activate();
        }
	}
	
	void Deactivate(){
		Debug.Log ("Deactivating");
		isActivated = false;
		isDeactivated = true;
        cooldownTime = actionCoolDown;
		_animator.SetTrigger ("Deactivate");

        foreach (var contObj in controlledObjects)
        {
            contObj.Deactivate();
        }
	}
	
	public bool IsActivated(){
		return isActivated;
	}
	
	public bool IsDeactivated(){
		return isDeactivated;
	}

	void OnDrawGizmos(){
		if(controlledObjects.Length < 1)
			return;

		foreach (var controlledObject in controlledObjects){
			Gizmos.DrawLine(transform.position, controlledObject.transform.position);
		}
	}

    void Update()
    {
        if (lookTime > 0)
            lookTime -= Time.deltaTime;
        else
        {
            if (isActivated)
                _mat.color = Color.green;
            else
                _mat.color = defaultColor;
        }

        if (cooldownTime >= 0)
            cooldownTime -= Time.deltaTime;
    }

    public bool LookAt()
    {
		Debug.Log (gameObject.name + " Is being looked at");
		Debug.Log (gameObject.name + "Parameters:  "+ cooldownTime+ " "+lookTime);
        if (cooldownTime >= 0)
            return false;

        lookTime += Time.deltaTime * 2;
        _mat.color = Color.blue;

        if (lookTime > timeToActivation)
        {
            return true;
        }
        return false;
    }

    public void Use()
    {
		Debug.Log ("Using");
        if (isActivated)
            Deactivate();
        else
            Activate();
    }
}
