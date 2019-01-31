using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NoloVR_TrackedDevice))]
public class NoloVR_SimplePointer : MonoBehaviour
{
    public Color pointerHitColor = new Color(0f, 0.5f, 0f, 1f);
    public Color pointerMissColor = new Color(0.8f, 0f, 0f, 1f);
    public Material pointerMaterial;
    public LayerMask layersToIgnore = Physics.IgnoreRaycastLayer;
    public GameObject customPointerCursor;

    public float pointerLength = 100f;
    public float pointerThickness = 0.002f;


    private GameObject pointerHolder;
    private GameObject pointer;
    private GameObject pointerTip;
    private Vector3 pointerTipScale = new Vector3(0.05f, 0.05f, 0.05f);
    [HideInInspector]
    public NoloDeviceType deviceType;
    [HideInInspector]
    public GameObject hoveringElement;

    private float pointerContactDistance = 0f;
    private RaycastHit pointerContactRaycastHit = new RaycastHit();
    private Transform pointerContactTarget = null;
    private Vector3 destinationPosition;

    void OnEnable()
    {
        InitPointer();
        deviceType = GetComponent<NoloVR_TrackedDevice>().deviceType;
    }

    void Update()
    {
        PointerActivate(true);
        Ray pointerRaycast = new Ray(transform.position, transform.forward);
        RaycastHit pointerCollidedWith;
        var rayHit = Physics.Raycast(pointerRaycast, out pointerCollidedWith, pointerLength, ~layersToIgnore);
        var pointerBeamLength = GetPointerBeamLength(rayHit, pointerCollidedWith);
        SetPointerTransform(pointerBeamLength, pointerThickness);
        EventSystem.current.IsPointerOverGameObject();
    }

    private void InitPointer()
    {
        //pointerHolder
        pointerHolder = new GameObject("PointerHolder");
        pointerHolder.transform.parent = transform;
        pointerHolder.transform.localPosition = Vector3.zero;
        //pointer
        pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pointer.transform.name = string.Format("Pointer");
        pointer.transform.parent = pointerHolder.transform;
        pointer.GetComponent<BoxCollider>().isTrigger = true;
        pointer.AddComponent<Rigidbody>().isKinematic = true;
        pointer.layer = LayerMask.NameToLayer("Ignore Raycast");
        var pointerRenderer = pointer.GetComponent<MeshRenderer>();
        pointerRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        pointerRenderer.receiveShadows = false;
        if (pointerMaterial != null)
        {
            pointerRenderer.material = pointerMaterial;
        }
        //pointerTip
        if (customPointerCursor)
        {
            pointerTip = Instantiate(customPointerCursor);
        }
        else
        {
            pointerTip = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            pointerTip.transform.localScale = pointerTipScale;

            var pointerTipRenderer = pointerTip.GetComponent<MeshRenderer>();
            pointerTipRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            pointerTipRenderer.receiveShadows = false;
            if (pointerMaterial != null)
            {
                pointerTipRenderer.material = pointerMaterial;
            }
        }
        pointerTip.transform.name = string.Format("PointerTip");
        pointerTip.transform.parent = pointerHolder.transform;
        pointerTip.GetComponent<Collider>().isTrigger = true;
        pointerTip.AddComponent<Rigidbody>().isKinematic = true;
        pointerTip.layer = LayerMask.NameToLayer("Ignore Raycast");
        PointerActivate(false);
    }

    private void PointerActivate(bool state)
    {
        pointer.SetActive(state);
        pointerTip.SetActive(state);
    }

    private void SetPointerTransform(float setLength, float setThicknes)
    {
        //if the additional decimal isn't added then the beam position glitches
        var beamPosition = setLength / (2 + 0.00001f);

        pointer.transform.localScale = new Vector3(setThicknes, setThicknes, setLength);
        pointer.transform.localPosition = new Vector3(0f, 0f, beamPosition);
        pointerTip.transform.localPosition = new Vector3(0f, 0f, setLength - (pointerTip.transform.localScale.z / 2));
        pointerHolder.transform.localRotation = Quaternion.identity;
    }

    private float GetPointerBeamLength(bool hasRayHit, RaycastHit collidedWith)
    {
        var actualLength = pointerLength;

        //reset if beam not hitting or hitting new collider
        if (!hasRayHit || (pointerContactRaycastHit.collider && pointerContactRaycastHit.collider != collidedWith.collider))
        {
            if (pointerContactRaycastHit.collider != null)
            {
                PointerOut();
            }

            pointerContactDistance = 0f;
            pointerContactTarget = null;
            pointerContactRaycastHit = new RaycastHit();
            destinationPosition = Vector3.zero;

            UpdatePointerMaterial(pointerMissColor);
        }

        //check if beam has hit a new target
        if (hasRayHit)
        {
            pointerContactDistance = collidedWith.distance;
            pointerContactTarget = collidedWith.transform;
            pointerContactRaycastHit = collidedWith;
            destinationPosition = pointerTip.transform.position;
            UpdatePointerMaterial(pointerHitColor);

            PointerIn();
        }

        //adjust beam length if something is blocking it
        if (hasRayHit && pointerContactDistance < pointerLength)
        {
            actualLength = pointerContactDistance;
        }

        return actualLength;
    }

    private void UpdatePointerMaterial(Color color)
    {
        var pointerRenderer = pointer.GetComponent<Renderer>();
        pointerRenderer.material.color = color;
        var pointerTipRenderer = pointerTip.GetComponent<Renderer>();
        pointerTipRenderer.material.color = color;
    }

    public void PointerOut()
    {
    }
    public void PointerIn()
    {

    }


    #region UI

    [HideInInspector]
    public PointerEventData pointerEventData;
    public event UIPointerEventHandler UIPointerElementEnter;
    public event UIPointerEventHandler UIPointerElementExit;
    
    public bool PointerActive()
    {
        return pointer.activeSelf&pointerTip.activeSelf;
    }
    void Awake()
    {
        var eventSystem = FindObjectOfType<EventSystem>();
        var eventSystemInput = eventSystem.GetComponent<NoloVR_InputModule>();

        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.pointerId = (int)deviceType + 1000;
        eventSystemInput.pointers.Add(this);
    }
    public virtual void OnUIPointerElementEnter(UIPointerEventArgs e)
    {
        if (UIPointerElementEnter != null)
        {
            UIPointerElementEnter(this, e);
        }
    }

    public virtual void OnUIPointerElementExit(UIPointerEventArgs e)
    {
        if (UIPointerElementExit != null)
        {
            UIPointerElementExit(this, e);

            if (!e.isActive && e.previousTarget)
            {
                pointerEventData.pointerPress = e.previousTarget;
            }
        }
    }

    public UIPointerEventArgs SetUIPointerEvent(GameObject currentTarget, GameObject lastTarget = null)
    {
        UIPointerEventArgs e;
        e.isActive = PointerActive();
        e.currentTarget = currentTarget;
        e.previousTarget = lastTarget;
        return e;
    }
    #endregion
}

public struct UIPointerEventArgs
{
    public bool isActive;
    public GameObject currentTarget;
    public GameObject previousTarget;
}
public delegate void UIPointerEventHandler(object sender, UIPointerEventArgs e);