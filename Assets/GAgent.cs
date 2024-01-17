using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class SubGoal
{
    public Dictionary<string, int> sgoals;
    public bool remove;

    public SubGoal(string s, int i, bool r)
    {
        sgoals = new Dictionary<string, int>();
        sgoals.Add(s, i);
        remove = r;
    }
}

public class GAgent : MonoBehaviour
{
    public List<GAction> actions = new List<GAction>();
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
    public WorldStates beliefs = new WorldStates();
    public GInventory inventory = new GInventory();
    public TMP_Text actionUI;

    GPlanner planner;
    Queue<GAction> actionQueue;
    public GAction currentAction;
    SubGoal currentGoal;

    // Start is called before the first frame update
    public void Start()
    {
        GAction[] acts = this.GetComponents<GAction>();
        foreach (GAction a in acts) {
            Debug.Log(a);
            actions.Add(a);
    }
    }


    bool invoked = false;
    void CompleteAction()
    {
        currentAction.running = false;
        currentAction.PostPerform();
        invoked = false;
    }

    private bool CanReachPosition(Vector3 position) {
        UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
        currentAction.agent.CalculatePath(position, path);
        Debug.Log(path.status);
        return path.status == UnityEngine.AI.NavMeshPathStatus.PathComplete;
    }

    void LateUpdate()
    {
        if (currentAction != null && currentAction.running)
        {
            // si el navmesh no está calculando bien el remaining distance, se puede
            //calcular la distancia a mano.
            float distanceToTarget = Vector3.Distance(currentAction.target.transform.position, this.transform.position);
            //if (distanceToTarget < 2f)
            //if (currentAction.agent.hasPath && distanceToTarget < 5f)
            if (!currentAction.agent.pathPending &&  currentAction.agent.remainingDistance < 5f)//!currentAction.agent.hasPath) 
            {
                if (!invoked)
                {
                    Invoke("CompleteAction", currentAction.duration);
                    invoked = true;
                }
            }
            if (!CanReachPosition(currentAction.agent.transform.position)) {
                Debug.Log("impossible destination");
                currentAction.running = false;
            }
            return;
        }

        if (planner == null || actionQueue == null)
        {
            planner = new GPlanner();

            var sortedGoals = from entry in goals orderby entry.Value descending select entry;

            foreach (KeyValuePair<SubGoal, int> sg in sortedGoals)
            {
                actionQueue = planner.plan(actions, sg.Key.sgoals, beliefs);
                if (actionQueue != null)
                {
                    currentGoal = sg.Key;
                    break;
                }
            }
        }

        if (actionQueue != null && actionQueue.Count == 0)
        {
            if (currentGoal.remove)
            {
                goals.Remove(currentGoal);
            }
            planner = null;
        }

        if (actionQueue != null && actionQueue.Count > 0)
        {
            currentAction = actionQueue.Dequeue();
            if (currentAction.PrePerform())
            {
                if (currentAction.target == null && currentAction.targetTag != "")
                    currentAction.target = GameObject.FindWithTag(currentAction.targetTag);

                if (currentAction.target != null)
                {

                    actionUI.text = currentAction.actionName;
                    currentAction.running = true;
                    currentAction.agent.SetDestination(currentAction.target.transform.position);
                }
            }
            else
            {
                actionUI.text = currentAction.actionName;
                actionQueue = null;
            }

        }

    }
}
