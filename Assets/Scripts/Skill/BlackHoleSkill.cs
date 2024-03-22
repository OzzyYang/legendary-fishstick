using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSkill : Skill
{
	[SerializeField] private GameObject blackHoleObject;
	[SerializeField] private GameObject hotKeyTextObject;
	[SerializeField] private float blackHoleMaxSize;
	[SerializeField] private float blackHoleGrowDuration;
	[SerializeField] private float qteDuration;
	[SerializeField] private AnimationCurve blackHoleGrowCurve;
	[SerializeField] private List<KeyCode> hotKetsSetting;


	public bool isReleasingSkill { get; private set; }
	public GameObject blackHole { get; private set; }
	public override bool CanUseSkill()
	{
		if (coolDownTimer <= 0 && !isReleasingSkill)
		{
			coolDownTimer = skillCoolDownTime;
			UseSkill();
			return true;
		}

		return false;
	}

	public override void UseSkill()
	{
		base.UseSkill();
		blackHole = blackHole == null ? Instantiate(blackHoleObject) : blackHole;
		blackHole.SetActive(false);
		StartCoroutine("FlyUpForAndReleaseBlackHole", 0.25f);
	}

	protected IEnumerator FlyUpForAndReleaseBlackHole(float _seconds)
	{
		player.SetVelocity(0, 24);
		yield return new WaitForSeconds(_seconds);
		player.stateMachine.ChangeState(player.blackHoleState);
		blackHole.SetActive(true);
		blackHole.GetComponent<BlackHoleController>().SetupBlackHole(blackHoleMaxSize, blackHoleGrowCurve, blackHoleGrowDuration, hotKetsSetting, qteDuration, player.transform.position);
		player.SetVelocity(0, -0.2f);
	}

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		base.Start();

	}

	protected override void Update()
	{
		base.Update();
		if (Input.GetKeyDown(KeyCode.Z))
		{
			if (CanUseSkill())
			{
				isReleasingSkill = true;
			}
		}
		if (blackHole == null) isReleasingSkill = false;
	}

}
