﻿
    // Semicolon (;) to ensure closing of earlier scripting
    // Encapsulation
    // $ is assigned to jQuery
    ; (function ($) {

        // DOM Ready
        $(function () {
            // Binding a click event
            // From jQuery v.1.7.0 use .on() instead of .bind()
            $('.withOutLogin').bind('click', function (e) {

                // Prevents the default action to be triggered. 
                e.preventDefault();

                // Triggering bPopup when click event is fired
                $('#element_to_pop_up').bPopup();

            });

        });

    })(jQuery);

    (function (b) { b.fn.bPopup = function (u, C) { function v() { a.modal && b('<div class="b-modal ' + d + '"></div>').css({ backgroundColor: a.modalColor, position: "fixed", top: 0, right: 0, bottom: 0, left: 0, opacity: 0, zIndex: a.zIndex + l }).each(function () { a.appending && b(this).appendTo(a.appendTo) }).fadeTo(a.speed, a.opacity); A(); c.data("bPopup", a).data("id", d).css({ left: "slideIn" === a.transition ? -1 * (h + g) : m(!(!a.follow[0] && n || f)), position: a.positionStyle || "absolute", top: "slideDown" === a.transition ? -1 * (j + g) : p(!(!a.follow[1] && q || f)), "z-index": a.zIndex + l + 1 }).each(function () { a.appending && b(this).appendTo(a.appendTo) }); D(!0) } function r() { a.modal && b(".b-modal." + c.data("id")).fadeTo(a.speed, 0, function () { b(this).remove() }); D(); return !1 } function E(s) { var b = s.width(), d = s.height(); a.contentContainer.css({ height: d, width: b }); d <= c.height() && (d = c.height()); b <= c.width() && (b = c.width()); t = c.outerHeight(!0); g = c.outerWidth(!0); a.contentContainer.css({ height: "auto", width: "auto" }); A(); c.dequeue().animate({ left: m(!(!a.follow[0] && n || f)), top: p(!(!a.follow[1] && q || f)), height: d, width: b }, 250, function () { s.show(); w = B() }) } function D(b) { switch (a.transition) { case "slideIn": c.show().animate({ left: b ? m(!(!a.follow[0] && n || f)) : -1 * (h + g) }, a.speed, a.easing, function () { x(b) }); break; case "slideDown": c.show().animate({ top: b ? p(!(!a.follow[1] && q || f)) : -1 * (j + t) }, a.speed, a.easing, function () { x(b) }); break; default: b ? c.fadeIn(a.speed, function () { x(b) }) : c.stop().fadeOut(a.speed, a.easing, function () { x(b) }) } } function x(s) { s ? (e.data("bPopup", l), c.delegate("." + a.closeClass, "click." + d, r), a.modalClose && b(".b-modal." + d).css("cursor", "pointer").bind("click", r), !F && (a.follow[0] || a.follow[1]) && e.bind("scroll." + d, function () { w && c.dequeue().animate({ left: a.follow[0] ? m(!f) : "auto", top: a.follow[1] ? p(!f) : "auto" }, a.followSpeed, a.followEasing) }).bind("resize." + d, function () { if (w = B()) A(), c.dequeue().each(function () { f ? b(this).css({ left: h, top: j }) : b(this).animate({ left: a.follow[0] ? m(!0) : "auto", top: a.follow[1] ? p(!0) : "auto" }, a.followSpeed, a.followEasing) }) }), a.escClose && y.bind("keydown." + d, function (a) { 27 == a.which && r() }), k(C)) : (a.scrollBar || b("html").css("overflow", "auto"), b(".bModal." + d).unbind("click"), y.unbind("keydown." + d), e.unbind("." + d).data("bPopup", 0 < e.data("bPopup") - 1 ? e.data("bPopup") - 1 : null), c.undelegate("." + a.closeClass, "click." + d, r).data("bPopup", null).hide(), k(a.onClose), a.loadUrl && (a.contentContainer.empty(), c.css({ height: "auto", width: "auto" }))) } function m(a) { return a ? h + y.scrollLeft() : h } function p(a) { return a ? j + y.scrollTop() : j } function k(a) { b.isFunction(a) && a.call(c) } function A() { var b; q ? b = a.position[1] : (b = ((window.innerHeight || e.height()) - t) / 2 - a.amsl, b = b < z ? z : b); j = b; h = n ? a.position[0] : ((window.innerWidth || e.width()) - g) / 2; w = B() } function B() { return (window.innerHeight || e.height()) > c.outerHeight(!0) + z && (window.innerWidth || e.width()) > c.outerWidth(!0) + z } b.isFunction(u) && (C = u, u = null); var a = b.extend({}, b.fn.bPopup.defaults, u); a.scrollBar || b("html").css("overflow", "hidden"); var c = this, y = b(document), e = b(window), F = /OS 6(_\d)+/i.test(navigator.userAgent), z = 20, l = 0, d, w, q, n, f, j, h, t, g; c.close = function () { a = this.data("bPopup"); d = "__b-popup" + e.data("bPopup") + "__"; r() }; return c.each(function () { if (!b(this).data("bPopup")) if (k(a.onOpen), l = (e.data("bPopup") || 0) + 1, d = "__b-popup" + l + "__", q = "auto" !== a.position[1], n = "auto" !== a.position[0], f = "fixed" === a.positionStyle, t = c.outerHeight(!0), g = c.outerWidth(!0), a.loadUrl) switch (a.contentContainer = b(a.contentContainer || c), a.content) { case "iframe": b('<iframe class="b-iframe" scrolling="no" frameborder="0"></iframe>').attr("src", a.loadUrl).appendTo(a.contentContainer); k(a.loadCallback); t = c.outerHeight(!0); g = c.outerWidth(!0); v(); break; case "image": v(); b("<img />").load(function () { k(a.loadCallback); E(b(this)) }).attr("src", a.loadUrl).hide().appendTo(a.contentContainer); break; default: v(), b('<div class="b-ajax-wrapper"></div>').load(a.loadUrl, a.loadData, function () { k(a.loadCallback); E(b(this)) }).hide().appendTo(a.contentContainer) } else v() }) }; b.fn.bPopup.defaults = { amsl: 50, appending: !0, appendTo: "body", closeClass: "b-close", content: "ajax", contentContainer: !1, easing: "swing", escClose: !0, follow: [!0, !0], followEasing: "swing", followSpeed: 500, loadCallback: !1, loadData: !1, loadUrl: !1, modal: !0, modalClose: !0, modalColor: "#000", onClose: !1, onOpen: !1, opacity: 0.7, position: ["auto", "auto"], positionStyle: "absolute", scrollBar: !0, speed: 250, transition: "fadeIn", zIndex: 9997} })(jQuery);

    $(document).ready(function () {
        $("#message_submit").click(function () {
            if ($("#txtResion").val() == '') {
                $("#rpMessage").css("display", "inline");
                $("#txtResion").focus();
            }
            else {
                var mydata = { postId: $("#Id").val(), resion: $("#txtResion").val() };
                SendRepost(mydata);
                alert("Cảm ơn sự đóng góp của bạn!");
                deselect();
                $("#report").addClass("reported");
                $("#txtResion").val('');
                $(this).addClass("b-close");
                $("#rpMessage").css("display", "none");
            }

        });
        function deselect() {
            $(".pop").slideFadeToggle(function () {
                $("#report").removeClass("selected");
            });
        }

        $("#report").live('click', function () {
            $("#txtResion").val('');
            if ($(this).hasClass("reported")) {
                alert("Báo cáo hoàn tất! Ban quan trị sẽ kiểm tra lại trong thời gian sớm nhất.");
            }
            else {
                if ($(this).hasClass("selected")) {
                    deselect();
                } else {
                    $(this).addClass("selected");
                    $(".pop").slideFadeToggle(function () {
                        $("#resion").focus();
                    });
                }
            }
            return false;
        });

        $("#message_cancel").live('click', function () {
            deselect();
            return false;
        });

        $.fn.slideFadeToggle = function (easing, callback) {
            return this.animate({ opacity: 'toggle', height: 'toggle' }, "fast", easing, callback);
        };


        function SendRepost(mydata) {
            $.ajax({
                url: "/service/ReportPost",
                type: "POST",
                data: mydata,
                error: function () {
                    //alert("");
                },
                success: function () {
                }
            });
        }

        var marker;
        var map;
        function initialize() {
            var mapProp = {
                center: new google.maps.LatLng($('#Lat').val(), $('#Lon').val()),
                zoom: 16,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            map = new google.maps.Map(document.getElementById("mapContainer"), mapProp);
            marker = new google.maps.Marker({
                position: new google.maps.LatLng($('#Lat').val(), $('#Lon').val()),
                map: map
            });

        }
        google.maps.event.addDomListener(window, 'load', initialize);

    });

    function addFavorite() {
        $.getJSON("/Post/AddFavorite", null, function (success) {
            if (success) {
                alert('Đã thêm bài vào danh mục');
                $("#favorite").attr('value', 'Gỡ khỏi danh mục');
                $("#favorite").attr('onclick', 'removeFavorite();');
            }
        });
    }
    function removeFavorite() {
        $.getJSON("/Post/RemoveFavorite", null, function (success) {
            if (success) {
                alert('Đã xóa bài khỏi danh mục');
                $("#favorite").attr('value', 'Thêm vào danh mục');
                $("#favorite").attr('onclick', 'addFavorite();');

            }
        });
    }