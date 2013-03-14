
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

var ChuSo = new Array(" không ", " một ", " hai ", " ba ", " bốn ", " năm ", " sáu ", " bảy ", " tám ", " chín ");
var Tien = new Array("", " nghìn", " triệu", " tỷ", " nghìn tỷ", " triệu tỷ");
//1. Hàm đọc số có ba chữ số;
function DocSo3ChuSo(baso) {
    var tram;
    var chuc;
    var donvi;
    var KetQua = "";
    tram = parseInt(baso / 100);
    chuc = parseInt((baso % 100) / 10);
    donvi = baso % 10;
    if (tram == 0 && chuc == 0 && donvi == 0) return "";
    if (tram != 0) {
        KetQua += ChuSo[tram] + " trăm ";
        if ((chuc == 0) && (donvi != 0)) KetQua += " linh ";
    }
    if ((chuc != 0) && (chuc != 1)) {
        KetQua += ChuSo[chuc] + " mươi";
        if ((chuc == 0) && (donvi != 0)) KetQua = KetQua + " linh ";
    }
    if (chuc == 1) KetQua += " mười ";
    switch (donvi) {
        case 1:
            if ((chuc != 0) && (chuc != 1)) {
                KetQua += " mốt ";
            }
            else {
                KetQua += ChuSo[donvi];
            }
            break;
        case 5:
            if (chuc == 0) {
                KetQua += ChuSo[donvi];
            }
            else {
                KetQua += " lăm ";
            }
            break;
        default:
            if (donvi != 0) {
                KetQua += ChuSo[donvi];
            }
            break;
    }
    return KetQua;
}

//2. Hàm đọc số thành chữ (Sử dụng hàm đọc số có ba chữ số)
function DocTienBangChu(SoTien) {
    var lan = 0;
    var i = 0;
    var so = 0;
    var KetQua = "";
    var tmp = "";
    var ViTri = new Array();
    if (SoTien < 0) return "Số tiền âm !";
    if (SoTien == 0) return "Không đồng !";
    if (SoTien > 0) {
        so = SoTien;
    }
    else {
        so = -SoTien;
    }
    if (SoTien > 8999999999999999) {
        //SoTien = 0;
        return "Số quá lớn!";
    }
    ViTri[5] = Math.floor(so / 1000000000000000);
    if (isNaN(ViTri[5]))
        ViTri[5] = "0";
    so = so - parseFloat(ViTri[5].toString()) * 1000000000000000;
    ViTri[4] = Math.floor(so / 1000000000000);
    if (isNaN(ViTri[4]))
        ViTri[4] = "0";
    so = so - parseFloat(ViTri[4].toString()) * 1000000000000;
    ViTri[3] = Math.floor(so / 1000000000);
    if (isNaN(ViTri[3]))
        ViTri[3] = "0";
    so = so - parseFloat(ViTri[3].toString()) * 1000000000;
    ViTri[2] = parseInt(so / 1000000);
    if (isNaN(ViTri[2]))
        ViTri[2] = "0";
    ViTri[1] = parseInt((so % 1000000) / 1000);
    if (isNaN(ViTri[1]))
        ViTri[1] = "0";
    ViTri[0] = parseInt(so % 1000);
    if (isNaN(ViTri[0]))
        ViTri[0] = "0";
    if (ViTri[5] > 0) {
        lan = 5;
    }
    else if (ViTri[4] > 0) {
        lan = 4;
    }
    else if (ViTri[3] > 0) {
        lan = 3;
    }
    else if (ViTri[2] > 0) {
        lan = 2;
    }
    else if (ViTri[1] > 0) {
        lan = 1;
    }
    else {
        lan = 0;
    }
    for (i = lan; i >= 0; i--) {
        tmp = DocSo3ChuSo(ViTri[i]);
        KetQua += tmp;
        if (ViTri[i] > 0) KetQua += Tien[i];
        if ((i > 0) && (tmp.length > 0)) KetQua += ','; //&& (!string.IsNullOrEmpty(tmp))
    }
    if (KetQua.substring(KetQua.length - 1) == ',') {
        KetQua = KetQua.substring(0, KetQua.length - 1);
    }
    KetQua = KetQua.substring(1, 2).toUpperCase() + KetQua.substring(2);
    return KetQua; //.substring(0, 1);//.toUpperCase();// + KetQua.substring(1);
}

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


/* Maximizes the pop-up dialog */
function maximizeWindow() {
    var currentDialog = SP.UI.ModalDialog.get_childDialog();
    if (currentDialog != null) {
        if (!currentDialog.$S_0) {
            currentDialog.$z();
        }
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