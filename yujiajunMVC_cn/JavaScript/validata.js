/*--------------------------------------------------------*/
/*-------|开发者:think_fish-------------------------------*/
/*-------|开发时间:2011-08-04---------------------------*/
/*-------|说明：jQuery通用验证库-----------------------*/
/*-------|版权申明：版权所有 think_fish @ 2011--------*/
/*-------|版本号:v1.0-------------------------------------*/
/*--------------------------------------------------------*/

//HTML中需要添加的自定义属性说明
//empty:bool值,是否允许为空
//emptyErrorMsg:不允许为空时，空值的错误信息
//validata:验证的内容,empty为空,其他需要验证数据格式的validata值请参照该文件下方regName的值
//errorMsg:数据格式有误时显示的错误信息
//url:发送ajax请求时的url地址
//trueMsg:ajax返回的data值为bool值。返回true时显示的信息
//falseMsg:返回false时显示的信息,若是其他字符类型则显示在errorMsg中。

$(function () {
    //对数据格式的验证
    //参数说明：
    //empty:是否允许为空。缺省为不可为空。bool:true/false
    //errorMsg:错误信息
    //emptyErrorMsg:为空的错误信息，缺省为红*
    //validata:验证类型：email,tel,phone,postcode,number,datetime,money.....
    $("input[validata!=''][validata!='password2']").blur(function () {
        var validataName = $(this).attr("validata");  //验证内容 
        var isAllowEmpty = $(this).attr("empty");  //是否允许为空
        var errorMsg = $(this).attr("errorMsg"); //错误提示信息
        var emptyErrorMsg = $(this).attr("emptyErrorMsg"); //为空时的错误信息
        var regString = returnRegString(validataName);
        if (validataName != "" || validataName != null || validataName != undefined) {
            if ($(this).next().attr("type") != "vmsg") {
                $(this).after("<span type='vmsg' style='color:#ff0000'></span>");
            }
            var errorSpan = $(this).next();
            //如果允许为空属性值为空或不写则默认为不允许为空，empty为bool值:true/false
            if (isAllowEmpty == "" || isAllowEmpty == null || isAllowEmpty == undefined || isAllowEmpty.toLowerCase() == "false") {
                //判空的同时对输入的数据进行验证
                if ($(this).val() == "") {
                    errorSpan.text("").text(returnErrorMsg(emptyErrorMsg));
                } else {
                    if ($(this).val().match(regString) == null) {
                        errorSpan.text("").text(returnErrorMsg(errorMsg));
                    } else {
                        errorSpan.text("");
                    }
                }
            } else if (isAllowEmpty.toLowerCase() == "true") {
                //允许为空，则只对输入数据一做验格式的验证
                if ($(this).val() != "") {
                    if ($(this).val().match(regString) == null) {
                        errorSpan.text("").text(returnErrorMsg(errorMsg));
                    } else {
                        errorSpan.text("");
                    }
                }
            }
        }
    });

    //密码验证
    $("input[validata='password2']").blur(function () {
        var errorMsg = $(this).attr("errorMsg");
        var diffErrorMsg = $(this).attr("diffMsg");
        if ($(this).next().attr("type") != "vmsg") {
            $(this).after("<span type='vmsg' style='color:#ff0000'></span>");
        }
        var errorSpan = $(this).next();
        if ($(this).val() == "") {
            errorSpan.text("").text(returnErrorMsg(returnErrorMsg(errorMsg)));
        } else {
            if ($(this).val() != $("input[type='password'][validata='empty']").val()) {
                errorSpan.text("").text(returnErrorMsg(returnErrorMsg(diffErrorMsg)));
            } else {
                errorSpan.text("");
            }
        }
    });

    //提交数据时对数据进行验证
    $("input[type='submit']").click(function () {
        var validataCollections = $("input[validata!='']");
        alert(validataCollections.length);
        var validataCount = 0;
        for (var i = 0; i < validataCollections.length; i++) {
            if (($("input[validata!='']:eq(" + i + ")").next().attr("type") == "vmsg") && ($("input[validata!='']:eq(" + i + ")").next().text() != "")) {
                alert($("input[validata!='']:eq(" + i + ")").next().text());
                return false;
            } else if (($("input[validata!='']:eq(" + i + ")").next().attr("type") == undefined) && ($("input[validata!='']:eq(" + i + ")").next().text() == "")) {
                alert($("input[validata!='']:eq(" + i + ")").next().attr("type"));
                alert("请输入完整，合法的数据！");
                return false;
            } else {
                validataCount = validataCount + 1;
            }
        }
        if (validataCount == validataCollections.length) {
            $(this).submit();
        } else {
            return false;
        }
    });
});

//根据不同的验证内容，返回相应的正则表达式
function returnRegString(regName) {
    if (regName == "email") {
        return "^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$";  //邮箱
    } else if (regName == "tel") {
        return "^(86)?(-)?(0[0-9]{2,3})?(-)?([0-9]{7,8})(-)?([0-9]{3,5})?$";  //电话
    } else if (regName == "phone") {
        return "^(13[0-9]|15[0-9]|18[0-9])([0-9]{8})$";  //手机
    } else if (regName == "postcode") {
        return "^([0-9]{6})$";    //邮编
    } else if (regName == "number") {
        return "^(0|([1-9]+[0-9]*))(.[0-9]+)?$";   //数字
    } else if (regName == "decimal") {
        return "^[0-9]+([.][0-9]+)?$";    //浮点
    } else if (regName == "money") {
        return "^([0-9])$";    //货币
    } else if (regName == "website") {  //网址
        return "(http://|https://){0,1}[\w\/\.\?\&\=]+";
    } else if (regName == "fax") {  //传真
        return "^[+]{0,1}([0-9]){1,3}[ ]?([-]?(([0-9])|[ ]){1,12})+$";
    } else if (regName == "int") {   //整数
        return "^(-){0,1}\d+$";
    } else if (regName == "pInt") {   //正整数
        return "^\d+$";
    } else if (regName == "nInt") {  //负整数
        return "^-\d+$";
    } else if (regName == "nandl") {   //数字与字母
        return "[a-zA-Z0-9]";
    } else if (regName == "chinese") {   //是否含有中文字符
        return "[\u4e00-\u9fa5]";
    }
}


//返回错误信息
function returnErrorMsg(errorMsg) {
    if (errorMsg == undefined) {
        return "*";
    } else {
        return errorMsg;
    }
}