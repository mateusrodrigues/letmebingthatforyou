$(function($) {
    $.fn.type = function(value, complete, index) {
        return this.each(function() {
            $this = $(this);
            if (index === undefined) {
                index = 0;
            }
            var val = value.substr(0, index + 1);
            $this.attr('value', val);
            if (index < value.length) {
                setTimeout(function() { $this.type(value, complete, index + 1); }, Math.random() * 240);
            }
            else {
                if (complete) {
                    setTimeout(function() { complete() }, 240);
                }
            }
        });
    };

    $.fn.doStep = function(type, action) {
        return this.each(function() {
            $this = $(this);
            $this.show(type, function() {
                if (jQuery.browser.msie) {
                    this.style.removeAttribute('filter');
                }
                action();
            });
        });
    };
});

$(function() {
    function clickSearch(mouse) {
        $(".step4").doStep("drop", function() {
            var searchButton = $("#sb_form_go");
            mouse.animate({
                top: (searchButton.offset().top + 10) + "px",
                left: (searchButton.offset().left + 10) + "px"
            }, 2000, 'swing', function() {
                searchButton.click();
            });
        });
    }

    var searchQuery = $("#sb_form_q");
    var fakeMouse = $("#fake_mouse");
    $("#lmbtfyResult").show("bounce", "fast", function() {
        if (jQuery.browser.msie) {
            this.style.removeAttribute('filter');
        }
        $(".step1").doStep("drop", function() {
            $(".step2").doStep("drop", function() {
                fakeMouse.show("bounce", "fast");
                fakeMouse.animate({
                    top: (searchQuery.offset().top + 15) + "px",
                    left: (searchQuery.offset().left + 10) + "px"
                }, 750, 'swing', function() {
                    searchQuery.focus();
                    fakeMouse.animate({ top: "+=18px", left: "+=10px" }, "fast");

                    $(".step3").doStep("drop", function() {
                        searchQuery.type(query, function() { clickSearch(fakeMouse); });
                    });
                });
            });
        });
    });
});