using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    private float currentStamina;
    [SerializeField] private float maxStamina;
    [SerializeField] private float rollCost;
    [SerializeField] bool hasRegen;
    [SerializeField] bool isSprint;

    [Range(0, 50)] [SerializeField] private float staminaUse;
    [Range(0, 50)] [SerializeField] private float staminaRegen;

    [SerializeField] private float sSpeed;

    [SerializeField] private Slider staminaBar;

    Coroutine Sprinting;
    private Player player;
    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
        currentStamina = maxStamina;
    }

    void Update()
    {
        if (!isSprint)
        {
            if(currentStamina <= maxStamina - 0.01f)
            {
                currentStamina += staminaRegen * Time.deltaTime;
                UpdateStamina();
                
                if(currentStamina >= maxStamina)
                {
                    hasRegen = true;
                }
            }
        }
    }
    void OnSprintIn()
    {
        if (player.isMove && hasRegen)
        {
            player.SetSprint(sSpeed);
            isSprint = true;
            anim.SetBool("Sprint", true);
            Sprinting = StartCoroutine(RunStart());
        }
    }
    void OnSprintOut()
    {
        if (!isSprint) return; 
        StopCoroutine(Sprinting);
        player.SetSprint(0);
        isSprint = false;
        anim.SetBool("Sprint", false);        
    }
    void OnRoll()
    {
        if (!player.isRoll && player.isMove && currentStamina >= rollCost)
        {
            currentStamina -= rollCost;
            anim.SetTrigger("Roll");
            player.PlayRoll();
            UpdateStamina();
        }
    }
    IEnumerator RunStart()
    {
        while (true)
        {
            currentStamina -= staminaUse * Time.deltaTime;
            UpdateStamina();
            if(currentStamina <= 0)
            {
                player.SetSprint(0);
                isSprint = false;
                hasRegen = false;
                anim.SetBool("Sprint", false);
                break;
            }
            yield return null;
        }
    }
    void UpdateStamina()
    {
        staminaBar.value = currentStamina / maxStamina;
    }

}
