using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lessons : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private SpriteRenderer _back;

    //links to test views
    //[SerializeField] private SomeView _someView;

    //links to logic managers
    //private SomeManeger _someManager;

    private void Start()
    {
        //SomeConfig config = Resources.Load("SomeConfig", typeof(SomeConfig))as   SomeConfig;
        //load some configs

        //_someManager = new SomeManager(config);
        //create some logic managers for tests
    }

    private void Update()
    {
        //Update logic managers
        //_someManager.Update();
    }
    private void OnDestroy()
    {
        //dispose logic managers
       //_someManager.Dispose();
     }
}
