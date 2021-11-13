using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ReverieTale;

namespace ReverieTale
{
    public class FadeManager_VR : MonoBehaviour
    {
        #region Singleton

        private static FadeManager_VR instance = new FadeManager_VR();
        private FadeManager_VR() { }

        public static FadeManager_VR Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = (FadeManager_VR)FindObjectOfType(typeof(FadeManager_VR));

                    //if (instance == null)
                    //{
                    //    Debug.LogError(typeof(FadeManager_VR) + "is nothing");
                    //}
                }

                return instance;
            }
        }


        #endregion Singleton

        /// <summary>フェード中の透明度</summary>
        private float fadeAlpha = 0;
        /// <summary>フェード中かどうか</summary>
        private bool isFading = false;
        /// <summary>フェード色</summary>
        public Color fadeColor = Color.black;
        Renderer spRenderer;
        /// <summary>MeshRendererがアタッチされていなかったときに追加するマテリアル </summary>
        [SerializeField] Material material;
        [SerializeField] float fadeDuration = 1;

        // Start is called before the first frame update
        void Start()
        {
            //Material material = new Material(Shader.Find("Standard"));
            //material.color = Color.black;
            //material.SetOverrideTag("RenderType", "Transparent");
            //material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            //material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            //material.SetInt("_ZWrite", 0);
            //material.DisableKeyword("_ALPHATEST_ON");
            //material.EnableKeyword("_ALPHABLEND_ON");
            //material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            //material.renderQueue = 3000;
            //gameObject.AddComponent<MeshRenderer>();
            //spRenderer = GetComponent<Renderer>();
            //spRenderer.material = material;
            //if (spRenderer == null)
            //{
            //    Console.WriteLine("NullReferenceExceptionMeshRendererコンポーネントが付与されていませんのでアタッチします。");

            //}
            StartCoroutine(Fadein());
        }


        public void Awake()
        {
            //SceneManager.sceneLoaded += SceneLoaded;
            DontDestroyOnLoad(this.gameObject);


        }

        /// <summary>
        /// 画面遷移 .
        /// </summary>
        /// <param name='scene'>シーン名</param>
        /// <param name='interval'>暗転にかかる時間(秒)</param>
        public void LoadScene(string scene)
        {

            StartCoroutine(Fadeout(scene));

        }

        /// <summary>
        /// シーン遷移用コルーチン .
        /// </summary>
        /// <param name='scene'>シーン名</param>
        /// <param name='interval'>暗転にかかる時間(秒)</param>
        private IEnumerator Fadeout(string scene)
        {
            transform.position = Camera.main.transform.position;
            transform.SetParent(Camera.main.transform);

            float time = 0;
            spRenderer = GetComponent<Renderer>();
            spRenderer.material = material;
            while(time <= fadeDuration)
            {
                //Debug.Log(this.fadeAlpha);
                this.fadeAlpha = Mathf.Lerp(0f,1f,time / fadeDuration);
                this.fadeColor.a = this.fadeAlpha;
                spRenderer.material.color = this.fadeColor;
                time += Time.deltaTime;
                yield return 0;
            }

            //シーン切替 .
            SceneManager.LoadScene(scene);

            ////だんだん明るく .
            //time = 0;
            //while (time <= interval)
            //{

            //    Debug.Log(this.fadeAlpha);
            //    this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            //    this.fadeColor.a = this.fadeAlpha;
            //    spRenderer.material.color = this.fadeColor;
            //    time += Time.deltaTime;
            //    yield return 0;
            //}
        }

        private IEnumerator Fadein()
        {
            transform.position = Camera.main.transform.position;
            float time = 0;
            spRenderer = GetComponent<Renderer>();
            spRenderer.material = material;
            while(time <= fadeDuration)
            {
                //Debug.Log(this.fadeAlpha);
                this.fadeAlpha = Mathf.Lerp(1f,0f,time / fadeDuration);
                this.fadeColor.a = this.fadeAlpha;
                spRenderer.material.color = this.fadeColor;
                time += Time.deltaTime;
                yield return 0;
            }

            yield return 0;
        }
        void SceneLoaded(Scene nextScene,LoadSceneMode mode)
        {
            StartCoroutine(Fadein());
        }

        public void MoveToScene(SceneName sceneName)
        {
            LoadScene(sceneName.ToString());
            //switch (seaneName)
            //{
            //    case SceneName.Title:

            //        LoadScene(seaneName, loadTime);
            //        break;

            //    case SceneName.Home:

            //        LoadScene(SceneName.Home.ToString(), loadTime);
            //        break;

            //    case SceneName.Battle:

            //        LoadScene(SceneName.Battle.ToString(), loadTime);
            //        break;

            //    case SceneName.Result:

            //        LoadScene(SceneName.Result.ToString(), loadTime);
            //        break;

            //        //case SceneName.PrepareBattle:       //ウェーブ開始前
            //        //    LoadScene(SceneName.Result.ToString(), loadTime);
            //        //    break;

            //}
        }
    }
}