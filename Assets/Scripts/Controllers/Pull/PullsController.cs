using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullsController : MonoBehaviour
{
    public static PullsController main;

    [SerializeField] private PullObjects _pullPref;
    [SerializeField] private List<PullObjects> _pulls = new List<PullObjects>();

    private void Awake()
    {
        if (main != null)
            Debug.Log($"Extra pulls");
        main = this;
    }

    public PullObjects GetPull(PullableObj obj)
    {
        List<PullObjects> pulls = _pulls.Where(x => x.ObjPref == obj).ToList();

        if (pulls.Count > 0)
            return pulls[0];

        _pulls.Add(Instantiate(_pullPref, transform));
        _pulls.Last().gameObject.SetActive(true);
        _pulls.Last().Init(obj);
        return _pulls.Last();
    }

    public void ClearAll()
    {
        foreach(PullObjects pull in _pulls)
        {
            pull.Clear();
        }
    }
}
