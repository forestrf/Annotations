using UnityEngine;

namespace Ashkatchap.Annotation {
	[ExecuteInEditMode]
	public class AnnotationSMB : StateMachineBehaviour {
		public string text;

		private void Awake() {
			this.hideFlags = HideFlags.DontSaveInBuild;
		}
	}
}
