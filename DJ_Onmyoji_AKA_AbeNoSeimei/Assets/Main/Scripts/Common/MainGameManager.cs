using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main.Presenter;
using Main.Audio;
using Main.InputSystem;

namespace Main.Common
{
    /// <summary>
    /// メインのゲームマネージャー
    /// </summary>
    public class MainGameManager : MonoBehaviour
    {
        /// <summary>メインのゲームマネージャー</summary>
        private static MainGameManager _instance;
        /// <summary>メインのゲームマネージャー</summary>
        public static MainGameManager Instance => _instance;

        /// <summary>プレゼンタ</summary>
        [SerializeField] private MainPresenter presenter;
        /// <summary>プレゼンタ</summary>
        public MainPresenter Presenter => presenter;
        /// <summary>オーディオのオーナー</summary>
        [SerializeField] private AudioOwner audioOwner;
        /// <summary>オーディオのオーナー</summary>
        public AudioOwner AudioOwner => audioOwner;
        /// <summary>シーンオーナー</summary>
        [SerializeField] private SceneOwner sceneOwner;
        /// <summary>シーンオーナー</summary>
        public SceneOwner SceneOwner => sceneOwner;
        /// <summary>カーソル表示</summary>
        [SerializeField] private CursorVisible cursorVisible;
        /// <summary>カーソル表示</summary>
        public CursorVisible CursorVisible => cursorVisible;
        /// <summary>InputSystemのオーナー</summary>
        [SerializeField] private InputSystemsOwner inputSystemsOwner;
        /// <summary>InputSystemのオーナー</summary>
        public InputSystemsOwner InputSystemsOwner => inputSystemsOwner;
        /// <summary>SkyBoxのオーナー</summary>
        [SerializeField] private SkyBoxOwner skyBoxOwner;
        /// <summary>SkyBoxのオーナー</summary>
        public SkyBoxOwner SkyBoxOwner => skyBoxOwner;
        /// <summary>Levelのオーナー</summary>
        [SerializeField] private LevelOwner levelOwner;
        /// <summary>Levelのオーナー</summary>
        public LevelOwner LevelOwner => levelOwner;
        /// <summary>アナリティクスのオーナー</summary>
        [SerializeField] private AnalyticsOwner analyticsOwner;
        /// <summary>アナリティクスのオーナー</summary>
        public AnalyticsOwner AnalyticsOwner => analyticsOwner;

        private void Reset()
        {
            presenter = GameObject.Find("Presenter").GetComponent<MainPresenter>();
            audioOwner = GameObject.Find("AudioOwner").GetComponent<AudioOwner>();
            sceneOwner = GameObject.Find("SceneOwner").GetComponent<SceneOwner>();
            cursorVisible = GameObject.Find("CursorVisible").GetComponent<CursorVisible>();
            inputSystemsOwner = GameObject.Find("InputSystemsOwner").GetComponent<InputSystemsOwner>();
            skyBoxOwner = GameObject.Find("SkyBoxOwner").GetComponent<SkyBoxOwner>();
            levelOwner = GameObject.Find("LevelOwner").GetComponent<LevelOwner>();
            analyticsOwner = GameObject.Find("AnalyticsOwner").GetComponent<AnalyticsOwner>();
        }

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
        }

        private void Start()
        {
            if (audioOwner.isActiveAndEnabled)
                audioOwner.OnStart();
            if (presenter.isActiveAndEnabled)
                presenter.OnStart();
            if (cursorVisible.isActiveAndEnabled)
                cursorVisible.OnStart();
            if (inputSystemsOwner.isActiveAndEnabled)
                inputSystemsOwner.OnStart();
            if (skyBoxOwner.isActiveAndEnabled)
                skyBoxOwner.OnStart();
            if (levelOwner.isActiveAndEnabled)
                levelOwner.OnStart();
        }
    }

    /// <summary>
    /// ゲームマネージャーとのインターフェース
    /// </summary>
    public interface IMainGameManager
    {
        /// <summary>
        /// オンスタート
        /// </summary>
        void OnStart();
    }
}
