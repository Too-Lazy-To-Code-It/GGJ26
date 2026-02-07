using UnityEngine;

namespace Code.Managers
{
    public class DistanceSwitchController : MonoBehaviour
    {
        [SerializeField] Transform runner;   
        [SerializeField] float checkDistance = 20f;
        [SerializeField] float switchProbability = 0.3f;

        float lastCheckX;

        private void Start()
        {
            if (runner == null)
            {
                Debug.LogError("Runner transform not assigned");
                enabled = false;
                return;
            }
            lastCheckX = runner.position.x;
        }

        private void Update()
        {
            if (GameManager.Instance.State != GameState.Playing)
                return;

            float distanceTraveled = runner.position.x - lastCheckX;

            if (distanceTraveled >= checkDistance)
            {
                GameManager.Instance.TryRandomSwitch(switchProbability);
                lastCheckX = runner.position.x;
            }
        }
    }
}