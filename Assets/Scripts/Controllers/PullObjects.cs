using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullObjects : MonoBehaviour
{
    [System.Serializable]
    private class Obj
    {
        public GameObject objectInScene;
        public PullableObj pullableObj;

        public Obj(GameObject obj, PullableObj pObj)
        {
            objectInScene = obj;
            pullableObj = pObj;
        }
    }

    [SerializeField] private Transform _defaultPosition;
    [SerializeField] private PullableObj _objPref;

    [SerializeField] private List<Obj> objs;

    public PullableObj AddObj()
    {
        List<Obj> deactiveObjs = objs.Where(x => !x.objectInScene.activeSelf).ToList();
        if(deactiveObjs.Count > 0)
        {
            GameObject obj = deactiveObjs.First().objectInScene;
            obj.SetActive(true);
            obj.transform.position = _defaultPosition.position;
            obj.transform.rotation = _defaultPosition.rotation;
            return deactiveObjs.First().pullableObj;
        }
        else
        {
            GameObject obj = Instantiate(_objPref.gameObject, _defaultPosition.position, _defaultPosition.rotation, transform);
            objs.Add(new Obj(obj, obj.GetComponent<PullableObj>()));
            return objs.Last().pullableObj;
        }
    }

    public PullableObj GetObj(int counter)
    {
        return objs[counter].pullableObj;
    }

    public void DeactivateObj(int counter)
    {
        objs[counter].pullableObj.Deactivate();
    }

    public void Clear()
    {
        objs.ForEach(x => x.pullableObj.Deactivate());
    }
}
