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

namespace Facebook.Unity.Internal
{
    using UnityEngine;

    public abstract class FacebookGameObject : MonoBehaviour, IFacebookCallbackHandler
    {
        public IFacebookImplementation Facebook { get; set; }

        public void Awake()
        {
            MonoBehaviour.DontDestroyOnLoad(this);
            AccessToken.CurrentAccessToken = null;

            // run whatever else needs to be setup
            this.OnAwake();
        }

        public void OnInitComplete(string message)
        {
            this.Facebook.OnInitComplete(new ResultContainer(message));
        }

        public void OnLoginComplete(string message)
        {
            this.Facebook.OnLoginComplete(new ResultContainer(message));
        }

        public void OnLogoutComplete(string message)
        {
            this.Facebook.OnLogoutComplete(new ResultContainer(message));
        }

        public void OnGetAppLinkComplete(string message)
        {
            this.Facebook.OnGetAppLinkComplete(new ResultContainer(message));
        }

        public void OnGroupCreateComplete(string message)
        {
            this.Facebook.OnGroupCreateComplete(new ResultContainer(message));
        }

        public void OnGroupJoinComplete(string message)
        {
            this.Facebook.OnGroupJoinComplete(new ResultContainer(message));
        }

        public void OnAppRequestsComplete(string message)
        {
            this.Facebook.OnAppRequestsComplete(new ResultContainer(message));
        }

        public void OnShareLinkComplete(string message)
        {
            this.Facebook.OnShareLinkComplete(new ResultContainer(message));
        }

        // use this to call the rest of the Awake function
        protected virtual void OnAwake()
        {
        }
    }
}
