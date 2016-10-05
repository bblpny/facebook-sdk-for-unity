using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facebook.Unity.Internal
{
	public interface IFacebookPlatform
	{
		OnDLLLoaded Install(
			string appId,
			bool cookie,
			bool logging,
			bool status,
			bool xfbml,
			bool frictionlessRequests,
			string authResponse,
			string javascriptSDKLocale,
			HideUnityDelegate onHideUnity,
			InitDelegate onInitComplete);

		UnityEngine.Object GetLoader();
	}
}
