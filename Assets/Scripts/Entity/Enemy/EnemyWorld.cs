using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWorld : MonoBehaviour
{
    public enum EnemyWorldState
    {
        idle,
        show,
        hide,

    }
    [SerializeField] private Transform _startPoint;
    [SerializeField] private EnemyController _enemy;

    [SerializeField] private AnimationEventController _enemyStartAnim;

    [SerializeField] private bool _startActive;

    [SerializeField] private LayerMask _floorLayers;
    [SerializeField] private LayerMask _playerLayers;
    [SerializeField] private BoxCollider2D _enemyCollider;
    [SerializeField] private AnimationClip _hideAnim;

    private void Start()
    {

        _enemy.OnTeleportateToPlayer += TeleportationToPlayer;

        if (!_startActive)
            _enemy.gameObject.SetActive(false);
    }

    private void Update()
    {
    }

    public void TeleportationToPlayer()
    {
        if (_enemy.IsDeath)
            return;

        Hide();
        StartCoroutine(TeleportateToPlayerCourotine());
    }

    public void Appearance()
    {
        if (_enemy.gameObject.activeSelf || _enemyStartAnim.GetState() == (int)EnemyWorldState.show || _enemy.IsDeath)
            return;

        _enemyStartAnim.transform.position = _enemy.transform.position;
        _enemyStartAnim.SetAnimatorState((int)EnemyWorldState.show);
    }

    public void Hide()
    {
        _enemyStartAnim.transform.position = _enemy.transform.position;

        _enemy.gameObject.SetActive(false);
        _enemyStartAnim.SetAnimatorState((int)EnemyWorldState.hide);
    }

    public void EnemyActive()
    {
        _enemy.gameObject.SetActive(true);

        _enemy.Restart();
    }

    private Vector2 FindPosition()
    {
        Vector2 position = _enemy.transform.position;
        Vector2 playerPos = (Vector2)Bootastrap.main.Player.transform.position + new Vector2(4.5f, 0);

        int[] distance = new int[_enemy.Model.TeleportateMaxDistance - _enemy.Model.TeleportateMinDistance + 1];


        int j = 0;
        for (int i = _enemy.Model.TeleportateMinDistance; i <= _enemy.Model.TeleportateMaxDistance; i++)
        {
            distance[j] = i;
            j++;
        }

        distance = Core.main.Shuffle<int>(distance);

        for (int i = 0; i < distance.Length; i++)
        {

            int random = Random.Range(0, 2) * 2 - 1;

            if (CheckPosition(playerPos + new Vector2(distance[i] * random, 0)))
            {
                position = playerPos + new Vector2(distance[i] * random, 0);
                break;
            }

            if (CheckPosition(playerPos + new Vector2(distance[i] * -random, 0)))
            {
                position = playerPos + new Vector2(distance[i] * -random, 0);
                break;
            }
        }

        return position;

        bool CheckPosition(Vector2 position)
        {
            Vector2 rayPoint;

            rayPoint = position + new Vector2(_enemyCollider.size.x / 2, 4);

            RaycastHit2D hitRight = Physics2D.Raycast(rayPoint, transform.TransformDirection(Vector3.down), 4.5f, _floorLayers);

            rayPoint = position + new Vector2(-_enemyCollider.size.x / 2, 4);

            RaycastHit2D hitLeft = Physics2D.Raycast(rayPoint, transform.TransformDirection(Vector3.down), 4.5f, _floorLayers);

            if (hitLeft.collider == null || hitRight.collider == null)
                return false;

            if (hitLeft.distance != hitRight.distance)
                return false;
            
            if (hitLeft.distance == 0 || hitRight.distance == 0)
                return false;

            RaycastHit2D hitInPlayer = Physics2D.Raycast(position + new Vector2(_enemyCollider.size.x / 2, _enemyCollider.size.y), Bootastrap.main.Player.Collider.bounds.center, 200f, _playerLayers);

            if (hitInPlayer.collider != null && hitInPlayer.collider.gameObject.layer != 3)
                return false;

            return true;
        }
    }

    private IEnumerator TeleportateToPlayerCourotine()
    {
        Vector2 position;

        yield return new WaitForSeconds(_hideAnim.length + 1 + Random.Range(_enemy.Model.TeleportationHideDelay.x, _enemy.Model.TeleportationHideDelay.y));

        yield return new WaitUntil(FindPositionUntil);

        _enemy.transform.position = FindPosition();
        Appearance();

        bool FindPositionUntil()
        {
            position = FindPosition();
            return position != (Vector2)_enemy.transform.position;
        }
    }
}
