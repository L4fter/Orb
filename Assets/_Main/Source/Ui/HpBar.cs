using Meta.PoorMansDi;
using UnityEngine.UI;

public class HpBar : DiMonoBehaviour
{
    public Image fill;
    private IPlayerStatsProvider playerStatsProvider;

    public void Init(IPlayerStatsProvider playerStatsProvider)
    {
        this.playerStatsProvider = playerStatsProvider;
    }

    private void Update()
    {
        fill.fillAmount = 1 - playerStatsProvider.HpPercentage;
    }
}