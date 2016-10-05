/**
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

namespace Facebook.Unity.Implementation
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
	using Internal;

    /// <summary>
    /// Static class for exposing the facebook integration.
    /// </summary>
    public abstract class FB<TPlatform> : InternalFB
		where TPlatform : struct, Internal.IFacebookPlatform
	{
        /// <summary>
        /// This is the preferred way to call FB.Init(). It will take the facebook app id specified in your "Facebook"
        /// => "Edit Settings" menu when it is called.
        /// </summary>
        /// <param name="onInitComplete">
        /// Delegate is called when FB.Init() finished initializing everything. By passing in a delegate you can find
        /// out when you can safely call the other methods.
        /// </param>
        /// <param name="onHideUnity">A delegate to invoke when unity is hidden.</param>
        /// <param name="authResponse">Auth response.</param>
        public static void Init(InitDelegate onInitComplete = null, HideUnityDelegate onHideUnity = null, string authResponse = null)
		{
            InternalFB.Init(
				default(TPlatform),
                FacebookSettings.AppId,
                FacebookSettings.Cookie,
                FacebookSettings.Logging,
                FacebookSettings.Status,
                FacebookSettings.Xfbml,
                FacebookSettings.FrictionlessRequests,
                authResponse,
                InternalFB.DefaultJSSDKLocale,
                onHideUnity,
                onInitComplete);
        }

        /// <summary>
        /// If you need a more programmatic way to set the facebook app id and other setting call this function.
        /// Useful for a build pipeline that requires no human input.
        /// </summary>
        /// <param name="appId">App identifier.</param>
        /// <param name="cookie">If set to <c>true</c> cookie.</param>
        /// <param name="logging">If set to <c>true</c> logging.</param>
        /// <param name="status">If set to <c>true</c> status.</param>
        /// <param name="xfbml">If set to <c>true</c> xfbml.</param>
        /// <param name="frictionlessRequests">If set to <c>true</c> frictionless requests.</param>
        /// <param name="authResponse">Auth response.</param>
        /// <param name="javascriptSDKLocale">
        /// The locale of the js sdk used see
        /// https://developers.facebook.com/docs/internationalization#plugins.
        /// </param>
        /// <param name="onHideUnity">
        /// A delegate to invoke when unity is hidden.
        /// </param>
        /// <param name="onInitComplete">
        /// Delegate is called when FB.Init() finished initializing everything. By passing in a delegate you can find
        /// out when you can safely call the other methods.
        /// </param>
        public static void Init(
            string appId,
            bool cookie = true,
            bool logging = true,
            bool status = true,
            bool xfbml = false,
            bool frictionlessRequests = true,
            string authResponse = null,
            string javascriptSDKLocale = InternalFB.DefaultJSSDKLocale,
            HideUnityDelegate onHideUnity = null,
            InitDelegate onInitComplete = null)
        {
            if (string.IsNullOrEmpty(appId))
            {
                throw new ArgumentException("appId cannot be null or empty!");
            }
			InternalFB.Init(default(TPlatform), appId, cookie, logging, status, xfbml, frictionlessRequests, authResponse, javascriptSDKLocale, onHideUnity, onInitComplete);
        }
    }
}
