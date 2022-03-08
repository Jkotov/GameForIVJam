using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using Spine.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public delegate void SolveHandle();
    [CanBeNull] public event SolveHandle Solved;
    public static GameManager Instance { get; private set; }
    [SerializeField] private List<Collider2D> doors = new List<Collider2D>();
    [HideInInspector] public string nextScene;
    [SerializeField] private Transform player;
    [SerializeField] private Transform columns;
    [SerializeField] private Transform liane;
    [SerializeField] private Transform bottomBg;
    [SerializeField] private Transform upBg;

    [SerializeField] private float playerFirstPos;
    [SerializeField] private float columnsFirstPos;
    [SerializeField] private float lianeFirstPos;
    [SerializeField] private float bottomBgFirstPos;
    [SerializeField] private float upBgFirstPos;
    [SerializeField] private float firstDuration;

    [HideInInspector] public float playerYMove;
    [HideInInspector] public Transform openingDoor;
    [HideInInspector] public SpriteRenderer doorShadow;
    [SerializeField] float doorYMove;
    [SerializeField] private float yMoveDuration;

    [SerializeField] private float playerEndPos;
    [SerializeField] private float columnsEndPos;
    [SerializeField] private float lianeEndPos;
    [SerializeField] private float bottomBgEndPos;
    [SerializeField] private float upBgEndPos;
    [SerializeField] private float endDuration;
    
    [SerializeField] private SkeletonAnimation anim;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        foreach (var door in doors)
        {
            door.enabled = false;
        }

        StartCoroutine(MoveToDoor());
    }


    private IEnumerator MoveToDoor()
    {
        player.DOMoveX(playerFirstPos, firstDuration);
        columns.DOMoveX(columnsFirstPos, firstDuration);
        liane.DOMoveX(lianeFirstPos, firstDuration);
        bottomBg.DOMoveX(bottomBgFirstPos, firstDuration);
        upBg.DOMoveX(upBgFirstPos, firstDuration);
        anim.AnimationName = "animation";
        yield return new WaitForSeconds(firstDuration);
        
        foreach (var door in doors)
        {
            door.enabled = true;
        }
        anim.AnimationName = "";
    }

    public void TurnOffDoorColliders()
    {
        foreach (var door in doors)
        {
            door.enabled = false;
        }
    }
    public void ResumeMove()
    {
        Solved?.Invoke();
        StartCoroutine(YMove());
    }
    public IEnumerator YMove()
    {
        player.DOMoveY(player.position.y + playerYMove, yMoveDuration);
        openingDoor.DOMoveY(openingDoor.position.y + doorYMove, yMoveDuration);
        anim.AnimationName = "animation";
        var shadowColor = doorShadow.color;
        shadowColor.a = 0;
        doorShadow.DOColor(shadowColor, yMoveDuration);
        yield return yMoveDuration;
        StartCoroutine(MoveOutDoor());
    }
    private IEnumerator MoveOutDoor()
    {
        player.DOMoveX(playerEndPos, endDuration);
        columns.DOMoveX(columnsEndPos, endDuration);
        liane.DOMoveX(lianeEndPos, endDuration);
        bottomBg.DOMoveX(bottomBgEndPos, endDuration);
        upBg.DOMoveX(upBgEndPos, endDuration);
        yield return new WaitForSeconds(endDuration);
        SceneManager.LoadScene(nextScene);
    }
}
