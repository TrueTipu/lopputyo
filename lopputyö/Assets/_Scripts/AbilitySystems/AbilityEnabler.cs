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


    [SerializeField] bool superDash;
    bool superDashActive;

    [GetSO] AbilityData abilityData;

    private void Start()
    {
        this.InjectGetSO();
    }

    private void Update()
    {

        #if UNITY_EDITOR
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

        #region SuperDash
        if (Input.GetKeyDown(KeyCode.L) && !abilityData.IsActive(PlayerAbility.SuperDash))
        {
            abilityData.AddActiveAbilities(PlayerAbility.SuperDash);
            superDash = true;
            superDashActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            abilityData.RemoveActiveAbilities(PlayerAbility.SuperDash);
            superDash = false;
            superDashActive = false;
        }

        if (superDash && !superDashActive)
        {
            superDashActive = true;
            abilityData.AddActiveAbilities(PlayerAbility.SuperDash);
        }
        else if (!superDash && superDashActive)
        {
            superDashActive = false;
            abilityData.RemoveActiveAbilities(PlayerAbility.SuperDash);
        }
        #endregion

        #endif
    }

}
