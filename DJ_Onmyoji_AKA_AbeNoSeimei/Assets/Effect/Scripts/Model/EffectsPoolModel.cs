using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Effect.Model
{
    /// <summary>
    /// エフェクトプール
    /// モデル
    /// </summary>
    public class EffectsPoolModel : MonoBehaviour, IEffectsPoolModel
    {
        /// <summary>トランスフォーム</summary>
        private Transform _transform;
        /// <summary>トランスフォーム</summary>
        public Transform Transform => _transform != null ? _transform : _transform = transform;
        /// <summary>プール数の上限</summary>
        [Tooltip("プール数の上限")]
        [SerializeField] private int countLimit = 30;
        /// <summary>プール完了</summary>
        public IReactiveProperty<bool> IsCompleted { get; private set; } = new BoolReactiveProperty();
        /// <summary>ダンスの衝撃波</summary>
        [SerializeField] private Transform danceShockwavePrefab;
        /// <summary>ダンスの衝撃波</summary>
        private List<Transform> _danceShockwave = new List<Transform>();
        /// <summary>ラップの爆発</summary>
        [SerializeField] private Transform shikigamiWrapExplosionPrefab;
        /// <summary>ラップの爆発</summary>
        private List<ParticleSystem> _shikigamiWrapExplosion = new List<ParticleSystem>();
        /// <summary>敵のヒットエフェクト</summary>
        [SerializeField] private Transform hitEffectPrefab;
        /// <summary>敵のヒットエフェクト</summary>
        private List<Transform> _hitEffect = new List<Transform>();
        /// <summary>敵がやられた時のエフェクト</summary>
        [SerializeField] private Transform enemyDownEffectPrefab;
        /// <summary>敵がやられた時のエフェクト</summary>
        private List<Transform> _enemyDownEffect = new List<Transform>();
        /// <summary>プレイヤーがやられた時のエフェクト</summary>
        [SerializeField] private Transform playerDownEffectPrefab;
        /// <summary>プレイヤーがやられた時のエフェクト</summary>
        private List<Transform> _playerDownEffect = new List<Transform>();

        private void Start()
        {
            Debug.Log("プール開始");
            for (int i = 0; i < countLimit; i++)
            {
                _shikigamiWrapExplosion.Add(InstancePrefabDisabledAndGetClone(shikigamiWrapExplosionPrefab, Transform).GetComponent<ParticleSystem>());
                _danceShockwave.Add(InstancePrefabDisabledAndGetClone(danceShockwavePrefab, Transform).GetComponent<Transform>());
                _hitEffect.Add(InstancePrefabDisabledAndGetClone(hitEffectPrefab, Transform).GetComponent<Transform>());
                _enemyDownEffect.Add(InstancePrefabDisabledAndGetClone(enemyDownEffectPrefab, Transform).GetComponent<Transform>());
                _playerDownEffect.Add(InstancePrefabDisabledAndGetClone(playerDownEffectPrefab, Transform).GetComponent<Transform>());
            }
            Debug.Log("プール完了");
            IsCompleted.Value = true;
        }

        /// <summary>
        /// プレハブを元に無効状態で生成
        /// ゲームオブジェクトを取得
        /// </summary>
        /// <param name="prefab">プレハブ</param>
        /// <param name="parent">親オブジェクト</param>
        /// <returns>生成後のオブジェクト</returns>
        private GameObject InstancePrefabDisabledAndGetClone(Transform prefab, Transform parent)
        {
            var obj = Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);

            return obj.gameObject;
        }

        public ParticleSystem GetShikigamiWrapExplosion()
        {
            return GetEffectComponent(_shikigamiWrapExplosion, shikigamiWrapExplosionPrefab);
        }

        public Transform GetDanceShockwave()
        {
            return GetEffectComponent(_danceShockwave, danceShockwavePrefab);
        }

        public Transform GetHitEffect()
        {
            return GetEffectComponent(_hitEffect, hitEffectPrefab);
        }

        public Transform GetEnemyDownEffect()
        {
            return GetEffectComponent(_enemyDownEffect, enemyDownEffectPrefab);
        }

        public Transform GetPlayerDownEffect()
        {
            return GetEffectComponent(_playerDownEffect, playerDownEffectPrefab);
        }

        /// <summary>
        /// エフェクトコンポーネントを取得
        /// 引数のエフェクトリストからアクティブでないオブジェクトを取得
        /// 全てアクティブなら新たにインスタンスしてリスト追加して、結果を取得
        /// </summary>
        /// <typeparam name="T">エフェクトのトランスフォーム</typeparam>
        /// <param name="effectList">エフェクトのリスト</param>
        /// <param name="prefab">エフェクトのプレハブ</param>
        /// <returns>エフェクトのオブジェクト</returns>
        private T GetEffectComponent<T>(List<T> effectList, Transform prefab) where T : Component
        {
            var inactiveComponent = effectList.FirstOrDefault(comp => !comp.gameObject.activeSelf);
            if (inactiveComponent == null)
            {
                Debug.LogWarning("プレハブ新規生成");
                var obj = Instantiate(prefab, Transform);
                var newComponent = obj.GetComponent<T>();
                effectList.Add(newComponent);
                return newComponent;
            }
            else
            {
                return inactiveComponent;
            }
        }

        public IEnumerator WaitForAllParticlesToStop(ParticleSystem[] particleSystems)
        {
            bool allStopped;
            do
            {
                yield return null; // 1フレーム待つ
                allStopped = true;
                foreach (var ps in particleSystems)
                {
                    if (ps.isPlaying)
                    {
                        allStopped = false;
                        break;
                    }
                }
            } while (!allStopped);
        }
    }

    /// <summary>
    /// エフェクトプール
    /// モデル
    /// インターフェース
    /// </summary>
    public interface IEffectsPoolModel
    {
        /// <summary>
        /// 式神ラップ用の爆発エフェクトを取得
        /// </summary>
        /// <returns>パーティクルシステム</returns>
        public ParticleSystem GetShikigamiWrapExplosion();
        /// <summary>
        /// ダンスの衝撃波のエフェクトを取得
        /// </summary>
        /// <returns>トランスフォーム</returns>
        public Transform GetDanceShockwave();
        /// <summary>
        /// 敵のヒットエフェクトを取得
        /// </summary>
        /// <returns>トランスフォーム</returns>
        public Transform GetHitEffect();
        /// <summary>
        /// 敵がやられた時のエフェクトを取得
        /// </summary>
        /// <returns>トランスフォーム</returns>
        public Transform GetEnemyDownEffect();
        /// <summary>
        /// プレイヤーがやられた時のエフェクトを取得
        /// </summary>
        /// <returns>トランスフォーム</returns>
        public Transform GetPlayerDownEffect();
        /// <summary>
        /// パーティクルの停止を待機する
        /// </summary>
        /// <param name="particleSystems">パーティクルシステム</param>
        /// <returns>コルーチン</returns>
        public IEnumerator WaitForAllParticlesToStop(ParticleSystem[] particleSystems);
    }
}
