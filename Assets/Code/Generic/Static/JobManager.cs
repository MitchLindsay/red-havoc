
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : MonoBehaviour
{
	private static JobManager instance = null;
	public  static JobManager Instance
	{
		get
		{
			if (!instance)
			{
				instance = FindObjectOfType(typeof(JobManager)) as JobManager;
				
				if (!instance)
				{
					GameObject obj 	= new GameObject("Job Manager");
					instance 		= obj.AddComponent<JobManager>();
				}
			}
			
			return instance;
		}
	}
	
	void OnApplicationQuit()
	{
		instance = null;
	}
}

public class Job
{
	public 	event System.Action<bool> 	JobComplete;
	public 	bool 						running 		{ get; private set; }
	public  bool 						paused 			{ get; private set; }
	private IEnumerator 				coroutine;
	private bool 						jobWasKilled;
	private Stack<Job> 					childJobStack;

	public Job(IEnumerator coroutine) : this (coroutine, true) { }
	public Job(IEnumerator coroutine, bool shouldStart)
	{
		this.coroutine = coroutine;
		if (shouldStart)
			Start();
	}
	
	public static Job Make(IEnumerator coroutine)
	{
		return new Job(coroutine);
	}
	
	public static Job Make(IEnumerator coroutine, bool shouldStart)
	{
		return new Job(coroutine, shouldStart);
	}
	
	private IEnumerator DoWork()
	{
		yield return null;
		
		while(running)
		{
			if (paused)
				yield return null;
			else
			{
				if (coroutine.MoveNext())
					yield return coroutine.Current;
				else
				{
					if (childJobStack != null && childJobStack.Count > 0)
					{
						Job childJob 	= childJobStack.Pop();
						coroutine 		= childJob.coroutine;
					}
					else
						running = false;
				}
			}
		}
		
		if (JobComplete != null)
			JobComplete(jobWasKilled);
	}
	
	public Job CreateAndAddChildJob(IEnumerator coroutine)
	{
		Job job = new Job(coroutine, false);
		AddChildJob(job);
		return job;
	}
	
	public void AddChildJob(Job childJob)
	{
		if (childJobStack == null)
			childJobStack = new Stack<Job>();
		childJobStack.Push(childJob);
	}
	
	public void RemoveChildJob(Job childJob)
	{
		if (childJobStack.Contains(childJob))
		{
			Stack<Job> 	childStack 			= new Stack<Job>(childJobStack.Count - 1);
			Job[] 		allCurrentChildren 	= childJobStack.ToArray();
			
			System.Array.Reverse(allCurrentChildren);
			
			for (int i = 0; i < allCurrentChildren.Length; i++)
			{
				Job job = allCurrentChildren[i];
				if (job != childJob)
					childStack.Push(job);
			}
			
			childJobStack = childStack;
		}
	}
	
	public void Start()
	{
		running = true;
		JobManager.Instance.StartCoroutine(DoWork());
	}
	
	public IEnumerator StartAsCoroutine()
	{
		running = true;
		yield return JobManager.Instance.StartCoroutine(DoWork());
	}
	
	public void Pause()
	{
		paused = true;
	}
	
	public void Unpause()
	{
		paused = false;
	}
	
	public void Kill()
	{
		jobWasKilled 	= true;
		running 		= false;
		paused 			= false;
	}
	
	public void Kill(float delayInSeconds)
	{
		int delay = (int)(delayInSeconds * 1000);
		new System.Threading.Timer(obj =>
		                           {
			lock(this)
			{
				Kill();
			}
		}, null, delay, System.Threading.Timeout.Infinite);
	}
}
