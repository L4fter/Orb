using Meta.PoorMansDi;
using UnityEngine;

public class Timestep : DiMonoBehaviour
{
    private IGameProvider gameProvider;

    public void Init(IGameProvider gameProvider)
    {
        this.gameProvider = gameProvider;
    }

    private void FixedUpdate()
    {
        gameProvider.CurrentGame.MakeTimestep(Time.fixedDeltaTime);
    }
}
