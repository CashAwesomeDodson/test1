using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAI : MonoBehaviour
{
    enum State
    {
        Roaming, 
        Chasing,
        Attack,
        Return,
        Death
    }
    State currentState;
    EnemyPathfinding enemyPathfinding;
    EnemyAttack enemyAttack;
    [SerializeField] float changeDirTime = 2;
    [SerializeField] float ChargeTime = 3;
    [SerializeField] float attackSpeed = 10;
    CircleCollider2D detectionRange;
    Animator enemyAnimator;
    [SerializeField] GameObject Player;
    void Awake()
    {
        currentState = State.Roaming;
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        enemyAnimator = GetComponent<Animator>();
        detectionRange = GetComponent<CircleCollider2D>();
        enemyAttack = GetComponent<EnemyAttack>();
    }
    void Start(){
        StartCoroutine(Roaming());
        enemyAnimator.SetBool("ifRun", false);
    }



    // Update is called once per frame
    void Update()
    {
       
        
    }

    public void StartAttack(){
        print("Attacking");
        currentState = State.Attack;
            StartCoroutine(Attacking());
            enemyAnimator.SetBool("ifRun", false);
    }

    IEnumerator Roaming(){
        while(currentState == State.Roaming){
            enemyAnimator.SetBool("ifRun", true);
            Vector2 roamPosition = new Vector2 (Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(changeDirTime);
        }
    }

    IEnumerator Attacking(){
        while(currentState == State.Attack){
        Vector2 followPlayer = (Player.transform.position - transform.position).normalized;
        new WaitForSeconds(ChargeTime);
        enemyPathfinding.MoveTo(followPlayer * attackSpeed);
        yield return new WaitForSeconds(changeDirTime);
        }
    }
}
