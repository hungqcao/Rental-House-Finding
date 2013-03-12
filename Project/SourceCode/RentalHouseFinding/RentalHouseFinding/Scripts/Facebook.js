function InitialiseFacebook(appId) {
    window.fbAsyncInit = function () {
        FB.init({
            appId: appId,
            status: true,
            cookie: true,
            xfbml: true
        });
        //        FB.Event.subscribe('auth.login', function (response) {
        //        alert('1');
        //        var credentials = { uid: response.authResponse.userID, accessToken: response.authResponse.accessToken };
        //        SubmitLogin(credentials);
        //        });
        FB.getLoginStatus(function (response) {
            if (response.status === 'connected') {
                var credentials = { uid: response.authResponse.userID, accessToken: response.authResponse.accessToken };
                //SubmitLogin(credentials);
                var url = 'FacebookUserDetail?token=' + response.authResponse.accessToken;
                url = url.replace('_id_', response.authResponse.userID);
                window.location.href = url;
            }
            else if (response.status === 'not_authorized') { }
            else { }

        });

//        function SubmitLogin(credentials) {            
//            $.ajax({
//                url: "/account/facebookLogin",
//                type: "POST",
//                data: credentials,
//                error: function () {
//                    //alert("error logging in to your facebook account.");
//                },
//                success: function () {
//                    window.location.reload();
//                }
//            });
//        }

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