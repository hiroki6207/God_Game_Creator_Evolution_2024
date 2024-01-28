using System.Collections;
using System.Collections.Generic;
using Main.Common;
using Main.Utility;
using UniRx;
using UnityEngine;
using Universal.Common;

namespace Main.Model
{
    /// <summary>
    /// 敵をスポーン
    /// </summary>
    public class EnemiesSpawnModel : SpawnModel, IEnemiesSpawnModel
    {
        /// <summary>最小半径</summary>
        [SerializeField] private float radiusMin = 10f;
        /// <summary>最大半径</summary>
        [SerializeField] private float radiusMax = 12f;
        /// <summary>トランスフォーム</summary>
        private Transform _target;
        /// <summary>陰陽（昼夜）の状態</summary>
        private float _onmyoState;  // TODO:敵生成の実装の際に昼／夜の判定を行う

        protected override void Start()
        {
            var utility = new MainCommonUtility();
            instanceRateTimeSec = utility.AdminDataSingleton.AdminBean.EnemiesSpawnModel.invincibleTimeSec;

            base.Start();
            Observable.FromCoroutine<Transform>(observer => WaitForTarget(observer))
                .Subscribe(x => _target = x)
                .AddTo(gameObject);
        }

        /// <summary>
        /// ターゲットが生成されるまで待機
        /// </summary>
        /// <param name="observer">トランスフォーム</param>
        /// <returns>コルーチン</returns>
        private IEnumerator WaitForTarget(System.IObserver<Transform> observer)
        {
            Transform target = null;
            while (target == null)
            {
                var obj = GameObject.FindGameObjectWithTag(ConstTagNames.TAG_NAME_PLAYER);
                if (obj != null)
                    target = obj.transform;
                yield return null;
            }
            observer.OnNext(target);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(_target != null ? _target.position : Vector2.zero, radiusMin);
            Gizmos.DrawWireSphere(_target != null ? _target.position : Vector2.zero, radiusMax);
        }

        protected override IEnumerator InstanceCloneObjects(float instanceRateTimeSec, ObjectsPoolModel objectsPoolModel)
        {
            // 一定間隔で敵を生成するための実装
            while (true)
            {
                if (_target != null)
                {
                    var enemy = objectsPoolModel.GetEnemyModel();
                    if (!enemy.Initialize(GetPositionOfAroundThePlayer(_target, radiusMin, radiusMax), _target))
                        Debug.LogError("Initialize");
                    if (!enemy.isActiveAndEnabled)
                        enemy.gameObject.SetActive(true);
                    yield return new WaitForSeconds(instanceRateTimeSec);
                }
                else
                    yield return null;
            }
        }

        /// <summary>
        /// ターゲットの指定半径範囲内にランダムで位置情報を返す
        /// </summary>
        /// <param name="target">ターゲット</param>
        /// <param name="radiusMin">最小半径</param>
        /// <param name="radiusMax">最大半径</param>
        /// <returns>位置情報</returns>
        private Vector2 GetPositionOfAroundThePlayer(Transform target, float radiusMin, float radiusMax)
        {
            float distance = Random.Range(radiusMin, radiusMax);
            float angle = Random.Range(0, 2 * Mathf.PI);
            Vector2 position = new Vector2(target.position.x + distance * Mathf.Cos(angle), target.position.y + distance * Mathf.Sin(angle));
            return position;
        }

        public bool SetOnmyoState(float onmyoState)
        {
            try
            {
                _onmyoState = onmyoState;

                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                return false;
            }
        }
    }

    /// <summary>
    /// 敵をスポーン
    /// インターフェース
    /// </summary>
    public interface IEnemiesSpawnModel
    {
        /// <summary>
        /// 陰陽（昼夜）の状態をセット
        /// </summary>
        /// <param name="onmyoState">陰陽（昼夜）の状態</param>
        /// <returns>成功／失敗</returns>
        public bool SetOnmyoState(float onmyoState);
    }
}
