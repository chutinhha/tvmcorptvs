
$(document).ready(function () {
    //Key down (number only)
    $(".purchase_detail_input_number").keydown(function (e) {
        if (e.shiftKey || e.ctrlKey || e.altKey) { // if shift, ctrl or alt keys held down
            e.preventDefault(); // Prevent character input
        }
        var key = e.charCode || e.keyCode || 0;
        // allow backspace, tab, delete, arrows, numbers and keypad numbers ONLY
        return (
            key == 8 ||
            key == 9 ||
            key == 13 || //Enter key
            key == 46 ||
            (key >= 37 && key <= 40) ||
            (key >= 48 && key <= 57) ||
            (key >= 96 && key <= 105) ||
            key == 190 //key "."*/
           );
    });

    //Format number user input
    $(".purchase_detail_input_number").keyup(function (e) {
        if ($.trim($(this).val()) == "" || $(this).val().indexOf("..") > -1) {
            $(this).val(formatNumber(0, ",", 3, 0));
        }
        else {
            $(this).val(formatNumber(removeComma($(this).val()), ",", 3, 0));
        }
    });

});

//remove comma
function removeComma(val) {
    var result = "";
    if (val.indexOf(",") > 0) {
        var temp = val.split(',');
        for (var i = 0; i < temp.length; i++) {
            result += temp[i].toString();
        }
    }
    else {
        result = val;
    }
    return result;
}

//round function
function round(n, dec) {
    n = parseFloat(n);
    if (!isNaN(n)) {
        if (!dec) var dec = 0;
        var factor = Math.pow(10, dec);
        return Math.floor(n * factor + ((n * factor * 10) % 10 >= 5 ? 1 : 0)) / factor;
    } else {
        return n;
    }
}

//format number function
function formatNumber(val, seperator, every, precision) {
    val = parseFloat(val).toFixed(precision);
    val += '';
    var arr = val.split('.', 2);
    var i = parseInt(arr[0]);
    if (isNaN(i)) return '';
    i = Math.abs(i);
    var n = new String(i);
    var d = arr.length > 1 ? '.' + arr[1] : '';
    var a = [];
    var nn;
    while (n.length > every) {
        nn = n.substr(n.length - every);
        a.unshift(nn);
        n = n.substr(0, n.length - every);
    }
    if (n.length > 0) a.unshift(n);
    n = a.join(seperator);
    return n + d;
};

//sample: formatNumber("1234512345.6789", ",", 3, 2);

//Enter to tab
function checkForEnter(event) {
    var lfound = false
    if (event.keyCode == 13) {
        var obj = this;
        $(".enter").each(function () {
            if (this == obj) {
                lfound = true
            }
            else {
                if (lfound) {
                    $(this).focus()
                    $(this).select();
                    event.preventDefault();
                    return false;
                }
            }
        });
    }
}


//Begin Call event
function callUSEvents() {
    
    if ($.browser.msie) {
        $(".purchase_detail_input_number").blur(function () {
            caculateTotal();
        });
    }
    else {
        $(".purchase_detail_input_number").focusout(function () {
            caculateTotal();
        });
    }
}