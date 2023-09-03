using UnityEngine;
using System.Collections;


/// <summary>
/// Debuggia varten tehty abilityjen enablauskoodi
/// </summary>
public class AbilityEnabler : MonoBehaviour
{

    [SerializeField] bool doubleJump;
    bool doubleJumpActive;

    [SerializeField] bool dash;
    bool dashActive;


    [SerializeField] bool wJump;
    bool wJumpActive;

    [SerializeField] AbilityData abilityData;

    private void Update()
    {
        #region DoubleJump
        if (Input.GetKeyDown(KeyCode.J) && !abilityData.IsActive(PlayerAbility.Doublejump))
        {
            abilityData.AddActiveAbilities(PlayerAbility.Doublejump);
            doubleJump = true;
            doubleJumpActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            abilityData.RemoveActiveAbilities(PlayerAbility.Doublejump);
            doubleJump = false;
            doubleJumpActive = false;
        }

        if (doubleJump && !doubleJumpActive)
        {
            doubleJumpActive = true;
            abilityData.AddActiveAbilities(PlayerAbility.Doublejump);
        }
        else if (!doubleJump && doubleJumpActive)
        {
            doubleJumpActive = false;
            abilityData.RemoveActiveAbilities(PlayerAbility.Doublejump);
        }
        #endregion

        #region Dash
        if (Input.GetKeyDown(KeyCode.K) && !abilityData.IsActive(PlayerAbility.Dash))
        {
            abilityData.AddActiveAbilities(PlayerAbility.Dash);
            dash = true;
            dashActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            abilityData.RemoveActiveAbilities(PlayerAbility.Dash);
            dash = false;
            dashActive = false;
        }

        if (dash && !dashActive)
        {
            dashActive = true;
            abilityData.AddActiveAbilities(PlayerAbility.Dash);
        }
        else if (!dash && dashActive)
        {
            dashActive = false;
            abilityData.RemoveActiveAbilities(PlayerAbility.Dash);
        }
        #endregion

        #region WallJump
        if (Input.GetKeyDown(KeyCode.L) && !abilityData.IsActive(PlayerAbility.Walljump))
        {
            abilityData.AddActiveAbilities(PlayerAbility.Walljump);
            wJump = true;
            wJumpActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            abilityData.RemoveActiveAbilities(PlayerAbility.Walljump);
            wJump = false;
            wJumpActive = false;
        }

        if (wJump && !wJumpActive)
        {
            wJumpActive = true;
            abilityData.AddActiveAbilities(PlayerAbility.Walljump);
        }
        else if (!wJump && wJumpActive)
        {
            wJumpActive = false;
            abilityData.RemoveActiveAbilities(PlayerAbility.Walljump);
        }
        #endregion

    }

}
