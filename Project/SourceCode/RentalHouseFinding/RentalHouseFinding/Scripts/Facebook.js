function InitialiseFacebook(appId) {
    window.fbAsyncInit = function () {
        FB.init({
            appId: appId,
            status: true,
            cookie: true,
            xfbml: true,
            oauth: true
        });
        FB.Event.subscribe('auth.login', function (response) {
            if (response.status === 'connected') {
                var url = 'FacebookUserDetail?token=' + response.authResponse.accessToken + '&returnUrl=' + getParameter('returnUrl');
                window.location.href = url;
            }
            else if (response.status === 'not_authorized') { }
            else {
                FB.login(function (response) {
                    if (response.status === 'unknown') {
                        $('#loading').css("display", "none");
                    }
                });
            }
        });
        FB.getLoginStatus(function (response) {
            if (response.status === 'connected') {
                var url = 'FacebookUserDetail?token=' + response.authResponse.accessToken + '&returnUrl=' + getParameter('returnUrl');
                window.location.href = url;
            }
            else if (response.status === 'not_authorized') { }
            else {
                FB.login(function (response) {
                    if (response.status === 'unknown') {
                        //                        location.reload();
                        $('#loading').css("display", "none");
                        var highlight = $('#openid_highlight');
                        if (highlight) {
                            highlight.replaceWith($('#openid_highlight a')[0]);
                        }
                    }
                });
            }

        });

        function getParameter(paramName) {
            var searchString = window.location.search.substring(1),
      i, val, params = searchString.split("&");

            for (i = 0; i < params.length; i++) {
                val = params[i].split("=");
                if (val[0] == paramName) {
                    return unescape(val[1]);
                }
            }
            return null;
        }

    };
  
        (function (d) {
            var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
            if (d.getElementById(id)) {
                return;
            }
            js = d.createElement('script');
            js.id = id;
            js.async = true;
            js.src = "//connect.facebook.net/en_US/all.js";
            ref.parentNode.insertBefore(js, ref);
        } (document));
}