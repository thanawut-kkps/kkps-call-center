$.fn.acceptChars = function (options) {
    var defaults = {
        chars: '1234567890.',
        casesensitive: false,
        alertOnError: false,
        alertMessage: 'An unacceptable character detect!'
    };
    var opts = $.extend(defaults, options);
    return this.each(function () {
        var $this = $(this);
        $(this).on("keypress", function (e) {
            if (e.which == 13 || e.which == 8 || e.which == 0) {
                return true;
            }
            if (e.ctrlKey || e.altKey) {
                return true;
            }
            var ch = String.fromCharCode(e.which);
            if (!opts.casesensitive) {
                ch = ch.toLowerCase();
                opts.chars = opts.chars.toLowerCase();
            }
            var allowed = opts.chars.indexOf(ch) > -1;
            if (opts.alertOnError && !allowed) {
                alert(opts.alertMessage);
            }
            return allowed;
        });
    });
};


function integer(x) {
    if (/^\s*(\+|-)?\d+\s*$/.test(String(x))) {
        return parseInt(x, 10);
    }
    return Number.NaN;
}


function isIE(version, comparison) {
    var cc = 'IE',
	    b = document.createElement('B'),
	    docElem = document.documentElement,
	    isIE;

    if (version) {
        cc += ' ' + version;
        if (comparison) { cc = comparison + ' ' + cc; }
    }

    b.innerHTML = '<!--[if ' + cc + ']><b id="iecctest"></b><![endif]-->';
    docElem.appendChild(b);
    isIE = !!document.getElementById('iecctest');
    docElem.removeChild(b);
    return isIE;
}

var stayAliveTimer;

function StartStayAlive() {
    // pulse every 10 seconds
    if (stayAliveTimer == null) {
        stayAliveTimer = setInterval("CallStayAlive()", 1000 * 60);
    }
}

function CallStayAlive() {
    $.ajax({
        type: "POST",
        url: $("#selection-container").data("stayalive"),
        dataType: "json",
        data: {}
    });
}

function htmlEncode(value) {
    //create a in-memory div, set it's inner text(which jQuery automatically encodes)
    //then grab the encoded contents back out.  The div never exists on the page.
    return $('<div/>').text(value).html();
}

function htmlDecode(value) {
    return $('<div/>').html(value).text();
}