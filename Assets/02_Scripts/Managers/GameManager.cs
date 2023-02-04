using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class GameManager
    {
        public int maxFloor = 3; //최대층
        public int currentFloor = 0; //현재층
        public int currentStage = 0; //현재던전스테이지

        public bool isFight = false;

        public string currentMapType = "";
        private int _monsterCount = -1;
        public int MonsterCount
        {
            get { return _monsterCount; }
            set
            {
                _monsterCount = value;
                if(_monsterCount <= 0)
                {
                    isFight = false;
                }
            }
        }

        public int gold;

        //포탈 만났을 때
        public void OnPortal(PointerEventData data = default)
        {
            currentMapType = SceneManager.GetActiveScene().name.Substring(0, SceneManager.GetActiveScene().name.Length - 1);

            Managers.GM.currentStage++;

            if (Managers.GM.currentStage == 1)
            {
                Managers.GM.currentStage = 0;

                //최종 층이 아니라면
                if (Managers.GM.currentFloor != Managers.GM.maxFloor - 1)
                {
                    Managers.UI.ShowPopupUI<UI_ShowLoading>();
                    Managers.Scene.LoadScene(Define.Scene.Root, Managers.Scene.changeSceneDelay);
                }
                else // 마지막 층
                {
                    Managers.UI.ShowPopupUI<UI_ShowLoading>();
                    Managers.Scene.LoadScene(Define.Scene.Ending, Managers.Scene.changeSceneDelay);
                }
            }
            else
            {
                Managers.UI.ShowPopupUI<UI_ShowLoading>();
                if (currentMapType == "InGameA")
                    Managers.Scene.LoadScene(Define.Scene.InGameA, Managers.Scene.changeSceneDelay);
                else if (currentMapType == "InGameB")
                    Managers.Scene.LoadScene(Define.Scene.InGameB, Managers.Scene.changeSceneDelay);
            }
        }
    }
}
