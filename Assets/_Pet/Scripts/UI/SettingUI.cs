using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Pet
{
	public class SettingUI : MonoBehaviour
	{
		[SerializeField] Button soundBtn;
		[SerializeField] Button musicBtn;

		private void Start()
		{
			soundBtn.onClick.AddListener(() =>
			{
				SoundManager.Instance.ToggleSound();
			});
			musicBtn.onClick.AddListener(() =>
			{
				SoundManager.Instance.ToggleSound();
			});
		}

	
	}
}