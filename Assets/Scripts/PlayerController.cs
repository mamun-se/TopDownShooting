using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(LineRenderer))]
public class PlayerController : MonoBehaviour
{
    private Camera maincam;
    private NavMeshAgent playerAgent;
    private RaycastHit hit;
    private Animator playerAnimator;
    private const string groundTag = "Ground";
    private const string playerTag = "Player";
    private int totalCollectedCoins = 0;
    private IEnumerator coinCollectionCoroutine;
    [SerializeField] private GameObject clickMarker = null;
    private LineRenderer playerPathRenderer;
    [SerializeField] private float pathRendererWidth = 0.15f;
    private int pathRendererPositionCount = 0;
    void Start()
    {
        maincam = Camera.main;
        playerAgent = GetComponent<NavMeshAgent>();
        playerAnimator = GetComponent<Animator>();
        clickMarker.transform.position = playerAgent.transform.position;
        playerPathRenderer = GetComponent<LineRenderer>();
        playerPathRenderer.startWidth = pathRendererWidth;
        playerPathRenderer.endWidth = pathRendererWidth;
        playerPathRenderer.positionCount = pathRendererPositionCount;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MovePlayer(Input.mousePosition);
        }

        if (playerAgent.remainingDistance > 0.2f)
        {
            playerAnimator.SetBool("isMoving",true);
            if (playerAgent.hasPath)
            {
                DrawPlayerPath();
            }
        }
        else
        {
            playerAnimator.SetBool("isMoving",false);
        }
    }

    private void MovePlayer(Vector3 tapPos)
    {
        Ray ray = maincam.ScreenPointToRay(tapPos);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag(groundTag))
            {
                playerAgent.SetDestination(hit.point);
                clickMarker.transform.position = hit.point;
            }

            else if ( hit.transform.CompareTag(playerTag))
            {
                playerAgent.SetDestination(playerAgent.transform.position);
                clickMarker.transform.position = playerAgent.transform.position;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if (other.CompareTag(coinTag))
        // {
        //     totalCollectedCoins++;
        //     playerAnimator.SetBool("shallCollect",true);
        //     coinCollectionCoroutine = CoinCollectWithAnimation(1f,other.transform.gameObject);
        //     StartCoroutine(coinCollectionCoroutine);
        // }
    }

    // IEnumerator CoinCollectWithAnimation(float waitTime , GameObject coinObj)
    // {
    //     yield return new WaitForSeconds(waitTime);
    //     Destroy(coinObj.transform.gameObject);
    //     playerAnimator.SetBool("shallCollect",false);
    //     UiManager.uiInstance.SetCollectedCoins(totalCollectedCoins);
    // }

    private void DrawPlayerPath()
    {
        playerPathRenderer.positionCount = playerAgent.path.corners.Length;
        playerPathRenderer.SetPosition(0,transform.position);
        if (playerAgent.path.corners.Length < 2)
        {
            return;
        }

        for (int i = 1; i < playerAgent.path.corners.Length; i++)
        {
            Vector3 pointPosition = new Vector3(playerAgent.path.corners[i].x,playerAgent.path.corners[i].y,playerAgent.path.corners[i].z);
            playerPathRenderer.SetPosition(i,pointPosition);
        }
    }

}
