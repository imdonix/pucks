using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puck : MonoBehaviour
{
    private enum TurnPhase { Start, Directing, Powering, Shooting, End }

    [SerializeField] private GameObject selectorCircle;
    [SerializeField] private DirectorArrow directorArrow;

    private bool isSelected = false;

    private new Rigidbody2D rigidbody;

    private TurnPhase turnPhase = TurnPhase.Start;

    private Player owner;

    #region Properties

    public float Radius { get; private set; }

    #endregion

    private void Awake()
    {
        Select();
        Radius = GetComponent<CircleCollider2D>().radius;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isSelected) return;

        if(Input.GetKeyDown(KeyCode.Space))
            MoveToNextPhase();
    }

    #region Public methods

    public void Select()
    {
        isSelected = true;
        turnPhase = TurnPhase.Start;
        selectorCircle.SetActive(true);
    }

    #endregion

    #region Helpers

    private void StartDirecting()
    {
        turnPhase = TurnPhase.Directing;
        selectorCircle.SetActive(false);
        directorArrow.gameObject.SetActive(true);
        directorArrow.Rotate(0.1f);
    }

    private void StartPowering()
    {
        directorArrow.StopRotating();
        turnPhase = TurnPhase.Powering;
        transform.rotation = directorArrow.transform.rotation;
        directorArrow.transform.rotation = transform.rotation;
        directorArrow.StartPowering();
    }

    private void Shoot()
    {
        directorArrow.StopPowering();
        turnPhase = TurnPhase.Shooting;
        rigidbody.AddForce(transform.up * directorArrow.CurrentPower, ForceMode2D.Impulse);
    }

    private void MoveToNextPhase()
    {
        switch (turnPhase)
        {
            case TurnPhase.Start:
                StartDirecting();
                break;

            case TurnPhase.Directing:
                StartPowering();
                break;

            case TurnPhase.Powering:
                Shoot();
                break;
        }
    }

    #endregion
}
