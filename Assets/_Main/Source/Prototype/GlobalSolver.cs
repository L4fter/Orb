using UnityEngine;

public class GlobalSolver : MonoBehaviour
{
    public Solver Solver;

    private void Awake()
    {
        Solver = new Solver();
    }

    private void FixedUpdate()
    {
        Solver.SimulateTimeStep(Time.fixedDeltaTime);
    }
}
