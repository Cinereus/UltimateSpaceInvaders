using PlayScene.Common;
using PlayScene.Common.ObjectPool;
using PlayScene.Swarms;
using UnityEngine;

namespace PlayScene.UI
{
    public class VictoryPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;

        public void ShowVictoryPanel()
        {
            _panel.SetActive(true);
            Cursor.visible = true;
            
            var allActiveBullets = FindObjectsOfType<Bullet>();
            
            foreach (var bullet in allActiveBullets)
            {
                bullet.GetComponent<PoolObject>().ReturnToPool();
            }
        }
        
        public void HideVictoryPanel()
        {
            _panel.SetActive(false);
            Cursor.visible = false;
        }
    }
}
