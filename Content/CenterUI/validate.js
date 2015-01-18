
   ///验证是否为空 ---通用页
    function CheckNull(name) {
      
        var value = $(name).val();
        if (value == "" || value.length == 0) {
            alert("此项为必填项");
            $(name).focus();
        }
    }
///验证是否为空，并保证密码相同 --注册页使用
//第二个密码确认的时候使用
function CheckNullConfirm(name) {
    var value = $(name).val();
    var reValue = $("#iptPassword").val();//上一次的密码
    if (reValue != "" || reValue.length != 0) {
       
        if (value == "" || value.length == 0) {
            alert("此项为必填项");
            $(name).focus();
        } else {
            if (value !== reValue) {
                alert("两次密码不一样，请重新填写");
                $(name).val("");
                $(name).focus();
            }
        }
    }
}
