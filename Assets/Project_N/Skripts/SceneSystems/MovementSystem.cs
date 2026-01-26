using UnityEngine;
using UnityEngine.AI;

public class MovementSystem : MonoBehaviour
{
    public static MovementSystem Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    /// <summary>
    /// Быстрая оценка стоимости перемещения
    /// </summary>
    public float EstimateMoveCost(Unit unit, Vector3 target)
    {
        // Простая проверка - точка на NavMesh?
        if (!NavMesh.SamplePosition(target, out NavMeshHit hit, 1f, NavMesh.AllAreas))
            return float.PositiveInfinity;
        
        // Прямое расстояние * стоимость за метр
        float distance = Vector3.Distance(unit.transform.position, hit.position);
        return distance * unit.moveCostPerMeter;
    }
    
    /// <summary>
    /// Точный расчёт через NavMesh
    /// </summary>
    public float CalculateExactMoveCost(Unit unit, Vector3 target)
    {
        NavMeshPath path = new NavMeshPath();
        
        if (NavMesh.CalculatePath(unit.transform.position, target, NavMesh.AllAreas, path))
        {
            float pathLength = 0f;
            for (int i = 1; i < path.corners.Length; i++)
            {
                pathLength += Vector3.Distance(path.corners[i-1], path.corners[i]);
            }
            
            return pathLength * unit.moveCostPerMeter;
        }
        
        return float.PositiveInfinity;
    }
    
    /// <summary>
    /// Можно ли переместиться?
    /// </summary>
    public bool CanMoveTo(Unit unit, Vector3 target)
    {
        float cost = CalculateExactMoveCost(unit, target);
        return cost < float.PositiveInfinity && unit.currentStamina >= cost;
    }
    
    /// <summary>
    /// Переместить юнита
    /// </summary>
    public void MoveUnit(Unit unit, Vector3 target)
    {
        if (!CanMoveTo(unit, target))
        {
            Debug.LogWarning($"Не могу переместить {unit.unitName} в {target}");
            return;
        }
        
        float cost = CalculateExactMoveCost(unit, target);
        unit.SpendStamina((int)cost);
        
        // Простое перемещение через NavMeshAgent
        NavMeshAgent agent = unit.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.SetDestination(target);
        }
        else
        {
            // Ручное перемещение
            StartCoroutine(MoveCoroutine(unit, target));
        }
    }
    
    private System.Collections.IEnumerator MoveCoroutine(Unit unit, Vector3 target)
    {
        float speed = 5f;
        float threshold = 0.1f;
        
        while (Vector3.Distance(unit.transform.position, target) > threshold)
        {
            unit.transform.position = Vector3.MoveTowards(
                unit.transform.position,
                target,
                speed * Time.deltaTime
            );
            yield return null;
        }
    }
}