using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main
{
    public class PersistentScene : MonoBehaviour
    {
        [SerializeField]
        private Object[] PersistentScenes = null;
        private void Awake()
        {
            foreach (Object scene in PersistentScenes)
            {
                Scene newS = SceneManager.GetSceneByName(scene.name);
                if (!newS.isLoaded || newS == null)
                {
                    SceneManager.LoadSceneAsync(scene.name, LoadSceneMode.Additive);
                }
            }
        }

        private void Start()
        {
            SceneManager.SetActiveScene(gameObject.scene);
        }
        Scene[] GetActiveScenes()
        {
            int countLoaded = SceneManager.sceneCount;
            Scene[] loadedScenes = new Scene[countLoaded];

            for (int i = 0; i < countLoaded; i++)
            {
                loadedScenes[i] = SceneManager.GetSceneAt(i);
            }
            return loadedScenes;
        }
    }
}

