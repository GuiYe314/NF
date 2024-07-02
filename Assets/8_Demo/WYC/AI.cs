using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


namespace NF.Demo
{
    public class AI : MonoBehaviour
    {

        protected NavMeshAgent agent;

        protected StarterAssetsInputs _input;
        protected ThirdPersonController _controller;

        public List<Button> buttons;
        public List<Transform> targets;


        protected Transform ta;


        [SerializeField]
        protected PersonHint personHint;


        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            _input = GetComponent<StarterAssetsInputs>();
            _controller = GetComponent<ThirdPersonController>();

            _controller.IsNavigation = false;
            agent.enabled = false;


            for (int i = 0; i < buttons.Count; i++) {

                int index = i;

                buttons[i].onClick.AddListener(() =>
                {
                    agent.enabled = true;
                    _controller.IsNavigation = true;
                    ta = targets[index];
                    MovePersonMoveTargets(ta.position);

                });
            }

        }


        // Update is called once per frame
        void Update()
        {


            //Debug.LogError(_controller.IsNavigation +"~~~~~~~~~~"+ Vector3.Distance(transform.position, ta.position));

            if (!_input.move.Equals(Vector2.zero)||(_controller.IsNavigation && Vector3.Distance(transform.position, ta.position) < 0.8f))
            {
                agent.enabled = false;
                _controller.IsNavigation = false;

                ta = null;
                personHint.ShowUI(false);
            }

        }

        
        /// <summary>
        /// 移动人物到目标位置
        /// </summary>
        /// <param name="vector3">目标位置</param>
        public void MovePersonMoveTargets(Vector3 target)
        {
            agent.SetDestination(target);

            personHint.ShowUI(true);
        }


        private void OnDestroy()
        {
            for (int i = 0; i < buttons.Count; i++)
            {

                buttons[i].onClick.RemoveAllListeners();
            }
        }

    }


}

