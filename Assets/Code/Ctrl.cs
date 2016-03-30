using UnityEngine;
using System.Collections;
using GDGeek;


public class Ctrl : MonoBehaviour {
	private FSM fsm_ = new FSM();
	// Use this for initialization
	public View _view = null;
	public void fsmPost(string msg){
		this.fsm_.post(msg);
	}
	State beginState(){
		StateWithEventMap state = new StateWithEventMap();
		state.onStart += delegate{
			_view.begin.gameObject.SetActive(true);
		};
		state.onOver += delegate {
			_view.begin.gameObject.SetActive(false);
		};
		state.addEvent("begin","play");
		return state;
	}

	State playState(){
		StateWithEventMap state = TaskState.Create(delegate{
			TaskWait tw = new TaskWait();
			tw.setAllTime(3);
			return tw;
		},fsm_,"end");
		state.onStart += delegate {
			_view.play.gameObject.SetActive(true);
		};
		state.onOver += delegate {
			_view.play.gameObject.SetActive(false);
		};
		return state;
	}

	State endState(){
		StateWithEventMap state = new StateWithEventMap();
		state.onStart+=delegate {
			_view.end.gameObject.SetActive(true);
		};
		state.onOver += delegate {
			_view.end.gameObject.SetActive(false);
		};	
		state.addEvent("end","begin");
		return state;
	}

	void Start () {
		fsm_.addState("begin",beginState());
		fsm_.addState("play",playState());
		fsm_.addState("end",endState());
		fsm_.init("begin");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
