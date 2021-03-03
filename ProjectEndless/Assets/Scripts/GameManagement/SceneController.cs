using UnityEngine;
using Base.Game.Signal;
using UnityEngine.SceneManagement;
namespace Main
{
    public class SceneController : Singleton<SceneController>
    {
        private void OnDisable()
        {
            SignalBus<SGLevelChange, int>.Instance.UnRegister(SCSceneChange);
        }

        private void Awake()
        {
            SignalBus<SGLevelChange, int>.Instance.Register(SCSceneChange);
        }

        public void SCSceneChange(int index)
        {
            SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(index).buildIndex);
        }

        public void SCSceneRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }
}

