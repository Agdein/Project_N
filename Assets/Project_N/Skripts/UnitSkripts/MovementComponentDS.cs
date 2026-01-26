using UnityEngine;
using UnityEngine.AI;

public class MovementComponentDS : MonoBehaviour
{
    [Header("Характеристики")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float moveCostPerMeter = 1f;
    [SerializeField] private float rotationSpeed = 180f;
    
    [Header("Ссылки")]
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;
    
    // Состояние
    private bool _isMoving = false;
    private Vector3 _targetPosition;
    private System.Action _onMoveComplete;
    
    // Свойства
    public bool IsMoving => _isMoving;
    public float MoveSpeed => moveSpeed;
    public float MoveCostPerMeter => moveCostPerMeter;
    
    private void Start()
    {
        if (navMeshAgent == null)
            navMeshAgent = GetComponent<NavMeshAgent>();
            
        if (animator == null)
            animator = GetComponent<Animator>();
            
        if (navMeshAgent != null)
        {
            navMeshAgent.speed = moveSpeed;
            navMeshAgent.angularSpeed = rotationSpeed;
        }
    }
    
    /// <summary>
    /// Начать перемещение к точке
    /// </summary>
    public void MoveTo(Vector3 position, System.Action onComplete = null)
    {
        _targetPosition = position;
        _onMoveComplete = onComplete;
        
        if (navMeshAgent != null && navMeshAgent.enabled)
        {
            // Используем NavMeshAgent
            navMeshAgent.SetDestination(position);
            _isMoving = true;
            
            if (animator != null)
                animator.SetBool("IsMoving", true);
        }
        else
        {
            // Ручное перемещение
            StartCoroutine(ManualMoveCoroutine(position));
        }
    }
    
    /// <summary>
    /// Остановить перемещение
    /// </summary>
    public void Stop()
    {
        _isMoving = false;
        
        if (navMeshAgent != null)
            navMeshAgent.isStopped = true;
            
        if (animator != null)
            animator.SetBool("IsMoving", false);
            
        _onMoveComplete = null;
    }
    
    /// <summary>
    /// Проверить, можно ли дойти до точки
    /// </summary>
    public bool CanReachPoint(Vector3 point, out float distance)
    {
        distance = 0f;
        
        if (navMeshAgent != null)
        {
            NavMeshPath path = new NavMeshPath();
            if (navMeshAgent.CalculatePath(point, path))
            {
                // Вычисляем длину пути
                for (int i = 1; i < path.corners.Length; i++)
                {
                    distance += Vector3.Distance(path.corners[i-1], path.corners[i]);
                }
                return path.status == NavMeshPathStatus.PathComplete;
            }
        }
        
        // Простая проверка по прямой
        distance = Vector3.Distance(transform.position, point);
        return true;
    }
    
    private System.Collections.IEnumerator ManualMoveCoroutine(Vector3 target)
    {
        _isMoving = true;
        
        if (animator != null)
            animator.SetBool("IsMoving", true);
        
        float threshold = 0.1f;
        
        while (Vector3.Distance(transform.position, target) > threshold)
        {
            // Движение
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                moveSpeed * Time.deltaTime
            );
            
            // Поворот в направлении движения
            Vector3 direction = (target - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            }
            
            yield return null;
        }
        
        CompleteMove();
    }
    
    private void Update()
    {
        if (_isMoving && navMeshAgent != null)
        {
            // Проверяем завершил ли NavMeshAgent движение
            if (!navMeshAgent.pathPending && 
                navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                CompleteMove();
            }
        }
    }
    
    private void CompleteMove()
    {
        _isMoving = false;
        
        if (animator != null)
            animator.SetBool("IsMoving", false);
        
        _onMoveComplete?.Invoke();
        _onMoveComplete = null;
    }
    
    // Визуализация в редакторе
    private void OnDrawGizmosSelected()
    {
        if (_isMoving)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, _targetPosition);
            Gizmos.DrawWireSphere(_targetPosition, 0.5f);
        }
    }
}
