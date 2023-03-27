using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TestScript : MonoBehaviour
{
	private string sharemsg;
	public GameObject canvass;
	public void Clicksharebtn()
    {
		canvass.SetActive(false);
		StartCoroutine(TakeScreenshotAndShare());
	}
	private IEnumerator TakeScreenshotAndShare()
	{
		yield return new WaitForEndOfFrame();

		Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		ss.Apply();

		string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
		File.WriteAllBytes(filePath, ss.EncodeToPNG());

		// To avoid memory leaks
		Destroy(ss);
		//canvass.SetActive(true);

		new NativeShare().AddFile(filePath)
			.SetSubject("AR Ruler").SetText("Hey guys this is the measurement of the things").SetUrl("https://www.amazon.in/ref=as_li_ss_tl?ie=UTF8&linkCode=ll2&tag=enin-edge-topsites-curate-ana-21&linkId=fbedcb44d04a4bae8eae32722a2f41c2&language=en_IN")
			.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
			.Share();
		canvass.SetActive(true);

	}
}
