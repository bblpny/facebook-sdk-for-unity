﻿/**
 * Copyright (c) 2014-present, Facebook, Inc. All rights reserved.
 *
 * You are hereby granted a non-exclusive, worldwide, royalty-free license to use,
 * copy, modify, and distribute this software in source code or binary form for use
 * in connection with the web services and APIs provided by Facebook.
 *
 * As with any software that integrates with the Facebook platform, your use of
 * this software is subject to the Facebook Developer Principles and Policies
 * [http://developers.facebook.com/policy/]. This copyright notice shall be
 * included in all copies or substantial portions of the software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

namespace Facebook.Unity
{
	using Internal;
	using Mobile.Android;
	using System;
	using UnityEngine;

	public sealed class FB : Implementation.FB<Platform>
	{
	}

	namespace Mobile.Android
	{
		public struct Platform : IFacebookPlatform
		{
			UnityEngine.Object IFacebookPlatform.GetLoader()
			{
				return ComponentFactory.GetComponent<AndroidFacebookLoader>();
			}

			OnDLLLoaded IFacebookPlatform.Install(string appId, bool cookie, bool logging, bool status, bool xfbml, bool frictionlessRequests, string authResponse, string javascriptSDKLocale, HideUnityDelegate onHideUnity, InitDelegate onInitComplete)
			{
				return delegate (IFacebook facebook)
				{
					((AndroidFacebook)facebook).Init(appId, onHideUnity, onInitComplete);
				};
			}
		}
	}
}