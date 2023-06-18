using UnityEngine;
using System.Collections;


//vain ja ainoastaan debuggia varten
public class AbilityEnabler : MonoBehaviour
{
    AbilityManager abilityManager;

    [SerializeField] bool doubleJump;
    bool doubleJumpActive;

    [SerializeField] bool dash;
    bool dashActive;


    [SerializeField] bool wJump;
    bool wJumpActive;

    private void Start()
    {
        abilityManager = GetComponent<AbilityManager>();
    }

    private void Update()
    {
        #region DoubleJump
        if (Input.GetKeyDown(KeyCode.J) && !abilityManager.IsActive(PlayerAbility.Doublejump))
        {
            abilityManager.ActivateAbility(PlayerAbility.Doublejump);
            doubleJump = true;
            doubleJumpActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            abilityManager.DeActivateAbility(PlayerAbility.Doublejump);
            doubleJump = false;
            doubleJumpActive = false;
        }

        if (doubleJump && !doubleJumpActive)
        {
            doubleJumpActive = true;
            abilityManager.ActivateAbility(PlayerAbility.Doublejump);
        }
        else if (!doubleJump && doubleJumpActive)
        {
            doubleJumpActive = false;
            abilityManager.DeActivateAbility(PlayerAbility.Doublejump);
        }
        #endregion

        #region Dash
        if (Input.GetKeyDown(KeyCode.K) && !abilityManager.IsActive(PlayerAbility.Dash))
        {
            abilityManager.ActivateAbility(PlayerAbility.Dash);
            dash = true;
            dashActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            abilityManager.DeActivateAbility(PlayerAbility.Dash);
            dash = false;
            dashActive = false;
        }

        if (dash && !dashActive)
        {
            dashActive = true;
            abilityManager.ActivateAbility(PlayerAbility.Dash);
        }
        else if (!dash && dashActive)
        {
            dashActive = false;
            abilityManager.DeActivateAbility(PlayerAbility.Dash);
        }
        #endregion

        #region WallJump
        if (Input.GetKeyDown(KeyCode.L) && !abilityManager.IsActive(PlayerAbility.Walljump))
        {
            abilityManager.ActivateAbility(PlayerAbility.Walljump);
            wJump = true;
            wJumpActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            abilityManager.DeActivateAbility(PlayerAbility.Walljump);
            wJump = false;
            wJumpActive = false;
        }

        if (wJump && !wJumpActive)
        {
            wJumpActive = true;
            abilityManager.ActivateAbility(PlayerAbility.Walljump);
        }
        else if (!wJump && wJumpActive)
        {
            wJumpActive = false;
            abilityManager.DeActivateAbility(PlayerAbility.Walljump);
        }
        #endregion

    }

}
